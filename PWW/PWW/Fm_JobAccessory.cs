using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using ZComm1;

namespace PWW
{
    public partial class Fm_JobAccessory : Form
    {
        public Fm_JobAccessory()
        {
            InitializeComponent();
        }

        DataSet dsCmbBrand = new DataSet(), dsDgvBrand = new DataSet(), dsCmbAccs = new DataSet(), dsDgvAccs = new DataSet(), dsDgvJatt = new DataSet(),dsTemp;
        private void Fm_JobAccessory_Load(object sender, EventArgs e)
        {
            dGV_Accessory.AutoGenerateColumns = false;
            dGV_Brand.AutoGenerateColumns = false;
            dGV_Jatt.AutoGenerateColumns = false;
            //default focus
            this.ActiveControl = this.text_Jobno;
            dsCmbBrand = DB.GetDSFromSql("select bran_code code,bran_code||' -- '||bran_desc display from ztat_brand_info where bran_status='1' order by bran_code");
            dsDgvBrand = DB.GetDSFromSql("select decode(bran_code,'01','1','0') bran_check ,bran_code,bran_desc,bran_icon_name from ztat_brand_info where bran_status='1' order by bran_code");
            cmbbrand.DataSource = dsCmbBrand.Tables[0];
            cmbbrand.DisplayMember = "DISPLAY";
            cmbbrand.ValueMember = "CODE";
            cmbbrand.SelectedIndex = 0;
            dGV_Brand.DataSource = dsDgvBrand.Tables[0];

            dsCmbAccs = DB.GetDSFromSql("select accs_code code,accs_code||' -- '||accs_desc_chn display from ztat_accs_accessory where accs_bran_code='"+cmbbrand.SelectedValue.ToString()+"' and accs_status='1' order by accs_code ");
            dsDgvAccs = DB.GetDSFromSql("select accs_code,accs_desc_chn,accs_desc,accs_icon_name from ztat_accs_accessory where accs_bran_code='" + cmbbrand.SelectedValue.ToString() + "' and accs_status='1' order by accs_code ");
            cmb_accessory.DataSource = dsCmbAccs.Tables[0];
            cmb_accessory.DisplayMember = "DISPLAY";
            cmb_accessory.ValueMember = "CODE";
            dGV_Accessory.DataSource = dsDgvAccs.Tables[0];

            text_Jobno.Tag = false;
            text_Jobno.GotFocus += new EventHandler(text_Jobno_GotFocus);
            text_Jobno.MouseUp += new MouseEventHandler(text_Jobno_MouseUp);         

            dGV_Brand.Rows[0].Cells["vBrand_check"].Value = 1;
            dGV_Brand.Rows[0].DefaultCellStyle.BackColor = Color.Yellow;

            dGV_Accessory.Rows[0].DefaultCellStyle.BackColor = Color.Yellow;
            dGV_Brand.Invalidate();
            dGV_Accessory.Invalidate();
            if (DB.HaveObjectRightsByUserId("PeiJianDelete", DB.loginUserName, "3", ""))
            {
                but_quit.Enabled = true;
            }
            else
            {
                but_quit.Enabled = false;
            }
        }
        void text_Jobno_MouseUp(object sender, MouseEventArgs e)
        {
            //如果鼠标左键操作并且标记存在，则执行全选             
            if (e.Button == MouseButtons.Left && (bool)text_Jobno.Tag == true)
            {
                text_Jobno.SelectAll();
            }

            //取消全选标记              
            text_Jobno.Tag = false;
        }


        void text_Jobno_GotFocus(object sender, EventArgs e)
        {
            text_Jobno.Tag = true;    //设置标记              
            text_Jobno.SelectAll();   //注意1         
        }

