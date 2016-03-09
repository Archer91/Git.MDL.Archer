using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using System.Text.RegularExpressions;
using CaseInquire.helperclass;
using System.Globalization;
using System.Threading;

namespace CaseInquire
{
    /*
     * 问单回复--自定义方法分部类
     */
    partial class Fm_BusinessReply
    {
        /// <summary>
        /// 显示货类
        /// </summary>
        /// <param name="pDt">货类数据源</param>
        /// <param name="pLv">显示控件ListView</param>
        private void ShowMgrpCode(DataTable pDt, ListView pLv)
        {
            if (null == pDt || pDt.Rows.Count <= 0)
            {
                return;
            }
            foreach (DataRow item in pDt.Rows)
            {
                ListViewItem lvi = new ListViewItem(item[0].ToString());
                lvi.SubItems.Add(item[1].ToString());
                lvi.Name = item[0].ToString();

                pLv.Items.Add(lvi);
            }
            pLv.Columns[1].ImageKey = imageList1.Images.Keys[0];
        }

        DataTable dt = null;
        DataTable dt2 = null;
        /// <summary>
        /// 根据选择的公司条码带出明细
        /// </summary>
        /// <param name="pJobNo">公司条码</param>
        /// <param name="pIsSubmit">是否转医生</param>
        /// <param name="pPnlCha">中文面板</param>
        /// <param name="pPnlEng">英文面板</param>
        private void ShowCaseInfo(string pJobNo, bool pIsSubmit, Panel pPnlCha, Panel pPnlEng)
        {
            if (pIsSubmit)
            {
                dt2 = GetCaseDetail(pJobNo, pIsSubmit);
                if (chkShow2.Checked)
                {
                    TemplatePreviewForChd(dt2, pPnlCha);
                    TemplatePreviewForEng(dt2, pPnlEng);
                }
                else
                {
                    TemplatePreviewForChdAll(dt2, pPnlCha);
                    TemplatePreviewForEngAll(dt2, pPnlEng);
                }
            }
            else
            {
                dt = GetCaseDetail(pJobNo, pIsSubmit);
                if (chkShow.Checked)
                {
                    TemplatePreviewForChd(dt, pPnlCha);
                    TemplatePreviewForEng(dt, pPnlEng);
                }
                else
                {
                    TemplatePreviewForChdAll(dt, pPnlCha);
                    TemplatePreviewForEngAll(dt, pPnlEng);
                }
            }
        }

