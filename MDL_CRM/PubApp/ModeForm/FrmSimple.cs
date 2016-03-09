using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PubApp.ModeForm;
using PubApp.Data;

namespace PubApp.ModeForm
{
    public partial class FrmSimple : FrmTool
    {

        public FrmSimple()
        {
            InitializeComponent();
            this.ImportVisible = false;
            this.ExportVisible = false;
            this.CopyVisible = false;
            this.RefreshVisible = false;
        }
 /// <summary>
        /// Input Parameter Table Name
        /// </summary>
        protected string sTable="";
        /// <summary>
        ///  Input Parameter View Name
        /// </summary>
        protected string sView="";
        /// <summary>
        /// Input Parameter Column Field For Example: Field1,Field2,Field3,And so on
        /// </summary>
        protected string sColumnIDs = "";
        protected string sWriteColumnIDs = "";
        /// <summary>
        /// Input Parameter Column Caption For Example: Caption1,Caption2,Caption3,And so on
        /// </summary>
        protected string sColumnCaptions = "";
        protected string sColumnWidths = "";
        /// <summary>
        /// Input Parameter Table Main Key Field 
        /// </summary>
        protected string sMainKey="";
        /// <summary>
        /// Input Parameter default Condition
        /// </summary>
        protected string sWhere = "";
        protected string sOrder= "";
        /// <summary>
        /// DataTable Name
        /// </summary>
        protected string sNotEmptyField = "";
        protected string sNotEmptyFieldDesc = "";
        protected string sBigZeroField = "";
        protected string sBigZeroFieldDesc = "";
        protected DataTable dTable;
        string gFilter = "";
        string gError = "";
        enum  FormSatus
        { 
            iNew=1,
            iEdit,
            iBrowse
        }
        int m_FormSatus=3;

        protected virtual void LoadGridData()
        {
            if (sView == "") { return; }
            string strTmp = "";
            strTmp = "select " + sWriteColumnIDs + " from " + sView +" where 1<>1";
 
            if (sOrder != "")
            {
                strTmp = strTmp + " order by " + sOrder;
            }
            dTable = Dal.GetDataTable(strTmp);

            
        }

        private void ControlEnable()
        {
            if (m_FormSatus ==(int) FormSatus.iBrowse)
            {
                this.NewEnable = true;
                this.EditEnable = true;
                this.DelEnable = true;
                this.SaveEnable = false;
                this.CancelEnable = false;

 
            }
            if (m_FormSatus == (int)FormSatus.iNew)
            {
                this.NewEnable = false;
                this.EditEnable = false;
                this.DelEnable = false;
                this.SaveEnable = true;
                this.CancelEnable = true;

               
            }
            if (m_FormSatus == (int)FormSatus.iEdit)
            {
                this.NewEnable = false;
                this.EditEnable = false;
                this.DelEnable = false;
                this.SaveEnable = true;
                this.CancelEnable = true;

 
            }
            Dal.EnabledControl(this.EditBar.Controls, m_FormSatus);   
            this.EnableButton();
            txtUpUser.ReadOnly = true;
            txtUpDate.ReadOnly = true;
            txtUpUser.BackColor = EditBar.BackColor;
            txtUpDate.BackColor = EditBar.BackColor;
        }

        private void FrmSimple_Load(object sender, EventArgs e)
        {
            LoadGridData();
            BindControl();//binding data
           ControlEnable();

        }
        private void BindControl()
        {
            Dal.BindingObject(this.EditBar.Controls, dTable);
 
        }
        protected override void btnNew_Click(object sender, EventArgs e)
        {


            FindFirstControl();

            m_FormSatus = 1;
            ControlEnable();
            Dal.BlankControl(this.EditBar.Controls);
            this.txtUpUser.Text = Dal.sUserID;
            this.txtUpDate.Text = Dal.GetServerDate(true);
        }

        protected override void btnEdit_Click(object sender, EventArgs e)
        {
            if (dTable.Rows.Count == 0) { return; }
            m_FormSatus = 2;
            ControlEnable();
            Dal.EditKeyControl(this.EditBar,sMainKey);
            FindFirstControl();
            this.txtUpUser.Text = Dal.sUserID;
            this.txtUpDate.Text = Dal.GetServerDate(true);
        }

