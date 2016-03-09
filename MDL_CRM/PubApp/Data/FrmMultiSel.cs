using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using PubApp.Data;
using System.Text.RegularExpressions;
namespace PubApp.Data
{
    public partial class FrmMultiSel : Form
    {
      bool m_Bcancel;
      string  m_strCaps="";
      string  m_intColWidth="";
      string  m_strSQL="";
      string  strinitOrder="";
      string  strinitWhere="";
      string[] strField=new string[]{};
      string m_strReturnValue = "";
      DataTable m_dTable;
      DataTable m_RedTable;
      int intRow = 0;
      bool m_blnMultiValue = false;
    private int CurrentRow;
    public string strReturnValue
    {
        get { return m_strReturnValue; }
    }
    public DataTable RedTable
    {
        get { return m_RedTable; }
    }
    public bool Bcancel
    {
        get { return m_Bcancel; }
    }
    public string strCaption
    {
        set { m_strCaps = value; }
    }
    public bool blnMultiValue
    {
        set { m_blnMultiValue = value; }
    }
    public DataTable dTable
    {
        set { m_dTable = value; }
    }
    public string strSQL
    {
        set { m_strSQL = value; }
    }
    public string intColWidth
    {
        set { m_intColWidth = value; }
    }
    public string strOrder
    {
        set { strinitOrder = value; }
    }
    public string strWhere
    {
        set { strinitWhere = value; }
    }
 private void FrmMultiSel_FormClosed( object sender,  System.Windows.Forms.FormClosedEventArgs e)  
 {

     if (m_dTable == null) { return; }
     m_dTable.Dispose();
    }
 private void FrmMultiSel_Load( object sender,  System.EventArgs e)  
{
        try {

            m_Bcancel = false;

            if (m_dTable != null)
            {
                LoadData();
            }
            else
            {
                if (m_strSQL == "") { return; }
                LoadDataBySql();
            }
           // Label1refresh(MGrid.Columns[0].HeaderText);
           // strFindField = MGrid.Columns[0].Name.Trim();

        } catch (Exception ex) { 
            MessageBox.Show(ex.Message);
        }
    }
 private void LoadData()
 {
     try
     {
         MGrid.DataSource = m_dTable;
         
         FormMGrid();
     }
     catch
     {

     }
 }
 private void LoadDataBySql(string  strWhere = "")
    {
         string  strSQL;
        try {
            strSQL = m_strSQL;
            if ( strinitWhere != "" ) {
                if ( strWhere != "" ) {
                    strSQL = strSQL + " where " + strinitWhere + " and " + strWhere;
                } else {
                    strSQL = strSQL + " where " + strinitWhere;
                }
            } else if ( strWhere != "" ) {
                strSQL = strSQL + " where " + strWhere;
            }
            if ( strinitOrder != "" ) {
                strSQL = strSQL + " order by " + strinitOrder;
            }
            m_dTable = Dal.GetDataTable(strSQL);
            MGrid.DataSource = m_dTable;
            FormMGrid();
        } catch  {

        }
    }
 private void Label1refresh( string  strCaption) {
        Label1.Text = "查找条件(根据:" + strCaption + "查找)";
        TextBox1.Left = Label1.Left + Label1.Width;
        TextBox1.Width = 400 - (Label1.Width - 50);
    }
 private void FormMGrid() {
         int intN;
         string[] strCapdiv=new string[]{};
         string[] intColwidths=new string[]{};
        try 
        {
                MGrid.DefaultCellStyle.ForeColor = Color.Blue;
                MGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
                MGrid.ColumnHeadersHeight = 30;
                MGrid.ColumnHeadersDefaultCellStyle.Alignment =DataGridViewContentAlignment.MiddleCenter;
                MGrid.ReadOnly = true;

                if (!(m_strCaps == ""))
                {
                    strCapdiv = m_strCaps.Split(',');
                    for ( intN = 0 ;intN<=strCapdiv.Length-1;intN++)
                    {
                        MGrid.Columns[intN].HeaderText = strCapdiv[intN];
                    } //
                }

                if (!(m_intColWidth == ""))
                {
                    intColwidths = m_intColWidth.Split(',');
                    for ( intN = 0 ;intN<=intColwidths.Length-1;intN++)
                    {
                        MGrid.Columns[intN].Width =Convert.ToInt32(intColwidths[intN]);
                    } //
                }
            }  
         catch (Exception ex) {
                        MessageBox.Show(ex.Message);

        }

    }
 private void cmdCancel_Click( object sender,  EventArgs e)  
        {
            m_Bcancel = true;
        this.Close();
    }