        private void but_save_Click(object sender, EventArgs e)
        {
            //save data
            if (Save_Jatt_Accessory_IU(text_Jobno.Text.Trim().ToUpper(), cmbbrand.SelectedValue.ToString().Trim().ToUpper(), cmb_accessory.SelectedValue.ToString().Trim().ToUpper(), text_qty.Text.Trim().ToUpper()))
            {
                //save update log
                bool blogsa = Save_Jatt_Accessory_IU_Log(text_Jobno.Text.Trim().ToUpper(), cmbbrand.SelectedValue.ToString().Trim().ToUpper(), cmb_accessory.SelectedValue.ToString().Trim().ToUpper());
                //get data refresh screen
                dsDgvJatt = DB.GetDSFromSql(@"select a1.*,bran_desc,accs_desc_chn,accs_desc  from ztat_jatt_job_accessory a1 ,ztat_accs_accessory,ztat_brand_info
where (jatt_bran_code=accs_bran_code(+) and jatt_accs_code=accs_code(+))
  and a1.jatt_bran_code=bran_code and jatt_jobm_no='" + text_Jobno.Text.Trim().ToUpper() + "' order by nvl(jatt_upd_on,jatt_crt_on) desc ");
                dGV_Jatt.DataSource = null;
                dGV_Jatt.DataSource = dsDgvJatt.Tables[0];
                //initial value focus Jobno
                Set_Label_Message("I", text_Jobno.Text + " 存档成功 ！" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                text_Jobno.Text = "";
                text_Jobno.Focus();
            }
            else
            {
                //Set_Label_Message("E", text_Jobno.Text + " 存档失败 ！" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
            }

        }

        private void but_quit_Click(object sender, EventArgs e)
        {
            if (dGV_Jatt.Rows.Count == 0)
            {
                Set_Label_Message("W", "无资料可供删除，请检查 ！");
                return;
            }
            if (dGV_Jatt.SelectedRows.Count == 0)
            {
                Set_Label_Message("W", "请先选择要删除的记录，再删除 ，请检查 ！");
                return;
            }
            if (MessageBox.Show("确定删除所选" + dGV_Jatt.SelectedRows.Count + "条记录 ? " + dGV_Jatt.SelectedRows[0].Cells["vJatt_Jobm_no"].Value.ToString() + " -- " + dGV_Jatt.SelectedRows[0].Cells["vBran_Desc"].Value.ToString()
                  + " -- " + dGV_Jatt.SelectedRows[0].Cells["vJattAccs_Desc"].Value.ToString() + " -- " + dGV_Jatt.SelectedRows[0].Cells["vJatt_Qty"].Value.ToString() + " ...", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                for (int i = 0; i < dGV_Jatt.SelectedRows.Count; i++)
                {
                    bool brsult = Save_Jatt_Accessory_Del(dGV_Jatt.SelectedRows[i].Cells["vJatt_Jobm_no"].Value.ToString(), dGV_Jatt.SelectedRows[i].Cells["vJatt_Bran_Code"].Value.ToString(), dGV_Jatt.SelectedRows[i].Cells["vJatt_Accs_Code"].Value.ToString());
                }
                Set_Label_Message("I", "删除成功 ！"  + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                //get data refresh screen
                dsDgvJatt = DB.GetDSFromSql(@"select a1.*,bran_desc,accs_desc_chn,accs_desc  from ztat_jatt_job_accessory a1 ,ztat_accs_accessory,ztat_brand_info
where (jatt_bran_code=accs_bran_code(+) and jatt_accs_code=accs_code(+))
  and a1.jatt_bran_code=bran_code and jatt_jobm_no='" + dGV_Jatt.SelectedRows[0].Cells["vJatt_Jobm_no"].Value.ToString().Trim().ToUpper() + "' order by nvl(jatt_upd_on,jatt_crt_on) desc ");
                dGV_Jatt.DataSource = null;
                dGV_Jatt.DataSource = dsDgvJatt.Tables[0];

            }
            else
            {
                Set_Label_Message("W", "取消删除 ！"  + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
            }    

        }

        private void dGv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void text_Jobno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //check the job order format
                string strjobm_no = text_Jobno.Text.Trim().ToUpper();
                text_Jobno.Text = strjobm_no;
                Regex regJobNo = new Regex(@"^J[A-Z][1-9A-C]\d{5}$");
                if (regJobNo.IsMatch(strjobm_no))
                {
                }
                else
                {
                    Set_Label_Message("W", "公司条码号格式不对，格式:JYM99999 ！" + strjobm_no);
                    return;
                }
                //get data refresh screen
                dsDgvJatt = DB.GetDSFromSql(@"select a1.*,bran_desc,accs_desc_chn,accs_desc  from ztat_jatt_job_accessory a1 ,ztat_accs_accessory,ztat_brand_info
where (jatt_bran_code=accs_bran_code(+) and jatt_accs_code=accs_code(+))
  and a1.jatt_bran_code=bran_code and jatt_jobm_no='" + text_Jobno.Text.Trim().ToUpper() + "' order by nvl(jatt_upd_on,jatt_crt_on) desc ");
                dGV_Jatt.DataSource = null;
                dGV_Jatt.DataSource = dsDgvJatt.Tables[0];

                cmbbrand.Focus();
                //dGV_Brand.Rows.Count
            }
        }

        private void cmbbrand_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void cmb_accessory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
            }
        }

        private void text_qty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //check must is int
                if (text_qty.Text.Trim() == "0")
                {
                    Set_Label_Message("W", "数量不能为 0 ！");
                    return;
                }
                Regex regQty = new Regex(@"^\d+$");
                if (regQty.IsMatch(text_qty.Text))
                { }
                else
                {
                    Set_Label_Message("W", "数量必须是整数 ！" + text_qty.Text);
                    return;
                }
                //save data
                if (Save_Jatt_Accessory_IU(text_Jobno.Text.Trim().ToUpper(), cmbbrand.SelectedValue.ToString().Trim().ToUpper(), cmb_accessory.SelectedValue.ToString().Trim().ToUpper(), text_qty.Text.Trim().ToUpper()))
                {
                    //save update log
                    bool blogsa = Save_Jatt_Accessory_IU_Log(text_Jobno.Text.Trim().ToUpper(), cmbbrand.SelectedValue.ToString().Trim().ToUpper(), cmb_accessory.SelectedValue.ToString().Trim().ToUpper());
                    //get data refresh screen
                    dsDgvJatt = DB.GetDSFromSql(@"select a1.*,bran_desc,accs_desc_chn,accs_desc  from ztat_jatt_job_accessory a1 ,ztat_accs_accessory,ztat_brand_info
where (jatt_bran_code=accs_bran_code(+) and jatt_accs_code=accs_code(+))
  and a1.jatt_bran_code=bran_code and jatt_jobm_no='" + text_Jobno.Text.Trim().ToUpper() + "' order by nvl(jatt_upd_on,jatt_crt_on) desc ");
                    dGV_Jatt.DataSource = null;
                    dGV_Jatt.DataSource = dsDgvJatt.Tables[0];
                    //initial value focus Jobno
                    Set_Label_Message("I", text_Jobno.Text + " 存档成功 ！" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                    //default change accessory
                    //text_Jobno.Text = "";
                    //text_Jobno.Focus();
                    cmb_accessory.Focus();
                }
                else
                {
                    //Set_Label_Message("E", text_Jobno.Text + " 存档失败 ！" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                }
            }

        }
        private bool Save_Jatt_Accessory_IU(string sJobno, string sBrand , string sAccessory, string sQty,string sAddSub="+")
        {
            //check and validate data 
            if (sJobno.Trim() == "")
            {
                Set_Label_Message("W", "公司条码号不能为空 ！" + sJobno);
                return false;
            }
            Regex regJobNo = new Regex(@"^J[A-Z][1-9A-C]\d{5}$");
            if (regJobNo.IsMatch(sJobno))
            {
            }
            else
            {
                Set_Label_Message("W", "公司条码号格式不对，格式:JYM99999 ！" + sJobno);
                return false;
            }
            dsTemp = DB.GetDSFromSql("select bran_code from ztat_brand_info where bran_code='"+sBrand+"'");
            if (DB.V(dsTemp)=="")
            {
                Set_Label_Message("W", "品牌编号不对 ！" + sBrand);
                return false;
            }
            dsTemp = DB.GetDSFromSql("select accs_code from ztat_accs_accessory where accs_bran_code='"+sBrand+"' and accs_code='"+sAccessory+"'");
            if (DB.V(dsTemp)=="")
            {
                Set_Label_Message("W", "品牌编号+配件编号不对 ！" + sBrand + " + " + sAccessory);
                return false;
            }
            try
            {
                int itest = System.Convert.ToInt32(sQty);
            }
            catch
            {
                Set_Label_Message("W", "数量只能是整数 ！" + sQty);
                return false;
            }

            //check exist or not 
            dsTemp = null;
            dsTemp = DB.GetDSFromSql("select jatt_jobm_no from ztat_jatt_job_accessory where jatt_jobm_no='"+sJobno+"' and jatt_bran_code='"+sBrand+"' and jatt_accs_code='"+sAccessory+"'");
            if (DB.V(dsTemp)=="")
            {
                // insert a new record
                return DB.ExecuteFromSql(@"insert into ztat_jatt_job_accessory (jatt_jobm_no,jatt_bran_code,jatt_accs_code,jatt_qty,jatt_crt_by) values("+DB.sp(sJobno)+","+DB.sp(sBrand)+","+DB.sp(sAccessory)+","+sQty+","+DB.sp(DB.loginUserName)+")");
            }
            else
            {
                // update exist record add qty 
                if (sAddSub == "-")
                {
                    return DB.ExecuteFromSql("update ztat_jatt_job_accessory set jatt_qty = nvl(jatt_qty,0) - " + sQty + ",jatt_upd_by=" + DB.sp(DB.loginUserName) + " where jatt_jobm_no='" + sJobno + "' and jatt_bran_code='" + sBrand + "' and jatt_accs_code='" + sAccessory + "'");
                }
                else
                {
                    return DB.ExecuteFromSql("update ztat_jatt_job_accessory set jatt_qty = nvl(jatt_qty,0) + " + sQty + ",jatt_upd_by=" + DB.sp(DB.loginUserName) + " where jatt_jobm_no='" + sJobno + "' and jatt_bran_code='" + sBrand + "' and jatt_accs_code='" + sAccessory + "'");
                }
            }

        }

        private bool Save_Jatt_Accessory_IU_Log(string sJobno, string sBrand, string sAccessory)
        {
            //check exist or not 
            dsTemp = null;
            dsTemp = DB.GetDSFromSql("select jatt_jobm_no from ztat_jatt_job_accessory where jatt_jobm_no='" + sJobno + "' and jatt_bran_code='" + sBrand + "' and jatt_accs_code='" + sAccessory + "'");
            if (DB.V(dsTemp) == "")
            {
                // no record , not need add log
                return false;
            }
            else
            {
                // add log into  
                return DB.ExecuteFromSql(@"insert into ZTAT_JATL_JOB_ACCESSORY_LOG (
  JATT_JOBM_NO,
  JATT_CUST_CASENO,
  JATT_BRAN_CODE,
  JATT_ACCS_CODE,
  JATT_QTY,
  JATT_DESC ,
  JATT_REMARK ,
  JATT_CRT_ON ,
  JATT_CRT_BY,
  JATT_UPD_ON ,
  JATT_UPD_BY,
  JATL_CRT_BY,
  JATL_CRT_ON,
  JATL_ACTION 
)
(select   JATT_JOBM_NO,
  JATT_CUST_CASENO,
  JATT_BRAN_CODE,
  JATT_ACCS_CODE,
  JATT_QTY,
  JATT_DESC ,
  JATT_REMARK ,
  JATT_CRT_ON ,
  JATT_CRT_BY,
  JATT_UPD_ON ,
  JATT_UPD_BY,
   " + DB.sp(DB.loginUserName)+@"  JATL_CRT_BY,
  sysdate JATL_CRT_ON,'Update' JATL_ACTION  from ZTAT_JATT_JOB_ACCESSORY where jatt_jobm_no=" + DB.sp(sJobno) + " and jatt_bran_code=" + DB.sp(sBrand) + " and jatt_accs_code=" + DB.sp(sAccessory)+" )");
            }
        }
        private bool Save_Jatt_Accessory_Del(string sJobno, string sBrand, string sAccessory)
        {
            // delete the record 
            // before delete save log 
            bool blogSav = DB.ExecuteFromSql(@"insert into ZTAT_JATL_JOB_ACCESSORY_LOG (
  JATT_JOBM_NO,
  JATT_CUST_CASENO,
  JATT_BRAN_CODE,
  JATT_ACCS_CODE,
  JATT_QTY,
  JATT_DESC ,
  JATT_REMARK ,
  JATT_CRT_ON ,
  JATT_CRT_BY,
  JATT_UPD_ON ,
  JATT_UPD_BY,
  JATL_CRT_BY,
  JATL_CRT_ON,
  JATL_ACTION 
)
(select   JATT_JOBM_NO,
  JATT_CUST_CASENO,
  JATT_BRAN_CODE,
  JATT_ACCS_CODE,
  JATT_QTY,
  JATT_DESC ,
  JATT_REMARK ,
  JATT_CRT_ON ,
  JATT_CRT_BY,
  JATT_UPD_ON ,
  JATT_UPD_BY,
   " + DB.sp(DB.loginUserName) + @"  JATL_CRT_BY,
  sysdate JATL_CRT_ON,'Delete' JATL_ACTION  from ZTAT_JATT_JOB_ACCESSORY where jatt_jobm_no=" + DB.sp(sJobno) + " and jatt_bran_code=" + DB.sp(sBrand) + " and jatt_accs_code=" + DB.sp(sAccessory)+" )");

            return DB.ExecuteFromSql("delete from ztat_jatt_job_accessory where jatt_jobm_no='"+sJobno+"' and jatt_bran_code='"+sBrand+"' and jatt_accs_code='"+sAccessory+"'");
        }

        private void Set_Label_Message(string messageType, string messageText)
        {
            this.labelmessage.Text = messageText;
            if (messageType.ToUpper() == "E" || messageType.ToUpper() == "W" || messageType.ToUpper() == "A")
            {
                this.labelmessage.ForeColor = Color.Crimson;
                this.labelmessage.BackColor = Color.Yellow;
            }
            else
            {
                this.labelmessage.ForeColor = Color.DarkBlue;
                this.labelmessage.BackColor = SystemColors.Control;
            }
        }

        private void cmbbrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbbrand.SelectedIndex == -1) return;
            for (int i = 0; i < dGV_Brand.Rows.Count; i++)
            {
                if (dGV_Brand.Rows[i].Cells["vBrand_Code"].Value.ToString() != cmbbrand.SelectedValue.ToString() && dGV_Brand.Rows[i].Cells["vBrand_check"].Value.ToString() == "1")
                {
                    dGV_Brand.Rows[i].Cells["vBrand_check"].Value = 0;
                    dGV_Brand.Rows[i].DefaultCellStyle.BackColor = Color.White;
                }
                else if (dGV_Brand.Rows[i].Cells["vBrand_Code"].Value.ToString() == cmbbrand.SelectedValue.ToString())
                {
                    dGV_Brand.Rows[i].Cells["vBrand_check"].Value = 1;
                    dGV_Brand.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }
            dsCmbAccs = DB.GetDSFromSql("select accs_code code,accs_code||' -- '||accs_desc_chn display from ztat_accs_accessory where accs_bran_code='" + cmbbrand.SelectedValue.ToString() + "' and accs_status='1' order by accs_code ");
            dsDgvAccs = DB.GetDSFromSql("select accs_code,accs_desc_chn,accs_desc,accs_icon_name from ztat_accs_accessory where accs_bran_code='" + cmbbrand.SelectedValue.ToString() + "' and accs_status='1' order by accs_code ");
            cmb_accessory.DataSource = null;
            cmb_accessory.DataSource = dsCmbAccs.Tables[0];
            cmb_accessory.DisplayMember = "DISPLAY";
            cmb_accessory.ValueMember = "CODE";
            dGV_Accessory.DataSource = null;
            dGV_Accessory.DataSource = dsDgvAccs.Tables[0];

        }

        private void but_ssav_A_Click(object sender, EventArgs e)
        {
            //save data
            if (Save_Jatt_Accessory_IU(text_Jobno.Text.Trim().ToUpper(), cmbbrand.SelectedValue.ToString().Trim().ToUpper(), cmb_accessory.SelectedValue.ToString().Trim().ToUpper(), text_qty.Text.Trim().ToUpper()))
            {
                //save update log
                bool blogsa = Save_Jatt_Accessory_IU_Log(text_Jobno.Text.Trim().ToUpper(), cmbbrand.SelectedValue.ToString().Trim().ToUpper(), cmb_accessory.SelectedValue.ToString().Trim().ToUpper());
                //get data refresh screen
                dsDgvJatt = DB.GetDSFromSql(@"select a1.*,bran_desc,accs_desc_chn,accs_desc  from ztat_jatt_job_accessory a1 ,ztat_accs_accessory,ztat_brand_info
where (jatt_bran_code=accs_bran_code(+) and jatt_accs_code=accs_code(+))
  and a1.jatt_bran_code=bran_code and jatt_jobm_no='" + text_Jobno.Text.Trim().ToUpper() + "' order by nvl(jatt_upd_on,jatt_crt_on) desc ");
                dGV_Jatt.DataSource = null;
                dGV_Jatt.DataSource = dsDgvJatt.Tables[0];
                //initial value focus accessory
                Set_Label_Message("I", text_Jobno.Text + " 存档成功 ！" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                cmb_accessory.Focus();
            }
            else
            {
                //Set_Label_Message("E", text_Jobno.Text + " 存档失败 ！" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
            }

        }

        private void cmb_accessory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_accessory.SelectedIndex == -1) return;
            for (int i = 0; i < dGV_Accessory.Rows.Count; i++)
            {
                if (dGV_Accessory.Rows[i].Cells["vAccs_Code"].Value.ToString() != cmb_accessory.SelectedValue.ToString() && dGV_Accessory.Rows[i].DefaultCellStyle.BackColor.ToArgb().ToString() == Color.Yellow.ToArgb().ToString())
                {
                    //dGV_Accessory.Rows[i].Cells["vBrand_check"].Value = 0;
                    dGV_Accessory.Rows[i].DefaultCellStyle.BackColor = Color.White;
                }
                else if (dGV_Accessory.Rows[i].Cells["vAccs_Code"].Value.ToString() == cmb_accessory.SelectedValue.ToString())
                {
                    //dGV_Accessory.Rows[i].Cells["vBrand_check"].Value = 1;
                    dGV_Accessory.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }

        }

        private void dGV_Brand_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            for (int i = 0; i < dGV_Brand.Rows.Count; i++)
            {
                if (i == e.RowIndex)
                {
                    dGV_Brand.Rows[i].Cells["vBrand_check"].Value = 1;
                    dGV_Brand.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
                else if (dGV_Brand.Rows[i].Cells["vBrand_check"].Value.ToString() == "1")
                {
                    dGV_Brand.Rows[i].Cells["vBrand_check"].Value = 0;
                    dGV_Brand.Rows[i].DefaultCellStyle.BackColor = Color.White;
                }
                 
            }
            if (cmbbrand.Items.Count > e.RowIndex) cmbbrand.SelectedIndex = e.RowIndex;
            dsCmbAccs = DB.GetDSFromSql("select accs_code code,accs_code||' -- '||accs_desc_chn display from ztat_accs_accessory where accs_bran_code='" + dGV_Brand.Rows[e.RowIndex].Cells["vBrand_Code"].Value.ToString() + "' and accs_status='1' order by accs_code ");
            dsDgvAccs = DB.GetDSFromSql("select accs_code,accs_desc_chn,accs_desc,accs_icon_name from ztat_accs_accessory where accs_bran_code='" + dGV_Brand.Rows[e.RowIndex].Cells["vBrand_Code"].Value.ToString() + "' and accs_status='1' order by accs_code ");
            cmb_accessory.DataSource = null;
            cmb_accessory.DataSource = dsCmbAccs.Tables[0];
            cmb_accessory.DisplayMember = "DISPLAY";
            cmb_accessory.ValueMember = "CODE";
            dGV_Accessory.DataSource = null;
            dGV_Accessory.DataSource = dsDgvAccs.Tables[0];

        }

        private void dGV_Accessory_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dGV_Jatt_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dGV_Accessory_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1) return;
            for (int i = 0; i < dGV_Accessory.Rows.Count; i++)
            {
                if (i == e.RowIndex)
                {
                    //dGV_Accessory.Rows[i].Cells["vBrand_check"].Value = 1;
                    dGV_Accessory.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
                else if (dGV_Accessory.Rows[i].DefaultCellStyle.BackColor.ToArgb().ToString() == Color.Yellow.ToArgb().ToString())
                {
                    //dGV_Accessory.Rows[i].Cells["vBrand_check"].Value = 0;
                    dGV_Accessory.Rows[i].DefaultCellStyle.BackColor = Color.White;
                }
                 
            }
            if (cmb_accessory.Items.Count > e.RowIndex) cmb_accessory.SelectedIndex = e.RowIndex;

            if (e.Button == MouseButtons.Left)
            {
                bool b1=Save_Jatt_Accessory_IU(text_Jobno.Text.Trim().ToUpper(), cmbbrand.SelectedValue.ToString() , cmb_accessory.SelectedValue.ToString(), "1","+");
                if (b1)
                {
                    //save update log
                    bool blogsa = Save_Jatt_Accessory_IU_Log(text_Jobno.Text.Trim().ToUpper(), cmbbrand.SelectedValue.ToString().Trim().ToUpper(), cmb_accessory.SelectedValue.ToString().Trim().ToUpper());
                    Set_Label_Message("I", text_Jobno.Text + " 存档成功 ！" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                }
                text_Jobno.Focus();
            }
            else if (e.Button == MouseButtons.Right)
            {
                bool b2 = Save_Jatt_Accessory_IU(text_Jobno.Text.Trim().ToUpper(), cmbbrand.SelectedValue.ToString(), cmb_accessory.SelectedValue.ToString(), "1", "-");
                if (b2)
                {
                    //save update log
                    bool blogsa = Save_Jatt_Accessory_IU_Log(text_Jobno.Text.Trim().ToUpper(), cmbbrand.SelectedValue.ToString().Trim().ToUpper(), cmb_accessory.SelectedValue.ToString().Trim().ToUpper());
                    Set_Label_Message("I", text_Jobno.Text + " 存档成功 ！" + string.Format("{0:HH:mm:ss}", System.DateTime.Now));
                }
                text_Jobno.Focus();
            }
            dsDgvJatt = DB.GetDSFromSql(@"select a1.*,bran_desc,accs_desc_chn,accs_desc  from ztat_jatt_job_accessory a1 ,ztat_accs_accessory,ztat_brand_info
where (jatt_bran_code=accs_bran_code(+) and jatt_accs_code=accs_code(+))
  and a1.jatt_bran_code=bran_code and jatt_jobm_no='" + text_Jobno.Text.Trim().ToUpper() + "' order by nvl(jatt_upd_on,jatt_crt_on) desc ");
            dGV_Jatt.DataSource = null;
            dGV_Jatt.DataSource = dsDgvJatt.Tables[0];

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                but_save_A.Focus();
                but_ssav_A_Click(sender, e);
            }
            else if (e.Control && e.KeyCode == Keys.J)
            {
                but_save.Focus();
                but_save_Click(sender, e);
            }
            else if (e.Control && e.KeyCode == Keys.D)
            {
                if (but_quit.Enabled)
                {
                    but_quit.Focus();
                    but_quit_Click(sender, e);
                }
            }
            else if (e.KeyData == Keys.F1)
            {
                but_save_A.Focus();
                but_ssav_A_Click(sender, e);
            }
            else if (e.KeyData == Keys.F2)
            {
                but_save.Focus();
                but_save_Click(sender, e);
            }
            else if (e.KeyData == Keys.F4)
            {
                if (but_quit.Enabled)
                {
                    but_quit.Focus();
                    but_quit_Click(sender, e);
                }
            }
        }

        private void dGV_Jatt_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex > dGV_Jatt.Rows.Count - 1)
                return;
            if (e.RowIndex == - 1)
                return;

            DataGridViewRow dgr = dGV_Jatt.Rows[e.RowIndex];
            try
            {
                if (dgr.Cells["vJatt_Bran_Code"].Value.ToString() == cmbbrand.SelectedValue.ToString() && dgr.Cells["vJatt_Accs_Code"].Value.ToString() == cmb_accessory.SelectedValue.ToString())
                {
                    dgr.DefaultCellStyle.BackColor = Color.Yellow;
                }
                else if (dgr.DefaultCellStyle.BackColor.ToArgb().ToString() == Color.Yellow.ToArgb().ToString())
                {
                    dgr.DefaultCellStyle.BackColor = Color.White;
                }
            }
            catch
            {
            }
        }

        private void dGV_Brand_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex > dGV_Brand.Rows.Count - 1)
                return;
            if (e.RowIndex == -1)
                return;

            DataGridViewRow dgr = dGV_Brand.Rows[e.RowIndex];
            try
            {
                if (dgr.Cells["vBrand_Code"].Value.ToString() == cmbbrand.SelectedValue.ToString())
                {
                    dgr.Cells["vBrand_check"].Value = 1;
                    dgr.DefaultCellStyle.BackColor = Color.Yellow; //vBrand_check
                }
                else if (dgr.DefaultCellStyle.BackColor.ToArgb().ToString() == Color.Yellow.ToArgb().ToString() || dgr.Cells["vBrand_check"].Value.ToString() == "1")
                {
                    dgr.Cells["vBrand_check"].Value = 0;
                    dgr.DefaultCellStyle.BackColor = Color.White;
                }
            }
            catch
            {
            }
        }