        /// <summary>
        /// 中文问单浏览(简易模式）
        /// </summary>
        /// <param name="pDt">模板数据</param>
        /// <param name="pnl">面板</param>
        private void TemplatePreviewForChd(DataTable pDt, Panel pnl)
        {
            //释放控件资源
            PublicMethod.DisposeControl(pnl);
            //pnl.Controls.Clear();
            if (pDt == null || pDt.Rows.Count <= 0)
            {
                return;
            }

            var batches = from m in pDt.AsEnumerable()
                          group m by m.Field<string>("ctrnm_batchno") into result
                          select result;
            //添加类型分组总体布局
            TableLayoutPanel ftlp = new TableLayoutPanel();
            ftlp.ColumnCount = 1;
            //ftlp.RowCount = all.Count()+1;
            ftlp.Dock = DockStyle.Fill;
            ftlp.AutoSize = true;
            ftlp.AutoScroll = true;
            ftlp.CellBorderStyle = TableLayoutPanelCellBorderStyle.OutsetDouble;
            ftlp.Show();
            int fRowIndex = 0;
            foreach (var batch in batches)
            {
                #region

                //按照问单类型进行总体分组
                var all = from m in batch
                          group m by new { form_id = m.Field<string>("ctrnm_form_id") } into result
                          select result;

                //添加类型分组总体布局
                //TableLayoutPanel ftlp = new TableLayoutPanel();
                //ftlp.ColumnCount = 1;
                //ftlp.RowCount = all.Count() +1;
                //ftlp.Dock = DockStyle.Fill;
                //ftlp.AutoSize = true;
                //ftlp.AutoScroll = true;
                //ftlp.CellBorderStyle = TableLayoutPanelCellBorderStyle.OutsetDouble;
                //ftlp.Show();
                ftlp.RowCount += all.Count();
                //int fRowIndex = 0;
                foreach (var f in all)
                {
                    #region 每个类型操作

                    //按照类目进行分组
                    var groups = from m in f
                                 group m by new { item_category = m.Field<string>("item_category"), icat_desc = m.Field<string>("icat_desc") } into result
                                 select result;

                    //根据分组添加总体布局
                    TableLayoutPanel tlp = new TableLayoutPanel();
                    tlp.ColumnCount = 2;
                    tlp.RowCount = groups.Count();
                    tlp.Dock = DockStyle.Fill;
                    tlp.AutoSize = true;
                    tlp.AutoScroll = true;
                    tlp.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
                    tlp.Show();
                    ftlp.SetColumn(tlp, 0);
                    ftlp.SetRow(tlp, fRowIndex);

                    int topPoint = 0, leftPoint = 0, groupRowIndex = 0, childGroupRowIndex = 0;
                    foreach (var group in groups)
                    {
                        #region
                        if (group.Key.item_category.Equals("header") || group.Key.item_category.Equals("footer"))
                        {
                            continue;
                        }
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

                        //添加内容
                        foreach (DataRow item in group)
                        {
                            #region
                            //针对内容的每一行先添加一个Panel
                            Panel childPnl = new Panel();
                            childPnl.Name = group.Key.item_category + item["item_id"].ToString();
                            childPnl.AutoSize = true;
                            childPnl.BorderStyle = BorderStyle.None;
                            childPnl.Dock = DockStyle.Fill;
                            childPnl.Show();
                            tlpChild.SetColumn(childPnl, 0);
                            tlpChild.SetRow(childPnl, childGroupRowIndex);
                            //tlpChild.Controls.Add(childPnl);

                            topPoint = 0;
                            leftPoint = 8;
                            if (item["item_parameter_count"].ToString().Equals("0"))
                            {
                                #region 当行没有参数时就为纯文本
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
                                tlpChild.Controls.Add(childPnl);
                                #endregion 当行没有参数时就为纯文本
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

                                bool isAdd = false;//整行有控件填写信息的标记
                                bool isRow = false;//该行每个控件是否填写信息的标记
                                for (int i = 0; i < contentArray.Length; i++)
                                {
                                    //为空则忽略
                                    if (string.IsNullOrEmpty(contentArray[i]))
                                    {
                                        continue;
                                    }

                                    if (Regex.IsMatch(contentArray[i], regexStr)) //满足参数占位格式
                                    {
                                        isRow = false;
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
                                            #region
                                            if (!item["item_category"].ToString().Equals("header") && !item["item_category"].ToString().Equals("footer") && !bool.Parse(strValue[paraIndex]))
                                            {
                                                //i++;
                                                //paraIndex++;
                                                continue;
                                            }
                                            CheckBox cb = new CheckBox();
                                            cb.AutoSize = true;
                                            cb.Left = leftPoint;
                                            cb.Top = topPoint + 3;
                                            cb.Checked = bool.Parse(strValue[paraIndex]);
                                            newControl = cb;
                                            #endregion
                                        }
                                        else if (ty.Name.Equals("RichTextBox"))//当为备注框时特殊处理
                                        {
                                            #region
                                            if (!item["item_category"].ToString().Equals("header") && !item["item_category"].ToString().Equals("footer") && string.IsNullOrEmpty(strValue[paraIndex].Trim()))
                                            {
                                                //i++;
                                                //paraIndex++;
                                                continue;
                                            }
                                            if (item["item_category"].ToString().Equals("header") || item["item_category"].ToString().Equals("footer"))
                                            {
                                                Label lblItem = new Label();
                                                lblItem.Text = strValue[paraIndex];
                                                lblItem.AutoSize = true;
                                                lblItem.Left = leftPoint;
                                                lblItem.Top = topPoint + 5;
                                                newControl = lblItem;
                                            }
                                            else
                                            {
                                                RichTextBox rtb = new RichTextBox();
                                                rtb.Height = 40;
                                                rtb.Width = 600;
                                                rtb.Left = leftPoint;
                                                rtb.Top = topPoint + 1;
                                                rtb.Text = strValue[paraIndex];
                                                newControl = rtb;
                                            }
                                            #endregion
                                        }
                                        else if (ty.Name.Equals("DateTimePicker")) //当为日期控件时特殊处理
                                        {
                                            #region
                                            if (item["item_category"].ToString().Equals("header") || item["item_category"].ToString().Equals("footer"))
                                            {
                                                Label lblItem = new Label();
                                                lblItem.Text = DateTime.Parse(strValue[paraIndex]).ToShortDateString();
                                                lblItem.AutoSize = true;
                                                lblItem.Left = leftPoint;
                                                lblItem.Top = topPoint + 5;
                                                if ((i - 1) >= 0)
                                                {
                                                    if (contentArray[i - 1].ToUpper().Trim().StartsWith("TIME"))
                                                    {
                                                        lblItem.Text = DateTime.Parse(strValue[paraIndex]).ToLongTimeString();
                                                    }
                                                }
                                                newControl = lblItem;
                                            }
                                            else
                                            {
                                                DateTimePicker dtp = new DateTimePicker();
                                                dtp.Width = 121;
                                                dtp.Left = leftPoint;
                                                dtp.Top = topPoint + 3;
                                                dtp.Format = DateTimePickerFormat.Short;
                                                if ((i - 1) >= 0)
                                                {
                                                    if (contentArray[i - 1].ToUpper().Trim().StartsWith("TIME"))
                                                    {
                                                        dtp.Width = 92;
                                                        dtp.Format = DateTimePickerFormat.Time;
                                                    }
                                                }
                                                dtp.Value = DateTime.Parse(strValue[paraIndex]);
                                                newControl = dtp;
                                            }
                                            #endregion
                                        }
                                        else if (ty.Name.Equals("RadioButton")) //当为单选框时特殊处理
                                        {
                                            #region
                                            RadioButton rb = new RadioButton();
                                            rb.Left = leftPoint;
                                            rb.Top = topPoint + 3;
                                            rb.Checked = bool.Parse(strValue[paraIndex]);
                                            newControl = rb;
                                            #endregion
                                        }
                                        else if (ty.Name.Equals("TextBox"))
                                        {
                                            #region
                                            if (!item["item_category"].ToString().Equals("header") && !item["item_category"].ToString().Equals("footer") && string.IsNullOrEmpty(strValue[paraIndex].Trim()))
                                            {
                                                //i++;
                                                //paraIndex++;
                                                continue;
                                            }

                                            if (item["item_category"].ToString().Equals("header") || item["item_category"].ToString().Equals("footer"))
                                            {
                                                Label lblItem = new Label();
                                                lblItem.Text = strValue[paraIndex];
                                                lblItem.AutoSize = true;
                                                lblItem.Left = leftPoint;
                                                lblItem.Top = topPoint + 5;
                                                newControl = lblItem;
                                            }
                                            else
                                            {
                                                TextBox tb = new TextBox();
                                                tb.Left = leftPoint;
                                                tb.Top = topPoint;
                                                tb.Text = strValue[paraIndex];
                                                try
                                                {
                                                    if (paraYaweiArray[paraIndex].Equals("1"))
                                                    {
                                                        tb.BackColor = Color.PeachPuff;
                                                    }
                                                }
                                                catch (Exception) { }
                                                newControl = tb;
                                            }
                                            #endregion
                                        }
                                        else
                                        {
                                            newControl.Text = strValue[paraIndex];
                                        }

                                        newControl.Enabled = false;
                                        newControl.Show();
                                        childPnl.Controls.Add(newControl);
                                        leftPoint += newControl.Width + 3;
                                        //paraIndex++;
                                        isRow = true;
                                        isAdd = true;
                                        #endregion
                                    }
                                    else //为文本
                                    {
                                        #region
                                        string[] tmpLable = contentArray[i].Split('@');
                                        if (string.IsNullOrEmpty(tmpLable[0]))
                                        {
                                            //最有可能的是每一行的开头分隔符（这样可以保证在没有勾选或填写第一个控件时，后面的文本能显示）
                                            Label lblItem = new Label();
                                            lblItem.Text = contentArray[i].Replace("@", "");
                                            lblItem.AutoSize = true;
                                            lblItem.Left = leftPoint;
                                            lblItem.Top = topPoint + 5;
                                            lblItem.Show();
                                            childPnl.Controls.Add(lblItem);
                                            leftPoint += lblItem.Width + 3;
                                        }
                                        else
                                        {
                                            if (isRow)//若该文本前的控件有输入信息，则控件后的文本都显示
                                            {
                                                Label lblItem = new Label();
                                                lblItem.Text = contentArray[i].Replace("@", "");
                                                lblItem.AutoSize = true;
                                                lblItem.Left = leftPoint;
                                                lblItem.Top = topPoint + 5;
                                                lblItem.Show();
                                                childPnl.Controls.Add(lblItem);
                                                leftPoint += lblItem.Width + 3;
                                            }
                                            else//若该文本前的控件没有输入信息，则控件后的第一个文本不显示
                                            {
                                                if (tmpLable.Length > 1)
                                                {
                                                    Label lblItem = new Label();
                                                    lblItem.Text = contentArray[i].Replace("@", "").Remove(0, tmpLable[0].Length);
                                                    lblItem.AutoSize = true;
                                                    lblItem.Left = leftPoint;
                                                    lblItem.Top = topPoint + 5;
                                                    lblItem.Show();
                                                    childPnl.Controls.Add(lblItem);
                                                    leftPoint += lblItem.Width + 3;
                                                }
                                            }
                                        }
                                        #endregion
                                    }
                                }
                                if (isAdd)
                                {
                                    topPoint += 25;
                                    tlpChild.Controls.Add(childPnl);
                                }
                                #endregion
                            }
                            childGroupRowIndex++;
                            #endregion
                        }
                        groupRowIndex++;
                        #endregion
                    }
                    fRowIndex++;
                    ftlp.Controls.Add(tlp);

                    #endregion 每个类型操作
                }
                //pnl.Controls.Add(ftlp);

                #endregion
            }
            ftlp.RowCount += 1;
            pnl.Controls.Add(ftlp);
        }