        protected override void btnDel_Click(object sender, EventArgs e)
        {
            if (dTable.Rows.Count == 0) { return; }
            if (Before_Del() == false) { return; };
            del();
        }
        private void del()
        {
            string strError;
            if (MessageBox.Show("Confirm to Delete it?", "Alter", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                dTable.Rows[0].Delete();
                string str = Dal.SaveSQL(sWriteColumnIDs, sTable, dTable, sMainKey);

                Dal.BatchSave("select " + sWriteColumnIDs + " from " + sTable, dTable, out strError);
                after_Del();
                Dal.BlankControl(this.EditBar.Controls);
            }
        }
        protected virtual bool Before_Del()
        {
            bool btn = true;

            return btn;
        }
        protected virtual void after_Del()
        {
 
        }
        protected virtual bool Before_SaveData()
        {
            bool btn = true;
            gError = "";
            gFilter = "";
            chkData(this.EditBar.Controls);
            sGetKeyWhere(this.EditBar.Controls);
            if (gFilter != "")
            {
                string str = Dal.strGetValue("select 1 from " + this.sTable + " where " + gFilter);
                if (str == "1")
                {
                    { gError = gError + "Main Key's values is already exists \r\n"; }
                }
            }
            if (gError != "")
            {
                MessageBox.Show(gError);
                btn = false;
            }
            return btn;
        }
        private void sGetKeyWhere(Control.ControlCollection ctls)
        {
            string strField = "";
            string strFilter = "";
            string[] strFld = new string[] { };
            strFld = sMainKey.Split(',');
            try
            {
                foreach (Control con in ctls)
                {
                    if (con.Controls.Count > 0 && Dal.IfContainer(con) == true)
                    {
                        sGetKeyWhere(con.Controls);
                    }
                    else
                    {
                        if (sMainKey.Trim() != "" && m_FormSatus == 1)
                        {
                            if (con.Tag != null)
                            {
                                strField = con.Tag.ToString();
                                for (int i = 0; i < strFld.Length; i++)
                                {
                                    if (strField == strFld[i].Trim())
                                    {

                                        switch (con.GetType().Name)
                                        {
                                            case "ComboBox":
                                            case "TextBox":
                                            case "RichTextBox":
                                            case "MaskedTextBox":
                                                {
                                                    if (con.Text.Trim() != "")
                                                    {
                                                        if (strFilter != "")
                                                        {
                                                            strFilter = strFilter + " and ";
                                                        }
                                                        strFilter = strFilter + strField + "='" + con.Text.Trim() + "'";
                                                        break;
                                                        //string str = Dal.strGetValue("select 1 from " + this.sTable + " where " + sMainKey + "='" + con.Text.Trim() + "'");
                                                        //if (str == "1")
                                                        //{
                                                        //    { strError = strError + "Main Key's values is already exists \r\n"; }
                                                        //}
                                                    }
                                                    break;
                                                }
                                            case "DateTimePicker":
                                                {
                                                    if (strFilter != "")
                                                    {
                                                        strFilter = strFilter + " and ";
                                                    }
                                                    if (Dal.strDataBaseTy == "MYSQL")
                                                    {
                                                        strFilter = strFilter + strField + "='" + con.Text.Trim() + "'";
                                                    }
                                                    else
                                                    {
                                                        strFilter = strFilter + strField + "='" + " to_date('" + con.Text.Trim() + "','yyyy-mm-dd HH24:mi:ss')";
                                                    }
                                                    break;
                                                }
                                            case "NumericUpDown":
                                                {
                                                    if (strFilter != "")
                                                    {
                                                        strFilter = strFilter + " and ";
                                                    }
                                                    NumericUpDown obj = (NumericUpDown)con;
                                                    strFilter = strFilter + strField + "=" + obj.Value;
                                                    break;
                                                }

                                        }
                                    }
                                }


                            }
                        }
                    }
                    //
                }

                if (gFilter != "" && strFilter != "")
                {
                    gFilter = gFilter + strFilter;
                }
                else if (strFilter != "")
                {
                    gFilter = strFilter;
                }
            }
            catch
            {

            }

        }
        private void chkData(Control.ControlCollection ctls)
        {
            string strField = "";
            string strError = "";
            string[] strFld = new string[] { };
            string[] strFldCap = new string[] { };
            string[] strFld1 = new string[] { };
            string[] strFldCap1 = new string[] { };
            strFld = sNotEmptyField.Split(',');
            strFldCap = sNotEmptyFieldDesc.Split(',');
            strFld1 = sBigZeroField.Split(',');
            strFldCap1 = sBigZeroFieldDesc.Split(',');
            try
            {
                foreach (Control con in ctls)
                {
                    if (con.Controls.Count > 0 && Dal.IfContainer(con) == true)
                    {
                        chkData(con.Controls);
                    }
                    else
                    {
                        if (sNotEmptyField != "")
                        {
                            if (con.Tag != null)
                            {
                                for (int intn = 0; intn < strFld.Length; intn++)
                                {
                                    strField = strFld[intn].Trim();

                                    switch (con.GetType().Name)
                                    {

                                        case "ComboBox":
                                        case "TextBox":
                                        case "RichTextBox":
                                        case "DateTimePicker":
                                        case "MaskedTextBox":
                                            {
                                                if (con.Tag.ToString() == strField)
                                                {
                                                    if (con.Text.Trim() == "")
                                                    { strError = strError + strFldCap[intn] + " Cann't Empty" + "\r\n"; }
                                                }
                                                break;
                                            }

                                    }
                                }
                            }
                        }
                        if (sBigZeroField != "")
                        {
                            if (con.Tag != null)
                            {
                                for (int intn = 0; intn < strFld1.Length; intn++)
                                {
                                    strField = strFld1[intn].Trim();

                                    switch (con.GetType().Name)
                                    {
                                        case "ComboBox":
                                        case "TextBox":
                                        case "DateTimePicker":
                                        case "RichTextBox":
                                        case "MaskedTextBox":
                                            {
                                                if (con.Tag.ToString() == strField)
                                                {
                                                    if (con.Text == "" || Convert.ToInt32(con.Text) <= 0)
                                                    { strError = strError + strFldCap1[intn] + " Must be Big Zero" + "\r\n"; }
                                                }
                                                break;
                                            }
                                        case "NumericUpDown":
                                            {
                                                if (con.Tag.ToString() == strField)
                                                {
                                                    NumericUpDown objcon = (NumericUpDown)con;
                                                    if (objcon.Value <= 0)
                                                    { strError = strError + strFldCap1[intn] + " Must be Big Zero" + "\r\n"; }
                                                }
                                                break;
                                            }
                                    }
                                }
                            }
                        }
 
                    }
                    //
                }

                if (gError != "" && strError != "")
                {
                    gError = gError + strError;
                }
                else if (strError != "")
                {
                    gError = strError;
                }
            }
            catch
            {

            }

        }
        protected virtual void After_SaveData()
        {
 
        }
        private void updateDataTable()
        {
            DataRow dr;
            if (m_FormSatus == 1)
            {
                dr = dTable.NewRow();
            }
            else
            {
                dr = dTable.Rows[0];
            }
            UpdateTable(this.EditBar.Controls, dr);
            if (m_FormSatus == 1)
            {
              dTable.Rows.Add(dr);
            }
            
        }
        private void UpdateTable(Control.ControlCollection con, DataRow dr)
        {
            string sFiled;
            try
            {
                foreach (Control o in con)
                {
                    if (o.Controls.Count > 0 && Dal.IfContainer(o) == true)
                    {
                        UpdateTable(o.Controls, dr);
                    }
                    else
                    {
                        if (o.Tag != null)
                        {
                            sFiled = o.Tag.ToString();
                            if (sFiled != "")
                            {
                                switch (o.GetType().Name)
                                {
                                    case "TextBox":
                                    case "MaskedTextBox":
                                    case "ComboBox":
                                    case "DateTimePicker":
                                        {
                                            dr[sFiled] = o.Text;
                                            break;
                                        }
                                    case "CheckBox":
                                        {
                                            CheckBox chk = (CheckBox)o;
                                            dr[sFiled] = chk.Checked;
                                            break;
                                        }
                                    case "RadioButton":
                                        {
                                            RadioButton chk = (RadioButton)o;
                                            dr[sFiled] = chk.Checked;
                                            break;
                                        }
                                    case "CheckedListBox":
                                        {
                                            CheckedListBox objcon = (CheckedListBox)o;
                                            string svalue = "";
                                            for (int intn = 0; intn < objcon.Items.Count; intn++)
                                            {
                                                if (objcon.GetItemChecked(intn))
                                                {
                                                    if (svalue != "")
                                                    {
                                                        svalue = svalue + ",";

                                                    }
                                                    svalue = svalue + objcon.GetItemText(objcon.Items[intn]);
                                                }
                                            }
                                            dr[sFiled] = svalue;
                                            break;
                                        }
                                    case "ListBox":
                                        {
                                            ListBox objcon = (ListBox)o;
                                            string svalue = "";
                                            for (int intn = 0; intn < objcon.Items.Count; intn++)
                                            {
                                                if (svalue != "")
                                                {
                                                    svalue = svalue + ",";
                                                }
                                                svalue = svalue + objcon.Items[intn].ToString();
                                            }
                                            dr[sFiled] = svalue;
                                            break;
                                        }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        protected override void btnSave_Click(object sender, EventArgs e)
        {
            if (Before_SaveData() == false) { return; }
            updateDataTable();
            string strError="";
            Dal.BatchSave("select " + sWriteColumnIDs + " from " + sTable, dTable, out strError);
            if (strError != "")
            {
                MessageBox.Show(strError + " Saving is failure ");
               
            }
            else
            {
                dTable.AcceptChanges();
                Dal.BindingObject(this.EditBar.Controls, dTable);
                m_FormSatus = 3;
                ControlEnable();

            }
        }

        protected override void btnCancel_Click(object sender, EventArgs e)
        {
            dTable.RejectChanges();
            m_FormSatus = 3;
            ControlEnable();
        }
        protected override void btnRefresh_Click(object sender, EventArgs e)
        {

        }
        protected override void btnCopy_Click(object sender, EventArgs e)
        {
            FrmCopy frm = new FrmCopy();
            frm.ShowDialog();

        }

        protected override void btnImport_Click(object sender, EventArgs e)
        {
           
        }

        protected override void btnExport_Click(object sender, EventArgs e)
        {
           
        }
        protected override void btnSearch_Click(object sender, EventArgs e)
        {

        }
        private void FindFirstControl()
        {
            try
            {
                foreach (Control o in EditBar.Controls)
                {
                    if (o.TabIndex == 0)
                    {
                        o.Focus();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {

        }

 


    }
}
