using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.IO;
using CaseInquire.helperclass;
using System.Text.RegularExpressions;

namespace CaseInquire
{
    /*
     * 问单报表--自定义方法分部类
     */
    partial class Fm_BusinessReport
    {
        DataTable dtAttachment = null;
        /// <summary>
        /// 中文问单浏览
        /// </summary>
        /// <param name="pDt">模板数据</param>
        /// <param name="pnl">面板</param>
        private void TemplatePreviewForChd(DataTable pDt, Panel pnl)
        {
            //先释放控件资源
            PublicMethod.DisposeControl(pnl);
            //pnl.Controls.Clear();
            if (pDt == null || pDt.Rows.Count <= 0)
            {
                return;
            }
            //按照类目进行分组
            var groups = from m in pDt.AsEnumerable()
                         group m by new { item_category = m.Field<string>("item_category"), icat_desc = m.Field<string>("icat_desc") } into result
                         select result;

            //根据分组添加总体布局
            TableLayoutPanel tlp = new TableLayoutPanel();
            tlp.ColumnCount = 3;
            tlp.RowCount = groups.Count() + 2;
            tlp.Dock = DockStyle.Fill;
            tlp.AutoSize = true;
            tlp.AutoScroll = true;
            tlp.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
            tlp.Show();
            //pnl.Controls.Add(tlp);

            #region
            int topPoint = 0, leftPoint = 0, groupRowIndex = 0, childGroupRowIndex = 0;
            foreach (var group in groups)
            {
                //添加类目
                Label lblCategory = new Label();
                lblCategory.Text = group.Key.icat_desc;
                lblCategory.Font = new System.Drawing.Font("宋体", 10f, FontStyle.Bold);
                lblCategory.AutoSize = true;
                lblCategory.Dock = DockStyle.Fill;
                lblCategory.TextAlign = ContentAlignment.MiddleRight;
                lblCategory.Show();
                tlp.SetColumn(lblCategory, 0);
                tlp.SetRow(lblCategory, groupRowIndex);
                tlp.Controls.Add(lblCategory);

                //每个类目对应的明细先添加一个Panel
                Panel cPnl = new Panel();
                cPnl.Name = group.Key.item_category;
                cPnl.AutoSize = true;
                cPnl.BorderStyle = BorderStyle.FixedSingle;
                cPnl.Dock = DockStyle.Fill;
                cPnl.Show();
                tlp.SetColumn(cPnl, 1);
                tlp.SetRow(cPnl, groupRowIndex);
                tlp.Controls.Add(cPnl);

                //再根据每个类目添加子布局
                TableLayoutPanel tlpChild = new TableLayoutPanel();
                tlpChild.Name = group.Key.item_category;
                tlpChild.ColumnCount = 1;
                tlpChild.RowCount = group.Count();
                tlpChild.AutoSize = true;
                tlpChild.Dock = DockStyle.Fill;
                tlpChild.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
                tlpChild.Show();
                //tlp.SetColumn(tlpChild, 1);
                //tlp.SetRow(tlpChild, groupRowIndex);
                //tlp.Controls.Add(tlpChild);
                cPnl.Controls.Add(tlpChild);

                childGroupRowIndex = 0;

                #region
                //添加内容
                foreach (DataRow item in group)
                {
                    //针对内容的每一行先添加一个Panel
                    Panel childPnl = new Panel();
                    childPnl.Name = group.Key.item_category + item["item_id"].ToString();
                    childPnl.AutoSize = true;
                    childPnl.BorderStyle = BorderStyle.None;
                    childPnl.Dock = DockStyle.Fill;
                    childPnl.Show();
                    tlpChild.SetColumn(childPnl, 0);
                    tlpChild.SetRow(childPnl, childGroupRowIndex);
                    tlpChild.Controls.Add(childPnl);

                    topPoint = 0;
                    leftPoint = 8;
                    if (item["item_parameter_count"].ToString().Equals("0"))
                    {
                        #region
                        //当行没有参数时就为纯文本
                        Label lblItem = new Label();
                        lblItem.Text = item["item_content"].ToString().Replace("@", "");
                        lblItem.AutoSize = true;
                        lblItem.Left = leftPoint;
                        lblItem.Top = topPoint;

                        if (item["item_category"].ToString().Equals("title"))
                        {
                            lblItem.Text = lblItem.Text + "    【" + item["ctrnm_cs_process_type"].ToString() + "】";
                            lblItem.Font = new Font("宋体", 14f, FontStyle.Bold);
                            //lblItem.Left = childPnl.Width / 3;
                        }

                        lblItem.Show();
                        childPnl.Controls.Add(lblItem);
                        topPoint += lblItem.Height + 5;
                        #endregion
                    }
                    else
                    {
                        #region
                        //当行存在参数时
                        int paraIndex = 0;
                        string regexStr = @"\{\d{0,}\}";//例如{0}、{1}

                        //拆分参数类型
                        string[] paraTypeArray = item["item_parameters_type"].ToString().Trim().Split(',');
                        //拆分模板内容
                        string[] contentArray = item["item_content"].ToString().Trim().Split('#');
                        //拆分参数值
                        string[] strValue = item["ctrnd_parameters_value"].ToString().Trim().Split('@');
                        //拆分参数牙位
                        string[] paraYaweiArray = item["item_parameters_is_yawei"].ToString().Trim().Split(',');

                        for (int i = 0; i < contentArray.Length; i++)
                        {
                            //为空则忽略
                            if (string.IsNullOrEmpty(contentArray[i]))
                            {
                                continue;
                            }

                            if (Regex.IsMatch(contentArray[i], regexStr)) //满足参数占位格式
                            {
                                #region
                                string regexStr2 = @"(\d+)";
                                paraIndex = Convert.ToInt32(Regex.Match(contentArray[i], regexStr2).Value);

                                //利用反射，可根据控件类型字符串来创建对应控件
                                string assemblyQualifiedName = typeof(System.Windows.Forms.Form).AssemblyQualifiedName;
                                string assemblyInformation = assemblyQualifiedName.Substring(assemblyQualifiedName.IndexOf(","));
                                Type ty = Type.GetType("System.Windows.Forms." + paraTypeArray[paraIndex] + assemblyInformation);
                                Control newControl = (Control)System.Activator.CreateInstance(ty);
                                newControl.Left = leftPoint;
                                newControl.Top = topPoint + 1;

                                if (ty.Name.Equals("CheckBox"))//当为复选框时特殊处理
                                {
                                    CheckBox cb = new CheckBox();
                                    cb.AutoSize = true;
                                    cb.Left = leftPoint;
                                    cb.Top = topPoint + 3;
                                    cb.Checked = bool.Parse(strValue[paraIndex]);
                                    newControl = cb;
                                }
                                else if (ty.Name.Equals("RichTextBox"))//当为备注框时特殊处理
                                {
                                    RichTextBox rtb = new RichTextBox();
                                    rtb.Height = 40;
                                    rtb.Width = 600;
                                    rtb.Left = leftPoint;
                                    rtb.Top = topPoint + 1;
                                    rtb.Text = strValue[paraIndex];
                                    newControl = rtb;
                                }
                                else if (ty.Name.Equals("DateTimePicker")) //当为日期控件时特殊处理
                                {
                                    DateTimePicker dtp = new DateTimePicker();
                                    dtp.Width = 121;
                                    dtp.Left = leftPoint;
                                    dtp.Top = topPoint + 3;
                                    dtp.Format = DateTimePickerFormat.Short;
                                    if ((i - 1) >= 0)
                                    {
                                        if (contentArray[i - 1].Trim().StartsWith("时间"))
                                        {
                                            dtp.Width = 92;
                                            dtp.Format = DateTimePickerFormat.Time;
                                        }
                                    }
                                    dtp.Value = DateTime.Parse(strValue[paraIndex]);
                                    newControl = dtp;
                                }
                                else if (ty.Name.Equals("RadioButton")) //当为单选框时特殊处理
                                {
                                    RadioButton rb = new RadioButton();
                                    rb.Left = leftPoint;
                                    rb.Top = topPoint + 3;
                                    rb.Checked = bool.Parse(strValue[paraIndex]);
                                    newControl = rb;
                                }
                                else
                                {
                                    newControl.Text = strValue[paraIndex];
                                    try
                                    {
                                        if (paraYaweiArray[paraIndex].Equals("1"))
                                        {
                                            newControl.BackColor = Color.PeachPuff;
                                        }
                                    }
                                    catch (Exception) { }
                                }

                                newControl.Show();
                                newControl.Enabled = false;
                                childPnl.Controls.Add(newControl);
                                leftPoint += newControl.Width + 3;
                                //paraIndex++;
                                #endregion
                            }
                            else //为文本
                            {
                                #region
                                Label lblItem = new Label();
                                lblItem.Text = contentArray[i].Replace("@", "");
                                lblItem.AutoSize = true;
                                lblItem.Left = leftPoint;
                                lblItem.Top = topPoint + 5;
                                lblItem.Show();
                                childPnl.Controls.Add(lblItem);
                                leftPoint += lblItem.Width + 3;
                                #endregion
                            }
                        }
                        topPoint += 25;
                        #endregion
                    }
                    childGroupRowIndex++;
                    //tlpChild.Controls.Add(childPnl);
                }
                #endregion

                groupRowIndex++;

                //cPnl.Controls.Add(tlpChild);
                //tlp.Controls.Add(cPnl);
            }
            #endregion

            Panel lPnl = new Panel();
            lPnl.AutoSize = true;
            lPnl.BorderStyle = BorderStyle.FixedSingle;
            lPnl.Dock = DockStyle.Fill;
            lPnl.Show();
            tlp.SetColumn(lPnl, 2);
            tlp.SetRow(lPnl, 1);
            tlp.SetRowSpan(lPnl, groups.Count() - 1);

            leftPoint = 0;
            topPoint = 0;

            #region 显示问单历史记录
            DataTable tmpDt = GetCaseRecords(dgvCaseList.SelectedRows[0].Cells["ctrnm_id"].Value.ToString());
            if (null != tmpDt && tmpDt.Rows.Count > 0)
            {
                Panel iPnl = new Panel();
                iPnl.AutoSize = true;
                iPnl.BorderStyle = BorderStyle.None;
                iPnl.Dock = DockStyle.Top;
                iPnl.Show();
                iPnl.Left = leftPoint;
                iPnl.Top = topPoint;

                foreach (DataRow item in tmpDt.Rows)
                {
                    if (item[0] is DBNull)
                    {
                        continue;
                    }
                    StringBuilder sb = new StringBuilder();
                    sb.Append(item[0].ToString());
                    sb.Append(">>>>");
                    sb.Append(item[1] is DBNull ? "" : item[1].ToString());
                    sb.Append(">>>>");
                    sb.Append(item[2].ToString());
                    if (item[2].ToString().Contains("生产"))
                    {
                        StringBuilder sbb = new StringBuilder();
                        sbb.Append("检查：【");
                        sbb.Append(dgvCaseList.SelectedRows[0].Cells["ctrnm_who_checked"].Value.ToString());
                        sbb.Append("】确认：【");
                        sbb.Append(dgvCaseList.SelectedRows[0].Cells["ctrnm_who_qa"].Value.ToString());
                        sbb.Append("】");
                        sb.AppendLine();
                        sb.AppendLine(sbb.ToString());
                    }
                    else if (item[2].ToString().Contains("回复"))
                    {
                        sb.AppendLine();
                        sb.AppendLine(dgvCaseList.SelectedRows[0].Cells["ctrnm_reply_content"].Value.ToString());
                    }

                    Label lblItem = new Label();
                    lblItem.Text = sb.ToString();
                    lblItem.AutoSize = true;
                    lblItem.Left = leftPoint;
                    lblItem.Top = topPoint + 5;
                    lblItem.Show();
                    iPnl.Controls.Add(lblItem);
                    topPoint += 25;
                }
                lPnl.Controls.Add(iPnl);
            }
            #endregion 显示问单历史记录

            #region 显示附件
            dtAttachment = null;
            dtAttachment = GetCaseAttachment(dgvCaseList.SelectedRows[0].Cells["ctrnm_id"].Value.ToString());
            if (null != dtAttachment && dtAttachment.Rows.Count > 0)
            {
                ListView lv = new ListView();
                lv.BackColor = SystemColors.AppWorkspace;
                lv.View = View.Details;
                lv.MultiSelect = false;
                lv.Scrollable = false;
                lv.Dock = DockStyle.Bottom;
                lv.Activation = ItemActivation.OneClick;
                lv.Columns.Add(new ColumnHeader()
                {
                    Name = "attachments",
                    Text = "附件",
                    Width = 600
                });
                lv.ItemActivate += new EventHandler(lv_ItemActivate);
                foreach (DataRow item in dtAttachment.Rows)
                {
                    ListViewItem lvi = new ListViewItem(Path.GetFileName(item[1].ToString()));
                    //lvi.SubItems.Add(item[0].ToString());
                    lvi.Name = PublicClass.FileServerPathBase + item[1].ToString();
                    lv.Items.Add(lvi);
                }
                lv.Left = leftPoint;
                lv.Top = topPoint;
                lv.Show();
                lPnl.Controls.Add(lv);
            }
            #endregion 显示附件

            tlp.Controls.Add(lPnl);

            //显示转医生的详细信息
            Panel bPnl = new Panel();
            bPnl.AutoSize = true;
            bPnl.BorderStyle = BorderStyle.FixedSingle;
            bPnl.Dock = DockStyle.Fill;
            bPnl.Show();
            tlp.SetColumn(bPnl, 1);
            tlp.SetRow(bPnl, groups.Count());

            Label lblSupport2 = new Label();
            StringBuilder sbSupport2 = new StringBuilder(dgvCaseList.SelectedRows[0].Cells["ctrnm_post_support2_content"].Value.ToString());
            lblSupport2.Text=sbSupport2.ToString();
            if (lblSupport2.Text.Trim().Length > 0)
            {
                lblSupport2.AutoSize = true;
                lblSupport2.Left = 0;
                lblSupport2.Top = 5;
                lblSupport2.Show();
                bPnl.Controls.Add(lblSupport2);

                tlp.Controls.Add(bPnl);
            }

            pnl.Controls.Add(tlp);
        }

        /// <summary>
        /// 根据选择的问单带出明细
        /// </summary>
        /// <param name="pFormId">问单类型ID</param>
        /// <param name="pCtrnmId">业务问单头ID</param>
        private void ShowCaseInfo(string pFormId, string pCtrnmId)
        {
            DataTable dt = null;
            try
            {
                //先获得问单明细
                dt = GetCaseDetail(pFormId, pCtrnmId);

                //根据问单明细显示出来
                TemplatePreviewForChd(dt, pnlCha);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dt = null;
            }
        }

        void lv_ItemActivate(object sender, EventArgs e)
        {
            try
            {
                ListView lv = sender as ListView;
                if (lv.Items.Count <= 0)
                {
                    return;
                }
                //System.Diagnostics.Process.Start("Explorer.exe", lv.SelectedItems[0].Name);
                tabControl1.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    
    }
}
