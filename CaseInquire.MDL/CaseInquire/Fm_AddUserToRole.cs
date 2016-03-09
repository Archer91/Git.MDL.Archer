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
    public partial class Fm_AddUserToRole : Form
    {
        public Fm_AddUserToRole()
        {
            InitializeComponent();
        }

        private void Fm_AddUserToRole_Load(object sender, EventArgs e)
        {
            try
            {
                //获取角色列表
                cmbRole.DataSource = GetRoles();
                cmbRole.DisplayMember = "role_code";
                cmbRole.ValueMember = "role_code";
                //获取所有用户列表
                dv.Table = GetUsers();
                dgvUser.DataSource =dv;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string tmpUser = dgvUser.SelectedRows[0].Cells[0].Value.ToString().Trim();
                string tmpRole = cmbRole.SelectedValue.ToString();
                //将用户添加到当前角色
                string sqlStr = string.Format("select uaro_user,uaro_role from ZT00_UARO_USERROLE where uaro_user='{0}' and uaro_role = '{1}'",tmpUser,tmpRole);
                DataTable tmpDt = ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
                if(tmpDt == null || tmpDt.Rows.Count <= 0)
                {
                    sqlStr = string.Format("insert into ZT00_UARO_USERROLE(Uaro_User,Uaro_Role) values('{0}','{1}')", tmpUser, tmpRole);
                    if (ZComm1.Oracle.DB.ExecuteFromSql(sqlStr))
                    {
                        MessageBox.Show("加入成功！", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("加入失败！", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("当前用户【" + tmpUser + "】已加入角色【" + tmpRole + "】", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        DataView dv = new DataView();
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //对用户编码、用户名进行筛选
                dv.RowFilter = "uacc_code like '%" + txtSearch.Text.Trim() +"%' or uacc_name like '%" + txtSearch.Text.Trim() + "%'";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        private DataTable GetRoles()
        {
            string sqlStr = "select role_code,role_description,case role_status when '1' then '有效' else '失效' end role_status from ZT00_ROLE_INFO where  role_code like 'R_CASEINQ%'";

            DataSet ds = ZComm1.Oracle.DB.GetDSFromSql1(sqlStr);
            return ds.Tables[0];
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        private DataTable GetUsers()
        {
            string sqlStr = "select uacc_code,uacc_name,case uacc_status when '1' then '有效' else '失效' end uacc_status from  ZT00_UACC_USERACCOUNT ";

            DataSet ds = ZComm1.Oracle.DB.GetDSFromSql1(sqlStr);
            return ds.Tables[0];
        }

        private void dgvUser_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            PublicMethod.dgv_RowPostPaint(sender, e);

        }

        private void dgvUser_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                if (e.Value.ToString().Equals("失效"))
                {
                    dgvUser.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                }
            }
        }

    }
}
