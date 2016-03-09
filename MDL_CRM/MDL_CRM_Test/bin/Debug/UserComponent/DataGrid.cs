using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace MDL_CRM.UserComponent
{
    public partial class DataGrid : DataGridView
    {
        public DataGrid()
        {
            InitializeComponent();
        }
        public event KeyEventHandler CellKeyDown; 

        public event KeyEventHandler CellKeyUp; 

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) 
        { 
        bool bResult; 
        if (CellKeyDown != null) 
        this.CellKeyDown(this, new KeyEventArgs(keyData)); 

        if (msg.WParam.ToInt32() == (int)Keys.Return) 
        { 
            bResult = true; 
        } 
        else 
        bResult = base.ProcessCmdKey(ref msg, keyData); 
        if (CellKeyUp != null) 
        this.CellKeyUp(this, new KeyEventArgs(keyData)); 

        return bResult; 
        } 

        public DataGrid(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
