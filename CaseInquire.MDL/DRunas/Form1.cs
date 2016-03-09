using System;
using System.Diagnostics;
using System.Security;
using System.Windows.Forms;

namespace DRunas
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}
		private void Form1_Load(object sender, EventArgs e)
		{
			string strPassWord = "8SC3bmiH";
			char[] pChar = strPassWord.ToCharArray();
			SecureString password = new SecureString();
			foreach (char c in pChar)
			{
				password.AppendChar(c);
			}
			//IntPtr ptr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(password);
			//string ss = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(ptr);
			try
			{
				Process process = new Process();
				process.StartInfo.FileName = "CaseInquire.exe";
				process.StartInfo.UserName = "dm-km";
				process.StartInfo.Password = password;
				process.StartInfo.Domain = "moderndentallab.net";

				process.StartInfo.LoadUserProfile = true;
				process.StartInfo.UseShellExecute = false;
				process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
				process.Start();
			}
			catch (Exception e1)
			{
				MessageBox.Show(e1.Message + "\r" + e1.StackTrace);
			}
			this.Close();
		}
	}
}