        /// <summary>
        /// 中文问单浏览(完全模式）
        /// </summary>
        /// <param name="pDt">模板数据</param>
        /// <param name="pnl">面板</param>
        private void TemplatePreviewForChdAll(DataTable pDt, Panel pnl)
        {
            //释放控件资源
            PublicMethod.DisposeControl(pnl);
            //pnl.Controls.Clear();
            if (pDt == null || pDt.Rows.Count <= 0)
            {
                return;
            }
            var batches = from m in pDt.AsEnumerable()
                          group m by m.Field<string>("ctrnm_batchno") into result
                          select result;
            TableLayoutPanel ftlp = new TableLayoutPanel();
            ftlp.ColumnCount = 1;
            //ftlp.RowCount = all.Count() + 1;
            ftlp.Dock = DockStyle.Fill;
            ftlp.AutoSize = true;
            ftlp.AutoScroll = true;
            ftlp.CellBorderStyle = TableLayoutPanelCellBorderStyle.OutsetDouble;
            ftlp.Show();

            int fRowIndex = 0;
            foreach (var batch in batches)
            {
                #region

                //按照问单类型进行总体分组
                var all = from m in batch
                          group m by new { form_id = m.Field<string>("ctrnm_form_id") } into result
                          select result;

                //添加类型分组总体布局
                //TableLayoutPanel ftlp = new TableLayoutPanel();
                //ftlp.ColumnCount = 1;
                //ftlp.RowCount = all.Count() + 1;
                //ftlp.Dock = DockStyle.Fill;
                //ftlp.AutoSize = true;
                //ftlp.AutoScroll = true;
                //ftlp.CellBorderStyle = TableLayoutPanelCellBorderStyle.OutsetDouble;
                //ftlp.Show();
                ftlp.RowCount += all.Count();
                //int fRowIndex = 0;
                foreach (var f in all)
                {
                    #region 每个类型操作

                    //按照类目进行分组
                    var groups = from m in f
                                 group m by new { item_category = m.Field<string>("item_category"), icat_desc = m.Field<string>("icat_desc") } into result
                                 select result;

                    //根据分组添加总体布局
                    TableLayoutPanel tlp = new TableLayoutPanel();
                    tlp.ColumnCount = 2;
                    tlp.RowCount = groups.Count();
                    tlp.Dock = DockStyle.Fill;
                    tlp.AutoSize = true;
                    tlp.AutoScroll = true;
                    tlp.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
                    tlp.Show();
                    ftlp.SetColumn(tlp, 0);
                    ftlp.SetRow(tlp, fRowIndex);

                    int topPoint = 0, leftPoint = 0, groupRowIndex = 0, childGroupRowIndex = 0;
                    foreach (var group in groups)
                    {
                        #region
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

                        //添加内容
                        foreach (DataRow item in group)
                        {
                            #region
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

                                        newControl.Enabled = false;
                                        newControl.Show();
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
                            #endregion
                        }
                        groupRowIndex++;
                        #endregion
                    }
                    fRowIndex++;
                    ftlp.Controls.Add(tlp);

                    #endregion 每个类型操作
                }
                //pnl.Controls.Add(ftlp);
                #endregion
            }
            ftlp.RowCount += 1;
            pnl.Controls.Add(ftlp);
        }

