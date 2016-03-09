using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Drawing;

namespace MDL_CRM
{
    public partial class Fm_Charge
    {
        /// <summary>
        /// 收费明细是否可编辑
        /// </summary>
        /// <param name="blnEnable"></param>
        private void enableGrid(bool blnEnable)
        {
            RightMenu.Enabled = blnEnable;
            btnSave.Enabled = blnEnable;

            dataGrid.Columns["colMaterial"].Visible = blnEnable;
            //dataGrid.Columns["SCHG_CHARGE_YN"].Visible = blnEnable;
            dataGrid.Columns["SCHG_GROUP_ID"].ReadOnly = !blnEnable;
            dataGrid.Columns["SCHG_PRODCODE"].ReadOnly = !blnEnable;
            dataGrid.Columns["SCHG_QTY"].ReadOnly = !blnEnable;
            dataGrid.Columns["SCHG_FDA_QTY"].ReadOnly = !blnEnable;
            dataGrid.Columns["SCHG_TOOTHPOS"].ReadOnly = !blnEnable;
            dataGrid.Columns["SCHG_TOOTHCOLOR"].ReadOnly = !blnEnable;
            dataGrid.Columns["SCHG_BATCHNO"].ReadOnly = !blnEnable;
            dataGrid.Columns["SCHG_REMARK"].ReadOnly = !blnEnable;
            if (blnEnable)
            {
                dataGrid.Columns["SCHG_GROUP_ID"].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 255);
                dataGrid.Columns["SCHG_PRODCODE"].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 255);
                dataGrid.Columns["SCHG_QTY"].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 255);
                dataGrid.Columns["SCHG_FDA_QTY"].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 255);
                dataGrid.Columns["SCHG_TOOTHPOS"].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 255);
                dataGrid.Columns["SCHG_TOOTHCOLOR"].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 255);
                dataGrid.Columns["SCHG_BATCHNO"].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 255);
                dataGrid.Columns["SCHG_REMARK"].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 255);
            }
            else
            {
                dataGrid.Columns["SCHG_GROUP_ID"].DefaultCellStyle.BackColor = Color.White;
                dataGrid.Columns["SCHG_PRODCODE"].DefaultCellStyle.BackColor = Color.White;
                dataGrid.Columns["SCHG_QTY"].DefaultCellStyle.BackColor = Color.White;
                dataGrid.Columns["SCHG_FDA_QTY"].DefaultCellStyle.BackColor = Color.White;
                dataGrid.Columns["SCHG_TOOTHPOS"].DefaultCellStyle.BackColor = Color.White;
                dataGrid.Columns["SCHG_TOOTHCOLOR"].DefaultCellStyle.BackColor = Color.White;
                dataGrid.Columns["SCHG_BATCHNO"].DefaultCellStyle.BackColor = Color.White;
                dataGrid.Columns["SCHG_REMARK"].DefaultCellStyle.BackColor = Color.White;
            }
        }

        private void loadgridValue(DataTable dt, int row)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                dataGrid.Rows[row].Cells["SCHG_PRODCODE"].Value = dt.Rows[0]["PROD_CODE"].ToString();
                dataGrid.Rows[row].Cells["PROD_DESC"].Value = dt.Rows[0]["PROD_DESC"].ToString();
                dataGrid.Rows[row].Cells["PROD_DESC_CHI"].Value = dt.Rows[0]["PROD_DESC_CHI"].ToString();
                dataGrid.Rows[row].Cells["SCHG_PRO_MAT"].Value = dt.Rows[0]["PROD_PRO_MAT"].ToString();
                dataGrid.Rows[row].Cells["SCHG_UNIT"].Value = dt.Rows[0]["PROD_UNIT"].ToString();
                dataGrid.Rows[row].Cells["SCHG_OTHER_NAME"].Value = dt.Rows[0]["PROD_OTHER_NAME"].ToString();
                // dataGrid.Rows[row].Cells["SCHG_FDA_CODE"].Value = dt.Rows[0]["ZPROD_FDAM_CODE"].ToString();
            }
            else
            {
                dataGrid.Rows[row].Cells["SCHG_PRODCODE"].Value = "";
                dataGrid.Rows[row].Cells["PROD_DESC"].Value = "";
                dataGrid.Rows[row].Cells["PROD_DESC_CHI"].Value = "";
                dataGrid.Rows[row].Cells["SCHG_PRO_MAT"].Value = "";
                dataGrid.Rows[row].Cells["SCHG_UNIT"].Value = "";
                dataGrid.Rows[row].Cells["SCHG_OTHER_NAME"].Value = "";
                // dataGrid.Rows[row].Cells["SCHG_FDA_CODE"].Value = "";
            }
            dt.Dispose();
        }

        private void GridRowDel()
        {
            if (ActiveControl.Name == dataGrid.Name)
            {
                if (dataGrid.Rows.Count == 1) { return; }
                if (MessageBox.Show("确定要删除这一行资料?", "MDL-提示", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    lstCharge.RemoveAt(dataGrid.CurrentCell.RowIndex);
                    dataGrid.DataSource = null;
                    dataGrid.DataSource = lstCharge;
                }
            }
        }

        private void GridRowCopy()//Copy
        {
            if (ActiveControl.Name == dataGrid.Name)
            {
                if (dataGrid.CurrentCell != null)
                {
                    OriDr = lstCharge[dataGrid.CurrentCell.RowIndex].Copy();
                }
            }
        }

        private void GridRowPaste()
        {
            if (ActiveControl.Name == dataGrid.Name)
            {
                if (OriDr != null)
                {
                    OriDr.SCHG_PRODCODE = string.Empty;
                    OriDr.SCHG_CHARGE_YN = 1;
                    OriDr.SCHG_CHARGE_DESC = "正常";
                    OriDr.SCHG_DISCOUNT = nudSO_Discount.Value;
                    lstCharge.Add(OriDr);
                    dataGrid.DataSource = null;
                    dataGrid.DataSource = lstCharge;
                    OriDr = null;
                }
            }
        }

        private bool chkData()
        {
            bool blnok = false;
            //明细检查
            string salter = "";
            if (lstCharge == null || lstCharge.Count <= 0)
            {
                salter = "必须有一条明细记录\r\n";
            }
            if (lstCharge.Where(jpv => jpv.SCHG_QTY == 0).Count() >= 1)
            {
                salter = salter + "明细资料有数量为零的记录\r\n";
            }
            if (lstCharge.Where(jpv => jpv.SCHG_CHARGE_YN.IsNullOrEmpty()).Count() >= 1)
            {
                salter = salter + "明细资料有收费项目为空的记录";
            }
            if (lstCharge.Where(jpv => jpv.SCHG_PRODCODE.IsNullOrEmpty()).Count() >= 1)
            {
                salter = salter + "明细资料有手工材料编号为空的记录";
            }
            for (int i = 0; i < lstCharge.Count; i++)
            {
                if (!lstCharge[i].SCHG_PRODCODE.IsNullOrEmpty())
                {
                    if (lstCharge.Where(jpv => jpv.SCHG_PRODCODE == lstCharge[i].SCHG_PRODCODE).Count() > 1)
                    {
                        salter = salter + "明细资料中手工材料编号[" + lstCharge[i].SCHG_PRODCODE + "]有重复存在";
                        break;
                    }
                }

            }
            //主表的检查
            if (salter != "")
            {
                txtError.Text = salter;
                blnok = true;
            }
            return blnok;
        }

        private bool blnOpenForm(Form objForm, out Form OpenForm)
        {
            bool blnExist = false;
            Form[] frm;
            frm = this.MdiParent.MdiChildren;
            OpenForm = null;
            for (int i = 0; i < frm.Length; i++)
            {
                if (frm[i].Name == objForm.Name)
                {
                    OpenForm = frm[i];
                    blnExist = true;
                    break;
                }
            }
            return blnExist;
        }

        private void saveData(string pJobNo, out string pError)
        {
            //校验工作单明细数据
            if (chkData())
            {
                pError = "收费明细校验失败";
                return;
            }

            //保存收费明细
            crHelper.saveCharge(lstCharge,pJobNo, out pError);
        }
    }
}
