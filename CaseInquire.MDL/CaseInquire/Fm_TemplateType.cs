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
    public partial class Fm_TemplateType : Form
    {
        public Fm_TemplateType()
        {
            InitializeComponent();
        }

        DataView dv = new DataView();
        private void Fm_TemplateType_Load(object sender, EventArgs e)
        {
            try
            {
                //获取部门信息
                cmbDept.DisplayMember = "udc_value";
                cmbDept.ValueMember = "udc_code";
                cmbDept.DataSource = PublicMethod.GetDepartment();
                

                //获取模板类型信息
                //dv.Table = PublicMethod.GetCaseType(false);
                //dgvType.DataSource = dv;
                //dgvType.Columns[0].Visible = false;

                //cmbTypeName.DisplayMember = "form_name"; 
                //cmbTypeName.DataSource = dv.ToTable(true, "form_name");
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbDept.SelectedValue.ToString().Trim().Length <= 0 || cmbTypeName.Text.Trim().Length <= 0 || txtVersion.Text.Trim().Length <= 0 || txtCode.Text.Trim().Length <= 0)
                {
                    MessageBox.Show("请完整输入信息！", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //保存模板类型
                SaveTemplateType(cmbDept.SelectedValue.ToString(), cmbTypeName.Text.Trim(), txtVersion.Text.Trim(),txtCode.Text.Trim());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 保存模板类型
        /// </summary>
        /// <param name="pDept">部门Code</param>
        /// <param name="pTypeName">模板类型名</param>
        /// <param name="pTypeVersion">模板类型版本</param>
        /// <param name="pCode">模板类型编码</param>
        private void SaveTemplateType(string pDept, string pTypeName, string pTypeVersion,string pCode)
        {
            string sqlStr = string.Format(@"select form_id from ztci_form_master where form_name='{0}' and form_ver='{1}'", pTypeName, pTypeVersion);
            DataTable tmpDt = ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
            if(tmpDt != null && tmpDt.Rows.Count > 0)
            {
                MessageBox.Show("模板类型【"+pTypeName+"】已存在！","MDL-提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            sqlStr = string.Format(@"insert into ztci_form_master(form_code,form_department,form_name,form_ver) values('{0}','{1}','{2}','{3}')",
                pCode,pDept, pTypeName, pTypeVersion);

            if (ZComm1.Oracle.DB.ExecuteFromSql(sqlStr))
            {
                Fm_TemplateType_Load(null, null);
                cmbTypeName.Text = string.Empty;
                txtCode.Text = string.Empty;
                txtVersion.Text = string.Empty;
                MessageBox.Show("保存成功！", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("保存失败！", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvType_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            PublicMethod.dgv_RowPostPaint(sender, e);
        }

        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbTypeName.Text = string.Empty;
                txtCode.Text = string.Empty;
                txtVersion.Text = string.Empty;

                if (cmbDept.Items.Count <= 0)
                {
                    return;
                }
                //获取模板类型信息
                dv.Table = PublicMethod.GetCaseType(false);
                dgvType.DataSource = dv;
                dgvType.Columns[0].Visible = false;

                if (null == dv || null == dv.Table || dv.Table.Rows.Count <= 0)
                {
                    return;
                }

                dv.RowFilter = "form_department = '"+cmbDept.SelectedValue.ToString()+"'";
                cmbTypeName.DisplayMember = "form_name"; 
                cmbTypeName.DataSource = dv.ToTable(true, "form_name");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //启用模板
        private void tsmiEnable_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvType.Rows.Count <= 0 || dgvType.SelectedRows.Count <= 0)
                {
                    return;
                }
                ChangeCaseTypeStatus(dgvType.SelectedRows[0].Cells["form_id"].Value.ToString(), "1");
                //刷新
                cmbDept_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //失效模板
        private void tsmiDisable_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvType.Rows.Count <= 0 || dgvType.SelectedRows.Count <= 0)
                {
                    return;
                }
                ChangeCaseTypeStatus(dgvType.SelectedRows[0].Cells["form_id"].Value.ToString(), "0");
                //刷新
                cmbDept_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 更改问单模板状态（启用或失效）
        /// </summary>
        /// <param name="pFormId">问单模板ID</param>
        /// <param name="pStatus">状态值</param>
        private void ChangeCaseTypeStatus(string pFormId,string pStatus)
        {
            string sqlStr = string.Format(
            @"update ztci_form_master set form_status='{0}',form_upd_by='{1}' where form_id='{2}' ",pStatus, PublicClass.LoginName,pFormId);
            ZComm1.Oracle.DB.ExecuteFromSql(sqlStr);
        }

    }
}
