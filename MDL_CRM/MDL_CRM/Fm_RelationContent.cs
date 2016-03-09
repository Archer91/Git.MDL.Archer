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
    public partial class Fm_RelationContent : Form
    {
         int m_iType=0;
         string m_strJobNo=string.Empty;
         string m_strOrdNo=string.Empty;
         string m_Entity = string.Empty;

        Fm_JobItem.delegateScan m_dscan;
        public Fm_JobItem.delegateScan dscan
        {
            set { m_dscan = value; }
        }

        public Fm_RelationContent()
        {
            InitializeComponent();
        }

        public Fm_RelationContent(string pEntity, string pSO, string pWO, int pType) : this() 
        {
            m_Entity = pEntity;
            m_strOrdNo = pSO;
            m_strJobNo = pWO;
            m_iType = pType;
        }

        private void Fm_RelationContent_Load(object sender, EventArgs e)
        {
            try
            {
                tabControl1.SelectedIndex = m_iType;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="strsql"></param>
        private void loadData(DataGridView grid, string strsql)
        {
            grid.AutoGenerateColumns = false;
            grid.ReadOnly = true;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToAddRows = false;
            DataTable dt = Dal.GetDataTable(strsql);
            grid.DataSource = dt;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    {
                        //工作进度
                        sb.Clear();
                        sb.AppendFormat(@"SELECT REGISTRATION.CHKP_ID,REGISTRATION.CPRE_CREATEBY,REGISTRATION.CPRE_CREATEDATE, CHKP_DESCRIPTION 
                                    FROM REGISTRATION LEFT JOIN CHECK_POINT ON REGISTRATION.CHKP_ID= CHECK_POINT.CHKP_ID 
                                    WHERE JOBM_NO='{0}' ORDER BY REGISTRATION.CPRE_CREATEDATE", m_strJobNo);
                        loadData(ProcessGrid, sb.ToString());
                        break;
                    }
                case 1:
                    {
                        #region 相关工作单
                        if (m_Entity.Equals(pubcls.MDLEntity))//若为深圳公司，则走旧有MDMS系统
                        {
                            sb.Clear();
                            sb.AppendFormat(@"select JOBM_NO,JOBM_RELATEJOB,JOBM_CUSTCASENO,JOBM_RECEIVEDATE,JOBM_DELIVERYDATE,
                                    GET_JOB_NATURE_Name(JOBM_JOB_NATURE) JOBM_JOB_NATURE,GET_StageName(JOBM_STAGE) JOBM_STAGE 
                                    from job_order m  start with  m.JOBM_NO in(select JOBM_NO from
                            ( select JOBM_NO,JOBM_RELATEJOB from job_order m  start with  m.JOBM_NO='{0}'  connect
                             by   prior m.JOBM_RELATEJOB= m.JOBM_NO 
                             ) where JOBM_RELATEJOB is null)  connect
                             by    m.JOBM_RELATEJOB=prior m.JOBM_NO order by JOBM_NO", m_strJobNo);
                        }
                        else
                        {
                            sb.Clear();
                            sb.AppendFormat(@"select JOBM_NO,JOBM_RELATEJOB,JOBM_CUSTCASENO,JOBM_RECEIVEDATE,JOBM_DELIVERYDATE,
                                    GET_JOB_NATURE_Name(JOBM_JOB_NATURE) JOBM_JOB_NATURE,GET_StageName(JOBM_STAGE) JOBM_STAGE 
                                    from zt00_job_order m  start with  m.JOBM_NO in(select JOBM_NO from 
                            ( select JOBM_NO,JOBM_RELATEJOB from zt00_job_order m  start with  m.JOBM_NO='{0}'  connect
                             by   prior m.JOBM_RELATEJOB= m.JOBM_NO 
                             ) where JOBM_RELATEJOB is null)  connect
                             by    m.JOBM_RELATEJOB=prior m.JOBM_NO order by JOBM_NO", m_strJobNo);
                        }
                        loadData(JobGrid, sb.ToString());
                        break;
                        #endregion 相关工作单
                    }
                case 2:
                    {
                        #region 相关工作单明细
                        if (m_Entity.Equals(pubcls.MDLEntity))
                        {
                            sb.Clear();
                            sb.AppendFormat(@" WITH JOBLIST AS 
                                   (select JOBM_NO,JOBM_RECEIVEDATE from job_order m  start with  m.JOBM_NO in(select JOBM_NO from 
                                   select JOBM_NO,JOBM_RELATEJOB from job_order m  start with  m.JOBM_NO='{0}'  connect
                                   by   prior m.JOBM_RELATEJOB= m.JOBM_NO  
                                    ) where JOBM_RELATEJOB is null)  connect
                                    by    m.JOBM_RELATEJOB=prior m.JOBM_NO 
                                    )   SELECT A.*,jdtl_group_id, jdtl_prodcode, prod_desc,jdtl_other_name, jdtl_qty, jdtl_unit, jdtl_toothpos,jdtl_charge_yn,jdtl_toothcolor FROM JOBLIST A INNER JOIN (SELECT JOBM_NO,jdtl_group_id, jdtl_prodcode, prod_desc,jdtl_other_name, jdtl_qty, jdtl_unit, jdtl_toothpos, GET_UD_Value('MDLCRM','SO','CHARGE',jdtl_charge_yn) jdtl_charge_yn,jdtl_toothcolor FROM job_product left join product ON
                                           jdtl_prodcode=prod_code
                                     ) B ON  A.JOBM_NO=B.JOBM_NO", m_strJobNo);
                        }
                        else
                        {
                            sb.Clear();
                            sb.AppendFormat(@" WITH JOBLIST AS
                                     (select JOBM_NO,JOBM_RECEIVEDATE from zt00_job_order m  start with  m.JOBM_NO in(select JOBM_NO from 
                                    ( select JOBM_NO,JOBM_RELATEJOB from zt00_job_order m  start with  m.JOBM_NO='{0}'  connect
                                     by   prior m.JOBM_RELATEJOB= m.JOBM_NO 
                                     ) where JOBM_RELATEJOB is null)  connect
                                     by    m.JOBM_RELATEJOB=prior m.JOBM_NO
                                     )  SELECT A.*,jdtl_group_id, jdtl_prodcode, prod_desc,jdtl_other_name, jdtl_qty, jdtl_unit, jdtl_toothpos,jdtl_charge_yn,jdtl_toothcolor FROM JOBLIST A INNER JOIN
                                    (SELECT JOBM_NO,jdtl_group_id, jdtl_prodcode, prod_desc,jdtl_other_name, jdtl_qty, jdtl_unit, jdtl_toothpos, GET_UD_Value('MDLCRM','SO','CHARGE',jdtl_charge_yn) jdtl_charge_yn,jdtl_toothcolor FROM zt00_job_product left join product ON
                                            jdtl_prodcode=prod_code
                                      ) B ON  A.JOBM_NO=B.JOBM_NO", m_strJobNo);
                        }
                        loadData(JobDetailGrid, sb.ToString());
                        break;
                        #endregion 相关工作单明细
                    }
                case 3:
                    {
                        #region 相关发票
                        if (m_Entity.Equals(pubcls.MDLEntity))
                        {
                            sb.Clear();
                            sb.AppendFormat(@"select DISTINCT a.arhr_invno, arhr_date,case arhr_status when 'C' then '完成'  when 'N' THEN '生成中' else '取消' end arhr_status  
                                    from INVOICE a inner join INVOICE_DTL b on a.arhr_invno= b.arhr_invno 
                                    where ardt_jobno='{0}' order by arhr_date", m_strJobNo);
                        }
                        else
                        {
                            sb.Clear();
                            sb.AppendFormat(@"select DISTINCT a.invh_invno arhr_invno, invh_date arhr_date,
                                    case invh_status when 'C' then '完成'  when 'N' THEN '生成中' else '取消' end arhr_status  
                                    from ZT10_INVOICE_MSTR a inner join zt10_invoice_dtl b on a.invh_invno= b.invd_invno 
                                    where invd_jobno='{0}' order by invh_date", m_strJobNo);
                        }
                        loadData(invoiceGrid, sb.ToString());
                        break;
                        #endregion 相关发票
                    }
                case 4:
                    {
                        //credit note
                        sb.Clear();
                        sb.AppendFormat(@"select DISTINCT a.cnhr_no,cnhr_date ,case cnhr_status when 'F' then '审核'  when 'N' THEN '待审' else '取消' end cnhr_status 
                                    from CREDIT_NOTE a inner join CREDIT_NOTE_DTL b on a.cnhr_no=b.cnhr_no 
                                    where cndt_jobno='{0}' order by cnhr_date",m_strJobNo);
                        loadData(CreditGrid, sb.ToString());
                        break;
                    }
                case 5:
                    {
                        #region 估计日期
                        if (m_Entity.Equals(pubcls.MDLEntity))
                        {
                            sb.Clear();
                            sb.AppendFormat(@"SELECT jrdl_requestdate, GET_TIMENAME(jrdl_timf_code) jrdl_timf_code, jrdl_reason, jrdl_lmodby, jrdl_lmoddate 
                                    FROM JOB_REQUESTDATE_LOG   
                                    WHERE JOBM_NO='{0}' ORDER BY jrdl_lmoddate",m_strJobNo);
                        }
                        else
                        {
                            sb.Clear();
                            sb.AppendFormat(@"SELECT jrdl_requestdate, GET_TIMENAME(jrdl_timf_code) jrdl_timf_code, jrdl_reason, jrdl_lmodby, jrdl_lmoddate 
                                    FROM zt00_JOB_REQUESTDATE_LOG   
                                    WHERE JOBM_NO='{0}' ORDER BY jrdl_lmoddate",m_strJobNo);
                        }
                        loadData(chGrid, sb.ToString());
                        break;
                        #endregion 估计日期
                    }
                case 6:
                    {
                        #region 状态
                        if (m_Entity.Equals(pubcls.MDLEntity))
                        {
                            sb.Clear();
                            sb.AppendFormat(@" select GET_StageName(jsgl_stage) jsgl_stage, jsgl_lmodby, jsgl_lmoddate 
                                    FROM JOB_STAGE_LOG   
                                    WHERE JOBM_NO='{0}' ORDER BY jsgl_lmoddate",m_strJobNo);
                        }
                        else
                        {
                            sb.Clear();
                            sb.AppendFormat(@" select GET_StageName(jsgl_stage) jsgl_stage, jsgl_lmodby, jsgl_lmoddate 
                                    FROM zt00_JOB_STAGE_LOG   
                                    WHERE JOBM_NO='{0}' ORDER BY jsgl_lmoddate",m_strJobNo);
                        }
                        loadData(StageGrid, sb.ToString());
                        break;
                        #endregion 状态
                    }
                case 7:
                    {
                        #region 折扣/额外收费
                        if (m_Entity.Equals(pubcls.MDLEntity))
                        {
                            sb.Clear();
                            sb.AppendFormat(@" select jdsl_discount, jdsl_lmodby, jdsl_lmoddate 
                                    FROM  JOB_DISCOUNT_LOG   
                                    WHERE JOBM_NO='{0}' ORDER BY jdsl_lmoddate",m_strJobNo);
                        }
                        else
                        {
                            sb.Clear();
                            sb.AppendFormat(@" select jdsl_discount, jdsl_lmodby, jdsl_lmoddate 
                                    FROM  zt00_JOB_DISCOUNT_LOG   
                                    WHERE JOBM_NO='{0}' ORDER BY jdsl_lmoddate",m_strJobNo);
                        }
                        loadData(DisGrid, sb.ToString());
                        break;
                        #endregion 折扣/额外收费
                    }
                case 8:
                    {
                        #region 扩充信息
//                        if (m_Entity.Equals(pubcls.MDLEntity))
//                        {
//                            sb.Clear();
//                            sb.AppendFormat(@" select JADD_INFO_01,JADD_INFO_02,JADD_INFO_03,JADD_INFO_04,JADD_INFO_05,JADD_INFO_06,
//                            JADD_INFO_07,JADD_INFO_08,JADD_INFO_09,JADD_INFO_10 
//                                    FROM  JOB_ADDINFO  WHERE JOBM_NO='{0}'",m_strJobNo);
//                        }
//                        else
//                        {
//                            sb.Clear();
//                            sb.AppendFormat(@" select JADD_INFO_01,JADD_INFO_02,JADD_INFO_03,JADD_INFO_04,JADD_INFO_05,JADD_INFO_06,
//                            JADD_INFO_07,JADD_INFO_08,JADD_INFO_09,JADD_INFO_10 
//                                    FROM  zt00_JOB_ADDINFO  WHERE JOBM_NO='{0}'",m_strJobNo);
//                        }
//                        DataTable dt = Dal.GetDataTable(sb.ToString());
//                        Dal.LoadDefControlValue(panel2.Controls, dt, 0);
                        txtJobNo.Text = m_strJobNo;
                        btnAddInfoSave.Enabled = true;
                        break;
                        #endregion 扩充信息
                    }
                case 9:
                    {
                        #region 相关订单
                        sb.Clear();
                        sb.AppendFormat(@"select SO_NO,SO_DATE,SO_DENTNAME,SO_CUSTCASENO,GET_StageName(SO_STAGE) SO_STAGEDesc,SO_RECEIVEDATE,SO_DELIVERYDATE 
                                    from ZT10_SO_SALES_ORDER m  start with  m.SO_NO in(select SO_NO from
                        ( select SO_NO,SO_RELATE_SO from ZT10_SO_SALES_ORDER m  start with  m.SO_NO='{0}'  connect
                         by   prior m.SO_RELATE_SO= m.SO_NO  
                        ) where SO_RELATE_SO is null)  connect
                        by    m.SO_RELATE_SO=prior m.SO_NO order by SO_NO", m_strOrdNo);
                        loadData(OrdGrid, sb.ToString());
                        break;
                        #endregion 相关订单
                    }
                case 10:
                    {
                        #region 送货要求
                        if (m_Entity.Equals(pubcls.MDLEntity))
                        {
                            sb.Clear();
                            sb.AppendFormat(@"select jobm_no jadd_jobm_no,jadd_lineno,JADD_INFO_07 JADD_INFO_01,JADD_INFO_08 JADD_INFO_02,JADD_INFO_09 JADD_INFO_03   
                                    FROM  JOB_ADDINFO  WHERE JOBM_NO='{0}' and jadd_info_09='送货要求'",m_strJobNo);
                        }
                        else
                        {
                            sb.Clear();
                            sb.AppendFormat(@"select jobm_no jadd_jobm_no,jadd_lineno,JADD_INFO_01,JADD_INFO_02,JADD_INFO_03  
                                    FROM  zt00_JOB_ADDINFO  WHERE JOBM_NO='{0}' and jadd_info_03='送货要求'", m_strJobNo);
                        }
                        loadData(dgvShipReq, sb.ToString());
                        break;
                        #endregion 送货要求
                    }
            }
        }

        private void JobGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) { return; }
            if (m_dscan == null) { return; }
            m_strJobNo = JobGrid.Rows[e.RowIndex].Cells["JOBM_NO"].Value.ToString();
            if (m_strJobNo != "")
            {
                m_dscan(m_strJobNo);
                this.Close();
            }
        }

        private void btnAddInfoSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtJobNo.Text.Trim().IsNullOrEmpty() || 
                    txtInfo1.Text.Trim().IsNullOrEmpty() || 
                    txtInfo2.Text.Trim().IsNullOrEmpty() || 
                    cmbInfo3.Text.Trim().IsNullOrEmpty())
                {
                    return;
                }

                StringBuilder sb = new StringBuilder();
                if (m_Entity.Equals(pubcls.MDLEntity))//旧系统
                {
                    sb.Clear();
                    sb.AppendFormat(@"select count(*) from job_addinfo where jobm_no='{0}' and jadd_lineno = 1",txtJobNo.Text.Trim());
                    string tmpR = ZComm1.Oracle.DB.GetDSFromSql1(sb.ToString()).Tables[0].Rows[0][0].ToString();
                    if (tmpR.Equals("0"))
                    {
                        sb.Clear();
                        sb.AppendFormat(@" insert into JOB_ADDINFO(jobm_no,jadd_lineno,JADD_INFO_07,JADD_INFO_08,JADD_INFO_09) 
                        values('{0}',{1},'{2}','{3}','{4}')",
                            txtJobNo.Text.Trim(), 1, txtInfo1.Text.Trim(), txtInfo2.Text.Trim(), cmbInfo3.Text.Trim());
                    }
                    else
                    {
                        sb.Clear();
                        sb.AppendFormat(@" update job_addinfo set JADD_INFO_07='{0}',JADD_INFO_08='{1}',JADD_INFO_09='{2}' where jobm_no='{3}' and jadd_lineno=1",
                            txtInfo1.Text.Trim(),txtInfo2.Text.Trim(),cmbInfo3.Text.Trim(),txtJobNo.Text.Trim());
                    }
                }
                else
                {
                    sb.Clear();
                    sb.AppendFormat(@"select nvl(max(jadd_lineno),0) from zt00_JOB_ADDINFO where jobm_no='{0}'", txtJobNo.Text.Trim());
                    int tmpIndex = Convert.ToInt32(ZComm1.Oracle.DB.GetDSFromSql1(sb.ToString()).Tables[0].Rows[0][0].ToString());

                    sb.Clear();
                    sb.AppendFormat(@" insert into zt00_JOB_ADDINFO(jobm_no,jadd_lineno,JADD_INFO_01,JADD_INFO_02,JADD_INFO_03,JADD_INFO_04,JADD_INFO_05,JADD_INFO_06,                                  JADD_INFO_07,JADD_INFO_08,JADD_INFO_09,JADD_INFO_10) values('{0}',{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')",
                        txtJobNo.Text.Trim(),++tmpIndex,txtInfo1.Text.Trim(),txtInfo2.Text.Trim(),cmbInfo3.Text.Trim(),txtInfo4.Text.Trim(),txtInfo5.Text.Trim(),txtInfo6.Text.Trim(),
                        txtInfo7.Text.Trim(),txtInfo8.Text.Trim(),txtInfo9.Text.Trim(),txtInfo10.Text.Trim());
                }
                if (ZComm1.Oracle.DB.ExecuteFromSql(sb.ToString()))
                {
                    MessageBox.Show("保存成功！", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("保存失败！", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
