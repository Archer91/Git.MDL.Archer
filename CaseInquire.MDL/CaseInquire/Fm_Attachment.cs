using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CaseInquire.helperclass;
using System.IO;

namespace CaseInquire
{
    public partial class Fm_Attachment : Form
    {
        public Fm_Attachment()
        {
            InitializeComponent();
        }

        public Fm_Attachment(string pCtrnmId, string pDocNo,string pJobmNo)
            : this()
        {
            ctrnmId = pCtrnmId;
            docNo = pDocNo;
            jobmNo = pJobmNo;
        }
        private string ctrnmId = string.Empty, docNo = string.Empty, jobmNo = string.Empty;
        private void Fm_Attachment_Load(object sender, EventArgs e)
        {
            txtDocNo.Text = docNo + "_" + jobmNo;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                //获取需要上传的文件
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    foreach (string f in openFileDialog1.FileNames)
                    {
                        txtFile.Text = txtFile.Text + f + ";";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDocNo.Text.Trim()) || string.IsNullOrEmpty(txtFile.Text.Trim()))
                {
                    return;
                }
                //在保存前判断问单状态
                if (IsNotEdit(docNo))
                {
                    throw new Exception("问单【" + docNo + "】不能编辑！");
                }

                string[] files = txtFile.Text.TrimEnd(';').Split(';');
                List<ZComm1.StrI> ls = new List<ZComm1.StrI>();
                int indexNum = 0;
                foreach (string item in files)
                {
                    //获取服务器日期
                    string svrDate = ZComm1.Oracle.DB.GetDSFromSql1("select sysdate from dual").Tables[0].Rows[0][0].ToString();
                    string ftpPath = CopyToServer(item,docNo, svrDate,jobmNo);

                    ls.Add(SaveUploadFile(ctrnmId, item, ftpPath, txtRemark.Text.Trim(),indexNum));
                    indexNum++;
                }
                
                //保存所有问单附件到附件表
                ZComm1.Oracle.DB.ExeTransSI(ls);

                //日志记录
                PublicMethod.Logging(ctrnmId, "上传附件");
                MessageBox.Show("问单【"+docNo+"】附件上传成功！", "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MDL-提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 问单是否不能编辑
        /// </summary>
        /// <param name="pDocNo">问单编号</param>
        /// <returns>true表示不能编辑，false表示可以编辑</returns>
        private bool IsNotEdit(string pDocNo)
        {
            string sqlStr = string.Format(
            @"select count(*) 
            from ztci_ctrnm_tran_master cm
            where cm.ctrnm_docno = '{0}' 
            and cm.ctrnm_status in ('00','11') 
            and ctrnm_edit_flag = '0'", pDocNo);
            string tmpResult = ZComm1.Oracle.DB.GetDSFromSql1(sqlStr).Tables[0].Rows[0][0].ToString();
            if (tmpResult.Equals("0"))
            {
                return true;
            }
            else
            {
                return false;
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
        private string CopyToServer(string pSFile ,string pDocNo, string pSvrDate,string pJobmNo)
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
            if (!Directory.Exists(Path.Combine(PublicClass.FileServerPathBase,sb.ToString())))
            {
                Directory.CreateDirectory(Path.Combine(PublicClass.FileServerPathBase, sb.ToString()));
            }
            sb.Append(Path.GetFileNameWithoutExtension(pSFile));
            sb.Append("_");
            sb.Append(DateTime.Parse(pSvrDate).TimeOfDay.TotalMilliseconds);
            sb.Append(Path.GetExtension(pSFile));
            FileInfo f = new FileInfo(pSFile);
            f.CopyTo(Path.Combine(PublicClass.FileServerPathBase,sb.ToString()));
            
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
        private ZComm1.StrI SaveUploadFile(string pCtrnmId, string pOrgName, string pFileName, string pRemark,int pIndex)
        {
            string sqlStr = string.Format(
            @"insert into ztci_ctrna_tran_attachment(ctrna_ctrnm_id,ctrna_attachment_org_name,ctrna_attachment_file_name,ctrna_remark,ctrna_crt_by) 
            values('{0}','{1}','{2}','{3}','{4}')", pCtrnmId, pOrgName, pFileName, pRemark, PublicClass.LoginName);
            return new ZComm1.StrI(sqlStr, pIndex);
        }

    }
}
