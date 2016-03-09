using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OracleClient;
using ZComm1.Oracle;

namespace PWW
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            //txtLog.Text = "zyb";
            //txtPwd.Text = "1234";

	        ZComm1.Oracle.DB.DBConnectionString = DB.DBConnectionString;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //live remove remark //test environment
            if (!login(txtLog.Text.Trim(), txtPwd.Text.Trim()))
            {
                MessageBox.Show("LoginName Or Password Incorrect!");
                return;
               
            }
            //need new mdi and pass parameter this.txtLog.Text.Trim().ToUpper()
            this.Hide();
			DB.loginUserName = ZComm1.Oracle.DB.loginUserName = txtLog.Text.Trim();
            string initMenu = "", topMenu = "0020";
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
                topMenu = "0020";
            }
            PWMDI_W pj = new PWMDI_W(txtLog.Text.Trim(), initMenu,topMenu);
            pj.ShowDialog();
            this.Close();
          
        }

        public bool login(string logname ,string password)
        {
            bool result = false;
            string strSQL = "select * from zt00_uacc_useraccount where uacc_code ='" + logname + "' and uacc_password='" + password + "' and uacc_status='1'";
            //op = new OracleConnection(connectionString);
            //op.Open();
            //OracleDataAdapter oraDap = new OracleDataAdapter(strSQL, op);
            DataSet ds = DB.GetDSFromSql(strSQL);
            //oraDap.Fill(ds);
            if (ds!=null&&ds.Tables.Count>0&& ds.Tables[0].Rows.Count>0)
            {
                result = true;
            }
            return result;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //Project pj = new Project();
            //pj.ShowDialog();
            this.Close();
            
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //if (Keys.Enter == keyData || Keys.Right == keyData)
            //{
            //    SendKeys.Send("\t");
            //}
            //return base.ProcessCmdKey(ref msg, keyData);
            if ((Keys.Enter == keyData) && !(ActiveControl is System.Windows.Forms.Button) && !(ActiveControl is System.Windows.Forms.CheckBox) && !(ActiveControl is System.Windows.Forms.RadioButton))
            {
                //SendKeys.Send("\t");
                SendKeys.SendWait("{Tab}");
                return true;
            }
            if ((Keys.Right == keyData || Keys.Down == keyData) && !(ActiveControl is System.Windows.Forms.DateTimePicker))
            {
                //SendKeys.Send("\t");
                SendKeys.SendWait("{Tab}");
                return true;
            }
            if ((Keys.Left == keyData || keyData == Keys.Up) && !(ActiveControl is System.Windows.Forms.DateTimePicker))
            {
                SendKeys.SendWait("+{Tab}");
                return true;
            }
            if (Keys.PageDown == keyData)
            {
                //SendKeys.Send("\t");
                SendKeys.SendWait("{Tab}");
                return true;
            }
            if (Keys.PageUp == keyData)
            {
                SendKeys.SendWait("+{Tab}");
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);

        }

        private void Login_Load(object sender, EventArgs e)
        {
            if (DB.DBConnectionString.IndexOf("mdltest") >= 0)
            {
                this.cbxDbs.SelectedIndex = 1;
            }
            else
            {
                this.cbxDbs.SelectedIndex = 0;
            }
        }
    }
}
