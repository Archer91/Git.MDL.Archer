using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using ZComm1;

namespace MDL_CRM
{
	public partial class MDI_W : Form
	{
		public DataTable menuModuleTable;
		public bool isAdmin;
		private List<string> lRoles;
		private string topMenuItem = "0090";
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
            this.userLoginName.Text = "Welcome : " + pubcls.UserName+" 公司："+pubcls.CompanyName; //DB.V(DB.GetDSFromSql("select uacc_name from zt00_uacc_useraccount where uacc_code=" + DB.sp(this.LoginName.Text)));
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

            Text = "MDL CRM系统V1.0 " + DB.ConnectedDBName + "  User:" + DB.loginUserName;
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

        private void mnugl_Click(object sender, EventArgs e)
        {
            Fm_SaleOrder frm = new Fm_SaleOrder();
            frm.MdiParent = this;            
            frm.Show();

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

        //private void 图片加载示例ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    ///获取数据库中的字符串
        //    List<string> getFileListFromDatabase = new List<string>();
        //    //getFileListFromDatabase.Add(@"D:\flash\Test\1.Jpeg");
        //    //getFileListFromDatabase.Add(@"D:\flash\Test\2.Jpeg");

        //    getFileListFromDatabase.Add(@"D:\work\photo\图片测试\IMG20150801143022.jpg");
        //    getFileListFromDatabase.Add(@"D:\work\photo\图片测试\sss222.pdf");
        //    getFileListFromDatabase.Add(@"\\fileserver\公司活动图片集\20150801阳江游\IMG20150801143022.jpg");
        //    getFileListFromDatabase.Add(@"\\fileserver\公司活动图片集\20150801阳江游\IMG20150802130845.jpg");
        //    getFileListFromDatabase.Add(@"\\fileserver\公司活动图片集\20150801阳江游\IMG20150802130600.jpg");


        //    if (!CheckchildFrmExist("FormPhotoTest"))
        //    {
        //        Fm_Photo_Test win = new Fm_Photo_Test();
        //        win.MdiParent = this;
        //        win.WindowState = FormWindowState.Maximized;
        //        win.LoadJpe(getFileListFromDatabase);
        //        win.Show();
        //    }
        //}

        //private void fDA维护界面ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    if (!CheckchildFrmExist("Fm_FDA"))
        //    {
        //        Fm_FDA frmfda = new Fm_FDA();
        //        frmfda.MdiParent = this;
        //        frmfda.WindowState = FormWindowState.Maximized;
        //        frmfda.Show();
        //    }
        //}

        //private void jOB计费输入ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    if (!CheckchildFrmExist("Fm_Job_Pro"))
        //    {
        //        Fm_Job_Pro frmfda = new Fm_Job_Pro();
        //        frmfda.MdiParent = this;
        //        frmfda.WindowState = FormWindowState.Maximized;
        //        frmfda.Show();
        //    }
        //}

        //private void fDAToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    if (!CheckchildFrmExist("Fm_FDARpt2"))
        //    {
        //        Fm_FDARpt2 frmfda = new Fm_FDARpt2();
        //        frmfda.MdiParent = this;
        //        frmfda.WindowState = FormWindowState.Maximized;
        //        frmfda.Show();
        //    }
            
        //}

        //private void test2ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    if (!CheckchildFrmExist("Fm_FDARpt"))
        //    {
        //        Fm_FDARpt frmfda = new Fm_FDARpt();
        //        frmfda.MdiParent = this;
        //        frmfda.WindowState = FormWindowState.Maximized;
        //        frmfda.Show();
        //    }
           
        //}

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

        private void MDI_W_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.ApplicationExitCall)
            {
                return;
            }

            if (DialogResult.Yes == MessageBox.Show("MDMS系统正在使用中,确认退出系统?", "MDL-提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                System.Environment.Exit(System.Environment.ExitCode);
            }
            else
            {
                e.Cancel = true;
            }
        }
	}
}