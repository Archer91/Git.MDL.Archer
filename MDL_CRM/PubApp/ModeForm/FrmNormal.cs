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
using System.Text.RegularExpressions;

namespace PubApp.ModeForm
{
    public partial class FrmNormal : FrmTool

    {
        /// <summary>
        /// Input Parameter Table Name
        /// </summary>
        protected string sTable = "";
        /// <summary>
        ///  Input Parameter View Name
        /// </summary>
        protected string sView = "";
        /// <summary>
        /// Input Parameter Column Field For Example: Field1,Field2,Field3,And so on
        /// </summary>
        protected string sColumnIDs = "";
        protected string sWriteColumnIDs = "";
        protected string sNotEditColumnIDs = "";
        /// <summary>
        /// Input Parameter Column Caption For Example: Caption1,Caption2,Caption3,And so on
        /// </summary>
        protected string sColumnCaptions = "";
        protected string sColumnWidths = "";
        /// <summary>
        /// Input Parameter Table Main Key Field 
        /// </summary>
        protected string sMainKey = "";
        /// <summary>
        /// Input Parameter default Condition
        /// </summary>
        protected string sWhere = "";
        protected string sOrder = "";
        /// <summary>
        /// DataTable Name
        /// </summary>
        protected string sNotEmptyField = "";
        protected string sNotEmptyFieldDesc = "";
        protected string sBigZeroField = "";
        protected string sBigZeroFieldDesc = "";
        protected string CheckedColumns = "";
        protected string HideColumns = "";
        protected DataTable dTable;
        protected string sNotDelCondition= "";
        protected string sNotEditCondition= "";
        string gError = "";
        string gFilter = "";
        enum  FormSatus
        { 
            iNew=1,
            iEdit,
            iBrowse
        }
        protected int m_FormSatus = 3;
        protected int intCurRow = 0;
        int intRow = 0;
        string NewWhere = "";
        public FrmNormal()
        {
            InitializeComponent();
        }
        protected virtual void LoadGridData()
        {
            if (sView == "") { return; }
            if (NewWhere != "")
            {
                if (sWhere != "")
                {
                    NewWhere = NewWhere + " and " + sWhere;
                }
            }
            else
            {
                if (sWhere != "")
                {
                    NewWhere = sWhere;
                }
            }
            this.Cursor = Cursors.WaitCursor;
            string strTmp = "";
            strTmp = "select distinct " + sColumnIDs + " from " + sView;
            if (NewWhere != "")
            {
                strTmp = strTmp + " where " + NewWhere;
            }
            else
            { 

            }
            if (sOrder != "")
            {
                strTmp = strTmp + " order by " + sOrder;
            }
            dTable = Dal.GetDataTable(strTmp);
            Dal.FormatGrid(Grid, dTable, sColumnIDs, sColumnCaptions, sColumnWidths, CheckedColumns, HideColumns);
            Grid.DataSource = dTable;
            this.Cursor = Cursors.Default;
        }
        protected  void ControlEnable()
        {
            if (m_FormSatus ==(int) FormSatus.iBrowse)
            {
                this.NewEnable = true;
                this.EditEnable = true;
                this.DelEnable = true;
                this.SaveEnable = false;
                this.CancelEnable = false;
                this.ImportEnable = true;
                this.ExportEnable = true;
                this.CopyEnable = true;
                this.RefreshEnable = true;
                this.Grid.Enabled = true;
                this.btnQuery.Enabled = true;
            }
            if (m_FormSatus == (int)FormSatus.iNew)
            {
                this.NewEnable = false;
                this.EditEnable = false;
                this.DelEnable = false;
                this.SaveEnable = true;
                this.CancelEnable = true;
                this.ImportEnable = false;
                this.ExportEnable = false;
                this.CopyEnable = false;
                this.Grid.Enabled = false;
                this.RefreshEnable = false;
                this.btnQuery.Enabled = false;
            }
            if (m_FormSatus == (int)FormSatus.iEdit)
            {
                this.NewEnable = false;
                this.EditEnable = false;
                this.DelEnable = false;
                this.SaveEnable = true;
                this.CancelEnable = true;
                this.ImportEnable = false;
                this.ExportEnable = false;
                this.CopyEnable = false;
                this.Grid.Enabled = false;
                this.RefreshEnable = false;
                this.btnQuery.Enabled = false;

 
            }
            Dal.EnabledControl(this.EditBar.Controls, m_FormSatus);   
            this.EnableButton();
 
        }
 
