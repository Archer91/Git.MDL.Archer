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
    public partial class Fm_MgrpGroup : Form
    {
        public Fm_MgrpGroup()
        {
            InitializeComponent();
        }

        private void Fm_MgrpGroup_Load(object sender, EventArgs e)
        {
            try
            {
                //获取角色列表
                DataTable tmpDt = GetRoles();
                cmbRole.DataSource = tmpDt;
                cmbRole.DisplayMember = "role_code";
                cmbRole.ValueMember = "role_code";
                //获取ObjCode
                cmbObj.DataSource = GetObjValue();
                cmbObj.DisplayMember = "sobj_code";
                cmbObj.ValueMember = "sobj_code";
                //获取货类
                cmbMgrp.DataSource = GetMgrpCode();
                cmbMgrp.DisplayMember = "mgrp_code";
                cmbMgrp.ValueMember = "mgrp_code";

                dgvRole.DataSource = tmpDt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 获取货类
        /// </summary>
        /// <returns></returns>
        private DataTable GetMgrpCode()
        {
            string sqlStr = "select distinct mgrp_code from account order by mgrp_code";
            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
        }

        /// <summary>
        /// 获取ObjCode列表
        /// </summary>
        /// <returns></returns>
        private DataTable GetObjValue()
        {
            string sqlStr = "select sobj_code,sobj_name,sobj_description from zt00_sobj_securityobject where sobj_code like 'R_CASEINQ%' and sobj_status ='1' order by sobj_code";
            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
        }

        /// <summary>
        /// 获取角色列表(只获取问单系统涉及的角色R_CASEINQ开头的)
        /// </summary>
        /// <returns></returns>
        private DataTable GetRoles()
        {
            string sqlStr = "select role_code,role_description,case role_status when '1' then '有效' else '失效' end role_status from ZT00_ROLE_INFO where  role_code like 'R_CASEINQ%'";

            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
        }

        //选择角色
        private void dgvRole_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvRole.CurrentRow == null)
                {
                    return;
                }

                //根据角色编码获取对应的货类列表
                dgvMgrp.DataSource = GetMgrpByRole(dgvRole.CurrentRow.Cells[0].Value.ToString().Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 根据角色Code获取对应的货类信息
        /// </summary>
        /// <param name="pRoleCode">角色Code</param>
        /// <returns></returns>
        private DataTable GetMgrpByRole(string pRoleCode)
        {
            string sqlStr = string.Format("select auto_code,auto_obj_code,auto_obj_value,auto_rights from zt00_auto_authobject where auto_code = '{0}' order by auto_obj_value",pRoleCode);
            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
        }

        private void dgvRole_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            PublicMethod.dgv_RowPostPaint(sender, e);
        }

        private void dgvMgrp_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            PublicMethod.dgv_RowPostPaint(sender, e);
        }

        //添加货类到分组绑定
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbRole.SelectedIndex < 0 || cmbObj.SelectedIndex < 0 || cmbMgrp.Text.Trim().Length <= 0)
                {
                    MessageBox.Show("请完整填写信息！", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //写入货类与角色的关联信息
                if(SaveMgrpWithRole(cmbRole.SelectedValue.ToString(),cmbObj.SelectedValue.ToString(),cmbMgrp.Text.Trim()))
                {
                    dgvRole_SelectionChanged(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 存入货类与分组关联关系
        /// </summary>
        /// <param name="pRoleCode">角色Code</param>
        /// <param name="pObjCode">ObjCode</param>
        /// <param name="pObjValue">ObjValue</param>
        /// <returns></returns>
        private bool SaveMgrpWithRole(string pRoleCode, string pObjCode, string pObjValue)
        {
            string sqlStr = string.Format("insert into zt00_auto_authobject(auto_code,auto_obj_code,auto_obj_value,auto_rights,auto_crt_by) values('{0}','{1}','{2}','{3}','{4}')",pRoleCode,pObjCode,pObjValue,'3',PublicClass.LoginName);
            return ZComm1.Oracle.DB.ExecuteFromSql(sqlStr);
        }



    }
}
