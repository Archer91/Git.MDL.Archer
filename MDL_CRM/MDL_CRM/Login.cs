using System;
using System.Data;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using ZComm1;
using PubApp.Data;
using MDL_CRM.Classes;

namespace MDL_CRM
{
	public partial class Login : ZFormLogin
	{
		public Login()
		{
			InitializeComponent();
            txtLog.Text = "011239";
            txtPwd.Text = "011239";
			ZComm1.Oracle.DB.DBConnectionString = DB.DBConnectionString;//add by yb 20140715
			Init(txtLog, txtPwd,
				"select upper(UACC_CODE) as code,UACC_PASSWORD as password,UACC_NAME as name from zt00_uacc_useraccount where uacc_status='1'",
				btnLogin, btnExit, ShowMDI
				);
		}
		public void ShowMDI()
		{
			string initMenu = "", topMenu = "0090";
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
				topMenu = "0090";
			}
			ZComm1.Oracle.DB.DBConnectionString = DB.DBConnectionString;//add by yb 20140715
			DB.loginUserName = ZComm1.Oracle.DB.loginUserName = txtLog.Text.Trim();
            pubcls.UserName = Dal.strGetValue("select UACC_NAME  from zt00_uacc_useraccount where uacc_status='1' AND UACC_CODE='" + txtLog.Text.Trim() + "'");
            pubcls.CompanyCode = cmbCompany.SelectedValue.ToString();
            pubcls.CompanyName = cmbCompany.Text;
			MDI_W pj = new MDI_W(txtLog.Text.Trim(), initMenu, topMenu);
			pj.ShowDialog();
		}
		private void Login_Load(object sender, EventArgs e)
		{
			DB.GetDSFromSql("select upper(UACC_CODE) from zt00_uacc_useraccount where uacc_code='1'"); //for autoload dll
			dbName.Text = DBName;
			lbDate.Text = PrjDate;
            DB.ConnectedDBName = "(" + dbName.Text + " " + lbDate.Text + ")";
            //加载公司
            //DataTable dt = Dal.GetDataTable("select ENT_CODE, ENT_NAME from ZT00_ENTITY ");
            //cmbCompany.DisplayMember = "ENT_NAME";
            //cmbCompany.ValueMember = "ENT_CODE";
            //cmbCompany.DataSource = pubcls.getEntityByUser(txtLog.Text.Trim());
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

        private void txtLog_Validated(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtLog.Text.Trim()))
                {
                    return;
                }
                cmbCompany.DisplayMember = "ENT_NAME";
                cmbCompany.ValueMember = "ENT_CODE";
                cmbCompany.DataSource = pubcls.getEntityByUser(txtLog.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
	}
}