        private void FrmNormal_Load(object sender, EventArgs e)
        {
            Grid.AutoGenerateColumns = false;
            CheckedColumns = CheckedColumns.ToUpper();
            LoadGridData();//load grid data
            //BindControl();//binding data
            ControlEnable();

        }
        private void BindControl()
        {
            Dal.BindingObject(this.EditBar.Controls, dTable);
 
        }

        protected override void btnNew_Click(object sender, EventArgs e)
        {
            dTable.DefaultView.Sort = "";
            if (Grid.Rows.Count > 0)
            { intCurRow = Grid.CurrentCell.RowIndex; }
            else
            { intCurRow = -1; }
            FindFirstControl();
            m_FormSatus = 1;
            ControlEnable();
            Dal.BlankControl(this.EditBar.Controls);
 
        }

        protected override void btnEdit_Click(object sender, EventArgs e)
        {
            if (Grid.Rows.Count == 0) { return; }
            m_FormSatus = 2;
            intCurRow = Dal.GetCurrentRowIndex(Grid, dTable);
            ControlEnable();
            Dal.EditKeyControl(this.EditBar,sMainKey);
            FindFirstControl();
 
        }
        private void del()
        {
            string strError;

            intCurRow = Dal.GetCurrentRowIndex(Grid,dTable);
            if (MessageBox.Show("Confirm to Delete it?", "Alter", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                dTable.Rows[intCurRow].Delete();
                string str = Dal.SaveSQL(sWriteColumnIDs, sTable, dTable, sMainKey);
                if (str != "")
                {
                    Dal.ExeCommnd(str,out strError);
                    dTable.AcceptChanges();
                }
               // Dal.BatchSave("select " + sWriteColumnIDs + " from " + sTable, dTable, out strError);
                after_Del();
                LocateRec();
            }
        }
        protected override void btnDel_Click(object sender, EventArgs e)
        {
            if (Grid.Rows.Count == 0) { return; }
            //dTable.DefaultView.Sort = "";
            if (Before_Del() == false) { return; };
            del();

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
            chkData(this.EditBar.Controls);
            gFilter = "";
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
        protected virtual void After_SaveData()
        {
 
        }
        private void UpdateTable(Control.ControlCollection con, DataRow dr,DataTable dt=null)
        {
            string sFiled;
            try
            {
                foreach (Control o in con)
                {
                    if (o.Controls.Count > 0 &&Dal.IfContainer(o) == true)
                    {
                        UpdateTable(o.Controls, dr);
                    }
                    else
                    {
                        if (o.Tag != null)
                        {
                            sFiled = o.Tag.ToString().Trim();
                            if (sFiled != "")
                            {
                                if (dt.Columns.Contains(sFiled))
                                {
                                    switch (o.GetType().Name)
                                    {
                                        case "TextBox":
                                        case "MaskedTextBox":
                                        case "ComboBox":
                                            {

                                                if (sFiled.ToLower().IndexOf("password") > 0)
                                                {
                                                   // dr[sFiled] = Dal.Encrypt(o.Text);
                                                    dr[sFiled] = o.Text;
                                                }
                                                else
                                                {
                                                    dr[sFiled] = o.Text;

                                                }
                                                break;
                                            }
                                        case "DateTimePicker":
                                            {
                                                DateTimePicker chk = (DateTimePicker)o;
                                                dr[sFiled] = chk.Value;
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        protected void updateDataTable()
        {

            int index;
            DataRow dr;

            if (m_FormSatus == 1)
            {
                dr = dTable.NewRow();
                
            }
            else
            {
                if (Grid.CurrentRow != null)
                {
                    index = intCurRow;
                }
                else
                {
                    index = dTable.Rows.Count - 1;
                }
  
                dr = dTable.Rows[index];
             }

            UpdateTable(this.EditBar.Controls, dr, dTable);
             if (m_FormSatus == 1)
             {
                 dTable.Rows.Add(dr);

             }
             

        }
        protected override void btnSave_Click(object sender, EventArgs e)
        {

            if (Before_SaveData() == false) { return; }
             updateDataTable();

            string strError="";
            // string str=(Dal.CheckDataRow(dr, sMainKey, "", sNotEmptyField, sNotEmptyFieldDesc, sBigZeroField, sBigZeroFieldDesc);
            // if  (str!= "") { return; }
            string str = Dal.SaveSQL(sWriteColumnIDs, sTable, dTable, sMainKey);
            Dal.ExeCommnd(str, out strError);
            if (strError != "")
            {
                MessageBox.Show(strError + " Saving is failure ");
                return;
            }
            else
            {
                dTable.AcceptChanges();
                if (m_FormSatus == 1)
                {
                    intCurRow = dTable.Rows.Count - 1;
                    LocateRec();
                }

            }

            m_FormSatus = 3;
            ControlEnable();
        }

        protected override void btnCancel_Click(object sender, EventArgs e)
        {
            //dTable.RejectChanges();
            //Dal.BindingObject(this.EditBar.Controls, dTable);
            m_FormSatus = 3;
            ControlEnable();
            LocateRec();
        }
        protected void LocateRec()//定位
        {
            if (Grid.Rows.Count == 0)
            {
                Dal.BlankControl(this.EditBar.Controls);
                return;
            }
            if (intCurRow > Grid.Rows.Count - 1)
            {
                intCurRow = Grid.Rows.Count - 1;
            }
            if (intCurRow >= 0)
            {

                Grid.CurrentCell = Grid.Rows[intCurRow].Cells[0];
  
                Grid_SelectionChanged(null, null);

            }
        }
        protected override void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadGridData();
           // BindControl();
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
            Dal.ExportDataGridViewToExcel(Grid, progressBar1, saveFileDialog1);
        }
        protected override void btnSearch_Click(object sender, EventArgs e)
        {
            if (m_FormSatus !=(int) FormSatus.iBrowse) { return; }
            if (Grid.Rows.Count == 0) { return; }
            dTable.DefaultView.Sort = "";
            try
            {
 
                if (dTable == null) { return; }
                if (this.txtSearch.Text!= "")
                {
                    Regex r = new Regex(txtSearch.Text, RegexOptions.IgnoreCase);
                    for (int intn = intRow; intn <= dTable.Rows.Count - 1; intn++)
                    {
                        for (int intm = 0; intm <= dTable.Columns.Count - 1; intm++)
                        {
                            if (r.IsMatch(dTable.Rows[intn][intm].ToString()))
                            {
                                Grid.CurrentCell = Grid.Rows[intRow].Cells[0];
                                intRow = intRow + 1;
                                if (intn == dTable.Rows.Count - 1)
                                { intRow = 0; }
                                goto g1;
                            }
                        }
                        intRow = intRow + 1;
                        if (intn == dTable.Rows.Count - 1)
                        { intRow = 0; }
                    }
                }
            g1:
                { }
            }
            catch
            { }

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
           Dal.gWhere = "";
           Dal.GetControlFindSQL(this.FindBar.Controls, sView);
           NewWhere = Dal.gWhere;
           
           LoadGridData();
        }

        protected virtual void Grid_SelectionChanged(object sender, EventArgs e)
        {
            if (Grid.CurrentCell == null) { return; }
            try
            {
                if (dTable.Rows.Count > 0)
                {
  
                    int index;
                    index = Dal.GetCurrentRowIndex(Grid,dTable);
                    if (index == -1) { return; }
                    DataRow dr = dTable.Rows[index];
                    Dal.LoadDefControlValue(this.EditBar.Controls, dTable, index);
                    Dal.LoadControlValue(this.EditBar.Controls, dr);
                }
                else
                {
                    Dal.BlankControl(this.EditBar.Controls);
                }
            }
            catch(Exception ex)
            { MessageBox.Show(ex.Message); }
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
                                                            strFilter = strFilter + strField + "='" + con.Text.Trim()+"'";
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
                        if (sMainKey != "" && m_FormSatus == 1)
                        {
                            if (con.Tag != null)
                            {
                                strField = sMainKey;
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
                                                if (con.Text.Trim() != "")
                                                {
                                                    string str = Dal.strGetValue("select 1 from " + this.sTable + " where " + sMainKey + "='" + con.Text.Trim() + "'");
                                                    if (str == "1")
                                                    {
                                                        { strError = strError +  "Main Key's values is already exists \r\n"; }
                                                    }
                                                }
                                            }
                                            break;
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
        private void Grid_DoubleClick(object sender, EventArgs e)
        {
           // btnEdit_Click(null, null);
        }

       
 
    }
}
