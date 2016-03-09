using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Data;
using CaseInquire.helperclass;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;

namespace CaseInquire
{
    /*
     * 新建问单--自定义方法分部类
     */
    partial class Fm_BusinessNew
    {
        DataTable dt = null;
        DataTable dt2 = null;
        string docNo = string.Empty, rCtrnmId = string.Empty;
        string formId = string.Empty, formCode = string.Empty, formVer = string.Empty;
        /// <summary>
        /// 选择具体问单模板后触发
        /// </summary>
        /// <param name="pFormId">问单模板ID</param>
        /// <param name="pFormCode">问单Code</param>
        /// <param name="pFormVer">问单版本</param>
        private void caseTypeForm_ReturnCaseTypeEvent(string pFormId, string pFormCode, string pFormVer)
        {
            if (string.IsNullOrEmpty(pFormId))
            {
                lblInfo.Text = "未选择问单类型";
                lblInfo.ForeColor = Color.DarkOliveGreen;
                btnCreate.Focus();
            }
            else
            {
                #region 选择了具体的问单类型

                formId = pFormId;
                formCode = pFormCode;
                formVer = pFormVer;

                try
                {
                    //获取所选模板类型已配置的信息
                    dt = null;
                    dt = GetTemplateItem(formId);
                    //获取模板
                    TemplatePreview(dt, pnlWrite);

                    if (dt == null || dt.Rows.Count <= 0)
                    {
                        lblInfo.Text = "当前问单类型未作模板设计，请先到模板设计进行设置";
                        lblInfo.ForeColor = Color.DarkOliveGreen;
                        return;
                    }
                    lstAttachment.Items.Clear();
                    //根据公司条码、所选问单类型、操作者获取是否已有暂存状态的问单
                    //若有，则直接带出供编辑；若没有，则表示新建
                    dt2 = null;
                    dt2 = GetCaseByJobNoAndFormId(txtJobNo2.Text.Trim(), formId);
                    if (null == dt2 || dt2.Rows.Count <= 0)
                    {
                        //新建问单
                        //自动对表头、表尾等基本信息进行填写（不需人为再输入）
                        lblShow.Text = "新建";
                        lblInfo.Text = "创建新问单";
                        lblInfo.ForeColor = Color.DarkOliveGreen;
                        lblDocNo.Text = string.Empty;
                        AutoFillInfo();
                        btnUpload.Enabled = false;
                        btnCancel.Enabled = false;

                        btnCache.Enabled = true;
                        btnSubmit.Enabled = true;

                        pnlWrite.Enabled = true;
                        pnlWrite.Focus();
                    }
                    else
                    {
                        docNo = dt2.Rows[0]["ctrnm_docno"].ToString();
                        rCtrnmId = dt2.Rows[0]["ctrnm_id"].ToString();
                        //已有暂存问单明细
                        GetCaseDetailAndShow(dt2.Rows[0]["ctrnm_form_id"].ToString(), dt2.Rows[0]["ctrnm_id"].ToString());

                        lblShow.Text = "暂存";
                        lblInfo.Text = "暂存问单编号为【" + docNo + "】可以进行编辑";
                        lblInfo.ForeColor = Color.DarkOliveGreen;
                        lblDocNo.Text = "问单编号【" + docNo + "】";
                        btnUpload.Enabled = true;
                        btnCancel.Enabled = true;

                        //获取问单附件
                        ShowAttachment(rCtrnmId);

                        btnCache.Enabled = true;
                        btnSubmit.Enabled = true;

                        pnlWrite.Enabled = true;
                        pnlWrite.Focus();

                        List<string> getFileListFromDatabase = new List<string>();
                        foreach (DataRow item in dtAttachment.Rows)
                        {
                            getFileListFromDatabase.Add(PublicClass.FileServerPathBase + item[1].ToString());
                        }
                        photoControl1.LoadJpe(getFileListFromDatabase);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                #endregion 选择了具体的问单类型
            }
        }

        /// <summary>
        /// 模板预览
        /// </summary>
        /// <param name="pDt">模板数据</param>
        /// <param name="pnl">面板</param>
        private void TemplatePreview(DataTable pDt, Panel pnl)
        {
            //释放控件资源
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
            tlp.ColumnCount = 2;
            tlp.RowCount = groups.Count() + 1;
            tlp.Dock = DockStyle.Fill;
            tlp.AutoSize = true;
            tlp.AutoScroll = true;
            tlp.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
            tlp.Show();
            //pnl.Controls.Add(tlp);

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
                //cPnl.Name = group.Key.item_category;
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
                            lblItem.Font = new Font("宋体", 14f, FontStyle.Bold);
                            lblItem.Left = childPnl.Width / 3;
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
                        string regexStr = @"\{\d{0,}\}";//例如{0}、{1}(\d+)

                        //拆分参数类型
                        string[] paraTypeArray = item["item_parameters_type"].ToString().Trim().Split(',');
                        //拆分模板内容
                        string[] contentArray = item["item_content"].ToString().Trim().Split('#');
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
                                //获取配置信息中参数的键值（它的范围一定在参数个数之内）
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
                                    newControl = cb;
                                }
                                else if (ty.Name.Equals("RichTextBox"))//当为备注框时特殊处理
                                {
                                    RichTextBox rtb = new RichTextBox();
                                    rtb.Height = 40;
                                    rtb.Width = 600;
                                    rtb.Left = leftPoint;
                                    rtb.Top = topPoint + 1;
                                    newControl = rtb;
                                }
                                else if (ty.Name.Equals("DateTimePicker"))
                                {
                                    DateTimePicker dtp = new DateTimePicker();
                                    dtp.Width = 121;
                                    dtp.Left = leftPoint;
                                    dtp.Top = topPoint + 3;
                                    dtp.Enabled = false;
                                    dtp.Format = DateTimePickerFormat.Short;
                                    if ((i - 1) >= 0)
                                    {
                                        if (contentArray[i - 1].Trim().StartsWith("时间"))
                                        {
                                            dtp.Width = 92;
                                            dtp.Format = DateTimePickerFormat.Time;
                                        }
                                    }
                                    newControl = dtp;
                                }
                                else if (ty.Name.Equals("TextBox"))
                                {
                                    TextBox tb = new TextBox();
                                    tb.Left = leftPoint;
                                    tb.Top = topPoint + 1;
                                    try
                                    {
                                        //判断是否为牙位
                                        if (paraYaweiArray[paraIndex].Equals("1"))
                                        {
                                            tb.BackColor = Color.PeachPuff;
                                        }
                                    }
                                    catch (Exception) { }

                                    tb.TextChanged += ((sender, e) =>
                                    {
                                        try
                                        {
                                            if (!string.IsNullOrEmpty(tb.Text.Trim()))
                                            {
                                                for (int j = childPnl.Controls.Count - 1; j >= 0; j--)
                                                {
                                                    //if (childPnl.Controls[j] is TextBox && childPnl.Controls[j].Text.Trim().Equals(tb.Text.Trim()))
                                                    if (childPnl.Controls[j].Focused)
                                                    {
                                                        for (int jj = j; jj >= 0; jj--)
                                                        {
                                                            if (childPnl.Controls[jj] is CheckBox)
                                                            {
                                                                CheckBox cbb = new CheckBox();
                                                                cbb = childPnl.Controls[jj] as CheckBox;
                                                                cbb.Checked = true;
                                                                break;
                                                            }
                                                        }
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                        catch (Exception) { }
                                    });

                                    tb.Validating += ((sender, e) => {
                                        if (!string.IsNullOrEmpty(tb.Text.Trim()))
                                        {
                                            if (tb.BackColor == Color.PeachPuff)
                                            {
                                                if (!CheckYawei(tb.Text.Trim()))
                                                {
                                                    //MessageBox.Show("牙位信息填写有误！只能填写数字、逗号','、减号'-'", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information); //show detail info 20160109
                                                    MessageBox.Show("牙位信息填写有误！只能填写数字、英文逗号','、英文减号'-' ( " + tb.Text.Trim() + " )", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    //tb.Text = string.Empty; //keep value , user can modify
                                                    e.Cancel=true; // failure validate , keep in the control
                                                } 
                                                else if (!PublicMethod.ValidateYaiWeiNum(tb.Text.Trim(), "EN")) //EN US check //20160109
                                                {
                                                    MessageBox.Show("牙位信息填写有误！牙位数字不对 ( " + tb.Text.Trim() + " )", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    e.Cancel = true; // failure validate , keep in the control
                                                }
                                            }
                                        }
                                    });
                                    
                                    newControl = tb;
                                }

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
            pnl.Controls.Add(tlp);
        }

        /// <summary>
        /// 自动填充问单基本信息
        /// </summary>
        private void AutoFillInfo()
        {
            //循环遍历模板再赋值
            foreach (Control pnl in pnlWrite.Controls)
            {
                if (pnl is TableLayoutPanel)//TableLayoutPanel
                {
                    foreach (Control cpnl in pnl.Controls)
                    {
                        if (cpnl is Panel)
                        {
                            foreach (Control lpnl in cpnl.Controls)
                            {
                                if (lpnl is TableLayoutPanel)//每个类目的TableLayoutPanel
                                {
                                    if (lpnl.Name.Equals("header"))//为表头类目时
                                    {
                                        #region
                                        foreach (Control ipnl in lpnl.Controls)
                                        {
                                            if (ipnl is Panel)//详细内容的每行panel
                                            {
                                                bool isFlag = false;
                                                for (int i = 0; i < ipnl.Controls.Count; i++)
                                                {
                                                    #region
                                                    if (ipnl.Controls[i] is Label)
                                                    {
                                                        Label lbl = ipnl.Controls[i] as Label;
                                                        if (lbl.Text.Contains("货类"))
                                                        {
                                                            ipnl.Controls[i + 1].Text = lblMgrpCode.Text.Trim();
                                                            ipnl.Controls[i + 1].Enabled = false;
                                                            isFlag = true;
                                                        }
                                                        else if (lbl.Text.Contains("编号"))
                                                        {
                                                            ipnl.Controls[i + 1].Text = txtCaseNo.Text.Trim();
                                                            ipnl.Controls[i + 1].Enabled = false;
                                                            isFlag = true;
                                                        }
                                                        else if (lbl.Text.Contains("出货日期"))
                                                        {
                                                            DateTimePicker dtp = ipnl.Controls[i + 1] as DateTimePicker;
                                                            dtp.Format = DateTimePickerFormat.Short;
                                                            dtp.Value = DateTime.Parse(txtEstimateDate.Text.Trim());
                                                            dtp.Enabled = false;
                                                            isFlag = true;
                                                        }
                                                    }
                                                    #endregion
                                                }
                                                if (isFlag)
                                                {
                                                    break;
                                                }
                                            }
                                        }
                                        #endregion
                                    }
                                    else if (lpnl.Name.Equals("footer"))//为表尾类目时
                                    {
                                        #region
                                        foreach (Control ipnl in lpnl.Controls)
                                        {
                                            if (ipnl is Panel)//详细内容的每行panel
                                            {
                                                bool isFlag = false;
                                                for (int i = 0; i < ipnl.Controls.Count; i++)
                                                {
                                                    #region
                                                    if (ipnl.Controls[i] is Label)
                                                    {
                                                        Label lbl = ipnl.Controls[i] as Label;
                                                        if (lbl.Text.Contains("问单") || lbl.Text.Contains("问货") || lbl.Text.Contains("工号"))
                                                        {
                                                            ipnl.Controls[i + 1].Text = PublicClass.LoginName;
                                                            ipnl.Controls[i + 1].Enabled = false;
                                                            isFlag = true;
                                                            break;
                                                        }
                                                    }
                                                    #endregion
                                                }
                                                if (isFlag)
                                                {
                                                    break;
                                                }
                                            }
                                        }
                                        #endregion
                                    }
                                    else//为其它类目时参数值都为默认值
                                    {
                                        #region
                                        foreach (Control ipnl in lpnl.Controls)
                                        {
                                            if (ipnl is Panel)
                                            {
                                                for (int i = 0; i < ipnl.Controls.Count; i++)
                                                {
                                                    #region
                                                    if (ipnl.Controls[i] is Label)
                                                    {
                                                        continue;
                                                    }
                                                    if (ipnl.Controls[i] is TextBox)
                                                    {
                                                        ipnl.Controls[i].Text = string.Empty;
                                                    }
                                                    else if (ipnl.Controls[i] is RichTextBox)
                                                    {
                                                        ipnl.Controls[i].Text = string.Empty;
                                                    }
                                                    else if (ipnl.Controls[i] is CheckBox)
                                                    {
                                                        CheckBox cb = ipnl.Controls[i] as CheckBox;
                                                        cb.Checked = false;
                                                    }
                                                    else if (ipnl.Controls[i] is DateTimePicker)
                                                    {
                                                        //
                                                    }
                                                    else if (ipnl.Controls[i] is RadioButton)
                                                    {
                                                        RadioButton rb = ipnl.Controls[i] as RadioButton;
                                                        rb.Checked = false;
                                                    }
                                                    #endregion
                                                }
                                            }
                                        }
                                        #endregion
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        DataTable dtHasValue = null;
        /// <summary>
        /// 获取业务问单详细信息并显示
        /// </summary>
        /// <param name="pFormId">问单模板ID</param>
        /// <param name="pCtrnmId">问单ID</param>
        private void GetCaseDetailAndShow(string pFormId, string pCtrnmId)
        {
            //从数据库获取问单值信息
            dtHasValue = null;
            dtHasValue = GetCaseDetail(pFormId, pCtrnmId);
            //显示在模板上(选择循环遍历模板依次赋值)
            foreach (Control pnl in pnlWrite.Controls)
            {
                if (pnl is TableLayoutPanel)//TableLayoutPanel
                {
                    foreach (Control cpnl in pnl.Controls)
                    {
                        if (cpnl is Panel)
                        {
                            foreach (Control lpnl in cpnl.Controls)
                            {
                                if (lpnl is TableLayoutPanel)//每个类目的TableLayoutPanel
                                {
                                    foreach (Control ipnl in lpnl.Controls)
                                    {
                                        if (ipnl is Panel)//详细内容的每行panel
                                        {
                                            string[] strValue = null;
                                            int everRowIndex = 0;
                                            foreach (DataRow item in dtHasValue.Rows)
                                            {
                                                if (ipnl.Name.Contains(item["ctrnd_item_id"].ToString()))
                                                {
                                                    strValue = item["ctrnd_parameters_value"].ToString().Trim().Split('@');
                                                    break;
                                                }
                                            }

                                            for (int i = 0; i < ipnl.Controls.Count; i++)
                                            {
                                                #region
                                                if (ipnl.Controls[i] is Label)
                                                {
                                                    continue;
                                                }

                                                if (ipnl.Controls[i] is TextBox)
                                                {
                                                    ipnl.Controls[i].Text = strValue[everRowIndex];

                                                    try
                                                    {
                                                        if ((cpnl.Name.Equals("header") || cpnl.Name.Equals("footer")) && ipnl.Controls[i - 1] is Label)
                                                        {
                                                            Label tmplbl = ipnl.Controls[i - 1] as Label;
                                                            if (tmplbl.Text.Contains("货类") || tmplbl.Text.Contains("编号") || tmplbl.Text.Contains("问单") || tmplbl.Text.Contains("问货") || tmplbl.Text.Contains("工号"))
                                                            {
                                                                ipnl.Controls[i].Enabled = false;
                                                            }
                                                        }
                                                    }
                                                    catch { }
                                                }
                                                else if (ipnl.Controls[i] is RichTextBox)
                                                {
                                                    ipnl.Controls[i].Text = strValue[everRowIndex];
                                                }
                                                else if (ipnl.Controls[i] is CheckBox)
                                                {
                                                    CheckBox cb = ipnl.Controls[i] as CheckBox;
                                                    cb.Checked = bool.Parse(strValue[everRowIndex]);
                                                }
                                                else if (ipnl.Controls[i] is DateTimePicker)
                                                {
                                                    DateTimePicker dtp = ipnl.Controls[i] as DateTimePicker;
                                                    dtp.Format = DateTimePickerFormat.Short;
                                                    dtp.Value = DateTime.Parse(strValue[everRowIndex]);
                                                    dtp.Enabled = false;
                                                }
                                                else if (ipnl.Controls[i] is RadioButton)
                                                {
                                                    RadioButton rb = ipnl.Controls[i] as RadioButton;
                                                    rb.Checked = bool.Parse(strValue[everRowIndex]);
                                                }
                                                everRowIndex++;
                                                #endregion
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        DataTable dtAttachment = null;
        /// <summary>
        /// 显示附件
        /// </summary>
        /// <param name="pCtrnmId">问单ID</param>
        private void ShowAttachment(string pCtrnmId)
        {
            try
            {
                lstAttachment.Items.Clear();
                //获取问单附件
                dtAttachment = GetCaseAttachment(pCtrnmId);
                if (null == dtAttachment || dtAttachment.Rows.Count <= 0)
                {
                    return;
                }

                splitContainer5.Panel2Collapsed = false;
                foreach (DataRow item in dtAttachment.Rows)
                {
                    ListViewItem lvi = new ListViewItem(Path.GetFileName(item[1].ToString()));
                    lvi.SubItems.Add(item[0].ToString());
                    lvi.Name = PublicClass.FileServerPathBase + item[1].ToString();
                    lstAttachment.Items.Add(lvi);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        string whoChecked = string.Empty, whoQA = string.Empty, whoDate = string.Empty;
        string chkCW = string.Empty, chkAD = string.Empty;
        /// <summary>
        /// 获取问单明细的值
        /// </summary>
        /// <param name="pl">容器Panel</param>
        /// <returns></returns>
        private Dictionary<string, List<Dictionary<string, string>>> GetCaseAllValue(Panel pl)
        {
            Dictionary<string, List<Dictionary<string, string>>> allValue = new Dictionary<string, List<Dictionary<string, string>>>();

            foreach (Control pnl in pl.Controls)
            {
                if (pnl is TableLayoutPanel)//TableLayoutPanel
                {
                    foreach (Control cpnl in pnl.Controls)
                    {
                        if (cpnl is Panel)
                        {
                            foreach (Control lpnl in cpnl.Controls)
                            {
                                if (lpnl is TableLayoutPanel)//每个类目的TableLayoutPanel
                                {
                                    List<Dictionary<string, string>> everPanel = new List<Dictionary<string, string>>();
                                    foreach (Control ipnl in lpnl.Controls)
                                    {
                                        if (ipnl is Panel)//详细内容的每行panel
                                        {
                                            Dictionary<string, string> itemValue = new Dictionary<string, string>();
                                            StringBuilder sb = new StringBuilder();
                                            for (int i = 0; i < ipnl.Controls.Count; i++)
                                            {
                                                #region

                                                if (ipnl.Controls[i] is Label)
                                                {
                                                    continue;
                                                }
                                                //其它控件，则获取对应值
                                                if (ipnl.Controls[i] is TextBox)
                                                {
                                                    sb.Append(ipnl.Controls[i].Text.Trim());
                                                    sb.Append("@");
                                                    try
                                                    {
                                                        if (lpnl.Name.Equals("footer"))
                                                        {
                                                            if (ipnl.Controls[i - 1].Text.Contains("检查"))
                                                            {
                                                                whoChecked = ipnl.Controls[i].Text.Trim();
                                                            }
                                                            else if (ipnl.Controls[i - 1].Text.Contains("QA") || ipnl.Controls[i - 1].Text.Contains("确认"))
                                                            {
                                                                whoQA = ipnl.Controls[i].Text.Trim();
                                                            }
                                                        }
                                                    }
                                                    catch (Exception) { }
                                                }
                                                else if (ipnl.Controls[i] is RichTextBox)
                                                {
                                                    sb.Append(ipnl.Controls[i].Text.Trim());
                                                    sb.Append("@");
                                                }
                                                else if (ipnl.Controls[i] is CheckBox)
                                                {
                                                    CheckBox cb = ipnl.Controls[i] as CheckBox;
                                                    sb.Append(cb.Checked);
                                                    sb.Append("@");
                                                    try
                                                    {
                                                        if (lpnl.Name.Equals("footer"))
                                                        {
                                                            if (ipnl.Controls[i + 1].Text.Contains("CW"))
                                                            {
                                                                chkCW = cb.Checked ? "True" : "False";
                                                            }
                                                            else if (ipnl.Controls[i + 1].Text.Contains("AD"))
                                                            {
                                                                chkAD = cb.Checked ? "True" : "False";
                                                            }
                                                        }
                                                    }
                                                    catch (Exception) { }
                                                }
                                                else if (ipnl.Controls[i] is DateTimePicker)
                                                {
                                                    DateTimePicker dtp = ipnl.Controls[i] as DateTimePicker;
                                                    if ((i - 1) >= 0)
                                                    {
                                                        if (!ipnl.Controls[i - 1].Text.Contains("出货日期"))
                                                        {
                                                            sb.Append(svrDate);
                                                        }
                                                        else
                                                        {
                                                            sb.Append(dtp.Value);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        sb.Append(dtp.Value);
                                                    }
                                                    sb.Append("@");

                                                    try
                                                    {
                                                        if (lpnl.Name.Equals("footer"))
                                                        {
                                                            whoDate = svrDate;
                                                        }
                                                    }
                                                    catch (Exception) { }
                                                }
                                                else if (ipnl.Controls[i] is RadioButton)
                                                {
                                                    RadioButton rb = ipnl.Controls[i] as RadioButton;
                                                    sb.Append(rb.Checked);
                                                    sb.Append("@");
                                                }

                                                #endregion
                                            }
                                            //itemValue.Add(ipnl.Name, sb.ToString().TrimEnd('@'));
                                            itemValue.Add(ipnl.Name, sb.ToString());
                                            everPanel.Add(itemValue);
                                        }
                                    }
                                    allValue.Add(lpnl.Name, everPanel);
                                }
                            }
                        }
                    }
                }
            }
            return allValue;
        }

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
            tlp.ColumnCount = 2;
            tlp.RowCount = groups.Count() + 1;
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
                                //获取配置信息中参数的键值（它的范围一定在参数个数之内）
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
                                //newControl.Enabled = false;
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

            pnl.Controls.Add(tlp);
        }

        /// <summary>
        /// 显示已提交问单明细
        /// </summary>
        /// <param name="pFormId">问单类型ID</param>
        /// <param name="pCtrnmId">问单ID</param>
        private void ShowCaseInfo(string pFormId, string pCtrnmId)
        {
            DataTable tmpDt = null;
            try
            {
                //先获得问单明细
                tmpDt = GetCaseDetailCompleted(pCtrnmId, pFormId);

                //根据问单明细显示出来
                TemplatePreviewForChd(tmpDt, pnlWrite);

                //显示附件
                ShowAttachment(pCtrnmId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                tmpDt = null;
            }
        }

        /// <summary>
        /// 校验录入牙位信息是否符合规范
        /// </summary>
        /// <param name="pYawei">录入的牙位信息</param>
        /// <returns></returns>
        private bool CheckYawei(string pYawei)
        {
            //add by  yf 20160109
            if (pYawei.Trim() == "")
            {
                return true;
            }
            //string strRegex = @"^(\d+)(\d+|-|,|，|\d+)*$"; //yf 20160109 match function split only , -  no ，  
            //"下托","下托","下托","下托","下托","上托","上托","上托","上托","上托","上/下托"
            //"lower","lowerarch","mandibular","mandible","man","upper","upperarch","maxillary","maxilla","max","upper/lower"    
            //string strRegex = @"^(\d+)(\d+|-|,|\d+)*$";
            string strRegex = @"^(\d+|lower|lowerarch|mandibular|mandible|man|upper|upperarch|maxillary|maxilla|max|upper/lower|下托|下托|下托|下托|下托|上托|上托|上托|上托|上托|上/下托)(\d+|lower|lowerarch|mandibular|mandible|man|upper|upperarch|maxillary|maxilla|max|upper/lower|下托|下托|下托|下托|下托|上托|上托|上托|上托|上托|上/下托|-|,|\d+|lower|lowerarch|mandibular|mandible|man|upper|upperarch|maxillary|maxilla|max|upper/lower|下托|下托|下托|下托|下托|上托|上托|上托|上托|上托|上/下托)*$";
            Regex rg = new Regex(strRegex);
            if (rg.IsMatch(pYawei))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
