using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cobainsoft.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.IO;
using System.Drawing.Imaging;

namespace PWW
{
    public partial class Fr_EmpPosBar : Form
    {
        public Fr_EmpPosBar()
        {
            InitializeComponent();
        }

        private void Fr_EmpPosBar_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
            DataSet dsdept = DB.GetDSFromSql(" select distinct dept_code,dept_code||'--'||dept_description dept_description,DEPT_VIEW_CODE from ZTPW_DEPT_INFO where DEPT_STATUS=1 order by DEPT_VIEW_CODE,dept_code");
            for (int i = 0; i < dsdept.Tables[0].Rows.Count; i++)
            {

                CheckComboBoxTest.CCBoxItem item = new CheckComboBoxTest.CCBoxItem(dsdept.Tables[0].Rows[i]["DEPT_DESCRIPTION"].ToString(), i, dsdept.Tables[0].Rows[i]["DEPT_CODE"].ToString());
                ccb_dept.Items.Add(item);

            }
            // If more then 5 items, add a scroll bar to the dropdown.
            ccb_dept.MaxDropDownItems = 10;
            // Make the "Name" property the one to display, rather than the ToString() representation.
            ccb_dept.DisplayMember = "Name";
            ccb_dept.ValueSeparator = ", ";
            // Check the first 2 items.
            //ccb_dept.SetItemChecked(0, true);
            //ccb_dept.SetItemChecked(1, true);

            //add 20140303 by YF the first default to user department if in ztpw_wpos_workposition , deault uacc_code is emp_code
            DataSet dsDftWpos = DB.GetDSFromSql("select * from ZTPW_WPOS_WORKPOSITION where wpos_emp_code='" + DB.loginUserName + "'");
            //add 20140303 by YF the first default to user department if in ztpw_wpos_workposition , deault uacc_code is emp_code
            if (dsDftWpos != null && dsDftWpos.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsdept.Tables[0].Rows.Count; i++)
                {
                    if (dsdept.Tables[0].Rows[i]["DEPT_CODE"].ToString() == dsDftWpos.Tables[0].Rows[0]["WPOS_DEPT_CODE"].ToString())
                    {
                        ccb_dept.SetItemChecked(i, true);
                        break;
                    }
                }
            }

