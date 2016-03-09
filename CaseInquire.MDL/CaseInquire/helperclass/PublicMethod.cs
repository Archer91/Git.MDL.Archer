using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace CaseInquire.helperclass
{
    internal class PublicMethod
    {
        //SQL语句字符串
        private static string sqlStr = string.Empty;

        /// <summary>
        /// 获取自定义部门信息（Code、Value）
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDepartment()
        {
            sqlStr = @"select udc_code,udc_value from zt00_udc_udcode where udc_sys_code ='CASEINQ' and udc_key = 'DEPT'";
            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
        }

        public static DataTable GetDepartment2()
        {
            sqlStr = @"select '' udc_code,'--部门筛选--' udc_value from dual 
                       union 
                       select udc_code,udc_value from zt00_udc_udcode where udc_sys_code ='CASEINQ' and udc_key = 'DEPT' 
                       order by 1 desc";
            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
        }

        /// <summary>
        /// 获取问单类型（模版类型）
        /// </summary>
        /// <param name="pIsShow">是否显示名称与版本的合并</param>
        /// <returns></returns>
        public static DataTable GetCaseType(bool pIsShow)
        {
            if (pIsShow)
            {
                sqlStr = " select form_id,form_code,form_name,form_ver,form_department, form_name || '-' || form_ver oname from ztci_form_master order by oname";
            }
            else
            {
                sqlStr = "select form_id,form_code,form_name,form_ver,form_department,case form_status when '1' then '有效' else '失效' end form_status from ztci_form_master ";
            }
            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
        }

        /// <summary>
        /// 根据部门Code获取对应问单类型
        /// </summary>
        /// <param name="pDeptCode">部门Code</param>
        /// <returns></returns>
        public static DataTable GetCaseTypeByDeptCode(string pDeptCode)
        {
            sqlStr = string.Format("select form_id,form_code,form_name,form_ver,form_department, form_name || '-' || form_ver oname from ztci_form_master where form_department = '{0}' order by oname", pDeptCode);
            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0];
        }

        /// <summary>
        /// 判断字符串中是否有单引号，如有则替换为两个单引号
        /// </summary>
        /// <param name="Sstr">字符串</param>
        /// <returns></returns>
        public static string CheckString(string Sstr)
        {
            string returnStr = "";
            if (Sstr.IndexOf("'") != -1) //判断字符串是否含有单引号
            {
                returnStr = Sstr.Replace("'", "''");
                Sstr = returnStr;
            }
            return Sstr;
        }


        /// <summary>
        /// DataGridView 行头显示序号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void dgv_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, dgv.RowHeadersWidth - 4, e.RowBounds.Height);

            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), dgv.RowHeadersDefaultCellStyle.Font, rectangle, dgv.RowHeadersDefaultCellStyle.ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        /// <summary>
        /// DataGridVew 状态列格式化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="colIndex">指定列序号</param>
        public static void dgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e,int colIndex)
        {
            DataGridView dgv = sender as DataGridView;
            if (e.ColumnIndex == colIndex)
            {
                switch (e.Value.ToString())
                {
                    case "暂存":
                        {
                            dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightBlue;
                        }
                        break;
                    case "提交":
                        {
                            dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Goldenrod;
                        }
                        break;
                    case "在问":
                        {
                            dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.GreenYellow;
                        }
                        break;
                    case "已回复":
                        {
                            dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Green;
                        }
                        break;
                    case "关闭":
                        {
                            dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// 根据出货日期，判别问单是属于星期几，并显示对应颜色
        /// </summary>
        /// <param name="pDate">出货日期</param>
        /// <param name="plbl">状态显示控件</param>
        public static void CheckEstimateDate(string pDate,Control plbl)
        {
            if (string.IsNullOrEmpty(pDate) || null == plbl)
            {
                plbl.BackColor = SystemColors.Control;
                plbl.ForeColor = Color.Black;
                return;
            }

            switch (DateTime.Parse(pDate).DayOfWeek)
            {
                case DayOfWeek.Friday:
                    {
                        plbl.BackColor = Color.Black;
                        plbl.ForeColor = Color.White;
                    }
                    break;
                case DayOfWeek.Monday:
                    {
                        plbl.BackColor = Color.Gray;
                        plbl.ForeColor = Color.Black;
                    }
                    break;
                case DayOfWeek.Saturday:
                    {
                        plbl.BackColor = Color.White;
                        plbl.ForeColor = Color.Black;
                    }
                    break;
                case DayOfWeek.Sunday:
                    {
                        plbl.BackColor = Color.Red;
                        plbl.ForeColor = Color.Black;
                    }
                    break;
                case DayOfWeek.Thursday:
                    {
                        plbl.BackColor = Color.Blue;
                        plbl.ForeColor = Color.White;
                    }
                    break;
                case DayOfWeek.Tuesday:
                    {
                        plbl.BackColor = Color.Yellow;
                        plbl.ForeColor = Color.Black;
                    }
                    break;
                case DayOfWeek.Wednesday:
                    {
                        plbl.BackColor = Color.Purple;
                        plbl.ForeColor = Color.White;
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 获取问单状态列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetCaseStatus()
        {
            DataTable tmpDt = new DataTable();
            tmpDt.Columns.Add("Code", typeof(string));
            tmpDt.Columns.Add("Value", typeof(string));
            tmpDt.Rows.Add("100", "所有");
            tmpDt.Rows.Add("00", "暂存");
            tmpDt.Rows.Add("11", "提交");
            tmpDt.Rows.Add("22", "已回复(转医生)");
            tmpDt.Rows.Add("33", "已回复");
            tmpDt.Rows.Add("99", "取消");
            return tmpDt;
        }

        /// <summary>
        /// 获取问单附件路径
        /// </summary>
        /// <returns></returns>
        public static string GetCaseAttachmentPath()
        {
            sqlStr = @"select udc_value from zt00_udc_udcode where udc_sys_code ='CASEINQ' and udc_key = 'CASE' and udc_code = 'PATH'";
            return ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0].Rows[0][0].ToString();
        }

        /// <summary>
        /// 释放容器控件资源
        /// </summary>
        /// <param name="pCtrol">容器控件</param>
        public static void DisposeControl(Control pCtrol)
        {
            while (pCtrol.Controls.Count > 0)
            {
                if (pCtrol.Controls[0] != null)
                {
                    pCtrol.Controls[0].Dispose();
                }
            }
        }

        /// <summary>
        /// 日志记录
        /// </summary>
        /// <param name="pCtrnmId">问单ID</param>
        /// <param name="pAction">操作</param>
        /// <returns></returns>
        public static bool Logging(string pCtrnmId,string pAction)
        {
            sqlStr = string.Format(@"insert into ztci_ctrnl_tran_log(ctrnl_ctrnm_id,ctrnl_user_id,ctrnl_ip,ctrnl_action) 
                                    values('{0}','{1}','{2}','{3}')",pCtrnmId,PublicClass.LoginName,PublicClass.HostIP,pAction);
            return ZComm1.Oracle.DB.ExecuteFromSql(sqlStr);
        }

        /// <summary>
        /// 美国牙位转换为国际牙位
        /// </summary>
        /// <param name="pYawei">牙位数据</param>
        /// <returns></returns>
        public static string FromUSToEN(string pYawei)
        {
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
                                       "上/下托"};
            pYawei = pYawei.TrimStart('\'');
            string[] sA = pYawei.Split(',');//, '-'
            List<string> ls1 = new List<string>();
            foreach (string s1 in sA)
            {
                List<string> ls2 = new List<string>();
                string[] sA2 = s1.Split('-');//, '-'
                foreach (string s2 in sA2)
                {
                    string sN = s2;
                    string s3 = s2.Replace(" ", "").ToLower();
                    int ii = lUs.IndexOf(s3);
                    if (ii != -1) sN = sEn[ii];
                    ls2.Add(sN);
                }
                ls1.Add(string.Join("-", ls2));
            }
            pYawei = string.Join(",", ls1);

            return pYawei;
        }

        /// <summary>
        /// 国际牙位转换为美国牙位
        /// </summary>
        /// <param name="pYawei">牙位数据</param>
        /// <returns></returns>
        public static string FromENToUS(string pYawei)
        {
            string[] sUs ={"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18",
                            "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32",
"lower","lowerarch","mandibular","mandible","man",
"upper","upperarch","maxillary","maxilla","max",
                            "upper/lower"};
            //List<string> lUs = sUs.ToList();
            string[] sEn = { "18", "17", "16", "15", "14", "13", "12", "11", "21", "22", "23", "24", "25", "26", "27", "28", "38",
                                           "37", "36", "35", "34", "33", "32", "31", "41", "42", "43", "44", "45", "46", "47", "48",
                                       "下托","下托","下托","下托","下托",
                                       "上托","上托","上托","上托","上托",
                                       "上/下托"};
            List<string> lEn = sEn.ToList();
            pYawei = pYawei.TrimStart('\'');
            string[] sA = pYawei.Split(',');//, '-'
            List<string> ls1 = new List<string>();
            foreach (string s1 in sA)
            {
                List<string> ls2 = new List<string>();
                string[] sA2 = s1.Split('-');//, '-'
                foreach (string s2 in sA2)
                {
                    string sN = s2;
                    string s3 = s2.Replace(" ", "").ToLower();
                    int ii = lEn.IndexOf(s3);
                    if (ii != -1) sN = sUs[ii];
                    ls2.Add(sN);
                }
                ls1.Add(string.Join("-", ls2));
            }
            pYawei = string.Join(",", ls1);

            return pYawei;
        }
        /// <summary>
        /// 牙位
        /// </summary>
        /// <param name="pYawei">牙位数据</param>
        /// <returns></returns>
        public static bool ValidateYaiWeiNum(string pYawei,string pStandard="EN")
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
                    if (ii == -1 && s3 !="")
                    {
                        return false;
                    }
                }
            }
            return true;
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
    
    
    }

    public static class MyExtensions
    {
        /// <summary>
        /// 返回数据库结果中的首行首列内容
        /// </summary>
        /// <param name="pp"></param>
        /// <param name="pSqlStr">SQL语句</param>
        /// <returns></returns>
        public static string getStr(this ZComm1.Oracle.DB pp,string pSqlStr)
        {
            DataTable dt = ZComm1.Oracle.DB.GetDSFromSql1(pSqlStr).Tables[0];
            if (null == dt || dt.Rows.Count <= 0 || dt.Rows[0][0] is DBNull)
            {
                return "";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }
    }
}
