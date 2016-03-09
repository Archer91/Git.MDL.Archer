using System;
using System.Data;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using ZComm1;
using ZComm1.Oracle;
using CaseInquire.helperclass;


namespace CaseInquire
{
	public partial class Login : ZFormLogin
	{
		public Login()
		{
			InitializeComponent();
            //txtLog.Text = "011239";
            //txtPwd.Text = "011239";
			ZComm1.Oracle.DB.DBConnectionString = DB.DBConnectionString;//add by yb 20140715
			Init(txtLog, txtPwd,
				"select upper(UACC_CODE) as code,UACC_PASSWORD as password,UACC_NAME as name from zt00_uacc_useraccount where uacc_status='1'",
				btnLogin, btnExit, ShowMDI
				);
		}
		public void ShowMDI()
		{
			string initMenu = "", topMenu = "0070";
			System.Configuration.AppSettingsReader asra = new System.Configuration.AppSettingsReader();
			try
			{
				initMenu = asra.GetValue("initialMenuItem", typeof(string)).ToString();
			}
			catch (Exception exc)
			{
				initMenu = "";
			}
			try
			{
				topMenu = asra.GetValue("topMenuItem", typeof(string)).ToString();
			}
			catch (Exception exc)
			{
				topMenu = "0070";
			}
			ZComm1.Oracle.DB.DBConnectionString = DB.DBConnectionString;//add by yb 20140715
			DB.loginUserName = ZComm1.Oracle.DB.loginUserName = txtLog.Text.Trim();
            PublicClass.LoginName = DB.loginUserName;
			MDI_W pj = new MDI_W(txtLog.Text.Trim(), initMenu, topMenu);
			pj.ShowDialog();
		}
		private void Login_Load(object sender, EventArgs e)
		{
			DB.GetDSFromSql("select upper(UACC_CODE) from zt00_uacc_useraccount where uacc_code='1'"); //for autoload dll
			dbName.Text = DBName;
			lbDate.Text = PrjDate;
            DB.ConnectedDBName = "(" + dbName.Text + " " + lbDate.Text + ")";
            
		}

        public string PrjDate
        {
            get
            {
                Assembly assem = Assembly.GetExecutingAssembly();
                FileInfo fileInfo = new FileInfo(assem.Location);
                return fileInfo.LastWriteTime.ToYmdStr();
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

 
	}
}
