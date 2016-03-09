using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDL_CRM.Model
{
    public class ZT_SS_DATA_MODLOG
    {
        #region Fields

        private string dmlg_Id;

        public string Dmlg_Id
        {
            get { return dmlg_Id; }
            set { dmlg_Id = value; }
        }
        private string dmlg_User_Id;

        public string Dmlg_User_Id
        {
            get { return dmlg_User_Id; }
            set { dmlg_User_Id = value; }
        }
        private string dmlg_Ip;

        public string Dmlg_Ip
        {
            get { return dmlg_Ip; }
            set { dmlg_Ip = value; }
        }
        private DateTime? dmlg_ActionTime;

        public DateTime? Dmlg_ActionTime
        {
            get { return dmlg_ActionTime; }
            set { dmlg_ActionTime = value; }
        }
        private string dmlg_From_System;

        public string Dmlg_From_System
        {
            get { return dmlg_From_System; }
            set { dmlg_From_System = value; }
        }
        private string dmlg_Function;

        public string Dmlg_Function
        {
            get { return dmlg_Function; }
            set { dmlg_Function = value; }
        }
        private string dmlg_Action;

        public string Dmlg_Action
        {
            get { return dmlg_Action; }
            set { dmlg_Action = value; }
        }
        private int? dmlg_Result;

        public int? Dmlg_Result
        {
            get { return dmlg_Result; }
            set { dmlg_Result = value; }
        }
        private string dmlg_Result_Desc;

        public string Dmlg_Result_Desc
        {
            get { return dmlg_Result_Desc; }
            set { dmlg_Result_Desc = value; }
        }
        private string dmlg_Table_Name;

        public string Dmlg_Table_Name
        {
            get { return dmlg_Table_Name; }
            set { dmlg_Table_Name = value; }
        }
        private string dmlg_Key_Filed;

        public string Dmlg_Key_Filed
        {
            get { return dmlg_Key_Filed; }
            set { dmlg_Key_Filed = value; }
        }
        private string dmlg_Key_Value;

        public string Dmlg_Key_Value
        {
            get { return dmlg_Key_Value; }
            set { dmlg_Key_Value = value; }
        }

        private string dmlg_Chg_Field;

        public string Dmlg_Chg_Field
        {
            get { return dmlg_Chg_Field; }
            set { dmlg_Chg_Field = value; }
        }
        private string dmlg_From_Value;

        public string Dmlg_From_Value
        {
            get { return dmlg_From_Value; }
            set { dmlg_From_Value = value; }
        }
        private string dmlg_To_Value;

        public string Dmlg_To_Value
        {
            get { return dmlg_To_Value; }
            set { dmlg_To_Value = value; }
        }
        private string dmlg_Remark;

        public string Dmlg_Remark
        {
            get { return dmlg_Remark; }
            set { dmlg_Remark = value; }
        }
        private DateTime? dmlg_Crt_On;

        public DateTime? Dmlg_Crt_On
        {
            get { return dmlg_Crt_On; }
            set { dmlg_Crt_On = value; }
        }
        private string dmlg_Crt_By;

        public string Dmlg_Crt_By
        {
            get { return dmlg_Crt_By; }
            set { dmlg_Crt_By = value; }
        }

        #endregion Fields

        #region Method

        public bool Insert(ZT_SS_DATA_MODLOG pModLog)
        {
            if (pubcls.checkIsNull(pModLog))
            {
                throw new Exception("所传参数为空");
            }

            string sqlStr = string.Format(
            @"insert into zt_ss_data_modlog(dmlg_user_id,dmlg_ip,dmlg_actiontime,dmlg_from_system,dmlg_function,
            dmlg_action,dmlg_result,dmlg_result_desc,dmlg_table_name,dmlg_key_filed,dmlg_key_value,
            dmlg_chg_field,dmlg_from_value,dmlg_to_value,dmlg_crt_on,dmlg_crt_by)
         values('{0}','{1}',sysdate,'{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}',sysdate,'{13}')",
            pModLog.Dmlg_User_Id,
            pModLog.Dmlg_Ip,
            pModLog.Dmlg_From_System,
            pModLog.Dmlg_Function,
            pModLog.Dmlg_Action,
            pModLog.Dmlg_Result,
            pModLog.Dmlg_Result_Desc,
            pModLog.Dmlg_Table_Name,
            pModLog.Dmlg_Key_Filed,
            pModLog.Dmlg_Key_Value,
            pModLog.Dmlg_Chg_Field,
            pModLog.Dmlg_From_Value,
            pModLog.Dmlg_To_Value,
            pModLog.Dmlg_Crt_By);
            return ZComm1.Oracle.DB.ExecuteFromSql(sqlStr);
        }

        #endregion Method

    }
}
