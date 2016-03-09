using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using CaseInquire.helperclass;

namespace CaseInquire
{
    public partial class Fm_Main : Form
    {
        public Fm_Main()
        {
            InitializeComponent();
        }

        DataTable dt = null;
        private void Fm_Main_Load(object sender, EventArgs e)
        {
            try
            {
                //状态栏信息显示
                tsslDate.Text = DateTime.Now.ToString();
                tsslComputer.Text = PublicClass.HostName + "/" + PublicClass.HostIP;
                tsslLogin.Text = PublicClass.LoginName + "/" + PublicClass.UserName;
                tsslWeek.Text = DateTime.Now.DayOfWeek.ToString();
                
                //获取问单系统菜单信息
                //dt = GetInfo();
                dt = GetMenuWithUser(PublicClass.LoginName);
                if (dt == null || dt.Rows.Count <= 0)
                {
                    MessageBox.Show("当前用户【" + PublicClass.LoginName + "】在本系统未授予任何权限！", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DataRow[] rootRows = dt.Select("menu_parentid = '0070'");
                foreach (DataRow dr in rootRows)
                {
                    //创建一个菜单项
                    ToolStripMenuItem topMenu = new ToolStripMenuItem();
                    //给菜单项Text值
                    topMenu.Text = dr["menu_name"].ToString();
                    //判断是否为叶子节点
                    if (dr["isleaf"].ToString().Equals("0"))
                    {
                        //以ref的方式将顶层菜单传递参数,加载子菜单
                        LoadSubMenu(ref topMenu, dr["menu_menuid"].ToString(), dt);
                    }

                    //显示应用程序中已打开的MDI子窗体列表的菜单项
                    mnsMain.MdiWindowListItem = topMenu;
                    //将递归附加好的菜单加到菜单根项上
                    mnsMain.Items.Add(topMenu);
                }
                //获取用户对应的角色Code
                //PublicClass.RoleCode = rootRows[0]["uaro_role"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Fm_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            //如果程序关闭原因是ApplicationExit();则直接关闭，不执行后续判断
            if (e.CloseReason == CloseReason.ApplicationExitCall)
            {
                return;
            }

            if (DialogResult.Yes == MessageBox.Show("问单系统正在使用中,确认退出系统?", "MDL-提示", MessageBoxButtons.YesNo,MessageBoxIcon.Information))
            {
                System.Environment.Exit(System.Environment.ExitCode);             
            }
            else
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 获取问单系统菜单信息
        /// </summary>
        /// <returns></returns>
        private DataTable GetInfo()
        {
            //0070为问单系统
            string sqlStr = "select connect_by_isleaf as isleaf, menu_menuid,menu_parentid, rpad('',2*(level-1)) || menu_name as menu_name,menu_command from zt00_menu_info start with menu_parentid = '0070' connect by nocycle menu_parentid = prior menu_menuid";

            DataSet ds = ZComm1.Oracle.DB.GetDSFromSql1(sqlStr);
            return ds.Tables[0];
        }

        /// <summary>
        /// 加载子菜单
        /// </summary>
        /// <param name="topMenu">父菜单项</param>
        /// <param name="menuId">父菜单ID</param>
        /// <param name="dt">菜单数据集</param>
        private void LoadSubMenu(ref ToolStripMenuItem topMenu, string menuId, DataTable dt)
        {
            //过滤出当前父菜单下的所有子菜单数据（仅为下一层的）
            DataRow[] rows = dt.Select("menu_parentid = '" + menuId + "'");
            if (rows.Length == 0)
            {
                return;
            }

            foreach (DataRow dr in rows)
            {
                //创建子菜单
                ToolStripMenuItem subMenu = new ToolStripMenuItem();
                subMenu.Text = dr["menu_name"].ToString();
                //如果还有子菜单则继续递归加载
                if (dr["isleaf"].ToString().Equals("0"))
                {
                    LoadSubMenu(ref subMenu, dr["menu_menuid"].ToString(), dt);
                }
                else
                {
                    //扩展属性可以加任何想要的值，这里用menu_command属性来加载窗体
                    subMenu.Tag = dr["menu_command"].ToString();
                    //给没有子菜单的菜单项加事件
                    subMenu.Click += new EventHandler(subMenu_Click);
                }

                //其它的一些逻辑处理
                //TODO

                //将菜单加到顶层菜单下
                topMenu.DropDownItems.Add(subMenu);
            }
        }

        /// <summary>
        /// 菜单单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void subMenu_Click(object sender, EventArgs e)
        {
            //tag属性在这里有用到
            string formName = ((ToolStripMenuItem)sender).Tag.ToString();
            CreateFormInstance(formName);
        }

        /// <summary>
        /// 创建form实例
        /// </summary>
        /// <param name="formName">form的类名</param>
        private void CreateFormInstance(string formName)
        {
            bool flag = false;
            //遍历主窗口上的所有子菜单
            for (int i = 0; i < this.MdiChildren.Length; i++)
            {
                //如果所点击的窗口被打开则重新激活
                if (this.MdiChildren[i].Tag.ToString().ToLower() == formName.ToLower())
                {
                    this.MdiChildren[i].Activate();
                    this.MdiChildren[i].Show();
                    this.MdiChildren[i].WindowState = FormWindowState.Normal;
                    flag = true;
                    break;
                }
            }

            if (!flag)
            {
                try
                {
                    //如果不存在则用反射创建form窗体实例
                    Assembly asm = Assembly.Load("CaseInquire");//程序集名
                    object frmObj = asm.CreateInstance("CaseInquire." + formName);//程序集+form的类名
                    Form frms = (Form)frmObj;
                    //tag属性要重新写一次，否则在第二次的时候取不到
                    frms.Tag = formName.ToString();
                    frms.MdiParent = this;
                    frms.Show();
                    frms.WindowState = FormWindowState.Normal;
                    
                }
                catch (NullReferenceException ex)
                {
                    MessageBox.Show("不存在对象名：" + formName, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tsslDate.Text = DateTime.Now.ToString();
        }

        /// <summary>
        /// 根据登录用户获取对应菜单权限（根据用户找到对应的角色，再根据角色获取对应菜单权限）
        /// </summary>
        /// <param name="user">用户</param>
        /// <returns></returns>
        private DataTable GetMenuWithUser(string user)
        {
            string sqlStr = string.Format("select connect_by_iscycle,connect_by_isleaf as isleaf,menu_menuid,menu_parentid,rpad('',2*(level-1)) || menu_name as menu_name,menu_command from (select distinct c.* from ZT00_UARO_USERROLE a join ZT00_AUTM_AUTHMENU b on a.uaro_role = b.autm_code join ZT00_MENU_INFO c on b.autm_menuid = c.menu_menuid where upper(a.uaro_user) = '{0}') start with menu_parentid = '0070' connect by nocycle menu_parentid = prior menu_menuid", user.ToUpper());
            DataSet ds = ZComm1.Oracle.DB.GetDSFromSql1(sqlStr);
            return ds.Tables[0];
        }
    
    }
}