        private void dGV_Accessory_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex > dGV_Accessory.Rows.Count - 1)
                return;
            if (e.RowIndex == -1)
                return;

            DataGridViewRow dgr = dGV_Accessory.Rows[e.RowIndex];
            try
            {
                if (dgr.Cells["vAccs_Code"].Value.ToString() == cmb_accessory.SelectedValue.ToString())
                {
                    dgr.DefaultCellStyle.BackColor = Color.Yellow;
                }
                else if (dgr.DefaultCellStyle.BackColor.ToArgb().ToString() == Color.Yellow.ToArgb().ToString())
                {
                    dgr.DefaultCellStyle.BackColor = Color.White;
                }
            }
            catch
            {
            }
        }

        private void dGV_Brand_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex > -1 )
            {
                if (e.ColumnIndex == dGV_Brand.Columns["Trade_Icon"].Index)
                {
                    if (dGV_Brand.Rows[e.RowIndex].Cells["vTrade_Icon_Name"].Value != null && dGV_Brand.Rows[e.RowIndex].Cells["vTrade_Icon_Name"].Value.ToString() != "")
                    {
                        string sPath = "img\\" + dGV_Brand.Rows[e.RowIndex].Cells["vTrade_Icon_Name"].Value.ToString();
                        if (File.Exists(sPath))
                            e.Value = ZFileCom.GetBitmap(sPath);
                        else
                            e.Value = ZFileCom.GetBitmap("img\\blank.gif");
                    }
                    else
                    {
                        //int i = dgCaseProperties.Rows[e.RowIndex].GetPreferredHeight(e.RowIndex, DataGridViewAutoSizeRowMode.AllCells, false);
                        //int i1 = dgCaseProperties.Rows[e.RowIndex].GetPreferredHeight(e.RowIndex, DataGridViewAutoSizeRowMode.AllCells, true);
                        //dgCaseProperties.Rows[e.RowIndex].Height = dgCaseProperties.RowTemplate.Height;
                        e.Value = ZFileCom.GetBitmap("img\\blank.gif");
                    }
                }
            }
        }

        private void dGV_Accessory_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (e.ColumnIndex == dGV_Accessory.Columns["vAccs_Icon"].Index)
                {
                    if (dGV_Accessory.Rows[e.RowIndex].Cells["vAccs_Icon_Name"].Value != null && dGV_Accessory.Rows[e.RowIndex].Cells["vAccs_Icon_Name"].Value.ToString() != "")
                    {
                        string sPath = "img\\" + dGV_Accessory.Rows[e.RowIndex].Cells["vAccs_Icon_Name"].Value.ToString();
                        if (File.Exists(sPath))
                            e.Value = ZFileCom.GetBitmap(sPath);
                        else
                            e.Value = ZFileCom.GetBitmap("img\\blank.gif");
                    }
                    else
                    {
                        //int i = dgCaseProperties.Rows[e.RowIndex].GetPreferredHeight(e.RowIndex, DataGridViewAutoSizeRowMode.AllCells, false);
                        //int i1 = dgCaseProperties.Rows[e.RowIndex].GetPreferredHeight(e.RowIndex, DataGridViewAutoSizeRowMode.AllCells, true);
                        //dgCaseProperties.Rows[e.RowIndex].Height = dgCaseProperties.RowTemplate.Height;
                        e.Value = ZFileCom.GetBitmap("img\\blank.gif");
                    }
                }
            }
        }
    }
}
