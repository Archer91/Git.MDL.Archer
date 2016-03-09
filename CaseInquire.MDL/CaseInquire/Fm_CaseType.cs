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
    public partial class Fm_CaseType : Form
    {
        public Fm_CaseType()
        {
            InitializeComponent();
        }

        private void Fm_CaseType_Load(object sender, EventArgs e)
        {
            try
            {
                //获取部门信息
                cmbDept.DisplayMember = "udc_value";
                cmbDept.ValueMember = "udc_code";
                cmbDept.DataSource = PublicMethod.GetDepartment();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbType.Text = string.Empty;

                //获取问单类型信息
                cmbType.DisplayMember = "oname";
                cmbType.ValueMember = "form_id";
                cmbType.DataSource = GetTemplateTypeCopy(cmbDept.SelectedValue.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 获取已存在的模板类型(有效状态）
        /// </summary>
        /// <param name="pDeptCode">部门Code</param>
        /// <returns></returns>
        private DataTable GetTemplateTypeCopy(string pDeptCode)
        {
            string sqlStr = string.Format(
            @"select distinct b.form_id,form_code,form_name,form_ver,form_department,b.form_name || '-' || b.form_ver oname 
            from ztci_frmd_det a  
            join ztci_form_master b on a.frmd_form_id = b.form_id 
            where b.form_department = '{0}' and b.form_status = '1' 
            order by oname", pDeptCode);

            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
        }

        string formId = string.Empty, formCode = string.Empty, formVer = string.Empty;
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbType.Text.Trim().Length <= 0)
                {
                    MessageBox.Show("请选择具体的问单类型！", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                formId = cmbType.SelectedValue.ToString();
                DataTable tmpDt = cmbType.DataSource as DataTable;
                DataRow tmpDr = tmpDt.Select(string.Format(@"form_id = '{0}'", formId))[0];
                formCode=tmpDr["form_code"].ToString().Trim();
                formVer = tmpDr["form_ver"].ToString().Trim();

                ReturnCaseTypeEvent(formId,formCode,formVer);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            formId = string.Empty;
            formCode = string.Empty;
            formVer = string.Empty;
            ReturnCaseTypeEvent(formId, formCode, formVer);
            this.Close();
        }

        public delegate void CaseTypeCallback(string pFormId,string pFormCode,string pFormVer);
        public event CaseTypeCallback ReturnCaseTypeEvent;
    }
}
