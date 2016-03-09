using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CaseInquire.helperclass;

namespace CaseInquire
{
    public partial class Fm_DirectReply : Form
    {
        public Fm_DirectReply()
        {
            InitializeComponent();
        }

        public Fm_DirectReply(string pCtrnmId, string pJobNo)
            : this()
        {
            ctrnmId = pCtrnmId;
            jobNo = pJobNo;
        }

        string ctrnmId = string.Empty, jobNo = string.Empty;

        private void btnReply_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cmbRType.Text.Trim()))
                {
                    MessageBox.Show("请选择归类类型！", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //将问单状态设置为已回复状态
                string sqlStr = string.Format(
                @"update ztci_ctrnm_tran_master set ctrnm_status = '33',ctrnm_reply_content='{0}',ctrnm_reply_by='{1}',
                ctrnm_reply_on=sysdate,ctrnm_upd_by='{2}',ctrnm_isrepeat = {3} where ctrnm_jobm_no='{4}' and ctrnm_id = '{5}'", 
                rtbContent.Text.Trim(), PublicClass.LoginName, PublicClass.LoginName, cmbRType.SelectedValue.ToString().Trim().Equals("3") ? 1:0, jobNo, ctrnmId);

                if (ZComm1.Oracle.DB.ExecuteFromSql(sqlStr))
                {
                    //记录日志
                    PublicMethod.Logging(ctrnmId, "客服直接回复问单");
                }
                this.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Fm_DirectReply_Load(object sender, EventArgs e)
        {
            try
            {
                cmbRType.DisplayMember = "udc_value";
                cmbRType.ValueMember = "udc_code";
                cmbRType.DataSource = getRType();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 获取回复类型
        /// </summary>
        /// <returns></returns>
        private DataTable getRType()
        {
            string sqlStr = @"select udc_code,udc_value from zt00_udc_udcode where udc_sys_code='CASEINQ' and udc_key='RTYPE'";
            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
        }

        private void cmbRType_SelectedIndexChanged(object sender, EventArgs e)
        {
            rtbContent.Text = cmbRType.Text;
        }

        //问单归类
        private void btnCategory_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cmbRType.Text.Trim()))
                {
                    MessageBox.Show("请选择归类类型！", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //将问单归类
                string sqlStr = string.Format(
                @"update ztci_ctrnm_tran_master set ctrnm_cs_process_type = '{0}',ctrnm_upd_on=sysdate,ctrnm_upd_by='{1}',
                ctrnm_isrepeat = {2} where ctrnm_jobm_no='{3}' and ctrnm_id = '{4}'", 
                cmbRType.SelectedValue.ToString().Trim(), PublicClass.LoginName, cmbRType.SelectedValue.ToString().Trim().Equals("1") ? 1 : 0, jobNo, ctrnmId);

                if (ZComm1.Oracle.DB.ExecuteFromSql(sqlStr))
                {
                    //记录日志
                    PublicMethod.Logging(ctrnmId, "客服归类问单");
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
