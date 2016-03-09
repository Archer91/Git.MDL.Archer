using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace PWW
{
	public partial class PWMDI_W : Form
	{
		private DataTable menuModuleTable;
		private string topMenuItem = "0020";
		public PWMDI_W()
		{
			InitializeComponent();
			if (this.LoginName.Text.Trim() == "") this.LoginName.Text = DB.loginUserName;
			this.initialMenuItem.Text = "";
		}
		public PWMDI_W(string loginName)
		{
			InitializeComponent();
			this.LoginName.Text = loginName;
			if (this.LoginName.Text.Trim() == "") this.LoginName.Text = DB.loginUserName;
			this.initialMenuItem.Text = "";
		}
		public PWMDI_W(string loginName, string initMenuItem)
		{
			InitializeComponent();
			this.LoginName.Text = loginName;
			if (this.LoginName.Text.Trim() == "") this.LoginName.Text = DB.loginUserName;
			this.initialMenuItem.Text = initMenuItem;
		}
		public PWMDI_W(string loginName, string initMenuItem, string sysTopMenuItem)
		{
			InitializeComponent();
			this.LoginName.Text = loginName;
			if (this.LoginName.Text.Trim() == "") this.LoginName.Text = DB.loginUserName;
			this.initialMenuItem.Text = initMenuItem;
			if (sysTopMenuItem.Trim() != "") topMenuItem = sysTopMenuItem;
		}

		private void PWMDI_W_Load(object sender, EventArgs e)
		{
			if (DB.DBConnectionString.IndexOf("mdltest") >= 0)
			{
				this.Text += " -- Testing";
			}
			else
			{
				this.Text += " -- Production";
			}

			InitMenu();
			//20150323
			this.userLoginName.Text = "Welcome : " + DB.V(DB.GetDSFromSql("select uacc_name from zt00_uacc_useraccount where uacc_code=" + DB.sp(this.LoginName.Text)));
			// login user name then initial menu
			ToolStripMenuItem tmnu1 = new ToolStripMenuItem();
			if (this.initialMenuItem.Text.Trim() != "") // exist initial menuitem show it
			{
				for (int i = 0; i < SysMainMenu.Items.Count; i++)
				{
					ToolStripMenuItem item = (ToolStripMenuItem)SysMainMenu.Items[i];
					if (item.Name == this.initialMenuItem.Text)
					{
						SysMainMenu_Click(item, e);
						return;
					}
					if (item.DropDownItems.Count > 0)
					{
						foreach (ToolStripMenuItem subitem in item.DropDownItems)
						{
							EnumerateMenuStrip(subitem, e);
						}
					}
				}
			}
			toolTip1.SetToolTip(this.userLoginName, "请双击鼠标修改密码");
		}
		private void EnumerateMenuStrip(ToolStripMenuItem item, EventArgs e)
		{
			if (item.Name == this.initialMenuItem.Text)
			{
				SysMainMenu_Click(item, e);
				return;
			}
			if (item.DropDownItems.Count > 0)
			{
				foreach (ToolStripMenuItem subitem in item.DropDownItems)
				{
					EnumerateMenuStrip(subitem, e);
				}
			}
		}

		protected void InitMenu()
		{
			string strSql = @"select * from zt00_menu_info 
      where menu_menuid in ( SELECT distinct autm_menuid from ZT00_AUTM_AUTHMENU
          WHERE
          AUTM_CODE IN ( SELECT DISTINCT UARO_ROLE FROM ZT00_UARO_USERROLE WHERE NVL(UARO_STATUS,'0')<>'0'  AND UARO_USER ='" + this.LoginName.Text + @"' AND exists (select 'x'  from ZT00_UACC_USERACCOUNT where uacc_code='" + this.LoginName.Text + @"')
          UNION
          SELECT DISTINCT UARO_ROLE FROM ZT00_UARO_USERROLE WHERE NVL(UARO_STATUS,'0')<>'0' CONNECT BY UARO_ROLE = PRIOR UARO_USER START WITH UARO_ROLE IN (SELECT DISTINCT UARO_ROLE FROM ZT00_UARO_USERROLE WHERE NVL(UARO_STATUS,'0')<>'0'  AND UARO_USER ='" + this.LoginName.Text + @"' AND exists (select 'x'  from ZT00_UACC_USERACCOUNT where uacc_code='" + this.LoginName.Text + @"') UNION SELECT '" + this.LoginName.Text + @"' UARO_ROLE FROM DUAL )
          UNION
          SELECT DISTINCT UARO_USER AS UARO_ROLE FROM ZT00_UARO_USERROLE WHERE NVL(UARO_STATUS,'0')<>'0' and exists ( select 'x' from zt00_role_info where role_code=uaro_user) CONNECT BY UARO_ROLE = PRIOR UARO_USER START WITH UARO_ROLE IN (SELECT DISTINCT UARO_ROLE FROM ZT00_UARO_USERROLE WHERE NVL(UARO_STATUS,'0')<>'0'  AND UARO_USER ='" + this.LoginName.Text + @"' AND exists (select 'x'  from ZT00_UACC_USERACCOUNT where uacc_code='" + this.LoginName.Text + @"') UNION SELECT '" + this.LoginName.Text + @"' UARO_ROLE FROM DUAL )
          UNION SELECT '" + this.LoginName.Text + @"' UARO_ROLE FROM DUAL )  AND AUTM_STATUS = '1' )
connect by menu_parentid = prior menu_menuid start with menu_parentid=" + DB.sp(topMenuItem); //0020 is PWW system top menu ID 
			DataSet ds = DB.GetDSFromSql(strSql);
			menuModuleTable = ds.Tables[0]; //取得所有数据得到 DataTable
			//加载 MenuStrip 菜单
			ToolStripMenuItem topMenu = new ToolStripMenuItem();
			LoadSubMenu(ref topMenu, topMenuItem);
		}
		private void LoadSubMenu(ref ToolStripMenuItem topMenu, string ParentID)
		{
			DataView dvList = new DataView(menuModuleTable); //过滤出当前父菜单下在所有子菜单数据(仅为下一层的) 
			dvList.RowFilter = "MENU_PARENTID='" + ParentID.ToString() + "'";
			ToolStripMenuItem subMenu;
			foreach (DataRowView dv in dvList)
			{
				//创建子菜单项 
				subMenu = new ToolStripMenuItem();
				subMenu.Text = dv["MENU_NAME"].ToString();
				subMenu.Tag = dv["MENU_COMMAND"].ToString();
				subMenu.Name = dv["MENU_MENUID"].ToString();
				subMenu.Click += new EventHandler(SysMainMenu_Click);
				if (dv["MENU_MENUID"].ToString() == "0020.20.0010") subMenu.ShortcutKeys = Keys.Control | Keys.J;
				if (dv["MENU_MENUID"].ToString() == "0020.20.0020") subMenu.ShortcutKeys = Keys.Control | Keys.E;
				if (dv["MENU_MENUID"].ToString() == "0020.20.0030") subMenu.ShortcutKeys = Keys.Control | Keys.M;
				//判断是否为顶级菜单 
				if (ParentID == topMenuItem)
				{
					this.SysMainMenu.Items.Add(subMenu);
				}
				else
				{
					//subMenu.Click += new EventHandler(SysMainMenu_Click);
					topMenu.DropDownItems.Add(subMenu);
				}
				//递归调用 
				LoadSubMenu(ref subMenu, dv["MENU_MENUID"].ToString());
			}
		}

		private void SysMainMenu_Click(object sender, EventArgs e)
		{
			try
			{
				ToolStripMenuItem menuitem = (ToolStripMenuItem)sender;
				string strfrm = menuitem.Tag.ToString();
				if (strfrm != null && strfrm.Trim() != "")
				{
					foreach (Form childrenForm in this.MdiChildren)
					{
						//检测是不是当前子窗体名称
						if (childrenForm.Name == strfrm)
						{
							//MessageBox.Show("recall OK exists");
							//是的话就是把他显示
							childrenForm.Visible = true;
							//并激活该窗体
							childrenForm.Activate();
							childrenForm.WindowState = FormWindowState.Normal;
							childrenForm.Show();
							return;
						}
					}
					Assembly assem = Assembly.GetExecutingAssembly();
					Type t = assem.GetType("PWW." + strfrm);  //namespace.classname
					object obj1 = Activator.CreateInstance(t); //创建实例
					Form frm = (Form)obj1;
					frm.MdiParent = this;
					//add 20140623 text as menu text
					if (menuitem.Text != "")
					{
						frm.Text = menuitem.Text + "    [" + strfrm + "]";
					}
					frm.Show();
					//MethodInfo mi = t.GetMethod("Show"); //this.Show
					//object ret = mi.Invoke(obj1, null);
				}
			}
			catch (Exception ec)
			{
			}
		}
		//private void SysMainMenu_Click2(object sender, EventArgs e)
		//{
		//    try
		//    {
		//        ToolStripMenuItem menuitem = (ToolStripMenuItem)sender;
		//        string strfrm = menuitem.Tag.ToString();
		//        if (strfrm != null && strfrm.Trim() != "")
		//        {
		//            foreach (Form childrenForm in this.MdiChildren)
		//            {
		//                //检测是不是当前子窗体名称
		//                if (childrenForm.Name == strfrm)
		//                {
		//                    //是的话就是把他显示
		//                    childrenForm.Visible = true;
		//                    //并激活该窗体
		//                    childrenForm.Activate();
		//                    return;
		//                }
		//            }
		//            Assembly assem = Assembly.GetExecutingAssembly();
		//            Type t = assem.GetType("PWW." + strfrm);  //namespace.classname
		//            object obj1 = Activator.CreateInstance(t); //创建实例
		//            Form frm = (Form)obj1;
		//            frm.MdiParent = this;
		//            frm.Show();
		//            //MethodInfo mi = t.GetMethod("Show"); //this.Show
		//            //object ret = mi.Invoke(obj1, null);
		//        }
		//    }
		//    catch (Exception ec)
		//    {
		//    }
		//}
		private void LoginName_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			Fm_ChangePwd frmPwd = new Fm_ChangePwd(); //modify by YF add 
			frmPwd.Owner = this;
			//frmPwd.ShowDialog(this);
			frmPwd.ShowDialog();

		}

		private void PWMDI_W_Resize(object sender, EventArgs e)
		{
			userLoginName.Left = Width - userLoginName.Width - 100;
		}

		private void toolTip1_Popup(object sender, PopupEventArgs e)
		{

		}


	}
}