        /// <summary>
        /// 英文问单浏览（简易模式）
        /// </summary>
        /// <param name="pDt">模板数据</param>
        /// <param name="pnl">面板</param>
        private void TemplatePreviewForEng(DataTable pDt, Panel pnl)
        {
            //释放控件资源
            PublicMethod.DisposeControl(pnl);
            //pnl.Controls.Clear();
            if (pDt == null || pDt.Rows.Count <= 0)
            {
                return;
            }
            var batches = from m in pDt.AsEnumerable()
                          group m by m.Field<string>("ctrnm_batchno") into result
                          select result;
            TableLayoutPanel ftlp = new TableLayoutPanel();
            ftlp.ColumnCount = 1;
            //ftlp.RowCount = all.Count() + 1;
            ftlp.Dock = DockStyle.Fill;
            ftlp.AutoSize = true;
            ftlp.AutoScroll = true;
            ftlp.CellBorderStyle = TableLayoutPanelCellBorderStyle.OutsetDouble;
            ftlp.Show();

            int fRowIndex = 0;
            foreach (var batch in batches)
            {
                #region
                //按照问单类型进行总体分组
                var all = from m in batch
                          group m by new { form_id = m.Field<string>("ctrnm_form_id") } into result
                          select result;

                //添加类型分组总体布局
                //TableLayoutPanel ftlp = new TableLayoutPanel();
                //ftlp.ColumnCount = 1;
                //ftlp.RowCount = all.Count() + 1;
                //ftlp.Dock = DockStyle.Fill;
                //ftlp.AutoSize = true;
                //ftlp.AutoScroll = true;
                //ftlp.CellBorderStyle = TableLayoutPanelCellBorderStyle.OutsetDouble;
                //ftlp.Show();
                ftlp.RowCount += all.Count();
                //int fRowIndex = 0;
                foreach (var f in all)
                {
                    #region 每个类型操作

                    //按照类目进行分组
                    var groups = from m in f
                                 group m by new { item_category = m.Field<string>("item_category"), icat_desc = m.Field<string>("icat_desc") } into result
                                 select result;

                    //根据分组添加总体布局
                    TableLayoutPanel tlp = new TableLayoutPanel();
                    tlp.Name = "Form_" + groups.First().First()["ctrnm_id"].ToString();
                    tlp.ContextMenuStrip = contentMS;
                    tlp.ColumnCount = 2;
                    tlp.RowCount = groups.Count();
                    tlp.Dock = DockStyle.Fill;
                    tlp.AutoSize = true;
                    tlp.AutoScroll = true;
                    tlp.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
                    tlp.Show();
                    ftlp.SetColumn(tlp, 0);
                    ftlp.SetRow(tlp, fRowIndex);

                    int topPoint = 0, leftPoint = 0, groupRowIndex = 0, childGroupRowIndex = 0;
                    foreach (var group in groups)
                    {
                        #region

                        if (group.Key.item_category.Equals("footer"))
                        {
                            continue;
                        }

                        //添加类目
                        Label lblCategory = new Label();
                        lblCategory.Text = group.Key.item_category;
                        lblCategory.Font = new System.Drawing.Font("宋体", 10f, FontStyle.Bold);
                        lblCategory.AutoSize = true;
                        lblCategory.Dock = DockStyle.Fill;
                        lblCategory.TextAlign = ContentAlignment.MiddleRight;
                        lblCategory.Show();
                        tlp.SetColumn(lblCategory, 0);
                        tlp.SetRow(lblCategory, groupRowIndex);
                        //tlp.Controls.Add(lblCategory);

                        //每个类目对应的明细先添加一个Panel
                        Panel cPnl = new Panel();
                        cPnl.Name = group.Key.item_category;
                        cPnl.AutoSize = true;
                        cPnl.BorderStyle = BorderStyle.FixedSingle;
                        cPnl.Dock = DockStyle.Fill;
                        cPnl.Show();
                        tlp.SetColumn(cPnl, 1);
                        tlp.SetRow(cPnl, groupRowIndex);
                        //tlp.Controls.Add(cPnl);

                        //再根据每个类目添加子布局
                        TableLayoutPanel tlpChild = new TableLayoutPanel();
                        tlpChild.Name = group.Key.item_category;
                        tlpChild.ColumnCount = 1;
                        tlpChild.RowCount = group.Count();
                        tlpChild.AutoSize = true;
                        tlpChild.Dock = DockStyle.Fill;
                        tlpChild.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
                        tlpChild.Show();
                        ////tlp.SetColumn(tlpChild, 1);
                        ////tlp.SetRow(tlpChild, groupRowIndex);
                        ////tlp.Controls.Add(tlpChild);
                        //cPnl.Controls.Add(tlpChild);

                        childGroupRowIndex = 0;

                        //添加内容
                        bool isValue = false;
                        foreach (DataRow item in group)
                        {
                            #region
                            //针对内容的每一行先添加一个Panel
                            Panel childPnl = new Panel();
                            childPnl.Name = group.Key.item_category + item["item_id"].ToString();
                            childPnl.AutoSize = true;
                            childPnl.BorderStyle = BorderStyle.None;
                            childPnl.Dock = DockStyle.Fill;
                            childPnl.Show();
                            tlpChild.SetColumn(childPnl, 0);
                            tlpChild.SetRow(childPnl, childGroupRowIndex);
                            //tlpChild.Controls.Add(childPnl);

                            topPoint = 0;
                            leftPoint = 8;
                            if (item["item_parameter_count"].ToString().Equals("0"))
                            {
                                #region 当行没有参数时就为纯文本

                                Label lblItem = new Label();
                                lblItem.Text = item["item_content_eng"].ToString().Replace("@", "");
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
                                tlpChild.Controls.Add(childPnl);
                                isValue = true;
                                #endregion 当行没有参数时就为纯文本
                            }
                            else
                            {
                                #region 当行存在参数时

                                int paraIndex = 0;
                                string regexStr = @"\{\d{0,}\}";//例如{0}、{1}

                                //拆分参数类型
                                string[] paraTypeArray = item["item_parameters_type"].ToString().Trim().Split(',');
                                //拆分模板内容
                                string[] contentArray = item["item_content_eng"].ToString().Trim().Split('#');
                                //拆分参数值
                                string[] strValue = item["ctrnd_parameters_value"].ToString().Trim().Split('@');
                                //拆分参数牙位
                                string[] paraYaweiArray = item["item_parameters_is_yawei"].ToString().Trim().Split(',');

                                bool isAdd = false;
                                bool isRow = false;
                                for (int i = 0; i < contentArray.Length; i++)
                                {
                                    //为空则忽略
                                    if (string.IsNullOrEmpty(contentArray[i]))
                                    {
                                        continue;
                                    }

                                    if (Regex.IsMatch(contentArray[i], regexStr)) //满足参数占位格式
                                    {
                                        isRow = false;
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
                                            #region
                                            if (!item["item_category"].ToString().Equals("header") && !item["item_category"].ToString().Equals("footer") && !bool.Parse(strValue[paraIndex]))
                                            {
                                                //i++;
                                                //paraIndex++;
                                                continue;
                                            }
                                            if (item["item_category"].ToString().Equals("header"))
                                            {
                                                if (bool.Parse(strValue[paraIndex]))
                                                {
                                                    CheckBox cb = new CheckBox();
                                                    cb.AutoSize = true;
                                                    cb.Left = leftPoint;
                                                    cb.Top = topPoint + 3;
                                                    cb.Checked = bool.Parse(strValue[paraIndex]);
                                                    newControl = cb;
                                                }
                                                else
                                                {
                                                    //i++;
                                                    //paraIndex++;
                                                    continue;
                                                }
                                            }
                                            else
                                            {
                                                CheckBox cb = new CheckBox();
                                                cb.AutoSize = true;
                                                cb.Left = leftPoint;
                                                cb.Top = topPoint + 3;
                                                cb.Checked = bool.Parse(strValue[paraIndex]);
                                                newControl = cb;
                                            }
                                            #endregion
                                        }
                                        else if (ty.Name.Equals("RichTextBox"))//当为备注框时特殊处理
                                        {
                                            #region
                                            if (!item["item_category"].ToString().Equals("header") && !item["item_category"].ToString().Equals("footer") && string.IsNullOrEmpty(strValue[paraIndex].Trim()))
                                            {
                                                //i++;
                                                //paraIndex++;
                                                continue;
                                            }
                                            if (item["item_category"].ToString().Equals("header") || item["item_category"].ToString().Equals("footer"))
                                            {
                                                //Label lblItem = new Label();
                                                //lblItem.Text = strValue[paraIndex];
                                                //lblItem.AutoSize = true;
                                                //lblItem.Left = leftPoint;
                                                //lblItem.Top = topPoint + 5;
                                                //newControl = lblItem;
                                                //i++;
                                                //paraIndex++;
                                                continue;
                                            }
                                            else
                                            {
                                                RichTextBox rtb = new RichTextBox();
                                                rtb.Height = 40;
                                                rtb.Width = 600;
                                                rtb.Left = leftPoint;
                                                rtb.Top = topPoint + 1;
                                                rtb.Text = strValue[paraIndex];
                                                newControl = rtb;
                                            }
                                            #endregion
                                        }
                                        else if (ty.Name.Equals("DateTimePicker")) //当为日期控件时特殊处理
                                        {
                                            #region
                                            if (item["item_category"].ToString().Equals("header") || item["item_category"].ToString().Equals("footer"))
                                            {
                                                //Label lblItem = new Label();
                                                //lblItem.Text = DateTime.Parse(strValue[paraIndex]).ToShortDateString();
                                                //lblItem.AutoSize = true;
                                                //lblItem.Left = leftPoint;
                                                //lblItem.Top = topPoint + 5;
                                                //if ((i - 1) >= 0)
                                                //{
                                                //    if (contentArray[i - 1].ToUpper().Trim().StartsWith("TIME"))
                                                //    {
                                                //        lblItem.Text = DateTime.Parse(strValue[paraIndex]).ToLongTimeString();
                                                //    }
                                                //}
                                                //newControl = lblItem;

                                                //i++;
                                                //paraIndex++;
                                                continue;
                                            }
                                            else
                                            {
                                                DateTimePicker dtp = new DateTimePicker();
                                                dtp.Width = 121;
                                                dtp.Left = leftPoint;
                                                dtp.Top = topPoint + 3;
                                                dtp.Format = DateTimePickerFormat.Short;
                                                if ((i - 1) >= 0)
                                                {
                                                    if (contentArray[i - 1].ToUpper().Trim().StartsWith("TIME"))
                                                    {
                                                        dtp.Width = 92;
                                                        dtp.Format = DateTimePickerFormat.Time;
                                                    }
                                                }
                                                dtp.Value = DateTime.Parse(strValue[paraIndex]);
                                                newControl = dtp;
                                            }
                                            #endregion
                                        }
                                        else if (ty.Name.Equals("RadioButton")) //当为单选框时特殊处理
                                        {
                                            #region
                                            if (item["item_category"].ToString().Equals("header"))
                                            {
                                                //i++;
                                                //paraIndex++;
                                                continue;
                                            }
                                            RadioButton rb = new RadioButton();
                                            rb.Left = leftPoint;
                                            rb.Top = topPoint + 3;
                                            rb.Checked = bool.Parse(strValue[paraIndex]);
                                            newControl = rb;
                                            #endregion
                                        }
                                        else if (ty.Name.Equals("TextBox"))
                                        {
                                            #region
                                            if (!item["item_category"].ToString().Equals("header") && !item["item_category"].ToString().Equals("footer") && string.IsNullOrEmpty(strValue[paraIndex].Trim()))
                                            {
                                                //i++;
                                                //paraIndex++;
                                                continue;
                                            }

                                            if (item["item_category"].ToString().Equals("header") || item["item_category"].ToString().Equals("footer"))
                                            {
                                                //Label lblItem = new Label();
                                                //lblItem.Text = strValue[paraIndex];
                                                //lblItem.AutoSize = true;
                                                //lblItem.Left = leftPoint;
                                                //lblItem.Top = topPoint + 5;
                                                //newControl = lblItem;
                                                //i++;
                                                //paraIndex++;
                                                continue;
                                            }
                                            else
                                            {
                                                TextBox tb = new TextBox();
                                                tb.Left = leftPoint;
                                                tb.Top = topPoint;
                                                tb.Text = strValue[paraIndex];
                                                try
                                                {
                                                    if (paraYaweiArray[paraIndex].Equals("1"))
                                                    {
                                                        tb.BackColor = Color.PeachPuff;
                                                    }
                                                }
                                                catch (Exception) { }
                                                newControl = tb;
                                            }
                                            #endregion
                                        }
                                        else
                                        {
                                            if (item["item_category"].ToString().Equals("header"))
                                            {
                                                //i++;
                                                //paraIndex++;
                                                continue;
                                            }
                                            newControl.Text = strValue[paraIndex];
                                        }

                                        newControl.Enabled = false;
                                        newControl.Show();
                                        childPnl.Controls.Add(newControl);
                                        leftPoint += newControl.Width + 3;
                                        //paraIndex++;
                                        isRow = true;
                                        isAdd = true;
                                        #endregion
                                    }
                                    else //为文本
                                    {
                                        #region
                                        if (group.Key.item_category.Equals("header"))
                                        {
                                            if (contentArray[i].ToLower().Contains("picture"))
                                            {
                                                Label lblItem = new Label();
                                                lblItem.Text = contentArray[i].Replace("@", "");
                                                lblItem.AutoSize = true;
                                                lblItem.Left = leftPoint;
                                                lblItem.Top = topPoint + 5;
                                                lblItem.Show();
                                                childPnl.Controls.Add(lblItem);
                                                leftPoint += lblItem.Width + 3;
                                            }
                                        }
                                        else
                                        {
                                            string[] tmpLable = contentArray[i].Split('@');
                                            if (string.IsNullOrEmpty(tmpLable[0]))
                                            {
                                                //最有可能的是每一行的开头分隔符（这样可以保证在没有勾选或填写第一个控件时，后面的文本能显示）
                                                Label lblItem = new Label();
                                                lblItem.Text = contentArray[i].Replace("@", "");
                                                lblItem.AutoSize = true;
                                                lblItem.Left = leftPoint;
                                                lblItem.Top = topPoint + 5;
                                                lblItem.Show();
                                                childPnl.Controls.Add(lblItem);
                                                leftPoint += lblItem.Width + 3;
                                            }
                                            else
                                            {
                                                if (isRow)//若该文本前的控件有输入信息，则控件后的文本都显示
                                                {
                                                    Label lblItem = new Label();
                                                    lblItem.Text = contentArray[i].Replace("@", "");
                                                    lblItem.AutoSize = true;
                                                    lblItem.Left = leftPoint;
                                                    lblItem.Top = topPoint + 5;
                                                    lblItem.Show();
                                                    childPnl.Controls.Add(lblItem);
                                                    leftPoint += lblItem.Width + 3;
                                                }
                                                else//若该文本前的控件没有输入信息，则控件后的第一个文本不显示
                                                {
                                                    if (tmpLable.Length > 1)
                                                    {
                                                        Label lblItem = new Label();
                                                        lblItem.Text = contentArray[i].Replace("@", "").Remove(0, tmpLable[0].Length);
                                                        lblItem.AutoSize = true;
                                                        lblItem.Left = leftPoint;
                                                        lblItem.Top = topPoint + 5;
                                                        lblItem.Show();
                                                        childPnl.Controls.Add(lblItem);
                                                        leftPoint += lblItem.Width + 3;
                                                    }
                                                }
                                            }
                                        }
                                        #endregion
                                    }
                                }
                                if (isAdd)
                                {
                                    topPoint += 25;
                                    tlpChild.Controls.Add(childPnl);
                                    isValue = true;
                                }
                                #endregion 当行存在参数时
                            }

                            childGroupRowIndex++;
                            #endregion
                        }

                        groupRowIndex++;
                        if (isValue)
                        {
                            tlp.Controls.Add(lblCategory);
                            tlp.Controls.Add(cPnl);
                            cPnl.Controls.Add(tlpChild);
                        }
                        #endregion
                    }
                    fRowIndex++;
                    ftlp.Controls.Add(tlp);

                    #endregion 每个类型操作
                }
                //pnl.Controls.Add(ftlp);
                #endregion
            }
            ftlp.RowCount += 1;
            pnl.Controls.Add(ftlp);
        }

        /// <summary>
        /// 英文问单浏览（完全模式）
        /// </summary>
        /// <param name="pDt">模板数据</param>
        /// <param name="pnl">面板</param>
        private void TemplatePreviewForEngAll(DataTable pDt, Panel pnl)
        {
            //释放控件资源
            PublicMethod.DisposeControl(pnl);
            //pnl.Controls.Clear();
            if (pDt == null || pDt.Rows.Count <= 0)
            {
                return;
            }
            var batches = from m in pDt.AsEnumerable()
                          group m by m.Field<string>("ctrnm_batchno") into result
                          select result;
            TableLayoutPanel ftlp = new TableLayoutPanel();
            ftlp.ColumnCount = 1;
            //ftlp.RowCount = all.Count() + 1;
            ftlp.Dock = DockStyle.Fill;
            ftlp.AutoSize = true;
            ftlp.AutoScroll = true;
            ftlp.CellBorderStyle = TableLayoutPanelCellBorderStyle.OutsetDouble;
            ftlp.Show();

            int fRowIndex = 0;
            foreach (var batch in batches)
            {
                #region
                //按照问单类型进行总体分组
                var all = from m in batch
                          group m by new { form_id = m.Field<string>("ctrnm_form_id") } into result
                          select result;

                //添加类型分组总体布局
                //TableLayoutPanel ftlp = new TableLayoutPanel();
                //ftlp.ColumnCount = 1;
                //ftlp.RowCount = all.Count() + 1;
                //ftlp.Dock = DockStyle.Fill;
                //ftlp.AutoSize = true;
                //ftlp.AutoScroll = true;
                //ftlp.CellBorderStyle = TableLayoutPanelCellBorderStyle.OutsetDouble;
                //ftlp.Show();
                ftlp.RowCount += all.Count();
                //int fRowIndex = 0;
                foreach (var f in all)
                {
                    #region 每个类型操作

                    //按照类目进行分组
                    var groups = from m in f
                                 group m by new { item_category = m.Field<string>("item_category"), icat_desc = m.Field<string>("icat_desc") } into result
                                 select result;

                    //根据分组添加总体布局
                    TableLayoutPanel tlp = new TableLayoutPanel();
                    tlp.Name = "Form_" + groups.First().First()["ctrnm_id"].ToString();
                    tlp.ContextMenuStrip = contentMS;
                    tlp.ColumnCount = 2;
                    tlp.RowCount = groups.Count();
                    tlp.Dock = DockStyle.Fill;
                    tlp.AutoSize = true;
                    tlp.AutoScroll = true;
                    tlp.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
                    tlp.Show();
                    ftlp.SetColumn(tlp, 0);
                    ftlp.SetRow(tlp, fRowIndex);

                    int topPoint = 0, leftPoint = 0, groupRowIndex = 0, childGroupRowIndex = 0;
                    foreach (var group in groups)
                    {
                        #region
                        //添加类目
                        Label lblCategory = new Label();
                        lblCategory.Text = group.Key.item_category;
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

                        //添加内容
                        foreach (DataRow item in group)
                        {
                            #region
                            //针对内容的每一行先添加一个Panel
                            Panel childPnl = new Panel();
                            childPnl.Name = group.Key.item_category + item["item_id"].ToString();
                            childPnl.AutoSize = true;
                            childPnl.BorderStyle = BorderStyle.None;
                            childPnl.Dock = DockStyle.Fill;
                            childPnl.Show();
                            tlpChild.SetColumn(childPnl, 0);
                            tlpChild.SetRow(childPnl, childGroupRowIndex);
                            //tlpChild.Controls.Add(childPnl);

                            topPoint = 0;
                            leftPoint = 8;
                            if (item["item_parameter_count"].ToString().Equals("0"))
                            {
                                #region 当行没有参数时就为纯文本

                                Label lblItem = new Label();
                                lblItem.Text = item["item_content_eng"].ToString().Replace("@", "");
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
                                tlpChild.Controls.Add(childPnl);
                                #endregion 当行没有参数时就为纯文本
                            }
                            else
                            {
                                #region 当行存在参数时

                                int paraIndex = 0;
                                string regexStr = @"\{\d{0,}\}";//例如{0}、{1}

                                //拆分参数类型
                                string[] paraTypeArray = item["item_parameters_type"].ToString().Trim().Split(',');
                                //拆分模板内容
                                string[] contentArray = item["item_content_eng"].ToString().Trim().Split('#');
                                //拆分参数值
                                string[] strValue = item["ctrnd_parameters_value"].ToString().Trim().Split('@');
                                //拆分参数牙位
                                string[] paraYaweiArray = item["item_parameters_is_yawei"].ToString().Trim().Split(',');
                                bool isAdd = false;
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
                                            #region
                                            if (!item["item_category"].ToString().Equals("header") && !item["item_category"].ToString().Equals("footer") && !bool.Parse(strValue[paraIndex]))
                                            {
                                                i++;
                                                //paraIndex++;
                                                continue;
                                            }
                                            CheckBox cb = new CheckBox();
                                            cb.AutoSize = true;
                                            cb.Left = leftPoint;
                                            cb.Top = topPoint + 3;
                                            cb.Checked = bool.Parse(strValue[paraIndex]);
                                            newControl = cb;
                                            #endregion
                                        }
                                        else if (ty.Name.Equals("RichTextBox"))//当为备注框时特殊处理
                                        {
                                            #region
                                            if (!item["item_category"].ToString().Equals("header") && !item["item_category"].ToString().Equals("footer") && string.IsNullOrEmpty(strValue[paraIndex].Trim()))
                                            {
                                                i++;
                                                //paraIndex++;
                                                continue;
                                            }
                                            if (item["item_category"].ToString().Equals("header") || item["item_category"].ToString().Equals("footer"))
                                            {
                                                Label lblItem = new Label();
                                                lblItem.Text = strValue[paraIndex];
                                                lblItem.AutoSize = true;
                                                lblItem.Left = leftPoint;
                                                lblItem.Top = topPoint + 5;
                                                newControl = lblItem;
                                            }
                                            else
                                            {
                                                RichTextBox rtb = new RichTextBox();
                                                rtb.Height = 40;
                                                rtb.Width = 600;
                                                rtb.Left = leftPoint;
                                                rtb.Top = topPoint + 1;
                                                rtb.Text = strValue[paraIndex];
                                                newControl = rtb;
                                            }
                                            #endregion
                                        }
                                        else if (ty.Name.Equals("DateTimePicker")) //当为日期控件时特殊处理
                                        {
                                            #region
                                            if (item["item_category"].ToString().Equals("header") || item["item_category"].ToString().Equals("footer"))
                                            {
                                                Label lblItem = new Label();
                                                lblItem.Text = DateTime.Parse(strValue[paraIndex]).ToShortDateString();
                                                lblItem.AutoSize = true;
                                                lblItem.Left = leftPoint;
                                                lblItem.Top = topPoint + 5;
                                                if ((i - 1) >= 0)
                                                {
                                                    if (contentArray[i - 1].ToUpper().Trim().StartsWith("TIME"))
                                                    {
                                                        lblItem.Text = DateTime.Parse(strValue[paraIndex]).ToLongTimeString();
                                                    }
                                                }
                                                newControl = lblItem;
                                            }
                                            else
                                            {
                                                DateTimePicker dtp = new DateTimePicker();
                                                dtp.Width = 121;
                                                dtp.Left = leftPoint;
                                                dtp.Top = topPoint + 3;
                                                dtp.Format = DateTimePickerFormat.Short;
                                                if ((i - 1) >= 0)
                                                {
                                                    if (contentArray[i - 1].ToUpper().Trim().StartsWith("TIME"))
                                                    {
                                                        dtp.Width = 92;
                                                        dtp.Format = DateTimePickerFormat.Time;
                                                    }
                                                }
                                                dtp.Value = DateTime.Parse(strValue[paraIndex]);
                                                newControl = dtp;
                                            }
                                            #endregion
                                        }
                                        else if (ty.Name.Equals("RadioButton")) //当为单选框时特殊处理
                                        {
                                            #region
                                            RadioButton rb = new RadioButton();
                                            rb.Left = leftPoint;
                                            rb.Top = topPoint + 3;
                                            rb.Checked = bool.Parse(strValue[paraIndex]);
                                            newControl = rb;
                                            #endregion
                                        }
                                        else if (ty.Name.Equals("TextBox"))
                                        {
                                            #region
                                            if (!item["item_category"].ToString().Equals("header") && !item["item_category"].ToString().Equals("footer") && string.IsNullOrEmpty(strValue[paraIndex].Trim()))
                                            {
                                                i++;
                                                //paraIndex++;
                                                continue;
                                            }

                                            if (item["item_category"].ToString().Equals("header") || item["item_category"].ToString().Equals("footer"))
                                            {
                                                Label lblItem = new Label();
                                                lblItem.Text = strValue[paraIndex];
                                                lblItem.AutoSize = true;
                                                lblItem.Left = leftPoint;
                                                lblItem.Top = topPoint + 5;
                                                newControl = lblItem;
                                            }
                                            else
                                            {
                                                TextBox tb = new TextBox();
                                                tb.Left = leftPoint;
                                                tb.Top = topPoint;
                                                tb.Text = strValue[paraIndex];
                                                try
                                                {
                                                    if (paraYaweiArray[paraIndex].Equals("1"))
                                                    {
                                                        tb.BackColor = Color.PeachPuff;
                                                    }
                                                }
                                                catch (Exception) { }
                                                newControl = tb;
                                            }
                                            #endregion
                                        }
                                        else
                                        {
                                            newControl.Text = strValue[paraIndex];
                                        }

                                        newControl.Enabled = false;
                                        newControl.Show();
                                        childPnl.Controls.Add(newControl);
                                        leftPoint += newControl.Width + 3;
                                        //paraIndex++;
                                        isAdd = true;
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
                                if (isAdd)
                                {
                                    topPoint += 25;
                                    tlpChild.Controls.Add(childPnl);
                                }
                                #endregion 当行存在参数时
                            }

                            childGroupRowIndex++;
                            #endregion
                        }

                        groupRowIndex++;
                        #endregion
                    }
                    fRowIndex++;
                    ftlp.Controls.Add(tlp);

                    #endregion 每个类型操作
                }
                //pnl.Controls.Add(ftlp);
                #endregion
            }
            ftlp.RowCount += 1;
            pnl.Controls.Add(ftlp);
        }

        /// <summary>
        /// 获取问单主体英文信息并显示，以便CS操作
        /// </summary>
        /// <param name="pDt">问单数据明细</param>
        /// <returns>问单主体英文信息</returns>
        private string GetDetailEngAndShow(DataTable pDt)
        {
            if (pDt == null || pDt.Rows.Count <= 0)
            {
                return string.Empty;
            }
            StringBuilder sbAll = new StringBuilder();
            StringBuilder userInfo = new StringBuilder();
            //按批次进行分组
            var batches = from m in pDt.AsEnumerable()
                          group m by m.Field<string>("ctrnm_batchno") into result
                          select result;
            int groupIndex = 0;
            foreach (var batch in batches)
            {
                //按照问单类型进行总体分组
                var all = from m in batch
                          group m by new { form_id = m.Field<string>("ctrnm_form_id") } into result
                          select result;
                foreach (var f in all)
                {
                    //按照类目进行分组
                    var groups = from m in f
                                 group m by m.Field<string>("item_category") into result
                                 where result.Key != "title"
                                 where result.Key != "header"
                                 //where result.Key != "footer"
                                 select result;
                    foreach (var group in groups)
                    {
                        #region
                        ////写入类目
                        //sbAll.Append(group.Key);
                        //sbAll.AppendLine();
                        StringBuilder sbGroup = new StringBuilder();
                        int itemIndex = 0;
                        foreach (DataRow item in group)
                        {
                            StringBuilder sb = new StringBuilder();
                            if (Convert.ToInt32(item["item_parameter_count"].ToString()) > 0) //存在参数时
                            {
                                int paraIndex = 0;
                                string regexStr = @"\{\d\}";//例如{0}、{1}

                                //拆分模板内容
                                string[] contentArray = item["item_content_eng"].ToString().Trim().Split('#');
                                //拆分参数类型
                                string[] paraTypeArray = item["item_parameters_type"].ToString().Trim().Split(',');
                                //拆分参数值
                                string[] strValue = item["ctrnd_parameters_value"].ToString().Trim().Split('@');
                                //拆分牙位标记
                                string[] parmYaweiArray = item["item_parameters_is_yawei"].ToString().Trim().Split(',');

                                bool isAdd = false;
                                bool isRow = false;
                                for (int i = 0; i < contentArray.Length; i++)
                                {
                                    //为空则忽略
                                    if (string.IsNullOrEmpty(contentArray[i]))
                                    {
                                        continue;
                                    }

                                    if (Regex.IsMatch(contentArray[i], regexStr))//满足参数占位格式
                                    {
                                        isRow = false;
                                        #region
                                        string regexStr2 = @"(\d+)";
                                        paraIndex = Convert.ToInt32(Regex.Match(contentArray[i], regexStr2).Value);

                                        //为表尾时单独处理
                                        if (group.Key == "footer")
                                        {
                                            try
                                            {
                                                if (contentArray[i - 1].ToLower().Contains("user"))
                                                {
                                                    userInfo.Append(",");
                                                    userInfo.Append(strValue[paraIndex]);
                                                }
                                                if (contentArray[i - 1].ToLower().Contains("check"))
                                                {
                                                    if (strValue[paraIndex].Trim().Length > 0)
                                                    {
                                                         userInfo.Append("/");
                                                         userInfo.Append(strValue[paraIndex]);
                                                         userInfo.Append(",");
                                                    }
                                                }
                                                if(contentArray[i-1].ToLower().Contains("confirmation"))
                                                {
                                                    if (strValue[paraIndex].Trim().Length > 0)
                                                    {
                                                        userInfo.Append("/");
                                                        userInfo.Append(strValue[paraIndex]);
                                                        userInfo.Append(",");
                                                    }
                                                }
                                            }
                                            catch (Exception) { }
                                            continue;
                                        }

                                        if (paraTypeArray[paraIndex].Equals("CheckBox"))
                                        {
                                            #region
                                            if (bool.Parse(strValue[paraIndex])) //勾选
                                            {
                                                isRow = true;
                                                isAdd = true;
                                            }
                                            else
                                            {
                                                isRow = false;
                                            }
                                            #endregion
                                        }
                                        else if (paraTypeArray[paraIndex].Equals("DateTimePicker"))
                                        {
                                            #region
                                            sb.Append(strValue[paraIndex]);
                                            isRow = true;
                                            isAdd = true;
                                            #endregion
                                        }
                                        else if (paraTypeArray[paraIndex].Equals("RadioButton"))
                                        {
                                            #region
                                            if (bool.Parse(strValue[paraIndex])) //选择
                                            {
                                                isRow = true;
                                                isAdd = true;
                                            }
                                            else
                                            {
                                                isRow = false;
                                            }
                                            #endregion
                                        }
                                        else if (paraTypeArray[paraIndex].Equals("TextBox") || paraTypeArray[paraIndex].Equals("RichTextBox"))
                                        {
                                            #region
                                            if (strValue[paraIndex].Trim().Length > 0) //填写了信息
                                            {
                                                //判断是否为牙位
                                                try
                                                {
                                                    if (isUS && parmYaweiArray[paraIndex].Equals("1"))
                                                    {
                                                        //国际牙位转美国牙位
                                                        sb.Append(PublicMethod.FromENToUS(strValue[paraIndex]));
                                                    }
                                                    else
                                                    {
                                                        sb.Append(strValue[paraIndex]);
                                                    }
                                                }
                                                catch (Exception ex) { sb.Append(strValue[paraIndex]); }
                                                isRow = true;
                                                isAdd = true;
                                            }
                                            else
                                            {
                                                isRow = false;
                                            }
                                            #endregion
                                        }
                                        //paraIndex++;
                                        #endregion
                                    }
                                    else //为文本
                                    {
                                        #region
                                        string[] tmpLable = contentArray[i].Split('@');
                                        if (string.IsNullOrEmpty(tmpLable[0]))
                                        {
                                            //最有可能的是每一行的开头分隔符（这样可以保证在没有勾选或填写第一个控件时，后面的文本能显示）
                                            sb.Append(contentArray[i].Replace("@", ""));
                                        }
                                        else
                                        {
                                            if (isRow)//若该文本前的控件有输入信息，则控件后的文本都显示
                                            {
                                                sb.Append(contentArray[i].Replace("@", ""));
                                            }
                                            else//若该文本前的控件没有输入信息，则控件后的第一个文本不显示
                                            {
                                                if (tmpLable.Length > 1)
                                                {
                                                    sb.Append(contentArray[i].Replace("@", "").Remove(0, tmpLable[0].Length));
                                                }
                                            }
                                        }
                                        #endregion
                                    }
                                }
                                if (isAdd)
                                {
                                        ++itemIndex;
                                        //sbAll.AppendLine(sb.ToString());
                                        sbGroup.Append(" ");
                                        sbGroup.Append(itemIndex);
                                        sbGroup.Append(")、");
                                        sbGroup.AppendLine(sb.ToString());
                                }
                            }
                        }
                        if (sbGroup.Length > 0)
                        {
                            //写入类目
                            CultureInfo cinfo = Thread.CurrentThread.CurrentCulture;
                            TextInfo tInfo = cinfo.TextInfo;
                            if (batches.Count() > 1 || all.Count() > 1)
                            {
                                if (group.Key.Trim().ToLower().Equals("problem"))
                                {
                                    ++groupIndex;
                                    sbAll.Append(groupIndex);
                                    sbAll.Append(" ");
                                }
                                sbAll.AppendLine(tInfo.ToTitleCase(group.Key));
                                //sbAll.AppendLine();
                                if (!sbGroup.ToString().Contains("2)、"))
                                {
                                    sbAll.Append(sbGroup.Replace("1)、",""));
                                }
                                else
                                {
                                    sbAll.Append(sbGroup.ToString());
                                }
                            }
                            else
                            {
                                sbAll.AppendLine(tInfo.ToTitleCase(group.Key));
                                //sbAll.AppendLine();
                                if (!sbGroup.ToString().Contains("2)、"))
                                {
                                    sbAll.Append(sbGroup.Replace("1)、", ""));
                                }
                                else
                                {
                                    sbAll.Append(sbGroup.ToString());
                                }
                            }
                        }
                        #endregion
                    }
                }
            }
            #region
            //按照类目进行分组
            //var groups = from m in pDt.AsEnumerable()
            //             group m by m.Field<string>("item_category") into result
            //             where result.Key != "title"
            //             where result.Key != "header"
            //             where result.Key != "footer"
            //             select result;

            //foreach (var group in groups)
            //{
            //    //写入类目
            //    sbAll.Append(group.Key);
            //    sbAll.AppendLine();
            //    foreach (DataRow item in group)
            //    {
            //        StringBuilder sb = new StringBuilder();
            //        if (Convert.ToInt32(item["item_parameter_count"].ToString()) > 0) //存在参数时
            //        {
            //            int paraIndex = 0;
            //            string regexStr = @"\{\d\}";//例如{0}、{1}

            //            //拆分模板内容
            //            string[] contentArray = item["item_content_eng"].ToString().Trim().Split('#');
            //            //拆分参数类型
            //            string[] paraTypeArray = item["item_parameters_type"].ToString().Trim().Split(',');
            //            //拆分参数值
            //            string[] strValue = item["ctrnd_parameters_value"].ToString().Trim().Split('@');

            //            for (int i = 0; i < contentArray.Length; i++)
            //            {
            //                //为空则忽略
            //                if (string.IsNullOrEmpty(contentArray[i]))
            //                {
            //                    continue;
            //                }

            //                if (Regex.IsMatch(contentArray[i], regexStr))//满足参数占位格式
            //                {
            //                    #region
            //                    if (paraTypeArray[paraIndex].Equals("CheckBox"))
            //                    {
            //                        if (bool.Parse(strValue[paraIndex])) //勾选
            //                        {
            //                            if (Regex.IsMatch(contentArray[i + 1], regexStr))
            //                            { }
            //                            else
            //                            {
            //                                //如果下一个模板内容为文本，则直接加入字符串
            //                                sb.Append(contentArray[++i]);
            //                            }
            //                        }
            //                    }
            //                    else if (paraTypeArray[paraIndex].Equals("DateTimePicker"))
            //                    {
            //                        sb.Append(strValue[paraIndex]);
            //                        if (Regex.IsMatch(contentArray[i + 1], regexStr))
            //                        { }
            //                        else
            //                        {
            //                            //如果下一个模板内容为文本，则直接加入字符串
            //                            sb.Append(contentArray[++i]);
            //                        }
            //                    }
            //                    else if (paraTypeArray[paraIndex].Equals("RadioButton"))
            //                    {
            //                        if (bool.Parse(strValue[paraIndex])) //选择
            //                        {
            //                            if (Regex.IsMatch(contentArray[i + 1], regexStr))
            //                            { }
            //                            else
            //                            {
            //                                //如果下一个模板内容为文本，则直接加入字符串
            //                                sb.Append(contentArray[++i]);
            //                            }
            //                        }
            //                    }
            //                    else if (paraTypeArray[paraIndex].Equals("TextBox") || paraTypeArray[paraIndex].Equals("RichTextBox"))
            //                    {
            //                        if (strValue[paraIndex].Trim().Length > 0) //填写了信息
            //                        {
            //                            sb.Append(strValue[paraIndex]);
            //                            if (Regex.IsMatch(contentArray[i + 1], regexStr))
            //                            { }
            //                            else
            //                            {
            //                                //如果下一个模板内容为文本，则直接加入字符串
            //                                sb.Append(contentArray[++i]);
            //                            }
            //                        }
            //                    }
            //                    paraIndex++;
            //                    #endregion
            //                }
            //                else if (i == 0) //当每一行的起始是文本，不是控件时特殊处理
            //                {
            //                    #region
            //                    if (Regex.IsMatch(contentArray[i + 1], regexStr))
            //                    {
            //                        if (paraTypeArray[paraIndex].Equals("TextBox") || paraTypeArray[paraIndex].Equals("RichTextBox"))
            //                        {
            //                            if (strValue[paraIndex].Trim().Length > 0)
            //                            {
            //                                sb.Append(contentArray[i++]);
            //                                sb.Append(strValue[paraIndex++]);
            //                            }
            //                        }
            //                    }
            //                    #endregion
            //                }
            //            }
            //        }
            //        if (sb.Length > 0)
            //        {
            //            sbAll.AppendLine(sb.ToString());
            //        }
            //    }
            //}
            #endregion

            sbAll.Append("(");
            sbAll.Append(userInfo.ToString().Trim(',').Trim('/'));
            sbAll.Append(")");
            return sbAll.ToString();
        }

        /// <summary>
        /// 刷新当前工作单信息
        /// </summary>
        /// <param name="pJobNo">公司条码</param>
        /// <param name="pIsSubmit">是否转医生</param>
        private void RefreashJobNoInfo(string pJobNo, bool pIsSubmit)
        {

            if (pIsSubmit)
            {
                txtJobNo2.Text = pJobNo;
                Fm_BusinessReply_Load(null, null);
                txtJobNo2_KeyPress(null, null);
            }
            else
            {
                txtJobNo.Text = pJobNo;
                Fm_BusinessReply_Load(null, null);
                txtJobNo_KeyPress(null, null);
            }

        }

    }
}