            // If more then 5 items, add a scroll bar to the dropdown.
            ccb_groupno.MaxDropDownItems = 10;
            // Make the "Name" property the one to display, rather than the ToString() representation.
            ccb_groupno.DisplayMember = "Name";
            ccb_groupno.ValueSeparator = ", ";
            // Check the first 2 items.
            //ccb_groupno.SetItemChecked(0, true);
            //ccb_groupno.SetItemChecked(1, true);
            DataSet dsgroupno = DB.GetDSFromSql("select wpos_group_no from ztpw_wpos_workposition group by wpos_group_no order by wpos_group_no");
            for (int i = 0; i < dsgroupno.Tables[0].Rows.Count; i++)
            {

                CheckComboBoxTest.CCBoxItem item = new CheckComboBoxTest.CCBoxItem(dsgroupno.Tables[0].Rows[i]["WPOS_GROUP_NO"].ToString(), i, dsgroupno.Tables[0].Rows[i]["WPOS_GROUP_NO"].ToString());
                ccb_groupno.Items.Add(item);

            }
            if (dsDftWpos != null && dsDftWpos.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsgroupno.Tables[0].Rows.Count; i++)
                {
                    if (dsgroupno.Tables[0].Rows[i]["WPOS_GROUP_NO"].ToString() == dsDftWpos.Tables[0].Rows[0]["WPOS_GROUP_NO"].ToString())
                    {
                        ccb_groupno.SetItemChecked(i, true);
                        break;
                    }
                }
            }

        }

        private void Button_rpt_Click(object sender, EventArgs e)
        {
            if (ccb_dept.Text.Trim() == "" && ccb_groupno.Text.Trim() == "")
            {
                MessageBox.Show("请选择部门/组别，部门、组别不能同时为空 ！", "Notice Information");
                ccb_dept.Focus();
                return;
            }
            IList<Person> persons = new List<Person>();
            try
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                string strWhere = " and wpos_status='1' ";
                string sinstr = "(";
                if (ccb_dept.Text.Trim() != "")
                {
                    foreach (CheckComboBoxTest.CCBoxItem item in ccb_dept.CheckedItems)
                    {
                        sinstr = sinstr + DB.sp(item.SValue) + ",";
                    }
                    sinstr = sinstr.Substring(0, sinstr.Length - 1) + ")";
                    strWhere = " and wpos_dept_code in " + sinstr;
                }
                sinstr = "(";
                if (ccb_groupno.Text.Trim() != "")
                {
                    foreach (CheckComboBoxTest.CCBoxItem item in ccb_groupno.CheckedItems)
                    {
                        sinstr = sinstr + DB.sp(item.SValue) + ",";
                    }
                    sinstr = sinstr.Substring(0, sinstr.Length - 1) + ")";
                    strWhere = " and wpos_group_no in " + sinstr;
                }

                DataSet dswpos = DB.GetDSFromSql("select a1.*,a2.dept_description from ZTPW_WPOS_WORKPOSITION a1,ztpw_dept_info a2 where wpos_dept_code=dept_code and wpos_emp_code is not null " + strWhere + " order by WPOS_DEPT_CODE,wpos_group_no,wpos_group_leader desc,wpos_code");
                for (int i = 0; i < dswpos.Tables[0].Rows.Count; i++)
                {
                    Person jcl = new Person();
                    jcl.identity = dswpos.Tables[0].Rows[i]["WPOS_CODE"].ToString();
                    jcl.emp_code = dswpos.Tables[0].Rows[i]["WPOS_EMP_CODE"].ToString();
                    jcl.dept_name = dswpos.Tables[0].Rows[i]["DEPT_DESCRIPTION"].ToString();
                    jcl.group_name = dswpos.Tables[0].Rows[i]["WPOS_GROUP_NO"].ToString();
                    jcl.name = dswpos.Tables[0].Rows[i]["WPOS_EMP_NAME"].ToString();
                    //jcl.identityBarCodePng = ComClass.IdentityBarCodePng(dswpos.Tables[0].Rows[i]["WPOS_CODE"].ToString(),dswpos.Tables[0].Rows[i]["WPOS_EMP_CODE"].ToString());
                    persons.Add(jcl);
                }
                reportViewer1.LocalReport.ReportPath = @"Report1.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("BarCodeWinDemo_Person", persons));
                reportViewer1.RefreshReport();
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

    class ComClass
    {
        public static byte[] IdentityBarCodePng(string id, string title)
        {
            BarcodeControl barcode = new BarcodeControl();
            barcode.BarcodeType = BarcodeType.CODE128B;
            barcode.CopyRight = title; // 空字符串就会不显示标题
            barcode.Data = id;

            MemoryStream memStream = new MemoryStream();
            try
            {
                barcode.MakeImage(ImageFormat.Png, 1, 50, true, false, null, memStream);
                //Image barCodePngImage = Image.FromStream(memStream);
                return memStream.ToArray();
            }
            finally
            {
                memStream.Close();
                barcode.Dispose();
            }
            //return barCodePngImage;
        }
        public static BarcodeType GetBarcodeType(string cType)
        {
            BarcodeType bt = BarcodeType.CODE128A;
            switch (cType.ToUpper())
            {
                case "CODE128A":
                    bt = BarcodeType.CODE128A;
                    break;
                case "C2OF5":
                    bt = BarcodeType.C2OF5;
                    break;
                case "CODABAR":
                    bt = BarcodeType.CODABAR;
                    break;
                case "CODE128B":
                    bt = BarcodeType.CODE128B;
                    break;
                case "CODE128C":
                    bt = BarcodeType.CODE128C;
                    break;
                case "CODE39":
                    bt = BarcodeType.CODE39;
                    break;
                case "CODE39CHECK":
                    bt = BarcodeType.CODE39CHECK;
                    break;
                case "CODE93":
                    bt = BarcodeType.CODE93;
                    break;
                case "EAN128A":
                    bt = BarcodeType.EAN128A;
                    break;
                case "EAN128B":
                    bt = BarcodeType.EAN128B;
                    break;
                case "EAN128C":
                    bt = BarcodeType.EAN128C;
                    break;
                case "EAN13":
                    bt = BarcodeType.EAN13;
                    break;
                case "EAN13_2":
                    bt = BarcodeType.EAN13_2;
                    break;
                case "EAN13_5":
                    bt = BarcodeType.EAN13_5;
                    break;
                case "EAN8":
                    bt = BarcodeType.EAN8;
                    break;
                case "EAN8_2":
                    bt = BarcodeType.EAN8_2;
                    break;
                case "EAN8_5":
                    bt = BarcodeType.EAN8_5;
                    break;
                case "INTERLEAVED2OF5":
                    bt = BarcodeType.INTERLEAVED2OF5;
                    break;
                case "MSIPLESSEY":
                    bt = BarcodeType.MSIPLESSEY;
                    break;
                case "MSIPLESSEYCHECK10":
                    bt = BarcodeType.MSIPLESSEYCHECK10;
                    break;
                case "MSIPLESSEYCHECK1010":
                    bt = BarcodeType.MSIPLESSEYCHECK1010;
                    break;
                case "MSIPLESSEYCHECK11":
                    bt = BarcodeType.MSIPLESSEYCHECK11;
                    break;
                case "MSIPLESSEYCHECK1110":
                    bt = BarcodeType.MSIPLESSEYCHECK1110;
                    break;
                case "PLANET":
                    bt = BarcodeType.PLANET;
                    break;
                case "POSTNET":
                    bt = BarcodeType.POSTNET;
                    break;
                case "ROYALMAIL":
                    bt = BarcodeType.ROYALMAIL;
                    break;
                case "UPCA":
                    bt = BarcodeType.UPCA;
                    break;
                case "UPCA_2":
                    bt = BarcodeType.UPCA_2;
                    break;
                case "UPCA_5":
                    bt = BarcodeType.UPCA_5;
                    break;
                case "UPCE":
                    bt = BarcodeType.UPCE;
                    break;
                case "UPCE_2":
                    bt = BarcodeType.UPCE_2;
                    break;
                case "UPCE_5":
                    bt = BarcodeType.UPCE_5;
                    break;
            }
            return bt;
        }

    }
    public class Person
    {
        public string identity
        {
            get;
            set;
        }
        public byte[] identityBarCodePng
        {
            get
            {
                MemoryStream memStream = new MemoryStream();
                try
                {
                    BarcodeControl barcode = new BarcodeControl();
                    barcode.BarcodeType = BarcodeType.CODE128B;
                    if (group_name != null && group_name.Trim() != "")
                    {
                        barcode.CopyRight = group_name; // 空字符串就会不显示标题
                    }
                    else
                    {
                        barcode.CopyRight = name; // 空字符串就会不显示标题
                    }
                    barcode.Data = identity;

                    //MemoryStream memStream = new MemoryStream();
                    barcode.MakeImage(ImageFormat.Png, 1, 50, true, false, null, memStream);
                    //Image barCodePngImage = Image.FromStream(memStream);
                    //memStream.Close();
                    return memStream.ToArray();
                }
                finally
                {
                    memStream.Close();
                }
                //return barCodePngImage;
            }
            //set;
        }

        public Image imgBarCodePng
        {
            get
            {
                MemoryStream memStream = new MemoryStream();
                try
                {
                    BarcodeControl barcode = new BarcodeControl();
                    barcode.BarcodeType = BarcodeType.CODE128B;
                    if (group_name != null && group_name.Trim() != "")
                    {
                        barcode.CopyRight = group_name; // 空字符串就会不显示标题
                    }
                    else
                    {
                        barcode.CopyRight = name; // 空字符串就会不显示标题
                    }
                    barcode.Data = identity;

                    //MemoryStream memStream = new MemoryStream();
                    barcode.MakeImage(ImageFormat.Png, 1, 50, true, false, null, memStream);
                    Image barCodePngImage = Image.FromStream(memStream);
                    //memStream.Close();
                    //return memStream.ToArray();
                    return barCodePngImage;
                }
                finally
                {
                    memStream.Close();
                }
                //return barCodePngImage;
            }
            //set;
        }


        public string name
        {
            get;
            set;
        }
        //public DateTime birthday
        //{
        //    get;
        //    set;
        //}
        public string emp_code
        {
            get;
            set;
        }
        public string dept_name
        {
            get;
            set;
        }
        public string group_name
        {
            get;
            set;
        }
        //public string wktp_name
        //{
        //    get;
        //    set;
        //}


    }
}
