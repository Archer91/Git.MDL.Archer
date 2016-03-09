using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using System.Threading;
using CaseInquire.helperclass;
using CaseInquire;

namespace CaseInquire
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool isNew;
            Mutex m = new Mutex(false, "CaseInquire", out isNew);
            if (isNew)
            {
                try
                {
                    //获取基础信息
                    PublicClass.FileServerPathBase = PublicMethod.GetCaseAttachmentPath();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Login());
            }
            else
            {
                MessageBox.Show("问单系统已在运行！", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
