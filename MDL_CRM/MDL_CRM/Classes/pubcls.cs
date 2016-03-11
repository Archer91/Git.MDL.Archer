using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Net;
using MDL_CRM.VO;
using System.IO;

namespace MDL_CRM
{
    class pubcls
    {
        public static string UserName="";
        public static string CompanyCode = "";
        public static string CompanyName = "";

        private const string MDLENTITY = "MDL-SZ";
         /// <summary>
        /// 临时解决方案，工作单所在公司为深圳，则转到旧有MDMS系统
        /// </summary>
        public static string MDLEntity
        {
            get { return MDLENTITY; }
        }

        public static string FileSvrPath = @"\\192.168.1.23\it\IT\soAdditional\";
        public static string IP = Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();

        public static int TempInvoiceLength = 9;//临时发票长度

        public delegate void loadSOEventHandler(string pSO);
        private static loadSOEventHandler loadSODelegate;
        public static loadSOEventHandler LoadSODelegate
        {
            set { loadSODelegate = value; }
            get { return loadSODelegate; }
        }

        /// <summary>
        /// 根据用户获取已授权的公司列表
        /// </summary>
        /// <param name="pUser">用户</param>
        /// <returns></returns>
        public static DataTable getEntityByUser(string pUser)
        {
            string strSql = string.Format(@"select ent_code,ent_name from zt00_entity 
                                            where ent_code in(
                                                select b.auto_obj_value 
                                                from zt00_uaro_userrole a
                                                join zt00_auto_authobject b on a.uaro_role = b.auto_code 
                                                and b.auto_obj_code='MDLCRM_EMTITY' and b.auto_status='1'
                                                where upper(a.uaro_user)='{0}' and a.uaro_status='1')", pUser.Trim().ToUpper());
            return ZComm1.Oracle.DB.GetDSFromSql1(strSql).Tables[0];
        }
         /// <summary>
         /// 校验参数是否为空
         /// </summary>
         /// <param name="pObj">参数</param>
         /// <returns>true表示为空，false表示不为空</returns>
        public static bool checkIsNull(object pObj)
        {
            if (null == pObj || pObj is DBNull)
            {
                return true;
            }
            else if (pObj is string)
            {
                if (string.IsNullOrEmpty((string)pObj))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 日期转换
        /// </summary>
        /// <param name="pFlag">0表示从当前日期的0点0分0秒；1表示从当前日期的23点59分59秒</param>
        /// <param name="pDate">日期</param>
        /// <returns></returns>
        public static DateTime convertDate(int pFlag, DateTime pDate)
        {
            switch (pFlag)
            {
                default:
                case 0:
                    {
                        return new DateTime(pDate.Year, pDate.Month, pDate.Day, 0, 0, 0);
                    }
                    break;
                case 1:
                    {
                        return new DateTime(pDate.Year, pDate.Month, pDate.Day, 23, 59, 59);
                    }
                    break;
            }
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="progreesBar"></param>
        /// <param name="sfile"></param>
        public static void ExportDataGridViewToExcel(DataGridView dgv, ProgressBar progreesBar, SaveFileDialog sfile)
        {
            if (dgv.Rows.Count == 0)
            {
                throw new Exception("没有数据可供导出");
            }
            sfile.AddExtension = true;
            sfile.DefaultExt = ".xlsx";
            sfile.Filter = "(*.xlsx)|*.xlsx";
            if (sfile.ShowDialog() == DialogResult.OK)
            {
                progreesBar.Visible = true;
                object path = sfile.FileName;
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel._Workbook wk = excel.Application.Workbooks.Add(true);

                try
                {
                    //生成字段名称   
                    for (int i = 0; i < dgv.ColumnCount; i++)
                    {
                        excel.Cells[1, i + 1] = dgv.Columns[i].HeaderText;
                    }
                    //填充数据   
                    for (int i = 0; i < dgv.RowCount; i++)
                    {
                        for (int j = 0; j < dgv.ColumnCount; j++)
                        {
                            if (dgv[j, i].ValueType == typeof(string))
                            {
                                excel.Cells[i + 2, j + 1] = "'" + dgv[j, i].Value == null ?"":dgv[j,i].Value;
                            }
                            else
                            {
                                excel.Cells[i + 2, j + 1] = dgv[j, i].Value == null ?"":dgv[j,i].Value;
                            }
                        }
                        progreesBar.Value += 100 / dgv.RowCount;

                    }
                    excel.Visible = false;
                    excel.DisplayAlerts = false;
                    excel.AlertBeforeOverwriting = false;
                    wk.Save();
                    wk.SaveAs(path);
                    excel.Quit();
                    excel = null;
                    GC.Collect();
                    progreesBar.Value = 100;
                    progreesBar.Value = 0;
                    progreesBar.Visible = false;

                }
                catch (Exception ex)
                {
                    progreesBar.Visible = false;
                    throw ex;
                }
                finally
                {

                    GC.Collect();
                }

            }
        }

        /// <summary>
        /// 导出DataGridView数据到Excel（流模式）
        /// </summary>
        /// <param name="pDgv">DataGridView</param>
        public static void exportDataGridViewToExcel(DataGridView pDgv)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Execl files (*.xls)|*.xls";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = true;
            saveFileDialog.Title = "Export Excel File To";
            if (DialogResult.OK != saveFileDialog.ShowDialog())
            {
                return;
            }
            Stream myStream;
            myStream = saveFileDialog.OpenFile();
            //StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding("gb2312"));
            StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding(-0));
            string str = "";
            try
            {
                //写标题
                for (int i = 0; i < pDgv.ColumnCount; i++)
                {
                    if (i > 0)
                    {
                        str += "\t";
                    }
                    str += pDgv.Columns[i].HeaderText;
                }
                sw.WriteLine(str);
                //写内容
                for (int j = 0; j < pDgv.Rows.Count; j++)
                {
                    string tempStr = "";
                    for (int k = 0; k < pDgv.Columns.Count; k++)
                    {
                        if (k > 0)
                        {
                            tempStr += "\t";
                        }
                        tempStr += pDgv.Rows[j].Cells[k].Value.ToString();
                    }
                    sw.WriteLine(tempStr);
                }
                sw.Close();
                myStream.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                sw.Close();
                myStream.Close();
            }

        }

        /// <summary>
        /// 公司代码转公司字母简码
        /// </summary>
        /// <param name="pEntityCode">公司代码</param>
        /// <returns>公司字母简码</returns>
        public static string convertEntityShortCode(string pEntityCode)
        {
            string tmpCode = string.Empty;
            switch (pEntityCode)
            {
                case "MDL-SZ"://现代深圳公司
                    tmpCode = "S";
                    break;
                case "MDL-DG"://现代东莞公司
                    tmpCode = "D";
                    break;
                case "MDL-HK"://现代香港公司
                    tmpCode = "H";
                    break;
                case "MDIL"://现代澳门公司
                    tmpCode = "M";
                    break;
                case "YZJ"://杨紫荆深圳
                    tmpCode = "Y";
                    break;
                case "YZJ-BJ"://杨紫荆北京
                    tmpCode = "B";
                    break;
            }
            return tmpCode;
        }

        /// <summary>
        /// 获取服务器日期
        /// </summary>
        /// <returns></returns>
        public static string getSvrDate()
        {
            string strSql = @"select sysdate from dual";
            return ZComm1.Oracle.DB.GetDSFromSql1(strSql).Tables[0].Rows[0][0].ToString();
        }

        /// <summary>
        /// 获取年份简码
        /// </summary>
        /// <param name="pYear">YYYY年份</param>
        /// <returns></returns>
        public static string getCommonYear(string pYear="")
        {
            if (pYear.IsNullOrEmpty())
            {
                pYear = Convert.ToDateTime(getSvrDate()).ToString("yyyy");
            }
            string strSql = string.Format(@"select upletter from setcommonyear where year='{0}'", pYear);
            DataTable tmpDt = ZComm1.Oracle.DB.GetDSFromSql1(strSql).Tables[0];
            if (tmpDt != null && tmpDt.Rows.Count > 0)
            {
                return tmpDt.Rows[0][0].ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取月份简码
        /// </summary>
        /// <param name="pMonth">MM月份</param>
        /// <returns></returns>
        public static string getCommonMonth(string pMonth="")
        {
            if (pMonth.IsNullOrEmpty())
            {
                pMonth = Convert.ToDateTime(getSvrDate()).ToString("MM");
            }
            string strSql = string.Format(@" select upletter from setcommonmonth where month='{0}'", pMonth);
            DataTable tmpDt = ZComm1.Oracle.DB.GetDSFromSql1(strSql).Tables[0];
            if (tmpDt != null && tmpDt.Rows.Count > 0)
            {
                return tmpDt.Rows[0][0].ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        private static string getOldJobNo()
        {
            string v_lastid = "";
            try
            {
                IDataParameter[] dbParameters = {
                                        DB.NewOracleParameter(DB.NewParmClassR2("i_count", "varchar", "input", 
                                        "1", null)),
                                        DB.NewOracleParameter(DB.NewParmClassR2("o_lastid", "varchar", "output", 
                                        " ", 200)),
                                        DB.NewOracleParameter(DB.NewParmClassR2("o_result", "varchar", "output", 
                                        " ", 10)),
                                        DB.NewOracleParameter(DB.NewParmClassR2("o_message", "varchar", 
                                        "output", " ", 200))
                                                        };
                DB.RunDbProcedureNonQuery("SP_GEN_LAST_JOBNO_PROCEDURE", dbParameters);
                Dictionary<string, IDataParameter> dictParameters = DB.DBParmToDict(dbParameters);
                if (dictParameters["o_result"].Value.ToString() == "1")
                {
                    v_lastid = dictParameters["o_lastid"].Value.ToString();//00093
                }
            }
            catch (Exception)
            {
                v_lastid = "";
            }

            if (v_lastid != "")
            {
                //获取服务器日期
                DateTime tmpSvrDate = Convert.ToDateTime(getSvrDate());
                return "J" + getCommonYear(tmpSvrDate.ToString("yyyy")) + getCommonMonth(tmpSvrDate.ToString("MM")) + v_lastid;
            }
            return "";
        }
        private static string buildSerialNo(int pCurrVal, int pLength, int pStep)
        {
            int tmpVal = pCurrVal + pStep;
            int tmpLength=tmpVal.ToString().Length;
            if (tmpLength > pLength)
            {
                throw new Exception("单据序号超出设定长度");
            }
            if (tmpLength == pLength)
            {
                return tmpVal.ToString();
            }
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i <= (pLength - tmpLength); i++)
            {
                sb.Append("0");
            }
            sb.Append(tmpVal);
            return sb.ToString();
        }
        private static int getMaxSerialNo(int pLength)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i <= pLength; i++)
            {
                sb.Append("9");
            }
            return Convert.ToInt32(sb.ToString());
        }
        /// <summary>
        /// 获取单据号
        /// </summary>
        /// <param name="pEntity">公司代码</param>
        /// <param name="pSite">工厂代码</param>
        /// <param name="pType">单据类别</param>
        /// <returns></returns>
        public static FormSysSeqVO getDocNo(string pEntity, string pSite, DocType pType)
        {
            if (pEntity.IsNullOrEmpty() || pType.IsNullOrEmpty())
            {
                throw new Exception("获取单据号所传参数为空");
            }

            FormSysSeqVO tmpSeqVO = new FormSysSeqVO();

            if (pEntity.Equals(pubcls.MDLEntity) && pType.Equals(DocType.JobOrder))
            {
                #region 当为深圳公司的工作单时，按旧逻辑获取
                tmpSeqVO.Seq_Entity = pEntity;
                tmpSeqVO.Seq_Site = pSite;
                tmpSeqVO.Seq_Type = pType.ToString();

                tmpSeqVO.Seq_NO = getOldJobNo();//获取旧逻辑的工作单号

                return tmpSeqVO;
                #endregion
            }
            else
            {
                #region
                //获取单据规则(前缀，年月，后缀，序号长度)
                pSite = "00";//临时使用，目前只定义了与工厂代码无关的规则
                string strSql = string.Format(@"select udc_value from zt00_udc_udcode where udc_sys_code='MDLCRM' and udc_category='{0}' and udc_key='{1}' and udc_code='{2}'",
                    pEntity, pSite, pType.ToString());
                DataTable tmpDt = ZComm1.Oracle.DB.GetDSFromSql1(strSql).Tables[0];
                if (tmpDt == null || tmpDt.Rows.Count <= 0 || tmpDt.Rows[0][0] == null || tmpDt.Rows[0][0].ToString().Trim().IsNullOrEmpty())
                {
                    throw new Exception(string.Format(@"公司【{0}】工厂【{1}】票据类别【{2}】规则未定义", pEntity, pSite, pType.ToString()));
                }
                string[] tmpRule = tmpDt.Rows[0][0].ToString().Split(',');//单据规则

                //获取服务器日期
                DateTime tmpSvrDate = Convert.ToDateTime(getSvrDate());

                string tmpYYYYMM = string.Empty;
                string tmpPrefixYMD = string.Empty;
                
                switch (tmpRule[1])
                {
                    case "YYYY":
                        tmpYYYYMM = tmpSvrDate.ToString("yyyy");
                        tmpPrefixYMD = getCommonYear(tmpYYYYMM);//获取年份简码
                        break;
                    case "YYYYMM":
                        tmpYYYYMM = tmpSvrDate.ToString("yyyyMM");
                        tmpPrefixYMD = getCommonYear(tmpSvrDate.ToString("yyyy")) + getCommonMonth(tmpSvrDate.ToString("MM"));//获取年份、月份简码
                        break;
                    case "YYYYMMDD":
                        tmpYYYYMM = tmpSvrDate.ToString("yyyyMMdd");
                        tmpPrefixYMD = getCommonYear(tmpSvrDate.ToString("yyyy")) + getCommonMonth(tmpSvrDate.ToString("MM")) + tmpSvrDate.ToString("dd");
                        break;
                    case "-1"://与年月无关
                        tmpYYYYMM = "000000";
                        tmpPrefixYMD = string.Empty;
                        break;
                }

                strSql = string.Format(@"select sseq_prefix,sseq_suffix,sseq_upd_on, sseq_seq_length,sseq_step,sseq_curr_val,sseq_yyyymm,sseq_prefix_ymd from zt00_form_sysseq 
            where sseq_entity='{0}' and sseq_site='{1}' and sseq_type='{2}' and sseq_yyyymm='{3}'", pEntity, pSite, pType.ToString(), tmpYYYYMM);


                tmpDt = ZComm1.Oracle.DB.GetDSFromSql1(strSql).Tables[0];
                if (tmpDt == null || tmpDt.Rows.Count <= 0)
                {
                    tmpSeqVO.Seq_Entity = pEntity;
                    tmpSeqVO.Seq_Site = pSite;
                    tmpSeqVO.Seq_Type = pType.ToString();
                    tmpSeqVO.Seq_YYYYMM = tmpYYYYMM;

                    tmpSeqVO.Seq_Name = pType.ToString();
                    tmpSeqVO.Seq_Min_Val = 1;
                    tmpSeqVO.Seq_Max_Val = getMaxSerialNo(Convert.ToInt32(tmpRule[3]));
                    tmpSeqVO.Seq_Curr_Val = 1;
                    tmpSeqVO.Seq_Prefix = tmpRule[0].Equals("-1") ? string.Empty : tmpRule[0];
                    tmpSeqVO.Seq_Suffix = tmpRule[2].Equals("-1") ? string.Empty : tmpRule[2];
                    tmpSeqVO.Seq_Crt_By = DB.loginUserName;
                    tmpSeqVO.Seq_Length = Convert.ToInt32(tmpRule[3]);
                    tmpSeqVO.Seq_Prefix_YMD = tmpPrefixYMD;
                    tmpSeqVO.Seq_Step = 1;
                    tmpSeqVO.Seq_Flag = -1;//为新增

                    tmpSeqVO.Seq_NO = (tmpSeqVO.Seq_Prefix + tmpSeqVO.Seq_Prefix_YMD + buildSerialNo(0, Convert.ToInt32(tmpRule[3]), 1) + tmpSeqVO.Seq_Suffix).Trim();
                }
                else
                {
                    tmpSeqVO.Seq_Entity = pEntity;
                    tmpSeqVO.Seq_Site = pSite;
                    tmpSeqVO.Seq_Type = pType.ToString();
                    tmpSeqVO.Seq_YYYYMM = tmpDt.Rows[0]["sseq_yyyymm"].ToString();

                    tmpSeqVO.Seq_Upd_By = DB.loginUserName;
                    tmpSeqVO.Seq_Upd_On = Convert.ToDateTime(tmpDt.Rows[0]["sseq_upd_on"]);
                    tmpSeqVO.Seq_Flag = 1;//为更新

                    tmpSeqVO.Seq_NO = (tmpDt.Rows[0]["sseq_prefix"].ToString() +
                        tmpDt.Rows[0]["sseq_prefix_ymd"].ToString() +
                        buildSerialNo(
                        Convert.ToInt32(tmpDt.Rows[0]["sseq_curr_val"]),
                        Convert.ToInt32(tmpDt.Rows[0]["sseq_seq_length"]),
                        Convert.ToInt32(tmpDt.Rows[0]["sseq_step"])) +
                        tmpDt.Rows[0]["sseq_suffix"].ToString()).Trim();
                }

                return tmpSeqVO;
                #endregion
            }
        }

        /// <summary>
        /// 牙位
        /// </summary>
        /// <param name="pYawei">牙位数据</param>
        /// <returns></returns>
        public static bool ValidateYaiWeiNum(string pYawei, string pStandard = "EN")
        {
            if (pStandard.Trim() == "")
            {
                pStandard = "EN";
            }
            else if (pStandard.Trim().ToUpper() == "US")
            {
                pStandard = "US";
            }
            else
            {
                pStandard = "EN";
            }
            string[] sUs ={"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18",
                            "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32",
"lower","lowerarch","mandibular","mandible","man",
"upper","upperarch","maxillary","maxilla","max",
                            "upper/lower"};
            List<string> lUs = sUs.ToList();
            string[] sEn = { "18", "17", "16", "15", "14", "13", "12", "11", "21", "22", "23", "24", "25", "26", "27", "28", "38",
                                           "37", "36", "35", "34", "33", "32", "31", "41", "42", "43", "44", "45", "46", "47", "48",
                                       "下托","下托","下托","下托","下托",
                                       "上托","上托","上托","上托","上托",
                                       "上/下托"
,"lower","lowerarch","mandibular","mandible","man",
"upper","upperarch","maxillary","maxilla","max",
                            "upper/lower"                           };
            List<string> lEn = sEn.ToList();
            pYawei = pYawei.TrimStart('\'');
            string[] sA = pYawei.Split(',');//, '-'
            foreach (string s1 in sA)
            {
                string[] sA2 = s1.Split('-');//, '-'
                foreach (string s2 in sA2)
                {
                    string s3 = s2.Replace(" ", "").ToLower();
                    int ii = -1;
                    if (pStandard == "EN")
                    {
                        ii = lEn.IndexOf(s3);
                    }
                    else if (pStandard == "US")
                    {
                        ii = lUs.IndexOf(s3);
                    }
                    if (ii == -1 && s3 != "")
                    {
                        return false;
                    }
                }
            }
            return true;
        }

    }
    
    /// <summary>
    /// 单据类别
    /// </summary>
    public enum DocType
    {
        /// <summary>
        /// SaleOrder
        /// </summary>
        SaleOrder,
        /// <summary>
        /// JobOrder
        /// </summary>
        JobOrder,
        /// <summary>
        /// CreditNote
        /// </summary>
        CreditNote,
        /// <summary>
        /// Invoice
        /// </summary>
        Invoice 
    }

    /// <summary>
    /// 操作模式
    /// </summary>
    public enum EditMode
    {
        Add = 1,
        Edit = 2,
        Browse = 3,
        Copy = 4
    }

    public static class ObjectNullExtends
    {
        public static bool IsNullOrEmpty(this object obj)
        {
            if (null == obj || obj is DBNull)
            {
                return true;
            }
            else if (obj is string)
            {
                if (string.IsNullOrEmpty((string)obj))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
