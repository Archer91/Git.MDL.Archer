using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PubApp.Data;

namespace MDL_CRM
{
    public partial class Fm_ModityLog : Form
    {
        private string m_strKey;

        public Fm_ModityLog()
        {
            InitializeComponent();
        }

        public Fm_ModityLog(string pKey) 
            : this() 
        {
            m_strKey = pKey;
        }

        private void Fm_ModityLog_Load(object sender, EventArgs e)
        {
            LogGrid.AutoGenerateColumns = false;
            DataTable dt;
            dt = Dal.GetDataTable("select USER_ID,IP,ACTIONTIME,FUNCTION,ACTION,RESULT,RESULT_DESC,FROM_SYSTEM,FROM_KEY_VALUE from ZT_SS_LOG where FROM_KEY_VALUE='" + m_strKey + "'");
            LogGrid.DataSource = dt;
        }

        private void LogGrid_DoubleClick(object sender, EventArgs e)
        {
            if (LogGrid.CurrentCell != null)
            {
                if (LogGrid.Columns[LogGrid.CurrentCell.ColumnIndex].Name == "colRESULT_DESC")
                {
                    FrmBigMeno frm = new FrmBigMeno();
                    frm.ReturnValue = LogGrid.Rows[LogGrid.CurrentCell.RowIndex].Cells["colRESULT_DESC"].Value.ToString();
                    frm.blnReadOnly =true;
                    frm.Text = "修改描述";
                    frm.ShowDialog();
                    if (frm.Bcacnel) { return; }
                    LogGrid.Rows[LogGrid.CurrentCell.RowIndex].Cells["colRESULT_DESC"].Value = frm.ReturnValue;
                }
            }
        }
    }
}
