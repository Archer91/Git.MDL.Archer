using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MDL_CRM
{
    public partial class Fm_FDA : Form
    {
        public Fm_FDA()
        {
            InitializeComponent();
        }

        public string strword = string.Empty;
        public bool boolEdit = false;
        public void BindDataGridView(string _word)
        {
            string sql = "select pro.ZPROD_FDAM_CODE,'' comlist,pro.zprod_att_fda_qty,pro.prod_code,pro.pcat_code,pro.prod_desc,pro.prod_desc_chi,pro.prod_createdate,pro.prod_lmodby,pro.prod_lmoddate from Product pro where pro.prod_code like '%{0}%'";
            sql = string.Format(sql, _word);
            DataSet ds = DB.GetDSFromSql(sql);
            dataGridView1.DataSource = ds.Tables[0];
            BindComList();
        }

        public void BindComList()
        {
            DataGridViewComboBoxColumn cmbox = dataGridView1.Columns["Com_Fda"] as DataGridViewComboBoxColumn;

            string sql = "select FDAM_CODE, (FDAM_CODE || '--' ||  FDAM_DESC) as TITLE,FDAM_DESC from FDA_MASTER";
            DataSet ds_com = DB.GetDSFromSql(sql);
            DataView dv = ds_com.Tables[0].DefaultView;
            dv.Sort = "FDAM_CODE asc";
            cmbox.DataSource = dv;
            cmbox.DisplayMember = "TITLE";
            cmbox.ValueMember = "FDAM_CODE";
            
            //DataGridViewComboBoxColumn cmb = dataGridView1.Columns["zprod_att_fda_qty"] as DataGridViewComboBoxColumn; 
            //cmb.Items.Add("Y");
            //cmb.Items.Add("N");  
        }

        private void button1_Click(object sender, EventArgs e)
        {
            boolEdit = false;
            if (string.IsNullOrEmpty(txtCode.Text.ToUpper().Trim()))
            {
                MessageBox.Show("编号必需输入才能搜寻！", "注意", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCode.Text = "";
                txtCode.Focus();
            }
            else
            {

                BindDataGridView(txtCode.Text.ToUpper().Trim());
                if (dataGridView1.RowCount < 1)
                {
                    MessageBox.Show("所输入的资料不存在!", "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCode.Focus();
                }
                else
                {
                    this.dataGridView1.CurrentCell = this.dataGridView1.Rows[0].Cells["ZPROD_FDAM_CODE"];
                    this.dataGridView1.BeginEdit(true);
                }
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //MessageBox.Show(dataGridView1.SelectionMode.ToString(), "系统消息");
                //如果关键字段"type"，说明是在编辑新行的其它字段的值，不需要做如何操作；
                if (e.RowIndex != -1)
                {
                    string typeTemp = dataGridView1.Rows[e.RowIndex].Cells["ZPROD_FDAM_CODE"].FormattedValue.ToString().Trim();
                    //if (!string.IsNullOrEmpty(strword))
                    //{
                    //    typeTemp = strword;
                    //}
                    string proCode = string.Empty;
                    string sql = "select FDAM_code from FDA_MASTER where FDAM_code = '{0}'";
                    string sql_update = "update Product set ZPROD_FDAM_CODE = '{0}' where prod_code ='{1}'";
                    proCode = dataGridView1.Rows[e.RowIndex].Cells["prod_code"].FormattedValue.ToString();

                    if (typeTemp == "SYSTEM.DATA.DATAROWVIEW")
                    {
                        return;
                    }



                    sql = string.Format(sql, typeTemp.ToUpper());

                    if (string.IsNullOrEmpty(typeTemp))
                    {
                        //DB.ExecuteFromSql(string.Format(sql_update, typeTemp, proCode));
                        dataGridView1.Rows[e.RowIndex].Cells["ZPROD_FDAM_CODE"].Value = "";
                        boolEdit = true;
                    }
                    else
                    {
                        DataSet ds = DB.GetDSFromSql(sql);
                        if (ds.Tables[0].Rows.Count < 1)
                        {
                            int _curIndexRow = e.RowIndex;
                            MessageBox.Show("找不到相关FDA资料!", "注意", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dataGridView1.Rows[e.RowIndex].Cells["ZPROD_FDAM_CODE"].Value = "";
                            //dataGridView1.Rows[e.RowIndex].Cells["ZPROD_FDAM_CODE"].Selected = true;
                            this.dataGridView1.CurrentCell = this.dataGridView1.Rows[_curIndexRow].Cells["ZPROD_FDAM_CODE"];
                            this.dataGridView1.BeginEdit(true);
                        }
                        else
                        {
                            dataGridView1.Rows[e.RowIndex].Cells["ZPROD_FDAM_CODE"].Value = typeTemp.ToUpper();
                            //你确认要保存吗？
                            //if (MessageBox.Show("确定保存吗?", "注意", MessageBoxButtons.YesNo,MessageBoxIcon.Information) == DialogResult.Yes){
                            //    DB.ExecuteFromSql(string.Format(sql_update, typeTemp.ToUpper(), proCode));
                            //}
                            boolEdit = true;                            
                        }
                    }
                }
            }
            catch
            { 
            
            }
        }

        private void Fm_FDA_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            BindListBox();
            txtCode.Focus();
        }

        public void BindListBox()
        {
            string sql = "select FDAM_CODE, (FDAM_CODE || '--' ||  FDAM_DESC) as TITLE,FDAM_DESC from FDA_MASTER ";
             DataSet ds = DB.GetDSFromSql(sql);
            DataView dv = ds.Tables[0].DefaultView;
            dv.Sort = "FDAM_CODE asc";
            lst_fda.DataSource = dv;
            lst_fda.ValueMember = "FDAM_CODE";
            lst_fda.DisplayMember = "TITLE";
        }
       
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex !=0 )
            {
               strword = string.Empty;
               string typeTemp = dataGridView1.Rows[e.RowIndex].Cells["prod_code"].FormattedValue.ToString().Trim();
               dataGridView1.Rows[e.RowIndex].Cells["prod_code"].Selected = true;
               int _index = e.RowIndex;
               string proCode = string.Empty;
               proCode = dataGridView1.Rows[e.RowIndex].Cells["prod_code"].FormattedValue.ToString();
               Frm_Dialog_Faq fmdialog = new Frm_Dialog_Faq();
               fmdialog.BindListBox();
               fmdialog.ShowDialog();
               typeTemp = fmdialog.strFAQWord.ToUpper();
               
               string sql = "select FDAM_code from FDA_MASTER where FDAM_code = '{0}'";
               sql = string.Format(sql, fmdialog.strFAQWord.ToUpper());
               DataSet ds = DB.GetDSFromSql(sql);
               if (ds.Tables[0].Rows.Count < 1)
               {
                   MessageBox.Show("找不到相关FDA资料!", "注意", MessageBoxButtons.OK, MessageBoxIcon.Information);
               }
               else
               {
                   
                   //sql = "update Product set ZPROD_FDAM_CODE = '{0}' where prod_code ='{1}'";
                   //DB.ExecuteFromSql(string.Format(sql, typeTemp.ToUpper(), proCode));
                   //dataGridView1.Rows[_index].Cells["ZPROD_FDAM_CODE"].Value = typeTemp;
                   strword = typeTemp;
                   //MessageBox.Show(strword);
                   dataGridView1_CellValueChanged(sender, e);
               }
            }
        }

        private void 编辑toolMenuItem_Click(object sender, EventArgs e)
        {
            strword = string.Empty;
            string typeTemp = dataGridView1.SelectedRows[0].Cells["prod_code"].FormattedValue.ToString().Trim();
            dataGridView1.SelectedRows[0].Cells["prod_code"].Selected = true;
            string proCode = string.Empty;
            proCode = dataGridView1.SelectedRows[0].Cells["prod_code"].FormattedValue.ToString();
            Frm_Dialog_Faq fmdialog = new Frm_Dialog_Faq();
            fmdialog.BindListBox();
            fmdialog.ShowDialog();
            typeTemp = fmdialog.strFAQWord.ToUpper();

            string sql = "select FDAM_code from FDA_MASTER where FDAM_code = '{0}' ";
            sql = string.Format(sql, fmdialog.strFAQWord.ToUpper());
            DataSet ds = DB.GetDSFromSql(sql);
            if (ds.Tables[0].Rows.Count < 1)
            {
                MessageBox.Show("找不到相关FDA资料!", "注意", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                strword = typeTemp;
                sql = "update Product set ZPROD_FDAM_CODE = '{0}' where prod_code ='{1}'";
                dataGridView1.SelectedRows[0].Cells["ZPROD_FDAM_CODE"].Value = typeTemp.ToUpper();
                //DB.ExecuteFromSql(string.Format(sql, typeTemp.ToUpper(), proCode));
            }
        }

       
        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                string fda_code = dataGridView1.Rows[e.RowIndex].Cells["ZPROD_FDAM_CODE"].FormattedValue.ToString();
               
            }
        }

        private void ck_ListBoxFaq_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void Fm_FDA_Activated(object sender, EventArgs e)
        {
            txtCode.Focus();
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {               
                    ComboBox combox = sender as ComboBox;
                    //这里比较重要
                    combox.Leave += new EventHandler(combox_Leave);
                    string sql_update = "update Product set ZPROD_FDAM_CODE = '{0}' where prod_code ='{1}'";
                    string _fdaWord = string.Empty;

                    //在这里就可以做值是否改变判断
                    if (combox.SelectedValue != null)
                    {
                        _fdaWord = combox.SelectedValue.ToString().ToUpper();
                        string proCode = dataGridView1.CurrentRow.Cells["prod_code"].FormattedValue.ToString();

                        //if (MessageBox.Show("确定保存吗?", "注意", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        //{
                        //    DB.ExecuteFromSql(string.Format(sql_update, _fdaWord, proCode));                       
                        //}
                        boolEdit = true;
                        if (_fdaWord != "SYSTEM.DATA.DATAROWVIEW")
                        {
                            dataGridView1.CurrentRow.Cells["ZPROD_FDAM_CODE"].Value = _fdaWord;
                        }
                    }                
                          
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void combox_Leave(object sender, EventArgs e)
        {
            ComboBox combox = sender as ComboBox;
            //做完处理，须撤销动态事件
            combox.SelectedIndexChanged -= new EventHandler(ComboBox_SelectedIndexChanged);
        }
        public void dgv_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {           
            DataGridView dgv = sender as DataGridView;
            if (dgv.CurrentCell.GetType().Name == "DataGridViewComboBoxCell" && dgv.CurrentCell.RowIndex != -1 )
            {
                (e.Control as ComboBox).SelectedIndexChanged += new EventHandler(ComboBox_SelectedIndexChanged);
            }
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount < 1)
            {
                return;
            }                
            
            if (MessageBox.Show("确定保存吗?", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sql_update = "update Product set ZPROD_FDAM_CODE = '{0}',zprod_att_fda_qty='{1}' where prod_code ='{2}'";
                string typeTemp = string.Empty;
                string strBool = string.Empty;
                string proCode = string.Empty;
                int insetRow = 0;
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    typeTemp = dataGridView1.Rows[i].Cells["ZPROD_FDAM_CODE"].FormattedValue.ToString().Trim();
                    proCode =  dataGridView1.Rows[i].Cells["prod_code"].FormattedValue.ToString();
                    strBool = dataGridView1.Rows[i].Cells["zprod_att_fda_qty"].FormattedValue.ToString();
                    insetRow = DB.ExecuteFromSql(string.Format(sql_update, typeTemp.ToUpper(),strBool, proCode)) == true ? insetRow + 1 : insetRow;
                }
                //MessageBox.Show(insetRow.ToString() + " 行记录,保存成功!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MessageBox.Show("保存成功!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCode.Text = "";
                txtCode.Focus();
                boolEdit = false;
            }
        }

        private void Fm_FDA_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (boolEdit)
            {
                if (MessageBox.Show("修改还未保存，你确定退出吗?", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void Fm_FDA_KeyDown(object sender, KeyEventArgs e)
        {     
            if (e.KeyCode == Keys.S && e.Control)
            {
                txtCode.Focus();
                if (dataGridView1.RowCount < 1)
                {
                    return;
                }
                e.Handled = true;   //将Handled设置为true，指示已经处理过KeyPress事件
                //////////////////////


                string sql_update = "update Product set ZPROD_FDAM_CODE = '{0}',zprod_att_fda_qty='{1}' where prod_code ='{2}'";
                string typeTemp = string.Empty;
                string strBool = string.Empty;
                string proCode = string.Empty;
                int insetRow = 0;
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    typeTemp = dataGridView1.Rows[i].Cells["ZPROD_FDAM_CODE"].FormattedValue.ToString().Trim();
                    proCode = dataGridView1.Rows[i].Cells["prod_code"].FormattedValue.ToString();
                    strBool = dataGridView1.Rows[i].Cells["zprod_att_fda_qty"].FormattedValue.ToString();
                    insetRow = DB.ExecuteFromSql(string.Format(sql_update, typeTemp.ToUpper(), strBool, proCode)) == true ? insetRow + 1 : insetRow;
                }
                //MessageBox.Show(insetRow.ToString() + " 行记录,保存成功!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MessageBox.Show("保存成功!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCode.Text = "";
                txtCode.Focus();
                boolEdit = false;   
            }
            if (e.KeyCode == Keys.Home)
            {
                txtCode.Focus();
                txtCode.SelectAll();
            }
            if (e.KeyCode == Keys.Escape)
            {
                txtCode.Focus();
                txtCode.SelectAll();
            }

        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Home)
            {
                txtCode.Focus();
                txtCode.SelectAll();

            }
        }


    }
}
