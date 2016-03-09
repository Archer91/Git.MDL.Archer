using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;
using System.Collections;
using System.IO;
using CaseInquire.helperclass;
using System.Text.RegularExpressions;

namespace CaseInquire
{
    public partial class Fm_Category : Form
    {
        public Fm_Category()
        {
            InitializeComponent();
        }

        public delegate void SubmitCaseHandler(string pDeliveryDate,
                                               string pTitle,
                                               string pTopic,
                                               string pPostContent,
                                               string pPostKeyValue);
        public event SubmitCaseHandler SubmitCaseEvent;

        public Fm_Category(string pUserId,string pPwd,string pJobmNo,string pAccountId,string pContent,string pDocNo,List<string> pFileName,string pCtrnmId)
            : this()
        {
            support2UserId = pUserId;
            support2Pwd = pPwd;
            jobmNo = pJobmNo;
            accountId = pAccountId;
            content = pContent;
            docNo = pDocNo;
            fileName = pFileName;
            ctrnmId = pCtrnmId;
        }

        CaseWS.Service1Client caseWS = new CaseWS.Service1Client();
        const string CURRENT_ACCOUNT = "MDL-SZ";
        const string FROM_SYSTEM = "CaseEnq";
        string support2UserId = string.Empty, support2Pwd = string.Empty;
        string jobmNo = string.Empty, accountId = string.Empty,docNo=string.Empty,ctrnmId=string.Empty;
        string content = string.Empty;
        List<string> fileName = new List<string>();
        bool isUsed = false;//是否有提交过Topic
        private void Fm_Category_Load(object sender, EventArgs e)
        {
            try
            {
                //Priority
                string str = caseWS.GetPriority(support2UserId,support2Pwd);
                string[] strLines = str.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                if (str.Trim().ToUpper().StartsWith("ERROR"))
                {
                    lblNewInfo.Text = str;
                    lblReplyInfo.Text = str;
                    lblNewInfo.ForeColor = Color.Red;
                    lblReplyInfo.ForeColor = Color.Red;
                }
                else
                {
                    foreach (string item in strLines)
                    {
                        ListItem li = new ListItem(item.Split(',')[1], item.Split(',')[0]);
                        cmbNewPriority.ValueMember = li.Value;
                        cmbNewPriority.DisplayMember = li.Name;
                        cmbNewPriority.Items.Add(li);
                    }
                    cmbNewPriority.Text = "Normal";
                }
                //Title
                str = caseWS.GetTitleTopic(support2UserId, support2Pwd, jobmNo, "0", CURRENT_ACCOUNT, accountId);
                if (str.Trim().ToUpper().StartsWith("ERROR"))
                {
                    lblNewInfo.Text = str;
                    //lblReplyInfo.Text = str;
                    lblNewInfo.ForeColor = Color.Red;
                }
                else
                {
                    strLines = str.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string item in strLines)
                    {
                        string[] tmpArray = item.Split(',');
                        ListItem li = new ListItem(tmpArray[1], tmpArray[0] + (tmpArray[2].Trim().Equals("1") ? " -->已使用" : ""), tmpArray[2]);
                        cmbNewTitle.ValueMember = li.Value;
                        cmbNewTitle.DisplayMember = li.Name;
                        cmbNewTitle.Items.Add(li);

                        cmbReplyTitle.ValueMember = li.Value;
                        cmbReplyTitle.DisplayMember = li.Name;
                        cmbReplyTitle.Items.Add(li);

                        if (tmpArray[2].Trim().Equals("1"))
                        {
                            isUsed = true;
                        }
                    }
                }

                str = caseWS.GetTitleTopic(support2UserId, support2Pwd, jobmNo, "1", CURRENT_ACCOUNT, accountId);
                if (str.Trim().ToUpper().StartsWith("ERROR"))
                {
                   // lblNewInfo.Text = str;
                    lblReplyInfo.Text = str;
                    lblReplyInfo.ForeColor = Color.Red;
                }
                else
                {
                    strLines = str.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string item in strLines)
                    {
                        string[] tmpArray = item.Split(',');
                        ListItem li = new ListItem(tmpArray[1], tmpArray[0] + (tmpArray[3].Trim().ToUpper().Equals("A") ? " -->正常" : (tmpArray[3].Trim().ToUpper().Equals("H") ? " -->HOLD" : " -->完成")), tmpArray[2]);

                        cmbReplyTopic.ValueMember = li.Value;
                        cmbReplyTopic.DisplayMember = li.Name;
                        cmbReplyTopic.Items.Add(li);
                    }
                    if (cmbReplyTopic.Items.Count == 1)
                    {
                        cmbReplyTopic.SelectedIndex = 0;
                    }
                }

