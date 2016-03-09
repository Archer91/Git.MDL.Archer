using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using ZComm1;
using ZComm1.Oracle;

namespace CaseInquire
{
	public partial class MDI_W : Form
	{
		public DataTable menuModuleTable;
		public bool isAdmin;
		private List<string> lRoles;
		private string topMenuItem = "0070";
		public MDI_W()
		{
			InitializeComponent();
			if (this.LoginName.Text.Trim() == "") this.LoginName.Text = DB.loginUserName;
			this.initialMenuItem.Text = "";
		}
		public MDI_W(string loginName)
		{
			InitializeComponent();
			this.LoginName.Text = loginName;
			if (this.LoginName.Text.Trim() == "") this.LoginName.Text = DB.loginUserName;
			this.initialMenuItem.Text = "";
		}
		public MDI_W(string loginName, string initMenuItem)
		{
			InitializeComponent();
			this.LoginName.Text = loginName;
			if (this.LoginName.Text.Trim() == "") this.LoginName.Text = DB.loginUserName;
			this.initialMenuItem.Text = initMenuItem;
		}
		public MDI_W(string loginName, string initMenuItem, string sysTopMenuItem)
		{
			InitializeComponent();
			this.LoginName.Text = loginName;
			if (this.LoginName.Text.Trim() == "") this.LoginName.Text = DB.loginUserName;
			this.initialMenuItem.Text = initMenuItem;
			if (sysTopMenuItem.Trim() != "") topMenuItem = sysTopMenuItem;
		}

		private void PWMDI_W_Load(object sender, EventArgs e)
		{
			GetRoles();
            //20150323
            this.userLoginName.Text = "Welcome : " + DB.V(DB.GetDSFromSql("select uacc_name from zt00_uacc_useraccount where uacc_code=" + DB.sp(this.LoginName.Text)));
            toolTip1.SetToolTip(this.userLoginName, "请双击鼠标修改密码");

			string strSql = @"select zt00_menu_info.*,1 as MFUN_ORDER_NO from zt00_menu_info 
      where menu_menuid in ( SELECT distinct autm_menuid from ZT00_AUTM_AUTHMENU
          WHERE
          AUTM_CODE IN ('" + string.Join("','", lRoles) + @"')  AND AUTM_STATUS = '1' )
connect by menu_parentid = prior menu_menuid start with menu_parentid=" + DB.sp(topMenuItem); //0020 is PWW system top menu ID  ,0050 eForm 
			DataSet ds = DB.GetDSFromSql(strSql);
			menuModuleTable = ds.Tables[0]; //取得所有数据得到 DataTable
			ZMDIMenu zMenu;
			zMenu = new ZMDIMenu(SysMainMenu, menuModuleTable);
			zMenu.LoadSubMenu(null, topMenuItem);
			//zMenu.ShowForm(s11);

            Text = "问单系统V1.2 " + DB.ConnectedDBName + "  User:" + DB.loginUserName;
		}
		public void GetRoles()
		{
			string strSql = @"SELECT DISTINCT UARO_ROLE FROM ZT00_UARO_USERROLE WHERE NVL(UARO_STATUS,'0')<>'0'  AND UARO_USER ='" + this.LoginName.Text + @"' AND exists (select 'x'  from ZT00_UACC_USERACCOUNT where upper(UACC_CODE)='" + this.LoginName.Text + @"')
          UNION
          SELECT DISTINCT UARO_ROLE FROM ZT00_UARO_USERROLE WHERE NVL(UARO_STATUS,'0')<>'0' CONNECT BY UARO_ROLE = PRIOR UARO_USER START WITH UARO_ROLE IN (SELECT DISTINCT UARO_ROLE FROM ZT00_UARO_USERROLE WHERE NVL(UARO_STATUS,'0')<>'0'  AND UARO_USER ='" + this.LoginName.Text + @"' AND exists (select 'x'  from ZT00_UACC_USERACCOUNT where upper(uacc_code)='" + this.LoginName.Text + @"') UNION SELECT '" + this.LoginName.Text + @"' UARO_ROLE FROM DUAL )
          UNION
          SELECT DISTINCT UARO_USER AS UARO_ROLE FROM ZT00_UARO_USERROLE WHERE NVL(UARO_STATUS,'0')<>'0' and exists ( select 'x' from zt00_role_info where role_code=uaro_user) CONNECT BY UARO_ROLE = PRIOR UARO_USER START WITH UARO_ROLE IN (SELECT DISTINCT UARO_ROLE FROM ZT00_UARO_USERROLE WHERE NVL(UARO_STATUS,'0')<>'0'  AND UARO_USER ='" + this.LoginName.Text + @"' AND exists (select 'x'  from ZT00_UACC_USERACCOUNT where upper(uacc_code)='" + this.LoginName.Text + @"') UNION SELECT '" + this.LoginName.Text + @"' UARO_ROLE FROM DUAL )
          UNION SELECT '" + this.LoginName.Text + @"' UARO_ROLE FROM DUAL ";
			DataSet ds = DB.GetDSFromSql(strSql);
			lRoles = ds.ToList();
			isAdmin = lRoles.Contains("R_MDLCRM_ADMIN");

		}

        public bool CheckchildFrmExist(string childFrmName)
        {
            foreach (Form childFrm in this.MdiChildren)
            {
                if (childFrm.GetType().Name == childFrmName)
                {
                    if (childFrm.WindowState == FormWindowState.Minimized)
                    {
                        childFrm.WindowState = FormWindowState.Maximized;

                    }
                    childFrm.Activate();
                    return true;
                }
            }
            return false;
        }

        private void userLoginName_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Fm_ChangePwd frmPwd = new Fm_ChangePwd(); //modify by YF add 
            frmPwd.Owner = this;
            //frmPwd.ShowDialog(this);
            frmPwd.ShowDialog();
        }

        private void MDI_W_Resize(object sender, EventArgs e)
        {
            userLoginName.Left = Width - userLoginName.Width - 100;
        }
	}
}