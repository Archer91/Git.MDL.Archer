using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace CaseInquire.helperclass
{
    internal class PublicClass
    {
        private static string loginName;
        private static string userName;
        private static string hostName = Dns.GetHostName();
        private static string hostIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
        //private static string connStr = "Provider=OraOLEDB.Oracle.1;User ID=mdltest;Password=paper;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.1.41)(PORT = 1521))) (CONNECT_DATA = (SERVICE_NAME = mdlmdms)))";

        private static string ftpServerIP = "192.168.8.53";
        private static string ftpRemotePath = @"FileUpload";
        private static string ftpUserID = "xiong.zhang";
        private static string ftpPassword = "ves5bbn";

        //private static string fileServerPathBase = @"\\fileserver\it\IT\xh.li\";

        public static string FileServerPathBase
        {
            get;
            set;
        } 


        private static string roleCode;

        public static string LoginName
        {
            get { return loginName; }
            set { loginName = value; }
        }

        public static string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        public static string HostName
        {
            get { return hostName; }
        }

        public static string HostIP
        {
            get { return hostIP; }
        }

        //public static string ConnStr
        //{
        //    get { return connStr; }
        //}

        public static string FtpServerIP
        {
            get { return ftpServerIP; }
        }

        public static string FtpRemotePath
        {
            get { return ftpRemotePath; }
        }

        public static string FtpUserID
        {
            get { return ftpUserID; }
        }

        public static string FtpPassword
        {
            get { return ftpPassword; }
        }

        public static string RoleCode
        {
            get { return roleCode; }
            set { roleCode = value; }
        }



        /// <summary>
        /// 上传附件到FTP服务器
        /// </summary>
        /// <param name="pOldFileName">旧文件路径</param>
        /// <param name="pNewFileName">新文件名</param>
        /// <returns>返回文件在FTP服务器上的完整路径</returns>
        public static string FileUpload(string pOldFileName, string pNewFileName)
        {
            try
            {
                string ftpURI = "ftp://" + ftpServerIP + "/" + ftpRemotePath + "/";

                FileInfo fi = new FileInfo(pOldFileName);
                FtpWebRequest ftpReq = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI + pNewFileName));
                ftpReq.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                ftpReq.Method = WebRequestMethods.Ftp.UploadFile;
                ftpReq.KeepAlive = false;
                ftpReq.UseBinary = true;
                ftpReq.ContentLength = fi.Length;

                byte[] buff = new byte[2048];
                int contentLen = 0;
                FileStream fs = fi.OpenRead();

                Stream s = ftpReq.GetRequestStream();
                contentLen = fs.Read(buff, 0, 2048);
                while (contentLen != 0)
                {
                    s.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, 2048);
                }
                s.Close();
                fs.Close();

                return ftpURI + pNewFileName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 下载附件到指定路径
        /// </summary>
        /// <param name="pFilePath">文件路径</param>
        /// <param name="pFileName">文件名</param>
        /// <param name="pSaveFolder">保存的指定路径</param>
        public static void FileDownload(string pFilePath, string pFileName, string pSaveFolder)
        {
            try
            {
                string ftpURI = "ftp://" + ftpServerIP + "/" + ftpRemotePath + "/";

                FileStream outputStream = new FileStream(pSaveFolder + "/" + pFileName, FileMode.Create);
                FtpWebRequest reqFTP;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI + pFileName));
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                long cl = response.ContentLength;
                int readCount;
                byte[] buffer = new byte[2048];
                readCount = ftpStream.Read(buffer, 0, 2048);
                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, 2048);
                }
                ftpStream.Close();
                outputStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    
    }
}
