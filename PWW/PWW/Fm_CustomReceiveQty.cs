using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace PWW
{
    public partial class Fm_CustomReceiveQty : Form
    {
        public Fm_CustomReceiveQty()
        {
            InitializeComponent();
        }

        private void Fm_CustomReceiveQty_Load(object sender, EventArgs e)
        {
            try
            {
                //责任线
                cmbProductLine.DisplayMember = "udc_description";
                cmbProductLine.ValueMember = "udc_code";
                cmbProductLine.DataSource = getLines();

                dgvInfo.DataSource = getExistInfo(dtpDate.Value.Year.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                dgvInfo.DataSource = getExistInfo(dtpDate.Value.Year.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void chkType_CheckedChanged(object sender, EventArgs e)
        {
            if (chkType.Checked)
            {
                txtHKQty.Enabled = true;
                txtWGQty.Enabled = true;
                txtHKQty.Text = "0";
                txtWGQty.Text = "0";
            }
            else
            {
                txtHKQty.Enabled = false;
                txtWGQty.Enabled = false;
                txtHKQty.Text = "0";
                txtWGQty.Text = "0";
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtpDate.Text.Trim().Length <= 0 ||
                    cmbProductLine.Text.Trim().Length <= 0 ||
                    txtReceiveQty.Text.Trim().Length <= 0)
                {
                    MessageBox.Show("请完整填写标注星号的信息", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (chkType.Checked)
                {
                    if (txtHKQty.Text.Trim().Length <= 0 || txtWGQty.Text.Trim().Length <= 0)
                    {
                        MessageBox.Show("区分HK和外国线，请填写回货数", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                string strRegex = @"^\d+$";
                Regex rg = new Regex(strRegex);
                if (!rg.IsMatch(txtReceiveQty.Text.Trim()))
                {
                    MessageBox.Show("回货数只能填写数字", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (chkType.Checked)
                {
                    if (!rg.IsMatch(txtHKQty.Text.Trim()) || !rg.IsMatch(txtWGQty.Text.Trim()))
                    {
                        MessageBox.Show("回货数只能填写数字", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if ((Int32.Parse(txtHKQty.Text.Trim()) + Int32.Parse(txtWGQty.Text.Trim())) != Int32.Parse(txtReceiveQty.Text.Trim()))
                    {
                        MessageBox.Show("区分HK和外国线，回货数之和与总回货数不一致", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                //先校验所填写信息是否已存在记录
                DataTable dtResult = IsExist(dtpDate.Value.Year.ToString(), dtpDate.Value.ToString("yy/MM"), cmbProductLine.SelectedValue.ToString());
                if (null != dtResult && dtResult.Rows.Count > 0)
                {
                    if (DialogResult.Yes == MessageBox.Show("责任线在当前年月已存在回货数【" + dtResult.Rows[0]["JPLQ_RECEIVE_QTY"].ToString() + "】\n HK回货数【" + dtResult.Rows[0]["JPLQ_EXTEND_QTY1"].ToString() + "】\n 外国线回货数【" + dtResult.Rows[0]["JPLQ_EXTEND_QTY2"].ToString() + "】\n确定需要更新吗？", "MDL-提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2))
                    {
                        //更新现有记录
                        CustomInfo cInfo = new CustomInfo(1, dtpDate.Value.Year.ToString(), dtpDate.Value.ToString("yy/MM"), cmbProductLine.SelectedValue.ToString(), Int32.Parse(txtReceiveQty.Text.Trim()), rtbRemark.Text, chkType.Checked ? Int32.Parse(txtHKQty.Text.Trim()) : 0, chkType.Checked ? Int32.Parse(txtWGQty.Text.Trim()) : 0);
                        if (SaveInfo(cInfo, DB.loginUserName))
                        {
                            MessageBox.Show("保存信息成功！", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    //写入新记录
                    CustomInfo cInfo = new CustomInfo(0, dtpDate.Value.Year.ToString(), dtpDate.Value.ToString("yy/MM"), cmbProductLine.SelectedValue.ToString(), Int32.Parse(txtReceiveQty.Text.Trim()), rtbRemark.Text, chkType.Checked ? Int32.Parse(txtHKQty.Text.Trim()) : 0, chkType.Checked ? Int32.Parse(txtWGQty.Text.Trim()) : 0);
                    if (SaveInfo(cInfo, DB.loginUserName))
                    {
                        MessageBox.Show("保存信息成功！", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                dgvInfo.DataSource = getExistInfo(dtpDate.Value.Year.ToString());
            }
        }

        /// <summary>
        /// 获取责任线
        /// </summary>
        /// <returns></returns>
        private DataTable getLines()
        {
            string sqlStr = @"select udc_code , udc_description  
                            from zt00_udc_udcode 
                            where udc_sys_code='QC' 
                            and udc_category='VALUE' 
                            and udc_key='PRODUCTFLOOR'  
                            and udc_status='1' ";
            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
        }

        /// <summary>
        /// 获取当前年份下已经设置了的回货数记录
        /// </summary>
        /// <param name="pYear">年份</param>
        /// <returns></returns>
        private DataTable getExistInfo(string pYear)
        {
            string strSql = string.Format(
            @"select a.jplq_month 年月,b.udc_description 责任线,a.jplq_receive_qty 总回货数,
                     a.jplq_extend_qty1 HK线回货数,jplq_extend_qty2 外国线回货数 
            from zt_job_productline_qty a 
            join zt00_udc_udcode b on a.jplq_product_line = b.udc_code 
            and b.udc_sys_code='QC' 
            and b.udc_category='VALUE' 
            and b.udc_key='PRODUCTFLOOR'  
            and b.udc_status='1'
            where a.jplq_year = '{0}' and a.jplq_status = '1'",pYear);
            return ZComm1.Oracle.DB.GetDSFromSql1(strSql).Tables[0];
        }

        /// <summary>
        /// 校验是否已存在记录
        /// </summary>
        /// <param name="pYear">年份</param>
        /// <param name="pMonth">月份</param>
        /// <param name="pLine">责任线</param>
        /// <returns></returns>
        private DataTable IsExist(string pYear, string pMonth, string pLine)
        {
            string strSql =string.Format( @"
            select * from zt_job_productline_qty 
            where jplq_year = '{0}' and jplq_month='{1}' and jplq_product_line ='{2}' and jplq_status='1'", pYear, pMonth, pLine);
           return ZComm1.Oracle.DB.GetDSFromSql1(strSql).Tables[0];
            
        }

        /// <summary>
        /// 保存信息
        /// </summary>
        /// <param name="pFlag">标记：1为更新，0为新增</param>
        /// <param name="pYear">年份</param>
        /// <param name="pMonth">月份</param>
        /// <param name="pLine">责任线</param>
        /// <param name="pQty">回货份数</param>
        /// <param name="pRemark">备注</param>
        /// <param name="pUser">操作者</param>
        /// <returns></returns>
        private bool SaveInfo(CustomInfo pInfo, string pUser)
        {
            string strSql = string.Empty;
            if (pInfo.Flag.Equals(1))
            {
                strSql = string.Format(
                @"update zt_job_productline_qty set jplq_receive_qty={0},jplq_extend_qty1 ={1},
                jplq_extend_qty2={2},jplq_upd_on=sysdate,jplq_upd_by='{3}',jplq_remark='{4}'
                where jplq_year='{5}' and jplq_month='{6}' and jplq_product_line='{7}'",
                pInfo.Qty,pInfo.HKQty,pInfo.WGQty,pUser,pInfo.Remark,pInfo.Year,pInfo.Month,pInfo.Line);
            }
            else if(pInfo.Flag.Equals(0))
            {
                strSql = string.Format(
                @"insert into zt_job_productline_qty(jplq_year,jplq_month,jplq_product_line,
                jplq_receive_qty,jplq_extend_qty1,jplq_extend_qty2,jplq_status, jplq_crt_on,jplq_crt_by,jplq_remark)
                values('{0}','{1}','{2}',{3},{4},{5},'1',sysdate,'{6}','{7}')", 
                pInfo.Year,pInfo.Month,pInfo.Line,pInfo.Qty,pInfo.HKQty,pInfo.WGQty,pUser,pInfo.Remark);
            }

            return ZComm1.Oracle.DB.ExecuteFromSql(strSql);
        }

    }

    struct CustomInfo
    {
        public int Flag
        {
            get;
            private set;
        }

        public string Year
        {
            get;
            private set;
        }

        public string Month
        {
            get;
            private set;
        }

        public string Line
        {
            get;
            private set;
        }

        public int Qty
        {
            get;
            private set;
        }

        public string Remark
        {
            get;
            private set;
        }

        public int HKQty
        {
            get;
            private set;
        }

        public int WGQty
        {
            get;
            private set;
        }

        public CustomInfo(int pFlag,string pYear,string pMonth,string pLine,int pQty,string pRemark,int pHKQty,int pWGQty) :this()
        {
            this.Flag = pFlag;
            this.Year = pYear;
            this.Month = pMonth;
            this.Line = pLine;
            this.Qty = pQty;
            this.Remark = pRemark;
            this.HKQty = pHKQty;
            this.WGQty = pWGQty;
        }
    }
}
