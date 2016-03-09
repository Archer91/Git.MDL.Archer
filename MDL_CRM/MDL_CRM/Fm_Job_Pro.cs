using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MDL_CRM
{
    public partial class Fm_Job_Pro : Form
    {
        public Fm_Job_Pro()
        {
            InitializeComponent();
            txtCode.Focus();
        }
        public DataGridViewTextBoxEditingControl dgvTxt = null; // 声明 一个 CellEdit   
        public string strword = string.Empty;
        //定为可以编辑的首行
        public int _intRowEdit = -1;
        public bool boolEdit = false;
        public void BindDataGridView(string _word)
        {
            StringBuilder sb = new StringBuilder();
            string sql ="";
            sb.Append("  select job.jobm_no,job.jdtl_lineno,");
            sb.Append(" job.jdtl_prodcode, ");
            sb.Append(" pro.zprod_fdam_code, "); 
            sb.Append(" CASE WHEN nvl(job.zjdtl_fda_qty,0) > 0 THEN  job.zjdtl_fda_qty  ELSE  decode(Pro.Zprod_Att_Fda_Qty,'Y',jdtl_qty,job.zjdtl_fda_qty)  END   zjdtl_fda_qty, ");  
            sb.Append(" pro.prod_desc, ");
            sb.Append(" (select f.fdam_desc from fda_master f where f.fdam_code =  pro.zprod_fdam_code ) fda_desc, ");
            sb.Append(" nvl(prod_desc_chi,prod_desc) prod_desc_chi,");
            sb.Append(" job.jdtl_qty,job.jdtl_unit,");
            sb.Append(" job.jdtl_toothpos,job.jdtl_toothcolor,job.jdtl_batchno,job.jdtl_createby, ");
            sb.Append(" job.jdtl_createdate,job.jdtl_lmodby,job.jdtl_lmoddate,job.zjdtl_fda_qty value_real");
            sb.Append(" from JOB_PRODUCT job ,Product pro ");
            sb.Append(" where pro.prod_code = job.JDTL_Prodcode and job.jobm_no = '{0}' ");

            if (ck_Fda.Checked)
            {
                sb.Append(" and  pro.zprod_fdam_code  <> '~' ");
            }
            sb.Append("  ");
            
            sql = string.Format(sb.ToString(), _word);
            DataSet ds = DB.GetDSFromSql(sql);
            dataGridView1.DataSource = ds.Tables[0];
            
        }
        /// <summary>
        /// 是否已打印过有快递号码
        /// </summary>
        /// <returns></returns>
        public bool Is_Has_Express_No(string _code)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select i.zarhr_express_no,i.arhr_invno,jo.JOBM_STAGE, jo.JOBM_NO, jo.JOBM_ACCOUNTID, jo.JOBM_PATIENT,jo.JOBM_SYSTEMID ");
            sb.Append(" from job_order jo,invoice i,invoice_dtl idt ");
            sb.Append(" where i.arhr_invno = idt.arhr_invno   and idt.ardt_jobno = jo.jobm_no and length(i.zarhr_express_no) >5 ");
            sb.Append(" and  jo.jobm_no = '" + _code + "' ");

            DataTable dt_Express = DB.GetDSFromSql(sb.ToString()).Tables[0];

            if (dt_Express.Rows.Count > 0)
            {
                if (MessageBox.Show("此编号已打印过发票,发票号码为:" + dt_Express.Rows[0]["arhr_invno"].ToString() + "\t\n你确认要重新修改数据吗?", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }

        }


        /// <summary>
        /// 搜寻功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            boolEdit = false;
            if (string.IsNullOrEmpty(txtCode.Text.ToUpper().Trim()))
            {
                MessageBox.Show("编号必需输入才能搜寻！", "注意",MessageBoxButtons.OK,MessageBoxIcon.Information);
                txtCode.Text = "";
                txtCode.Focus();
            }
            else
            {
            ///查询快递号进行提示  
            if (!Is_Has_Express_No(txtCode.Text.ToUpper().Trim()))
            {
                return;
            }
            timer1.Enabled = true;
            _intRowEdit = -1;
            BindDataGridView(txtCode.Text.ToUpper().Trim());
            ToFocus();
            timer1.Start();
                if (dataGridView1.RowCount < 1)
                {
                    MessageBox.Show("所输入的资料不存在!", "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCode.Focus();
                    txt_BeginDate.Text = "";
                    txt_Invote.Text = "";
                    txt_Estmatedate.Text = "";
                    txt_State.Text = "";
                    txt_Account.Text = "";
                }
            }
            BindJobInfo(txtCode.Text.ToUpper().Trim());
        }
        private string ChandWordDis(string _en)
        {
            string _ch = string.Empty;
            switch (_en)
            {
                case "WAITPRINT":
                    _ch = "等印";
                    break;
                case "CANCEL":
                    _ch = "取消";
                    break;
                case "RETURN":
                    _ch = "返回";
                    break;
                case "NORMAL":
                    _ch = "正常";
                    break;
                case "WAITREPLY":
                    _ch = "等覆";
                    break;
                default:
                    _ch = _en;
                    break;
            }
            return _ch;
        }
        public void BindJobInfo(string _word )
        {
            StringBuilder sb = new StringBuilder();
            string sql = "";
            sb.Append(" select jo.JOBM_STAGE, jo.JOBM_NO, jo.JOBM_ACCOUNTID, jo.JOBM_DENTISTID, ");
            sb.Append(" jo.JOBM_PATIENT, jo.JOBM_DOCTORID, jo.JOBM_JOB_TYPE, jo.JOBM_JOB_NATURE, ");
            sb.Append(" jo.JOBM_SYSTEMID, jo.JOBM_STATUS, ");
            sb.Append(" to_char(jo.JOBM_RECEIVEDATE,'mm/dd/YYYY')  JOBM_RECEIVEDATE, ");
            sb.Append(" jo.JOBM_TIMF_CODE_REC, jo.JOBM_DELIVERYDATE,");
            sb.Append(" JOBM_TIMF_CODE_DEL, ");
            sb.Append(" to_char(jo.JOBM_REQUESTDATE,'mm/dd/YYYY') JOBM_REQUESTDATE,  ");
            sb.Append(" JOBM_TIMF_CODE_REQ, ");
            sb.Append(" to_char(jo.JOBM_ESTIMATEDATE,'mm/dd/YYYY') JOBM_ESTIMATEDATE,  ");
            sb.Append(" JOBM_TIMF_CODE_EST, ");
            sb.Append(" JOBM_DESC, JOBM_TOOTHPOS, JOBM_TOOTHCOLOR, JOBM_TOOTHCOLOR2, JOBM_TOOTHCOLOR3,  ");
            sb.Append(" JOBM_CUSTBATCHID, JOBM_CUSTCASENO, JOBM_RELATEJOB, JOBM_CUSTREMARK, JOBM_LOCATION,  ");
            sb.Append(" JOBM_DISCOUNT, JOBM_CREATEBY, JOBM_CREATEDATE, JOBM_LMODBY, JOBM_LMODDATE, ");
            sb.Append(" JOBM_DENTNAME, JOBM_INVNO, JOBM_COLOR_YN, JOBM_COMP_YN, JOBM_REDO_YN, ");
            sb.Append(" JOBM_TRY_YN, JOBM_URGENT_YN, JOBM_DOCINFO_1, JOBM_DOCINFO_2, ");
            sb.Append(" JOBM_SPECIAL_YN, JOBM_AMEND_YN, JOBM_COMPDATE, JOBM_PACKNO, ");
            sb.Append(" JOBM_BOXNUM, JOBM_SLNO, ZJOBM_RCV_BATCHNO,ac.mgrp_code,jo.jobm_amend_yn,jo.jobm_redo_yn");
            sb.Append("  from job_order jo,account ac where jo.jobm_accountid  = ac.acct_id  and  jo.jobm_no = '{0}' ");
            sb.Append("  ");

            sql = string.Format(sb.ToString(), _word);
            DataSet ds = DB.GetDSFromSql(sql);

            if (ds.Tables[0].Rows.Count > 0)
            {
                txt_BeginDate.Text = ds.Tables[0].Rows[0]["JOBM_RECEIVEDATE"].ToString();
                txt_Invote.Text = ds.Tables[0].Rows[0]["JOBM_INVNO"].ToString();
                txt_Estmatedate.Text = ds.Tables[0].Rows[0]["JOBM_ESTIMATEDATE"].ToString();
                txt_State.Text = ChandWordDis(ds.Tables[0].Rows[0]["JOBM_STAGE"].ToString().Trim());
                txt_Account.Text = ds.Tables[0].Rows[0]["JOBM_ACCOUNTID"].ToString();
                txt_Mgrp.Text = ds.Tables[0].Rows[0]["mgrp_code"].ToString();
                ck_redo.Checked = ds.Tables[0].Rows[0]["jobm_redo_yn"].ToString() == "0" ? false : true;
                ck_Modify.Checked = ds.Tables[0].Rows[0]["jobm_amend_yn"].ToString() == "0" ? false : true;
            }
        
        }
        public void ToFocus()
        {
            try
            {
                string _stateWord = string.Empty;
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    _stateWord = dataGridView1.Rows[i].Cells["zjdtl_fda_qty"].Tag == null ? "" : dataGridView1.Rows[i].Cells["zjdtl_fda_qty"].Tag.ToString();
                    if (_stateWord == "edit")
                    {
                        this.dataGridView1.CurrentCell = this.dataGridView1.Rows[i].Cells["zjdtl_fda_qty"];
                        this.dataGridView1.BeginEdit(true);
                        break;
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //如果关键字段"type"，说明是在编辑新行的其它字段的值，不需要做如何操作；
            if (e.RowIndex != -1)
            {
                string _jobm_no = dataGridView1.Rows[e.RowIndex].Cells["jobm_no"].FormattedValue.ToString();
                if (!string.IsNullOrEmpty(strword))
                {
                    _jobm_no = strword;
                }
                //string _lineno = string.Empty;
                //_lineno = dataGridView1.Rows[e.RowIndex].Cells["jdtl_lineno"].FormattedValue.ToString();
                //string _fda_qty = "0";
                //try
                //{
                   // if (InputIsNumber(dataGridView1.Rows[e.RowIndex].Cells["zjdtl_fda_qty"].FormattedValue.ToString().Trim()))
                    //{
                       // _fda_qty = (dataGridView1.Rows[e.RowIndex].Cells["zjdtl_fda_qty"].FormattedValue.ToString());
                       // string sql = "update JOB_PRODUCT set zjdtl_fda_qty ='{0}' where jobm_no ='{1}' and jdtl_lineno ='{2}'";
                        boolEdit = true;
                        //if (DB.ExecuteFromSql(string.Format(sql, _fda_qty, _jobm_no, _lineno)))
                        //{
                           // MessageBox.Show("计费数量已输入，保存成功~！", "系统信息");
                        //}
                   // }
               // }
                //catch (Exception)
                //{
                     
                //}
            }
        }

        public bool InputIsNumber(string _intstr)
        {
            try
            {
                if (string.IsNullOrEmpty(_intstr))
                {
                    return true;
                }
                //int _inta = Int32.Parse(_intstr)
                int _inta = (int)(Math.Floor(float.Parse(_intstr)));          
                return  true  ;
            }
            catch
            {
                return false;

            }
        }

        private void Fm_FDA_Load(object sender, EventArgs e)
        {
            txtCode.Focus();
            dataGridView1.AutoGenerateColumns = false;
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {            

        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
           e.Cancel = true;
        }
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (e.ColumnIndex == 4)
            {
                if (null == e.Value || e.Value.ToString().Trim().Length <= 1)
                {
                    dgv.Rows[e.RowIndex].Cells["zjdtl_fda_qty"].ReadOnly = true;
                    dgv.Rows[e.RowIndex].Cells["zjdtl_fda_qty"].Style.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
                    dgv.Rows[e.RowIndex].Cells["zjdtl_fda_qty"].Tag = "read";
                }
                else
                {
                    dgv.Rows[e.RowIndex].Cells["zjdtl_fda_qty"].ReadOnly = false;
                    dgv.Rows[e.RowIndex].Cells["zjdtl_fda_qty"].Tag = "edit";
                    _intRowEdit = _intRowEdit == -1 ? e.RowIndex : _intRowEdit;

                    if (dgv.Rows[e.RowIndex].Cells["zjdtl_fda_qty"].Value.ToString() != dgv.Rows[e.RowIndex].Cells["value_real"].Value.ToString())
                    {
                        dgv.Rows[e.RowIndex].Cells["zjdtl_fda_qty"].Style.BackColor = Color.Cyan;
                    }
                    else
                    {
                        dgv.Rows[e.RowIndex].Cells["zjdtl_fda_qty"].Style.BackColor =Color.Empty;
                    }
                    //MessageBox.Show(dgv.Rows[e.RowIndex].Cells["zjdtl_fda_qty"].Style.BackColor.ToString(), "");
                }
            }
            try
            {

            }
            catch (Exception)
            {
            }
        }
        #region 处理键盘事件

        private void dgv2_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

            dgvTxt = (DataGridViewTextBoxEditingControl)e.Control; // 赋值   
            dgvTxt.SelectAll();
            dgvTxt.KeyPress += Cells_KeyPress; // 绑定到事件             
        }
        // 自定义事件KeyPress事件
        private void Cells_KeyPress(object sender, KeyPressEventArgs e)
        {
            keyPressXS(e, dgvTxt);  
        }     

        public static void keyPressXS(KeyPressEventArgs e, DataGridViewTextBoxEditingControl dgvTxt)
        {
                       
            if (char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;       //让操作生效
                int j = 0;
                int k = 0;
                bool flag = false;
                if (dgvTxt.Text.Length == 0)
                {
                    if (e.KeyChar == '.'){
                        e.Handled = false;             //让操作失效
                    }
                }
                for (int i = 0; i < dgvTxt.Text.Length; i++)
                {

                    if (dgvTxt.Text[i] == '.')
                    {
                        j++;
                        flag = false;
                    }
                    if (flag)
                    {
                        if (char.IsNumber(dgvTxt.Text[i]) && e.KeyChar != (char)Keys.Back)
                        {
                            k++;
                        }
                    }
                    if (j >= 1)
                    {
                        if (e.KeyChar == '.'){

                            e.Handled = false;             //让操作失效
                        }
                    }
                    if (k == 2)
                    {

                        if (char.IsNumber(dgvTxt.Text[i]) && e.KeyChar != (char)Keys.Back)
                        {

                            if (dgvTxt.Text.Length - dgvTxt.SelectionStart < 3)
                            {

                                if (dgvTxt.SelectedText != dgvTxt.Text)
                                {
                                    e.Handled = true;             ////让操作失效

                                }

                            }

                        }

                    }

                }

            }

            else
            {
                e.Handled = true;
            }  
        }

        #endregion   处理键盘事件

        private void Fm_Job_Pro_Activated(object sender, EventArgs e)
        {
            txtCode.Focus();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                string _stateWord = string.Empty;
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    _stateWord = dataGridView1.Rows[i].Cells["zjdtl_fda_qty"].Tag==null?"":dataGridView1.Rows[i].Cells["zjdtl_fda_qty"].Tag.ToString();
                    if (_stateWord == "edit")
                    {
                        this.dataGridView1.CurrentCell = this.dataGridView1.Rows[i].Cells["zjdtl_fda_qty"];
                        this.dataGridView1.BeginEdit(true);
                        break;
                    }
                }
            }
            catch (Exception)
            {
            }
            timer1.Stop();
            timer1.Enabled = false;
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount < 1)
            {
                return;
            }           
            
            if (MessageBox.Show("确定保存吗?", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sql = "update JOB_PRODUCT set zjdtl_fda_qty ='{0}' where jobm_no ='{1}' and jdtl_lineno ='{2}'";
                int insetRow = 0;
                string _jobm_no = string.Empty;
                string _lineno = string.Empty;
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    string _fda_qty = "0";
                    try
                    {
                        if (InputIsNumber(dataGridView1.Rows[i].Cells["zjdtl_fda_qty"].FormattedValue.ToString().Trim()))
                        {
                            if (dataGridView1.Rows[i].Cells["zjdtl_fda_qty"].FormattedValue.ToString() == "")   //
                            {
                                _fda_qty = "";
                            }
                            else
                            {
                                _fda_qty = ((int)(Math.Floor(float.Parse((dataGridView1.Rows[i].Cells["zjdtl_fda_qty"].FormattedValue.ToString()))))).ToString();
                            }
                            _lineno = dataGridView1.Rows[i].Cells["jdtl_lineno"].FormattedValue.ToString();
                            _jobm_no = dataGridView1.Rows[i].Cells["jobm_no"].FormattedValue.ToString();
                            insetRow = DB.ExecuteFromSql(string.Format(sql, _fda_qty, _jobm_no, _lineno)) == true ? insetRow + 1 : insetRow;
                            dataGridView1.Rows[i].Cells["value_real"].Value = _fda_qty == "" ? "0" : _fda_qty;                             //
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }

                MessageBox.Show("保存成功!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCode.Text = "";
                txtCode.Focus();
                boolEdit = false;
            }
        }

        private void Fm_Job_Pro_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (boolEdit)
            {
                if (MessageBox.Show("修改还未保存，你确定退出吗?", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void Fm_Job_Pro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S && e.Control)
            {
                txtCode.Focus();
                if (dataGridView1.RowCount < 1)
                {
                    return;
                }   
                e.Handled = true;   //将Handled设置为true，指示已经处理过KeyPress事件
                //////////////////////
                string sql = "update JOB_PRODUCT set zjdtl_fda_qty ='{0}' where jobm_no ='{1}' and jdtl_lineno ='{2}'";
                int insetRow = 0;
                string _jobm_no = string.Empty;
                string _lineno = string.Empty;
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    string _fda_qty = "0";
                    try
                    {
                        if (InputIsNumber(dataGridView1.Rows[i].Cells["zjdtl_fda_qty"].FormattedValue.ToString().Trim()))
                        {
                            if (dataGridView1.Rows[i].Cells["zjdtl_fda_qty"].FormattedValue.ToString() == "")   //
                            {
                                _fda_qty = "";
                            }
                            else
                            {
                                _fda_qty = ((int)(Math.Floor(float.Parse((dataGridView1.Rows[i].Cells["zjdtl_fda_qty"].FormattedValue.ToString()))))).ToString();
                            }
                            _lineno = dataGridView1.Rows[i].Cells["jdtl_lineno"].FormattedValue.ToString();
                            _jobm_no = dataGridView1.Rows[i].Cells["jobm_no"].FormattedValue.ToString();
                            insetRow = DB.ExecuteFromSql(string.Format(sql, _fda_qty, _jobm_no, _lineno)) == true ? insetRow + 1 : insetRow;
                            dataGridView1.Rows[i].Cells["value_real"].Value = _fda_qty == "" ? "0" : _fda_qty;  //
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }

                MessageBox.Show("保存成功!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCode.Text = "";
                txtCode.Focus();
                boolEdit = false;
                ///////////////////////////////
            }

        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S && e.Control)
            {
                if (dataGridView1.RowCount < 1)
                {
                    return;
                }   
                e.Handled = true;   //将Handled设置为true，指示已经处理过KeyPress事件
                //////////////////////
                string sql = "update JOB_PRODUCT set zjdtl_fda_qty ='{0}' where jobm_no ='{1}' and jdtl_lineno ='{2}'";
                int insetRow = 0;
                string _jobm_no = string.Empty;
                string _lineno = string.Empty;
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    string _fda_qty = "0";
                    try
                    {
                        if (InputIsNumber(dataGridView1.Rows[i].Cells["zjdtl_fda_qty"].FormattedValue.ToString().Trim()))
                        {
                            //_fda_qty = (dataGridView1.Rows[i].Cells["zjdtl_fda_qty"].FormattedValue.ToString());
                            _fda_qty = ((int)(Math.Floor(float.Parse((dataGridView1.Rows[i].Cells["zjdtl_fda_qty"].FormattedValue.ToString()))))).ToString();
                            _lineno = dataGridView1.Rows[i].Cells["jdtl_lineno"].FormattedValue.ToString();
                            _jobm_no = dataGridView1.Rows[i].Cells["jobm_no"].FormattedValue.ToString();
                            insetRow = DB.ExecuteFromSql(string.Format(sql, _fda_qty, _jobm_no, _lineno)) == true ? insetRow + 1 : insetRow;

                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                //MessageBox.Show("保存成功!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                boolEdit = false;
                ///////////////////////////////
            }
            if (e.KeyCode == Keys.Home)
            {
                txtCode.Focus();
                txtCode.SelectAll();                
            }
            if (e.KeyCode == Keys.Escape)
            {
                txtCode.Focus();
                txtCode.SelectAll();

            }
        }
       
    }
}
