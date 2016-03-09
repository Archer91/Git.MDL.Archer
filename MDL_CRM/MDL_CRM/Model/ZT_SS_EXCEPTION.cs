using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDL_CRM.Model
{
    public class ZT_SS_EXCEPTION
    {
        #region Fields

        private string exp_Id;

        public string Exp_Id
        {
            get { return exp_Id; }
            set { exp_Id = value; }
        }
        private string exp_User_Name;

        public string Exp_User_Name
        {
            get { return exp_User_Name; }
            set { exp_User_Name = value; }
        }
        private DateTime? exp_Date;

        public DateTime? Exp_Date
        {
            get { return exp_Date; }
            set { exp_Date = value; }
        }
        private string exp_Message;

        public string Exp_Message
        {
            get { return exp_Message; }
            set { exp_Message = value; }
        }
        private string exp_From;

        public string Exp_From
        {
            get { return exp_From; }
            set { exp_From = value; }
        }
        private string exp_From_Key;

        public string Exp_From_Key
        {
            get { return exp_From_Key; }
            set { exp_From_Key = value; }
        }

        #endregion Fields

        #region Method

        /// <summary>
        /// 写入异常信息日志
        /// </summary>
        /// <param name="pException">异常信息</param>
        public bool Insert(ZT_SS_EXCEPTION pException)
        {
            if (pubcls.checkIsNull(pException))
            {
                throw new Exception("所传参数为空");
            }

            string sqlStr = string.Format(
            @"insert into zt_ss_exception(exp_user_name,exp_date,exp_message,exp_from,exp_from_key)
            values('{0}',sysdate,'{1}','{2}','{3}')",
                                                    pException.Exp_User_Name,
                                                    pException.Exp_Message,
                                                    pException.Exp_From,
                                                    pException.Exp_From_Key);
            return ZComm1.Oracle.DB.ExecuteFromSql(sqlStr);
        }

        #endregion Method

    }
}
