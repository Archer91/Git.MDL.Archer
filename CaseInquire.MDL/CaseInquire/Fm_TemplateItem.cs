using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CaseInquire.helperclass;
using System.Data.OracleClient;
using System.Collections;
using System.Web;
using System.Text.RegularExpressions;

namespace CaseInquire
{
    public partial class Fm_TemplateItem : Form, IMessageFilter
    {
        public Fm_TemplateItem()
        {
            InitializeComponent();
        }

        private void Fm_TemplateItem_Load(object sender, EventArgs e)
        {
            try
            {
                Application.AddMessageFilter(this);

                //获取问单类目信息
                cmbCategory.DisplayMember = "icat_desc";
                cmbCategory.ValueMember = "icat_code";
                cmbCategory.DataSource = GetItemCategory();
                //获取部门信息
                cmbDept.DisplayMember = "udc_value";
                cmbDept.ValueMember = "udc_code";
                cmbDept.DataSource = PublicMethod.GetDepartment();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Fm_TemplateItem_FormClosing(object sender, FormClosingEventArgs e)
        {
            PublicMethod.DisposeControl(pnlPreview);
        }


        //***第一步：选择部门
        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbType.Text = string.Empty;
                pnlPreview.Controls.Clear();
                dgvItem.DataSource = null;

                if (cmbDept.Items.Count <= 0)
                {
                    return;
                }
                //获取模板类型信息
                cmbType.DisplayMember = "oname";
                cmbType.ValueMember = "form_id";
                cmbType.DataSource = PublicMethod.GetCaseTypeByDeptCode(cmbDept.SelectedValue.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        //***第二步：选择模板类型
        DataTable dt = null;
        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbType.SelectedIndex < 0)
                {
                    return;
                }
                //获取所选模板类型已配置的信息
                dt = GetTemplateItem(cmbType.SelectedValue.ToString());

                dgvItem.DataSource = dt;
                dgvItem.Columns[0].Visible = false;
                dgvItem.Columns[1].Visible = false;
                dgvItem.Columns[2].Visible = false;
                //模板预览
                TemplatePreview(dt, pnlPreview);
                //类目选择刷新
                cmbCategory_SelectedIndexChanged(null, null);
                btnCategory.Enabled = true;
                txtYawei.ReadOnly = true;
                rtbRowInfo.ReadOnly = true;
                isEdit = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvItem_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            PublicMethod.dgv_RowPostPaint(sender, e);
        }


        //***第三步：行具体配置
        Dictionary<int, string> everRowParamType = new Dictionary<int, string>();
        int paramIndex = 0;
        bool isEdit = false;//是否为修改
        //选择类目
        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (null != sender && cmbCategory.Focused) //为程序前端人为操作
                {
                    if (isEdit)
                    {
                        //为修改状态时
                    }
                    else
                    {
                        //当选择的类目为标题时，就将模板类型的名称直接引用
                        if (cmbCategory.SelectedValue.ToString().Equals("title"))
                        {
                            rtbRowInfo.Text = cmbType.Text.Split('-')[0];
                        }
                        else
                        {
                            rtbRowInfo.Text = string.Empty;
                        }
                        rtbRowInfoEng.Text = string.Empty;
                        lblShow.Text = string.Empty;
                        txtYawei.Text = string.Empty;

                        paramIndex = 0;
                        everRowParamType = new Dictionary<int, string>();
                    }
                }
                else //为程序内刷新
                {
                    //当选择的类目为标题时，就将模板类型的名称直接引用
                    if (cmbCategory.SelectedValue.ToString().Equals("title"))
                    {
                        rtbRowInfo.Text = cmbType.Text.Split('-')[0];
                    }
                    else
                    {
                        rtbRowInfo.Text = string.Empty;
                    }
                    rtbRowInfoEng.Text = string.Empty;
                    lblShow.Text = string.Empty;
                    txtYawei.Text = string.Empty;

                    paramIndex = 0;
                    everRowParamType = new Dictionary<int, string>();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                //paramIndex = 0;
                //everRowParamType = new Dictionary<int, string>();
            }
        }

        //添加参数
        private void btnParam_Click(object sender, EventArgs e)
        {
            try
            {
                lblShow.Text = string.Empty;
                if (cmbParamType.Text.Trim().Length <= 0)
                {
                    lblShow.Text = "请选择参数类型";
                    return;
                }
                rtbRowInfo.Text += "#{" + paramIndex + "}#";
                //临时保存该行参数
                everRowParamType.Add(paramIndex, cmbParamType.Text.Trim());
                txtYawei.Text += chkYawei.Checked ? "1," : "0," ;

                paramIndex++;
                cmbParamType.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                chkYawei.Checked = false;
            }
        }

