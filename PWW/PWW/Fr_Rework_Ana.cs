using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PWW
{
    public partial class Fr_Rework_Ana : Form
    {
        public Fr_Rework_Ana()
        {
            InitializeComponent();
        }

        private void Button_rpt_Click(object sender, EventArgs e)
        {
            //if (ccb_dept.Text.Trim() == "" && ccb_groupno.Text.Trim() == "")
            //{
            //    MessageBox.Show("请选择部门/组别，部门、组别不能同时为空 ！", "Notice Information");
            //    ccb_dept.Focus();
            //    return;
            //}
            IList<Person> persons = new List<Person>();
            try
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                //string strWhere = " and wpos_status='1' ";
                //string sinstr = "(";
                //if (ccb_dept.Text.Trim() != "")
                //{
                //    foreach (CheckComboBoxTest.CCBoxItem item in ccb_dept.CheckedItems)
                //    {
                //        sinstr = sinstr + DB.sp(item.SValue) + ",";
                //    }
                //    sinstr = sinstr.Substring(0, sinstr.Length - 1) + ")";
                //    strWhere = " and wpos_dept_code in " + sinstr;
                //}
                //sinstr = "(";
                //if (ccb_groupno.Text.Trim() != "")
                //{
                //    foreach (CheckComboBoxTest.CCBoxItem item in ccb_groupno.CheckedItems)
                //    {
                //        sinstr = sinstr + DB.sp(item.SValue) + ",";
                //    }
                //    sinstr = sinstr.Substring(0, sinstr.Length - 1) + ")";
                //    strWhere = " and wpos_group_no in " + sinstr;
                //}

                //DataSet dswpos = DB.GetDSFromSql("select a1.*,a2.dept_description from ZTPW_WPOS_WORKPOSITION a1,ztpw_dept_info a2 where wpos_dept_code=dept_code and wpos_emp_code is not null " + strWhere + " order by WPOS_DEPT_CODE,wpos_group_no,wpos_group_leader desc,wpos_code");
                //for (int i = 0; i < dswpos.Tables[0].Rows.Count; i++)
                //{
                //    Person jcl = new Person();
                //    jcl.identity = dswpos.Tables[0].Rows[i]["WPOS_CODE"].ToString();
                //    jcl.emp_code = dswpos.Tables[0].Rows[i]["WPOS_EMP_CODE"].ToString();
                //    jcl.dept_name = dswpos.Tables[0].Rows[i]["DEPT_DESCRIPTION"].ToString();
                //    jcl.group_name = dswpos.Tables[0].Rows[i]["WPOS_GROUP_NO"].ToString();
                //    jcl.name = dswpos.Tables[0].Rows[i]["WPOS_EMP_NAME"].ToString();
                //    //jcl.identityBarCodePng = ComClass.IdentityBarCodePng(dswpos.Tables[0].Rows[i]["WPOS_CODE"].ToString(),dswpos.Tables[0].Rows[i]["WPOS_EMP_CODE"].ToString());
                //    persons.Add(jcl);
                //}
                //reportViewer1.LocalReport.ReportPath = @"Report1.rdlc";
                //reportViewer1.LocalReport.DataSources.Clear();
                //reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("BarCodeWinDemo_Person", persons));
                //reportViewer1.RefreshReport();
            }
            catch
            {

            }
            finally
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            }
        }
    }
}
