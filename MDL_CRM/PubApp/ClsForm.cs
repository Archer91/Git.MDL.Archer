using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace PubApp
{
    class ClsForm
    {
        public static void OpenForm(string strForm, out Form frm, string strTag = "")
        {

            string strDll;
            string[] str = new string[] { };
            str = strForm.Split('|');
            strDll = str[0];
            strForm = str[1];
            Assembly asm = Assembly.LoadFile(Application.StartupPath + "\\" + strDll + ".dll");  //"D:\\Job\\com"
            Type formtype = asm.GetType(strDll + "." + strForm);
            frm = OpenFrm(formtype);
            frm.Text = str[2];
            frm.Tag = strTag;
        }
        private static Form OpenFrm(Type formType)
        {
            Form frm = formType.InvokeMember(null,
            BindingFlags.DeclaredOnly
            | BindingFlags.Public
            | BindingFlags.NonPublic
            | BindingFlags.Instance
            | BindingFlags.CreateInstance,
            null, null, null) as Form;
            return frm;
        }
    }
}