                //Category
                str = caseWS.GetCateGory(support2UserId, support2Pwd, CURRENT_ACCOUNT, "0");
                if (str.Trim().ToUpper().StartsWith("ERROR"))
                {
                    lblNewInfo.Text = str;
                    lblReplyInfo.Text = str;
                    lblNewInfo.ForeColor = Color.Red;
                    lblReplyInfo.ForeColor = Color.Red;
                }
                else
                {
                    strLines = str.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string item in strLines)
                    {
                        cmbNewCategory.Items.Add(item);
                    }
                }

                //Partner Category
                str = caseWS.GetCateGory(support2UserId, support2Pwd, accountId, "0");
                if (str.Trim().ToUpper().StartsWith("ERROR"))
                {
                    lblNewInfo.Text = str;
                    lblReplyInfo.Text = str;
                    lblNewInfo.ForeColor = Color.Red;
                    lblReplyInfo.ForeColor = Color.Red;
                }
                else
                {
                    strLines = str.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string item in strLines)
                    {
                        cmbNewPartnerCategory.Items.Add(item);
                    }
                }

                rtbNewContent.Text = content;
                rtbReplyContent.Text = content;

                if (null != fileName && fileName.Count > 0)
                {
                    foreach (string item in fileName)
                    {
                        ListItem li = new ListItem(item, Path.GetFileName(item));
                        clbNewAtta.Items.Add(li);
                        clbReplyAtta.Items.Add(li);
                    }

                    for (int i = 0; i < clbNewAtta.Items.Count; i++)
                    {
                        clbNewAtta.SetItemCheckState(i, CheckState.Checked);
                    }
                    for (int i = 0; i < clbReplyAtta.Items.Count; i++)
                    {
                        clbReplyAtta.SetItemCheckState(i, CheckState.Checked);
                    }
                }

                if (isUsed)
                {
                    tabControl1.SelectedIndex =1;
                }
            }
            catch (FaultException ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cmbNewPriority.Text.Trim()) ||
                    string.IsNullOrEmpty(cmbNewTitle.Text.Trim()) ||
                    string.IsNullOrEmpty(cmbNewCategory.Text.Trim()) ||
                    string.IsNullOrEmpty(cmbNewPartnerCategory.Text.Trim()) ||
                    string.IsNullOrEmpty(rtbNewContent.Text.Trim()))
                {
                     lblNewInfo.Text="请完整填写信息";
                     lblNewInfo.ForeColor = Color.Blue;
                     return;
                }

                if (cmbNewTitle.Text.Contains("已使用"))
                {
                    if (!(DialogResult.Yes == MessageBox.Show("已创建过该Topic，确定要重复创建吗？", "MDL-提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2)))
                    {
                        return;
                    }
                }

                //附件
                fileName = new List<string>();
                List<byte[]> fileStream = new List<byte[]>();
                if (clbNewAtta.CheckedItems.Count > 0)
                {
                    foreach (ListItem item in clbNewAtta.CheckedItems)
                    {
                        fileName.Add(item.Name);
                        FileStream fs = new FileStream(PublicClass.FileServerPathBase + item.Value, FileMode.Open, FileAccess.Read);
                        try
                        {
                            byte[] buffur = new byte[fs.Length];
                            fs.Read(buffur, 0, (int)fs.Length);

                            fileStream.Add(buffur);
                        }
                        catch (Exception ex) { }
                        finally
                        {
                            if (null != fs)
                            {
                                fs.Close();
                            }
                        }
                    }
                }
 
                //提交Topic
               string result = caseWS.SubmitCaseListInfo(
                    support2UserId,
                    support2Pwd,
                    CURRENT_ACCOUNT,//当前用户所在公司Account
                    accountId,//对方公司Account
                    jobmNo,
                    rtbNewContent.Text.Replace("\n","\r\n"),
                    (cmbNewTitle.SelectedItem as ListItem).Value ,
                    "1",//是否新建
                    "",//回复标题
                    cmbNewCategory.Text.Trim(),
                    cmbNewPartnerCategory.Text.Trim(),
                     (cmbNewPriority.SelectedItem as ListItem).Value ,
                    FROM_SYSTEM,//来源系统
                    docNo, //KeyValue
                    "",//是否重复问单
                    "",//重复原因
                   fileName.ToArray(),//附件文件名
                   fileStream.ToArray() //附件文件流
                    );
               lblNewInfo.Text = result;
               if (!result.Trim().ToUpper().StartsWith("ERROR"))
               {
                   lblNewInfo.ForeColor = Color.Blue;
                   string regexStr = @"\[(.*?)\]";
                   string tmpKeyvalue = Regex.Match(result, regexStr).Groups[1].Value;
                   MessageBox.Show("提交成功！", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   SubmitCaseEvent("1901-01-01",
                                   "",
                                   (cmbNewTitle.SelectedItem as ListItem).Value,
                                   rtbNewContent.Text.Trim(),
                                   tmpKeyvalue);
                   this.Close();
               }
               else
               {
                   lblNewInfo.ForeColor = Color.Red;
               }
            }
            catch (FaultException ex)
            {
                string tmpResult = caseWS.CheckReplyIsSynchronized(FROM_SYSTEM, docNo);
                if (tmpResult.Split(',')[0].Equals("1"))
                {
                    //已提交至Support2系统
                    SubmitCaseEvent("1901-01-01",
                                   "",
                                   (cmbNewTitle.SelectedItem as ListItem).Value,
                                   rtbNewContent.Text.Trim(),
                                   tmpResult.Substring(tmpResult.IndexOf(',')+1));
                }
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                string tmpResult = caseWS.CheckReplyIsSynchronized(FROM_SYSTEM, docNo);
                if (tmpResult.Split(',')[0].Equals("1"))
                {
                    //已提交至Support2系统
                    SubmitCaseEvent("1901-01-01",
                                   "",
                                   (cmbNewTitle.SelectedItem as ListItem).Value,
                                   rtbNewContent.Text.Trim(),
                                   tmpResult.Substring(tmpResult.IndexOf(',')+1));
                }
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnReply_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cmbReplyTopic.Text.Trim()) || 
                    string.IsNullOrEmpty(cmbReplyTitle.Text.Trim()) ||
                    string.IsNullOrEmpty(rtbReplyContent.Text.Trim()) || 
                    (chkReplyRepeat.Checked && cmbReplyReason.Text.Trim().Length <= 0))
                {
                    lblReplyInfo.Text ="请完整填写信息";
                    lblReplyInfo.ForeColor = Color.Blue;
                    return;
                }

                //提醒已提交过的Topic状态
                if (cmbReplyTopic.Text.ToUpper().Contains("HOLD"))
                {
                    if (!(DialogResult.Yes == MessageBox.Show("当前Topic的状态为【Hold】，确定继续提交吗？", "MDL-提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2)))
                    {
                        return;
                    }
                }
                else if (cmbReplyTopic.Text.Contains("完成"))
                {
                    if (!(DialogResult.Yes == MessageBox.Show("当前Topic的状态为【完成】，确定继续提交吗？", "MDL-提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2)))
                    {
                        return;
                    }
                }

                //附件
                fileName = new List<string>();
                List<byte[]> fileStream = new List<byte[]>();
                if (clbReplyAtta.CheckedItems.Count > 0)
                {
                    foreach (ListItem item in clbReplyAtta.CheckedItems)
                    {
                        fileName.Add(item.Name);
                        FileStream fs = new FileStream(PublicClass.FileServerPathBase + item.Value, FileMode.Open, FileAccess.Read);
                        try
                        {
                            byte[] buffur = new byte[fs.Length];
                            fs.Read(buffur, 0, (int)fs.Length);

                            fileStream.Add(buffur);
                        }
                        catch (Exception ex) { }
                        finally
                        {
                            if (null != fs)
                            {
                                fs.Close();
                            }
                        }
                    }
                } 

                //提交Topic
               string result = caseWS.SubmitCaseListInfo(
                    support2UserId,
                    support2Pwd,
                    CURRENT_ACCOUNT,//当前用户所在公司Account
                    accountId,//对方公司Account
                    jobmNo,
                    rtbReplyContent.Text.Replace("\n","\r\n"),
                    (cmbReplyTopic.SelectedItem as ListItem).Value ,
                    "0",//是否新建
                    cmbReplyTitle.Text.Trim().Length > 0 ? (cmbReplyTitle.SelectedItem as ListItem).Value : "",//回复标题
                    "",
                    "",
                    "",//优先级
                    FROM_SYSTEM,//来源系统
                    docNo, //KeyValue
                    chkReplyRepeat.Checked ? "1":"0",//是否重复问单
                    chkReplyRepeat.Checked ? cmbReplyReason.Text.Trim() : "",//重复原因
                    fileName.ToArray(),//附件文件名
                    fileStream.ToArray() //附件文件流
                    );
               lblReplyInfo.Text = result;
               if (!result.Trim().ToUpper().StartsWith("ERROR"))
               {
                   lblReplyInfo.ForeColor = Color.Blue;
                   string regexStr = @"\[(.*?)\]";
                   string tmpKeyvalue = Regex.Match(result, regexStr).Groups[1].Value;
                   MessageBox.Show("提交成功！", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   SubmitCaseEvent(chkReplyDate.Checked ? dtpReplyDate.Value.ToString("yyyy/MM/dd") : "1901-01-01",
                                   cmbReplyTitle.Text.Trim().Length > 0 ? (cmbReplyTitle.SelectedItem as ListItem).Value : "",
                                   (cmbReplyTopic.SelectedItem as ListItem).Value,
                                   rtbReplyContent.Text.Trim(),
                                   tmpKeyvalue);
                   this.Close();
               }
               else
               {
                   lblReplyInfo.ForeColor = Color.Red;
               }
            }
            catch (FaultException ex)
            {
                string tmpResult = caseWS.CheckReplyIsSynchronized(FROM_SYSTEM, docNo);
                if (tmpResult.Split(',')[0].Equals("1"))
                {
                    //已提交至Support2系统
                    SubmitCaseEvent(chkReplyDate.Checked ? dtpReplyDate.Value.ToString("yyyy/MM/dd") : "1901-01-01",
                                   cmbReplyTitle.Text.Trim().Length > 0 ? (cmbReplyTitle.SelectedItem as ListItem).Value : "",
                                   (cmbReplyTopic.SelectedItem as ListItem).Value,
                                   rtbReplyContent.Text.Trim(),
                                   tmpResult.Substring(tmpResult.IndexOf(',')+1));
                }
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                string tmpResult = caseWS.CheckReplyIsSynchronized(FROM_SYSTEM, docNo);
                if (tmpResult.Split(',')[0].Equals("1"))
                {
                    //已提交至Support2系统
                    SubmitCaseEvent(chkReplyDate.Checked ? dtpReplyDate.Value.ToString("yyyy/MM/dd") : "1901-01-01",
                                   cmbReplyTitle.Text.Trim().Length > 0 ? (cmbReplyTitle.SelectedItem as ListItem).Value : "",
                                   (cmbReplyTopic.SelectedItem as ListItem).Value,
                                   rtbReplyContent.Text.Trim(),
                                   tmpResult.Substring(tmpResult.IndexOf(',')+1));
                }
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void chkReplyRepeat_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkReplyRepeat.Checked)
                {
                    //获取重复原因
                    string str = caseWS.GetRepeatReason(support2UserId, support2Pwd);
                    if (str.Contains("Error"))
                    {
                        lblReplyInfo.Text = str;
                        chkReplyRepeat.Checked = false;
                        lblReplyInfo.ForeColor = Color.Red;
                        return;
                    }
                    else
                    {
                        string[] strLines = str.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string item in strLines)
                        {
                            cmbReplyReason.Items.Add(item);
                        }
                        cmbReplyReason.Visible = true;
                    }
                }
                else
                {
                    cmbReplyReason.Visible = false;
                }
            }
            catch (FaultException ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnReplyAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //获取需要上传的文件
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    List<ZComm1.StrI> ls = new List<ZComm1.StrI>();
                    int indexNum = 0;
                    foreach (string f in openFileDialog1.FileNames)
                    {
                        //获取服务器日期
                        string svrDate = ZComm1.Oracle.DB.GetDSFromSql1("select sysdate from dual").Tables[0].Rows[0][0].ToString();
                        string ftpPath = CopyToServer(f, docNo, svrDate, jobmNo);
                        ListItem li = new ListItem(ftpPath, Path.GetFileName(ftpPath));
                        clbReplyAtta.Items.Add(li);
                        clbReplyAtta.SetItemCheckState(clbReplyAtta.Items.Count - 1, CheckState.Checked);
                        ls.Add(SaveUploadFile(ctrnmId, f, ftpPath, "", indexNum));
                        indexNum++;
                    }
                    //保存所有问单附件到附件表
                    ZComm1.Oracle.DB.ExeTransSI(ls);

                    //日志记录
                    PublicMethod.Logging(ctrnmId, "客服上传附件");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnNewAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //获取需要上传的文件
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    List<ZComm1.StrI> ls = new List<ZComm1.StrI>();
                    int indexNum = 0;
                    foreach (string f in openFileDialog1.FileNames)
                    {
                        //获取服务器日期
                        string svrDate = ZComm1.Oracle.DB.GetDSFromSql1("select sysdate from dual").Tables[0].Rows[0][0].ToString();
                        string ftpPath = CopyToServer(f, docNo, svrDate, jobmNo);
                        ListItem li = new ListItem(ftpPath, Path.GetFileName(ftpPath));
                        clbNewAtta.Items.Add(li);
                        clbNewAtta.SetItemCheckState(clbNewAtta.Items.Count - 1, CheckState.Checked);
                        ls.Add(SaveUploadFile(ctrnmId, f, ftpPath, "", indexNum));
                        indexNum++;
                    }
                    //保存所有问单附件到附件表
                    ZComm1.Oracle.DB.ExeTransSI(ls);

                    //日志记录
                    PublicMethod.Logging(ctrnmId, "客服上传附件");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 将附件复制到服务器上
        /// </summary>
        /// <param name="pSFile">源文件</param>
        /// <param name="pDocNo">问单编号</param>
        /// <param name="pSvrDate">系统日期</param>
        /// <param name="pJobmNo">公司条码</param>
        /// <returns></returns>
        private string CopyToServer(string pSFile, string pDocNo, string pSvrDate, string pJobmNo)
        {
            if (!Directory.Exists(PublicClass.FileServerPathBase))
            {
                throw new Exception("服务器目录路径不存在");
            }
            StringBuilder sb = new StringBuilder();

            sb.Append(DateTime.Parse(pSvrDate).ToString("yyyyMMdd"));
            sb.Append("\\");
            sb.Append(pDocNo);
            sb.Append("_");
            sb.Append(pJobmNo);
            sb.Append("\\");
            if (!Directory.Exists(Path.Combine(PublicClass.FileServerPathBase, sb.ToString())))
            {
                Directory.CreateDirectory(Path.Combine(PublicClass.FileServerPathBase, sb.ToString()));
            }
            sb.Append(Path.GetFileNameWithoutExtension(pSFile));
            sb.Append("_");
            sb.Append(DateTime.Parse(pSvrDate).TimeOfDay.TotalMilliseconds);
            sb.Append(Path.GetExtension(pSFile));
            FileInfo f = new FileInfo(pSFile);
            f.CopyTo(Path.Combine(PublicClass.FileServerPathBase, sb.ToString()));

            return sb.ToString();
        }

        /// <summary>
        /// 保存上传文件信息
        /// </summary>
        /// <param name="pCtrnmId">业务问单ID</param>
        /// <param name="pOrgName">本地文件名</param>
        /// <param name="pFileName">上传到FTP文件名</param>
        /// <param name="pRemark">备注</param>
        /// <param name="pIndex">序号</param>
        private ZComm1.StrI SaveUploadFile(string pCtrnmId, string pOrgName, string pFileName, string pRemark, int pIndex)
        {
            string sqlStr = string.Format(
            @"insert into ztci_ctrna_tran_attachment(ctrna_ctrnm_id,ctrna_attachment_org_name,ctrna_attachment_file_name,ctrna_remark,ctrna_crt_by) 
            values('{0}','{1}','{2}','{3}','{4}')", pCtrnmId, pOrgName, pFileName, pRemark, PublicClass.LoginName);
            return new ZComm1.StrI(sqlStr, pIndex);
        }

        private void cmbNewCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNewPartnerCategory.Items.Contains(cmbNewCategory.Text.Trim()))
            {
                cmbNewPartnerCategory.Text = cmbNewCategory.Text.Trim();
            }
        }

        class ListItem
        {
            public string Value { get; set; }
            public string Name { get; set; }
            public string Flag { get; set; }

            public ListItem() { }

            public ListItem(string pValue, string pName)
            {
                Value = pValue;
                Name = pName;
            }

            public ListItem(string pValue, string pName, string pFlag)
            {
                Value = pValue;
                Name = pName;
                Flag = pFlag;
            }

            public override string ToString()
            {
                return Name;
            }
        }
    }
}
