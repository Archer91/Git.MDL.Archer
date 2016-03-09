using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CaseInquire.helperclass;

namespace CaseInquire
{
    public partial class Fm_TemplateCopy : Form
    {
        public Fm_TemplateCopy()
        {
            InitializeComponent();
        }

        private void Fm_TemplateCopy_Load(object sender, EventArgs e)
        {
            try
            {
                //获取部门
                DataTable tmpDt = PublicMethod.GetDepartment();
                cmbSDept.DisplayMember = "udc_value";
                cmbSDept.ValueMember = "udc_code";
                cmbSDept.DataSource = tmpDt;

                cmbTDept.DisplayMember = "udc_value";
                cmbTDept.ValueMember = "udc_code";
                cmbTDept.DataSource = tmpDt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        #region 第一步：选择源部门

        private void cmbSDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbSDept.Items.Count <= 0 || cmbSDept.SelectedIndex < 0)
                {
                    return;
                }

                //获取已经存在的模板以便复用
                cmbSType.DisplayMember = "oname";
                cmbSType.ValueMember = "form_id";
                cmbSType.DataSource = GetTemplateTypeCopy(cmbSDept.SelectedValue.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 获取已存在的模板类型以便复用
        /// </summary>
        /// <param name="pDeptCode">部门Code</param>
        /// <returns></returns>
        private DataTable GetTemplateTypeCopy(string pDeptCode)
        {
            string sqlStr = string.Format(
            @"select distinct b.form_id,b.form_name || '-' || b.form_ver oname 
            from ztci_frmd_det a  
            join ztci_form_master b on a.frmd_form_id = b.form_id 
            where b.form_department = '{0}' 
            order by oname", pDeptCode);
            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
        }

        #endregion 第一步：选择源部门


        #region 第二步：选择目标部门

        private void cmbTDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbTType.Text = string.Empty;
                cmbTCode.Text = string.Empty;
                if (cmbTDept.Items.Count <= 0 || cmbTDept.SelectedIndex < 0)
                {
                    return;
                }

                //获取模板类型
                DataView dv = new DataView();
                dv.Table = PublicMethod.GetCaseTypeByDeptCode(cmbTDept.SelectedValue.ToString());
                cmbTType.DisplayMember = "oname";
                cmbTType.ValueMember = "form_id";
                cmbTType.DataSource = dv;

                cmbTCode.DisplayMember = "form_code";
                cmbTCode.ValueMember = "form_code";
                cmbTCode.DataSource = dv;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion 第二步：选择目标部门


        #region  第三步：模板复制

        private void btnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbSType.Text.Trim().Length <= 0)
                {
                    MessageBox.Show("无源模板供复制！", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (null != cmbTType.SelectedValue && IsCopy(cmbTType.SelectedValue.ToString()))
                {
                    MessageBox.Show("目标模板已进行模板设计，不能进行复制操作！", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //进行模板复用
                if (MessageBox.Show("确定要进行模板复制？", "MDL-提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.OK)
                {
                    if (null == cmbTType.SelectedItem || null == cmbTType.SelectedValue)
                    {
                        if (cmbTCode.Text.Trim().Length <= 0)
                        {
                            MessageBox.Show("新模板类型未输入模板编码！", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        string result = CheckTemplateType(cmbTType.Text.Trim());
                        if (string.IsNullOrEmpty(result))
                        {
                            MessageBox.Show("模板复制失败", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        TemplateItemCopy(result, cmbSType.SelectedValue.ToString());
                    }
                    else
                    {
                        TemplateItemCopy(cmbTType.SelectedValue.ToString(), cmbSType.SelectedValue.ToString());
                    }

                    Fm_TemplateCopy_Load(null, null);
                    MessageBox.Show("模板复制成功！", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 是否已配置，已配置则不能复用，未配置则可以进行复用
        /// </summary>
        /// <param name="pFormId">模板类型ID</param>
        /// <returns>true表示已配置，false表示未配置</returns>
        private bool IsCopy(string pFormId)
        {
            string sqlStr = string.Format(@"select count(*) from ztci_frmd_det where frmd_form_id = '{0}'", pFormId);
            if (Convert.ToInt32(ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0].Rows[0][0].ToString()) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 当目标模板类型不存在（新添加）则进行名称校验，并自动保存新的模板类型
        /// </summary>
        /// <param name="pName">模板类型名</param>
        /// <returns>新模板类型的FormId</returns>
        private string CheckTemplateType(string pName)
        {
            if (pName.IndexOf('-') != pName.LastIndexOf('-'))
            {
                throw new Exception("目标模板名称格式不正确，应为【名称-版本】");
            }
            string[] tmpStr = pName.Split('-');
            string sqlStr = string.Format(@"select form_id from ztci_form_master where form_name='{0}' and form_ver='{1}'", tmpStr[0], tmpStr[1]);
            DataTable tmpDt = ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
            if (tmpDt != null && tmpDt.Rows.Count > 0)
            {
                throw new Exception("模板类型【" + pName + "】已存在");
            }
            string fmId = ZComm1.Oracle.DB.GetDSFromSql1("select zsci_form_mstr_seq.nextval from dual").Tables[0].Rows[0][0].ToString();
            sqlStr = string.Format(
            @"insert into ztci_form_master(form_id,form_code,form_department,form_name,form_ver) values('{0}','{1}','{2}','{3}','{4}')", 
            fmId, cmbTCode.Text.Trim().ToUpper(), cmbTDept.SelectedValue.ToString(), tmpStr[0], tmpStr[1]);

            if (ZComm1.Oracle.DB.ExecuteFromSql(sqlStr))
            {
                return fmId;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 模板复用
        /// </summary>
        /// <param name="pFormId">目标模板类型ID</param>
        /// <param name="pFormIdCopy">源模板类型ID</param>
        /// <returns></returns>
        private string TemplateItemCopy(string pFormId, string pFormIdCopy)
        {
            //复制模板内容
            string sqlStr = string.Format(
            @"insert into ztci_item_info(item_code,item_category,item_content,item_content_eng,item_parameter_count,
                item_parameters_type,item_status,item_crt_by,item_category_p2,item_category_p3,item_parameters_is_yawei) 
            select a.item_code,a.item_category,a.item_content,a.item_content_eng,a.item_parameter_count,
                a.item_parameters_type,a.item_status,'{0}','{1}',a.item_id,item_parameters_is_yawei  
            from ztci_item_info a 
            join ztci_frmd_det b on a.item_id = b.frmd_item_id 
            where b.frmd_form_id = '{2}'", PublicClass.LoginName, pFormId, pFormIdCopy);

            //复制模板关联
            string sqlStr2 = string.Format(
            @"insert into ztci_frmd_det(frmd_form_id,frmd_lineno,frmd_item_id,frmd_ststus,frmd_crt_by) 
            select '{0}',b.frmd_lineno,a.item_id,b.frmd_ststus,'{1}' 
            from ztci_item_info a 
            join ztci_frmd_det  b on a.Item_Category_P3 = b.frmd_item_id and a.Item_Category_P2 = '{2}' 
            where b.frmd_form_id = '{3}'", pFormId, PublicClass.LoginName, pFormId, pFormIdCopy);

            //复制模板参数控制
            //string sqlStr3 = string.Format("insert into ztci_ictr_item_control(ictr_item_id,ictr_parameter_index,ictr_control_type,ictr_control_block,ictr_status,ictr_crt_by) select a.item_id,b.ictr_parameter_index,b.ictr_control_type,b.ictr_control_block,b.ictr_status,'{0}' from ztci_item_info a join ztci_ictr_item_control b on a.item_category_p3 = b.ictr_item_id", PublicClass.LoginName);

            List<ZComm1.StrI> ls = new List<ZComm1.StrI>();
            ls.AddRange(new ZComm1.StrI[]{
            new ZComm1.StrI(sqlStr,0),
            new ZComm1.StrI(sqlStr2,1)
            });
            return ZComm1.Oracle.DB.ExeTransSI(ls);

        }
        
        #endregion 第三步：模板复制
        
    }
}