        //添加文字
        private void btnWord_Click(object sender, EventArgs e)
        {
            lblShow.Text = string.Empty;
            if (txtWord.Text.Trim().Length <= 0)
            {
                lblShow.Text = "请输入文本";
                return;
            }
            rtbRowInfo.Text += txtWord.Text.Trim();
            txtWord.Text = string.Empty;
        }

        //当前行配置完成
        private void btnCategory_Click(object sender, EventArgs e)
        {
            lblShow.Text = string.Empty;
            try
            {
                //验证该问单类型是否已创建过业务问单
                if (IsCase(cmbType.SelectedValue.ToString()))
                {
                    MessageBox.Show("当前问单类型已有业务问单，故模板不能更改，请创建新的问单模板！", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (string.IsNullOrEmpty(rtbRowInfo.Text.Trim()))
                {
                    lblShow.Text = "行配置信息不能为空";
                    return;
                }
                if (string.IsNullOrEmpty(rtbRowInfoEng.Text.Trim()))
                {
                    lblShow.Text = "请复制行配置信息到英文翻译栏位进行翻译";
                    rtbRowInfo.SelectAll();
                    rtbRowInfo.Focus();
                    return;
                }

                //处理参数类型
                StringBuilder sb = new StringBuilder();
                string strParamType = string.Empty;
                string strRowInfo = rtbRowInfo.Text.Trim();
                string strRowInfoEng = rtbRowInfoEng.Text.Trim();
                if (everRowParamType != null && everRowParamType.Count > 0)
                {
                    //校验配置信息中的参数是否正确
                    if (!IsSame(ref strRowInfo,ref strRowInfoEng))
                    {
                        MessageBox.Show("配置信息中参数异常：\n中英文配置信息中的参数个数不一致；\n行配置信息中的参数个数大于由添加参数操作的参数个数；\n行配置信息中参数的键值重复；\n行配置信息中参数的键值超出了界限；\n中英文配置信息中参数键值不相符","MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    foreach (int i in everRowParamType.Keys)
                    {
                        sb.Append(everRowParamType[i]);
                        sb.Append(',');
                    }
                    //strParamType = sb.ToString().TrimEnd(',');//移除最后一个字符
                    strParamType = sb.ToString();
                }

                //处理参数是否为牙位标记
                string[] yaweiStr = txtYawei.Text.Trim().Length <= 0 ? new string[]{} : txtYawei.Text.Trim().TrimEnd(',').Split(',');
                if (yaweiStr.Length != everRowParamType.Count)
                {
                    MessageBox.Show(@"牙位标记与行配置信息中的参数个数不一致", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                string rgStr = @"^[0-1]$";
                Regex rg = new Regex(rgStr);
                foreach (string item in yaweiStr)
                {
                    if (!rg.IsMatch(item))
                    {
                        MessageBox.Show("牙位标记只能存在0,1", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                //保存当前类目下的行配置信息
                SaveTemplateItem(cmbType.SelectedValue.ToString(), "", cmbCategory.SelectedValue.ToString(), strRowInfo, strRowInfoEng, strParamType, everRowParamType.Count, nudLineNo.Value,txtYawei.Text.Trim());

                //刷新模板配置信息
                cmbType_SelectedIndexChanged(null, null);

                txtYawei.Text = string.Empty;
                rtbRowInfo.Text = string.Empty;
                rtbRowInfoEng.Text = string.Empty;
                lblShow.Text = string.Empty;
                nudLineNo.Value += 1;

                paramIndex = 0;
                everRowParamType = new Dictionary<int, string>();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbCategory_SelectedIndexChanged(null, null);
            }
        }

        //删除该行配置信息
        private void tsmiDelCurrentRowInfo_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvItem.SelectedRows.Count > 0)
                {
                    //验证该问单类型是否已创建过业务问单
                    if (IsCase(cmbType.SelectedValue.ToString()))
                    {
                        MessageBox.Show("当前问单类型已有业务问单，故模板不能更改，请创建新的问单模板！", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    //删除配置行信息（失效，但并不是真正删除）
                    DeleteTemplateItem(cmbType.SelectedValue.ToString(), dgvItem.CurrentRow.Cells[0].Value.ToString());
                    //刷新配置信息
                    cmbType_SelectedIndexChanged(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //修改该行配置
        private void tsmiEditCurrentRowInfo_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvItem.SelectedRows.Count > 0)
                {
                    //验证该问单类型是否已创建过业务问单
                    if (IsCase(cmbType.SelectedValue.ToString()))
                    {
                        MessageBox.Show("当前问单类型已有业务问单，故模板不能更改，请创建新的问单模板！", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    //显示行配置信息
                    ShowCurrentRowInfo();

                    isEdit = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        //***第四步：模板复制
        private void btnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                Fm_TemplateCopy templateCopy = new Fm_TemplateCopy();
                templateCopy.ShowDialog();

                Fm_TemplateItem_Load(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        
    }
}