 private void MGrid_CellClick( object sender,  System.Windows.Forms.DataGridViewCellEventArgs e)
      {
        CurrentRow = e.RowIndex;
    }


 private void cmdFind_Click( object sender,  EventArgs e) 
 {
     if (m_dTable != null)
     {
         if (MGrid.Rows.Count == 0) { return; }
         m_dTable.DefaultView.Sort = "";
         try
         {

             if (m_dTable == null) { return; }
             if (this.TextBox1.Text != "")
             {
                 Regex r = new Regex(TextBox1.Text, RegexOptions.IgnoreCase);
                 for (int intn = intRow; intn <= m_dTable.Rows.Count - 1; intn++)
                 {
                     for (int intm = 0; intm <= m_dTable.Columns.Count - 1; intm++)
                     {
                         if (r.IsMatch(m_dTable.Rows[intn][intm].ToString()))
                         {
                             MGrid.CurrentCell = MGrid.Rows[intRow].Cells[0];
                             intRow = intRow + 1;
                             if (intn == m_dTable.Rows.Count - 1)
                             { intRow = 0; }
                             goto g1;
                         }
                     }
                     intRow = intRow + 1;
                     if (intn == m_dTable.Rows.Count - 1)
                     { intRow = 0; }
                 }
             }
         g1:
             { }
         }
         catch
         { }
     }
 }

 private void cmdOK_Click( object sender,  EventArgs e) 
 {
         int intN;
        try {
            if (m_dTable.Rows.Count == 0) { return; }
            m_strReturnValue = "";
            if (m_blnMultiValue == true)
            {
                m_RedTable = m_dTable.Clone();
                for (intN = 0; intN < MGrid.Rows.Count; intN++)
                {
                    if (MGrid.Rows[intN].Selected == true)
                    {
                        DataRow dr = (MGrid.Rows[intN].DataBoundItem as DataRowView).Row;
                        m_RedTable.ImportRow(dr);
                    }
                    
                } 
            }
            else
            {
                for (intN = 0; intN < MGrid.Rows.Count; intN++)
                {
                    if (MGrid.Rows[intN].Selected == true)
                    {
                        if (m_strReturnValue != "")
                        {
                            m_strReturnValue = m_strReturnValue + ",";
                        }
                        m_strReturnValue = m_strReturnValue + MGrid.Rows[intN].Cells[0].Value; 
                    }
                } 
            }
              
        } catch 
     {

        }
        this.Close();
    }

 public FrmMultiSel() {

        // 此调用是 Windows 窗体设计器所必需的。
        InitializeComponent();

        // 在 InitializeComponent() 调用之后添加任何初始化。

 }

 private void MGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
 {
     if (m_dTable.Rows.Count == 0) { return; }
     cmdOK.PerformClick();
 }

 private void TextBox1_KeyDown(object sender, KeyEventArgs e)
 {
     if (e.KeyCode == Keys.Enter)
     {
         this.Select();
         if (TextBox1.Text.Trim() != "")
         {
             cmdFind.PerformClick();
         }
     }
     if (e.KeyCode == Keys.Down)
     {
         if (MGrid.Rows.Count > 0)
         {
             MGrid.Select();
            // MGrid.CurrentCell = MGrid.Rows[0].Cells[0];
         }
     }
 
 }

 private void FrmMultiSel_KeyDown(object sender, KeyEventArgs e)
 {

 }

 private void MGrid_KeyDown(object sender, KeyEventArgs e)
 {
     if (e.KeyCode == Keys.Enter)
     {
         cmdOK.PerformClick();
     }
     if (e.KeyCode == Keys.Home)
     {
       //  TextBox1.Focus();

     }
     string s=((char)e.KeyData).ToString();
     if (("ABCDEFGHIJKLMNOPQRSTUVWXYZ").IndexOf(s.ToUpper()) > -1 || ("0123456789").IndexOf(s) > -1)
     {
         if (e.KeyCode == Keys.CapsLock)
         {
             TextBox1.Text = s.ToUpper();
         }
         else
         {
             TextBox1.Text = s.ToLower();
         }
         TextBox1.Focus();
         TextBox1.SelectionStart = 1;
         
     }
   
 }

 private void FrmMultiSel_Activated(object sender, EventArgs e)
 {
     TextBox1.Focus();
 }

    }
}
