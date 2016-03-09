using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using CaseInquire.helperclass;
using System.Text.RegularExpressions;
using System.Drawing;

namespace CaseInquire
{
    /*
     * 模板内容--自定义方法分部类
     */
    partial class Fm_TemplateItem
    {
        /// <summary>
        /// 禁用鼠标滚动
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == 522)
            {
                return true;
            }

            else
            {
                return false;
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
                         group m by m.Field<string>("所属类目") into result
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

            int topPoint = 0, leftPoint = 0, groupRowIndex = 0;
            foreach (var group in groups)
            {
                #region
                //添加类目
                Label lblCategory = new Label();
                lblCategory.Text = group.Key;
                lblCategory.Font = new System.Drawing.Font("宋体", 10f, FontStyle.Bold);
                lblCategory.AutoSize = true;
                lblCategory.Dock = DockStyle.Fill;
                lblCategory.TextAlign = ContentAlignment.MiddleRight;
                lblCategory.Show();
                tlp.SetColumn(lblCategory, 0);
                tlp.SetRow(lblCategory, groupRowIndex);
                tlp.Controls.Add(lblCategory);

                Panel childPnl = new Panel();
                childPnl.AutoSize = true;
                childPnl.BorderStyle = BorderStyle.FixedSingle;
                childPnl.Dock = DockStyle.Fill;
                childPnl.Show();
                tlp.SetColumn(childPnl, 1);
                tlp.SetRow(childPnl, groupRowIndex);
                tlp.Controls.Add(childPnl);

                topPoint = 0;
                foreach (DataRow item in group)
                {
                    leftPoint = 8;
                    //添加内容
                    if (item["参数个数"].ToString().Equals("0"))
                    {
                        #region
                        //当行没有参数时就为纯文本
                        Label lblItem = new Label();
                        lblItem.Text = item["配置明细"].ToString().Replace("@", "");
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
                        string regexStr = @"\{\d{0,}\}";

                        //拆分参数类型
                        string[] paraTypeArray = item["参数类型"].ToString().Trim().Split(',');
                        //拆分模板内容
                        string[] contentArray = item["配置明细"].ToString().Trim().Split('#');
                        foreach (string m in contentArray)
                        {
                            //为空则忽略
                            if (string.IsNullOrEmpty(m))
                            {
                                continue;
                            }

                            if (Regex.IsMatch(m, regexStr)) //满足参数占位格式
                            {
                                #region
                                string regexStr2 = @"(\d+)";
                                paraIndex = Convert.ToInt32(Regex.Match(m, regexStr2).Value);

                                //利用反射，可根据控件类型字符串来创建对应控件
                                string assemblyQualifiedName = typeof(System.Windows.Forms.Form).AssemblyQualifiedName;
                                string assemblyInformation = assemblyQualifiedName.Substring(assemblyQualifiedName.IndexOf(","));
                                Type ty = Type.GetType("System.Windows.Forms." + paraTypeArray[paraIndex] + assemblyInformation);
                                Control newControl = (Control)System.Activator.CreateInstance(ty);
                                newControl.Left = leftPoint;
                                newControl.Top = topPoint + 1;

                                if (ty.Name.Equals("CheckBox"))
                                {
                                    CheckBox cb = new CheckBox();
                                    cb.AutoSize = true;
                                    cb.Left = leftPoint;
                                    cb.Top = topPoint + 3;
                                    newControl = cb;
                                }
                                else if (ty.Name.Equals("RichTextBox"))
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
                                    dtp.Top = topPoint + 1;
                                    dtp.Format = DateTimePickerFormat.Short;
                                    newControl = dtp;
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
                                lblItem.Text = m.Replace("@", "");
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
                }
                groupRowIndex++;
                #endregion
            }
            pnl.Controls.Add(tlp);
        }

        /// <summary>
        /// 判别两个字符串中的参数是否正确
        /// </summary>
        /// <param name="pFirstTxt">第一个字符串</param>
        /// <param name="pSecondTxt">第二个字符串</param>
        /// <returns>true表示参数正确，false表示参数不正确</returns>
        private bool IsSame(ref string pFirstTxt, ref string pSecondTxt)
        {
            if (string.IsNullOrEmpty(pFirstTxt) || string.IsNullOrEmpty(pSecondTxt))
            {
                return false;
            }

            string regexStr = @"(#\{\d{0,}\}#)";//例如#{0}#
            Regex rg = new Regex(regexStr);
            MatchCollection m1 = rg.Matches(pFirstTxt);
            MatchCollection m2 = rg.Matches(pSecondTxt);
            if (m1.Count == m2.Count)
            {
                //（已中文配置信息为准）
                if (m1.Count <= 0)
                {
                    //当配置信息中没有参数(多半是修改操作）
                    everRowParamType.Clear();
                    return true;
                }
                else if (m1.Count > everRowParamType.Count)
                {
                    //如果配置信息中的参数个数大于由添加参数操作的参数个数，视为错误
                    return false;
                }
                else if (m1.Count <= everRowParamType.Count)
                {
                    #region
                    //配置信息中的参数个数小于由添加参数操作的参数个数(多半是修改操作）
                    //减少添加参数操作所存储的信息
                    List<int> hasKeyEng = new List<int>();
                    regexStr = @"(\d+)";
                    rg = new Regex(regexStr);
                    int m = -1;
                    //英文配置信息
                    foreach (Match item in m2)
                    {
                        m = Convert.ToInt32(rg.Match(item.ToString()).Value);
                        if (everRowParamType.ContainsKey(m) && !hasKeyEng.Contains(m))//是否存在这个键值，不存在则视为错误
                        {
                            hasKeyEng.Add(m);
                            continue;
                        }
                        else
                        {
                            return false;
                        }
                    }

                    List<int> hasKey = new List<int>();
                    m = -1;
                    //中文配置信息
                    foreach (Match item in m1)
                    {
                        m = Convert.ToInt32(rg.Match(item.ToString()).Value);
                        if (everRowParamType.ContainsKey(m) && !hasKey.Contains(m))//是否存在这个键值，不存在则视为错误
                        {
                            hasKey.Add(m);
                            continue;
                        }
                        else
                        {
                            return false;
                        }
                    }

                    //如果中英文配置信息中参数的键值不相符，视为错误
                    foreach (int item in hasKey)
                    {
                        if (!hasKeyEng.Contains(item))
                        {
                            return false;
                        }
                    }
                    foreach (int item in hasKeyEng)
                    {
                        if (!hasKey.Contains(item))
                        {
                            return false;
                        }
                    }
                    #endregion

                    //if (hasKey.Count > 0)
                    //{
                    var listParamType = new List<int>();
                    int tmpN = everRowParamType.Count;
                    for (int i = 0; i <= tmpN; i++)
                    {
                        if (hasKey.Contains(i))
                        {
                            listParamType.Add(i);
                            continue;
                        }
                        else
                        {
                            everRowParamType.Remove(i);//移除不存在的键值
                        }
                    }
                    //}

                    //对配置信息中的参数键值进行重新设定
                    for (int i = 0; i < listParamType.Count; i++)
                    {
                        pFirstTxt = pFirstTxt.Replace("#{" + listParamType[i] + "}#", "#{" + i + "}#");
                        pSecondTxt = pSecondTxt.Replace("#{" + listParamType[i] + "}#", "#{" + i + "}#");
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 根据所选择的行，显示所配置的信息
        /// </summary>
        private void ShowCurrentRowInfo()
        {
            paramIndex = 0;
            everRowParamType = new Dictionary<int, string>();

            cmbCategory.SelectedValue = dgvItem.CurrentRow.Cells["item_category"].Value;
            nudLineNo.Value = Convert.ToDecimal(dgvItem.CurrentRow.Cells["所在行号"].Value);
            rtbRowInfo.Text = dgvItem.CurrentRow.Cells["配置明细"].Value.ToString();
            rtbRowInfoEng.Text = dgvItem.CurrentRow.Cells["英文配置明细"].Value.ToString();
            txtYawei.Text = dgvItem.CurrentRow.Cells["牙位标记"].Value.ToString();

            int typeCount = Convert.ToInt32(dgvItem.CurrentRow.Cells["参数个数"].Value.ToString());
            if (typeCount > 0)
            {
                string[] typeStr = dgvItem.CurrentRow.Cells["参数类型"].Value.ToString().Split(',');

                //如果存在参数
                while (paramIndex < typeCount)
                {
                    everRowParamType.Add(paramIndex, typeStr[paramIndex]);
                    paramIndex++;
                }
            }
            rtbRowInfo.ReadOnly = false;
            txtYawei.ReadOnly = false;
        }
    
    }
}
