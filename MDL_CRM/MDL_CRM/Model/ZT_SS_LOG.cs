using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDL_CRM.Model
{
    public class ZT_SS_LOG
    {
        #region Fields

        private string id;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        private string user_Id;

        public string User_Id
        {
            get { return user_Id; }
            set { user_Id = value; }
        }
        private string ip;

        public string Ip
        {
            get { return ip; }
            set { ip = value; }
        }
        private DateTime? actionTime;

        public DateTime? ActionTime
        {
            get { return actionTime; }
            set { actionTime = value; }
        }
        private string function;

        public string Function
        {
            get { return function; }
            set { function = value; }
        }
        private string action;

        public string Action
        {
            get { return action; }
            set { action = value; }
        }
        private int? result;

        public int? Result
        {
            get { return result; }
            set { result = value; }
        }
        private string result_Desc;

        public string Result_Desc
        {
            get { return result_Desc; }
            set { result_Desc = value; }
        }
        private string from_System;

        public string From_System
        {
            get { return from_System; }
            set { from_System = value; }
        }
        private string from_Key_Value;

        public string From_Key_Value
        {
            get { return from_Key_Value; }
            set { from_Key_Value = value; }
        }

        #endregion Fields

        #region Method

        /// <summary>
        /// 写入系统操作记录日志
        /// </summary>
        /// <param name="pLog">系统操作记录</param>
        public bool Insert(ZT_SS_LOG pLog)
        {
            if (pubcls.checkIsNull(pLog))
            {
                throw new Exception("所传参数为空");
            }

            string sqlStr = string.Format(
            @"insert into zt_ss_log(user_id,ip,actiontime,function,action,result,result_desc,from_system,from_key_value)
            values('{0}','{1}',sysdate,'{2}','{3}','{4}','{5}','{6}','{7}')",
                                                                            pLog.User_Id,
                                                                            pLog.Ip,
                                                                            pLog.Function,
                                                                            pLog.Action,
                                                                            pLog.Result,
                                                                            pLog.Result_Desc,
                                                                            pLog.From_System,
                                                                            pLog.From_Key_Value);
            return ZComm1.Oracle.DB.ExecuteFromSql(sqlStr);
        }

        #endregion Method

    }
}
