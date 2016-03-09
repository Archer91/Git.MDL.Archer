using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using System.Globalization;

namespace CaseInquire
{
    class DB
    {
//        public static string DBConnectionString = @"Password=paper;User ID=mdltest;Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.1.41)(PORT=1521)))
//                   (CONNECT_DATA=(SERVER=DEDICATED)(SID=mdlmdms)));"; //test environment
        public static string DBConnectionString = @"Password=paper;User ID=mdl;Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.1.41)(PORT=1521)))
                    (CONNECT_DATA=(SERVER=DEDICATED)(SID=mdlmdms)));"; //Prd environment
        public static string loginUserName = "";
        public static OracleConnection OraConnection = new OracleConnection(DBConnectionString);
        public static string ConnectedDBName;
        public static bool ExecuteFromSql(string strSql)
        {
            //can insert, update or delete from a sql.
            OracleConnection oc = new OracleConnection(DBConnectionString);
            OracleCommand cmd = new OracleCommand(strSql, oc);
            oc.Open();
            OracleTransaction trans = oc.BeginTransaction();
            try
            {

                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();
                trans.Commit();
                return true;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                string s = ex.ToString();
                return false;
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                cmd.Dispose();
                trans.Dispose();
            }
        }
        public static bool ExecuteFromSql(string strSql , OracleConnection oc ,OracleTransaction trans)
        {
            //can insert, update or delete from a sql.
            OracleCommand cmd = new OracleCommand(strSql, oc);
            if (oc.State == null || oc.State.ToString().ToUpper() != "OPEN")
                oc.Open();
            try
            {
                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                string s = ex.ToString();
                return false;
            }
            finally
            {
            }
        }

        public static DataSet GetDSFromSql(string strSql)
        {
            DataSet testDS = new DataSet();
            OracleConnection oc = new OracleConnection(DBConnectionString);
            OracleCommand cmd = new OracleCommand(strSql, oc);
            OracleDataAdapter oa = new OracleDataAdapter();
            try
            {
                oa.SelectCommand = cmd;
                oa.UpdateCommand = null;
                oa.DeleteCommand = null;
                oc.Open();
                oa.Fill(testDS);
                oc.Close();
            }
            catch (Exception ex)
            {
                testDS = null;
                throw ex;
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                cmd.Dispose();
                oa.Dispose();
            }
            return testDS;
        }
        public static DataSet GetDSFromSql(string strSql, OracleConnection oc, OracleTransaction trans)
        {
            DataSet testDS = new DataSet();
            OracleCommand cmd = new OracleCommand(strSql, oc);
            OracleDataAdapter oa = new OracleDataAdapter();
            if (oc.State == null || oc.State.ToString().ToUpper() != "OPEN")
                oc.Open();
            try
            {
                cmd.Transaction = trans;
                oa.SelectCommand = cmd;
                oa.UpdateCommand = null;
                oa.DeleteCommand = null;
                oa.Fill(testDS);
            }
            catch (Exception ex)
            {
                testDS = null;
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                oa.Dispose();
            }
            return testDS;
        }

        public static string sp(string s)
        {
            string r = "";
            if (s.Trim() != "")
            {
                r = s.Trim();
                r = r.Replace("'", "'||''''||'");
                r = "'" + r + "'";

            }
            else
            {
                r = "null";
            }
            //add by yf for & 20140626
            r = r.Replace("&", "'||chr(38)||'");
            return r;
        }
        public static bool ExeTrans(ArrayList arrList)
        {
            OracleConnection oc = new OracleConnection(DBConnectionString);
            oc.Open();
            OracleTransaction trans = oc.BeginTransaction();
            OracleCommand cmd =new OracleCommand();
            cmd.Connection = oc; 
            cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;
        
            try
            {
               
                for (int i = 0; i < arrList.Count; i++)
                {

                    if (arrList[i].ToString().Trim() != "")
                    {
                        cmd.CommandText = arrList[i].ToString();
                        cmd.ExecuteNonQuery(); 
                    }

                }
                trans.Commit();
                return true;
            }
            catch (Exception e)
            {
                trans.Rollback();
                return false;
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                cmd.Dispose();
                trans.Dispose();
            }
        }
        public static bool ExeTrans(ArrayList arrList, OracleConnection oc, OracleTransaction trans,bool commitBool)
        {
            if (oc.State == null || oc.State.ToString().ToUpper() != "OPEN")
                oc.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = oc;
            cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;
            try
            {

                for (int i = 0; i < arrList.Count; i++)
                {

                    if (arrList[i].ToString().Trim() != "")
                    {
                        cmd.CommandText = arrList[i].ToString();
                        cmd.ExecuteNonQuery();
                    }

                }
                if (commitBool)
                {
                    trans.Commit();
                }
                return true;
            }
            catch (Exception e)
            {
                trans.Rollback();
                return false;
            }
            finally
            {
                cmd.Dispose();
            }
        }
        public static void RunDbProcedureNonQuery(string storeProcedureName, IDataParameter[] DbParameters)
        {
            OracleConnection oc = new OracleConnection(DBConnectionString);
            oc.Open();
            OracleTransaction trans = oc.BeginTransaction(); 
            OracleCommand oracleCommand = new OracleCommand();
            oracleCommand.Connection = oc;
            try
            {
                oracleCommand.Transaction = trans;
                oracleCommand.CommandText = storeProcedureName;
                oracleCommand.CommandType = CommandType.StoredProcedure;
                oracleCommand.Parameters.Clear();
                foreach (OracleParameter parameter in DbParameters)
                {
                    oracleCommand.Parameters.Add(parameter);
                }
                oracleCommand.ExecuteNonQuery();
                IDataParameter[] rtnParameters = new IDataParameter[oracleCommand.Parameters.Count];
                oracleCommand.Parameters.CopyTo(rtnParameters, 0);
                DbParameters = rtnParameters;
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();//oracleCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oracleCommand.Dispose();
                trans.Dispose();
            }
        }
        public static DataSet RunDbProcedureDataSet(string storeProcedureName, IDataParameter[] DbParameters)
        {
            DataSet Data = new DataSet();
            OracleConnection oc = new OracleConnection(DBConnectionString);
            oc.Open();
            OracleTransaction trans = oc.BeginTransaction();
            OracleCommand oracleCommand = new OracleCommand();
            oracleCommand.Connection = oc;
            try
            {
                oracleCommand.Transaction = trans;
                oracleCommand.CommandText = storeProcedureName;
                oracleCommand.CommandType = CommandType.StoredProcedure;
                oracleCommand.Parameters.Clear();
                foreach (OracleParameter parameter in DbParameters)
                {
                    oracleCommand.Parameters.Add(parameter);
                }
                OracleDataAdapter Adapter = new OracleDataAdapter(oracleCommand);
                Adapter.Fill(Data);
                IDataParameter[] rtnParameters = new IDataParameter[oracleCommand.Parameters.Count];
                oracleCommand.Parameters.CopyTo(rtnParameters, 0);
                DbParameters = rtnParameters;
                trans.Commit();
                return Data;
            }
            catch (Exception ex)
            {
                trans.Rollback(); //oracleCommand.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oracleCommand.Dispose();
                trans.Dispose();
            }
        }
        public static void RunDbProcedureNonQuery(string storeProcedureName, IDataParameter[] DbParameters, OracleConnection oc, OracleTransaction trans, bool commitBool)
        {
            if (oc.State == null || oc.State.ToString().ToUpper() != "OPEN")
                oc.Open();
            OracleCommand oracleCommand = new OracleCommand();
            oracleCommand.Connection = oc;
            try
            {
                oracleCommand.Transaction = trans;
                oracleCommand.CommandText = storeProcedureName;
                oracleCommand.CommandType = CommandType.StoredProcedure;
                oracleCommand.Parameters.Clear();
                foreach (OracleParameter parameter in DbParameters)
                {
                    oracleCommand.Parameters.Add(parameter);
                }
                oracleCommand.ExecuteNonQuery();
                IDataParameter[] rtnParameters = new IDataParameter[oracleCommand.Parameters.Count];
                oracleCommand.Parameters.CopyTo(rtnParameters, 0);
                DbParameters = rtnParameters;
                if (commitBool)
                {
                    trans.Commit();
                }
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                oracleCommand.Dispose();
            }
        }
        public static DataSet RunDbProcedureDataSet(string storeProcedureName, IDataParameter[] DbParameters, OracleConnection oc, OracleTransaction trans, bool commitBool)
        {

            DataSet Data = new DataSet();
            if (oc.State == null || oc.State.ToString().ToUpper() != "OPEN")
                oc.Open();
            OracleCommand oracleCommand = new OracleCommand();
            oracleCommand.Connection = oc;
            try
            {
                oracleCommand.Transaction = trans;
                oracleCommand.CommandText = storeProcedureName;
                oracleCommand.CommandType = CommandType.StoredProcedure;
                oracleCommand.Parameters.Clear();
                foreach (OracleParameter parameter in DbParameters)
                {
                    oracleCommand.Parameters.Add(parameter);
                }
                OracleDataAdapter Adapter = new OracleDataAdapter(oracleCommand);
                Adapter.Fill(Data);
                IDataParameter[] rtnParameters = new IDataParameter[oracleCommand.Parameters.Count];
                oracleCommand.Parameters.CopyTo(rtnParameters, 0);
                DbParameters = rtnParameters;
                if (commitBool)
                {
                    trans.Commit();
                }

                return Data;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                oracleCommand.Dispose();
            }
        }

        public static double CalaulateEffQty(double actualQty, string countType, double countRate)
        {
            double r = actualQty;
            if (countType.Trim() == "*")
            {
                r = actualQty * countRate;

            }
            else if (countType.Trim() == "+")
            {
                r = actualQty + countRate;
            }
            else if (countType.Trim() == "-")
            {
                r = actualQty - countRate;
            }
            return r;
        }
        public static double CalaulateEffQty_Revise(double actualEffQty, string countType, double countRate)
        {
            double r = actualEffQty;
            if (countType.Trim() == "*")
            {
                r = actualEffQty / countRate;

            }
            else if (countType.Trim() == "+")
            {
                r = actualEffQty - countRate;
            }
            else if (countType.Trim() == "-")
            {
                r = actualEffQty + countRate;
            }
            return r;
        }

        public static CultureInfo ci_en_us = new CultureInfo("en-us");

        public static bool IsAdminUser(string vLoginName)
        {
            bool result = false;
            try
            {
                DataSet dstmp = GetDSFromSql(@" select * from ( SELECT DISTINCT UARO_ROLE FROM ZT00_UARO_USERROLE WHERE NVL(UARO_STATUS,'0')<>'0'  AND UARO_USER ='" + vLoginName + @"' AND exists (select 'x'  from ZT00_UACC_USERACCOUNT where uacc_code='" + vLoginName + @"')
              UNION
              SELECT DISTINCT UARO_ROLE FROM ZT00_UARO_USERROLE WHERE NVL(UARO_STATUS,'0')<>'0' CONNECT BY UARO_ROLE = PRIOR UARO_USER START WITH UARO_ROLE IN (SELECT DISTINCT UARO_ROLE FROM ZT00_UARO_USERROLE WHERE NVL(UARO_STATUS,'0')<>'0'  AND UARO_USER ='" + vLoginName + @"' AND exists (select 'x'  from ZT00_UACC_USERACCOUNT where uacc_code='" + vLoginName + @"') UNION SELECT '" + vLoginName + @"' UARO_ROLE FROM DUAL )
              UNION
              SELECT DISTINCT UARO_USER AS UARO_ROLE FROM ZT00_UARO_USERROLE WHERE NVL(UARO_STATUS,'0')<>'0' and exists ( select 'x' from zt00_role_info where role_code=uaro_user) CONNECT BY UARO_ROLE = PRIOR UARO_USER START WITH UARO_ROLE IN (SELECT DISTINCT UARO_ROLE FROM ZT00_UARO_USERROLE WHERE NVL(UARO_STATUS,'0')<>'0'  AND UARO_USER ='" + vLoginName + @"' AND exists (select 'x'  from ZT00_UACC_USERACCOUNT where uacc_code='" + vLoginName + @"') UNION SELECT '" + vLoginName + @"' UARO_ROLE FROM DUAL )
              UNION SELECT '" + vLoginName + @"' UARO_ROLE FROM DUAL ) aaa where UARO_ROLE='R_CASEINQ_ADMIN'");
                if (dstmp != null && dstmp.Tables[0].Rows.Count > 0) result = true;
            }
            catch (Exception ex)
            {
            }
            finally
            { 
            }
            return result;
        }
        public static bool HaveObjectRightsByUserId(string objectID, string userId, string rights1_2_3, string objectValue)
        {
            if (objectID.Trim() == "") return false;
            if (userId.Trim() == "") return false;
            if (rights1_2_3.Trim() == "") return false;
            if (rights1_2_3.Trim() == "1") rights1_2_3 = "1,2,3";
            if (rights1_2_3.Trim() == "2") rights1_2_3 = "2,3";

            string sql = @"SELECT * FROM ZT00_AUTO_AUTHOBJECT
          WHERE  AUTO_CODE IN ( 
          SELECT DISTINCT UARO_ROLE FROM ZT00_UARO_USERROLE WHERE NVL(UARO_STATUS,'0')<>'0'  AND UARO_USER ='" + userId.Replace("'", "''") + @"' AND exists (select 'x'  from ZT00_UACC_USERACCOUNT where uacc_code='" + userId.Replace("'", "''") + @"')
          UNION
          SELECT DISTINCT UARO_ROLE FROM ZT00_UARO_USERROLE WHERE NVL(UARO_STATUS,'0')<>'0' CONNECT BY UARO_ROLE = PRIOR UARO_USER START WITH UARO_ROLE IN 
                   (SELECT DISTINCT UARO_ROLE FROM ZT00_UARO_USERROLE WHERE NVL(UARO_STATUS,'0')<>'0'  AND UARO_USER ='" + userId.Replace("'", "''") + @"' AND exists (select 'x'  from ZT00_UACC_USERACCOUNT where uacc_code='" + userId.Replace("'", "''") + @"')  UNION SELECT '" + userId.Replace("'", "''") + @"' UARO_ROLE FROM DUAL )
          UNION
          SELECT DISTINCT UARO_USER AS UARO_ROLE FROM ZT00_UARO_USERROLE WHERE NVL(UARO_STATUS,'0')<>'0' and exists ( select 'x' from zt00_role_info where role_code=uaro_user) CONNECT BY UARO_ROLE = PRIOR UARO_USER START WITH UARO_ROLE IN 
                  (SELECT DISTINCT UARO_ROLE FROM ZT00_UARO_USERROLE WHERE NVL(UARO_STATUS,'0')<>'0'  AND UARO_USER ='" + userId.Replace("'", "''") + @"' AND exists (select 'x'  from ZT00_UACC_USERACCOUNT where uacc_code='" + userId.Replace("'", "''") + @"') UNION SELECT '" + userId.Replace("'", "''") + @"' UARO_ROLE FROM DUAL )
          UNION SELECT '" + userId + @"' UARO_ROLE FROM DUAL ) AND AUTO_OBJ_CODE='" + objectID.Replace("'", "''") + @"'  AND AUTO_STATUS = '1' AND AUTO_RIGHTS in ('" + rights1_2_3.Replace("'", "''").Replace(",", "','") + "') ";
            //AND (AUTO_OBJ_VALUE=:AUTO_OBJ_VALUE OR AUTO_OBJ_VALUE='*ALL')
            if (objectValue.Trim() != "")
                sql = sql + "  AND (AUTO_OBJ_VALUE='" + objectValue.Replace("'", "''") + "' OR AUTO_OBJ_VALUE='*ALL') ";
            DataSet dstmp = GetDSFromSql(sql);
            if (dstmp != null && dstmp.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool HaveMenuRightsByUserId(string menuID, string userId)
        {
            if (menuID.Trim() == "") return false;
            if (userId.Trim() == "") return false;

            string sql = @"SELECT * FROM ZT00_AUTM_AUTHMENU
          WHERE
          AUTM_CODE IN ( SELECT UARO_ROLE FROM ZT00_UARO_USERROLE WHERE NVL(UARO_STATUS,'0')<>'0'  CONNECT BY UARO_USER = PRIOR UARO_ROLE START WITH UARO_USER =" + sp(userId) + " UNION SELECT " + sp(userId) + " UARO_ROLE FROM DUAL ) AND AUTM_MENUID=" + sp(menuID) + " AND AUTM_STATUS = '1' ";
            DataSet dstmp = GetDSFromSql(sql);
            if (dstmp != null && dstmp.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static string GetModifyLimitedHours(string vLoginName)
        {
            string result = "0";
            DataSet dsH;
            if (IsAdminUser(vLoginName))
            {
                dsH = GetDSFromSql("select udc_value from ZT00_UDC_UDCODE where udc_sys_code='CASEINQ' and udc_category='VALUE' and udc_key='MODIFYLIMITEDHOURS' and udc_code='ADMIN'");
            }
            else
            {
                dsH = GetDSFromSql("select udc_value from ZT00_UDC_UDCODE where udc_sys_code='CASEINQ' and udc_category='VALUE' and udc_key='MODIFYLIMITEDHOURS' and udc_code='NORMAL'");
            }
            result = dsH.Tables[0].Rows[0][0].ToString();
            return result;
        }
        public static string GetEffDateLimitedDays(string vLoginName)
        {
            string result = "0";
            DataSet dsH;
            if (IsAdminUser(vLoginName))
            {
                dsH = GetDSFromSql("select udc_value from ZT00_UDC_UDCODE where udc_sys_code='CASEINQ' and udc_category='VALUE' and udc_key='EFFDATELIMITEDDAYS' and udc_code='ADMIN'");
            }
            else
            {
                dsH = GetDSFromSql("select udc_value from ZT00_UDC_UDCODE where udc_sys_code='CASEINQ' and udc_category='VALUE' and udc_key='EFFDATELIMITEDDAYS' and udc_code='NORMAL'");
            }
            result = dsH.Tables[0].Rows[0][0].ToString();
            return result;
        }

        public static OracleLob ToClob(OracleConnection orcn, OracleTransaction transaction, string sIn)
        {
            int ii = Math.Abs(sIn.GetHashCode());

            OracleType lobtype = OracleType.Clob;
            string CreateTempBlob = "DECLARE A" + ii + " " + lobtype + "; " +
                "BEGIN " +
                "DBMS_LOB.CREATETEMPORARY(A" + ii + ", FALSE); " +
                ":LOC" + ii + " := A" + ii + "; " +
                "END;";

            //  OracleTransaction transaction = orcn.BeginTransaction();
            OracleParameter[] param = new OracleParameter[1];
            param[0] = new OracleParameter("LOC" + ii, OracleType.Clob);

            OracleCommand cmdTemp = new OracleCommand(CreateTempBlob, orcn);

            param[0].Direction = ParameterDirection.Output;
            cmdTemp.Parameters.Add(param[0]);
            cmdTemp.Transaction = transaction;
            cmdTemp.ExecuteNonQuery();
            OracleLob tempLob = (OracleLob)param[0].Value;

            //将文件内容传入Blob变量
            tempLob.BeginBatch(OracleLobOpenMode.ReadWrite);
            byte[] bytes = System.Text.Encoding.Unicode.GetBytes(sIn);
            tempLob.Write(bytes, 0, bytes.Length);
            tempLob.Position = 0;
            return tempLob;
        }
        public static OracleParameter NewOracleParameter(ClassR2 parameterObj)
        {
            if (parameterObj == null || parameterObj.ParameterObj._Name.ToString() == "" || parameterObj.ParameterObj._DbType.ToString() == "") throw new Exception(" Parameter Can not null (Name , DbType) !");
            OracleParameter aParm = new OracleParameter();
            aParm.ParameterName = parameterObj.ParameterObj._Name.ToString();
            switch (parameterObj.ParameterObj._Direction.ToString().ToLower())
            {
                case "input":
                    aParm.Direction = ParameterDirection.Input;
                    break;
                case "output":
                    aParm.Direction = ParameterDirection.Output;
                    break;
                case "inputoutput":
                    aParm.Direction = ParameterDirection.InputOutput;
                    break;
                case "returnvalue":
                    aParm.Direction = ParameterDirection.ReturnValue;
                    break;
                default:
                    aParm.Direction = ParameterDirection.Input;
                    break;
            }
            switch (parameterObj.ParameterObj._DbType.ToString().ToLower())
            {
                case "bfile":
                    aParm.OracleType = OracleType.BFile;
                    break;
                case "blob":
                    aParm.OracleType = OracleType.Blob;
                    break;
                case "byte":
                    aParm.OracleType = OracleType.Byte;
                    if (parameterObj.ParameterObj._Size != null && parameterObj.ParameterObj._Size.ToString() != "")
                        aParm.Size = System.Convert.ToInt32(parameterObj.ParameterObj._Size);
                    if (parameterObj.ParameterObj._Value != null && parameterObj.ParameterObj._Value.ToString().Trim() != "")
                    {
                        aParm.Value = parameterObj.ParameterObj._Value;
                    }
                    else
                    {
                        aParm.Value = DBNull.Value;
                    }
                    break;
                case "char":
                    aParm.OracleType = OracleType.Char;
                    if (parameterObj.ParameterObj._Size != null && parameterObj.ParameterObj._Size.ToString() != "")
                        aParm.Size = System.Convert.ToInt32(parameterObj.ParameterObj._Size);
                    if (parameterObj.ParameterObj._Value != null && parameterObj.ParameterObj._Value.ToString().Trim() != "")
                    {
                        aParm.Value = parameterObj.ParameterObj._Value;
                    }
                    else
                    {
                        aParm.Value = DBNull.Value;
                    }
                    break;
                case "clob":
                    aParm.OracleType = OracleType.Clob;

                    break;
                case "cursor":
                    aParm.OracleType = OracleType.Cursor;
                    break;
                case "datetime":
                    aParm.OracleType = OracleType.DateTime;
                    if (parameterObj.ParameterObj._Value != null && parameterObj.ParameterObj._Value.ToString().Trim() != "")
                    {
                        aParm.Value = parameterObj.ParameterObj._Value;
                    }
                    else
                    {
                        aParm.Value = DBNull.Value;
                    }
                    break;
                case "double":
                    aParm.OracleType = OracleType.Double;
                    if (parameterObj.ParameterObj._Size != null && parameterObj.ParameterObj._Size.ToString() != "")
                        aParm.Size = System.Convert.ToInt32(parameterObj.ParameterObj._Size);
                    if (parameterObj.ParameterObj._Value != null && parameterObj.ParameterObj._Value.ToString().Trim() != "")
                    {
                        aParm.Value = parameterObj.ParameterObj._Value;
                    }
                    else
                    {
                        aParm.Value = DBNull.Value;
                    }
                    break;
                case "float":
                    aParm.OracleType = OracleType.Float;
                    if (parameterObj.ParameterObj._Size != null && parameterObj.ParameterObj._Size.ToString() != "")
                        aParm.Size = System.Convert.ToInt32(parameterObj.ParameterObj._Size);
                    if (parameterObj.ParameterObj._Value != null && parameterObj.ParameterObj._Value.ToString().Trim() != "")
                    {
                        aParm.Value = parameterObj.ParameterObj._Value;
                    }
                    else
                    {
                        aParm.Value = DBNull.Value;
                    }
                    break;
                case "int16":
                    aParm.OracleType = OracleType.Int16;
                    if (parameterObj.ParameterObj._Size != null && parameterObj.ParameterObj._Size.ToString() != "")
                        aParm.Size = System.Convert.ToInt32(parameterObj.ParameterObj._Size);
                    if (parameterObj.ParameterObj._Value != null && parameterObj.ParameterObj._Value.ToString().Trim() != "")
                    {
                        aParm.Value = parameterObj.ParameterObj._Value;
                    }
                    else
                    {
                        aParm.Value = DBNull.Value;
                    }
                    break;
                case "int32":
                    aParm.OracleType = OracleType.Int32;
                    if (parameterObj.ParameterObj._Size != null && parameterObj.ParameterObj._Size.ToString() != "")
                        aParm.Size = System.Convert.ToInt32(parameterObj.ParameterObj._Size);
                    if (parameterObj.ParameterObj._Value != null && parameterObj.ParameterObj._Value.ToString().Trim() != "")
                    {
                        aParm.Value = parameterObj.ParameterObj._Value;
                    }
                    else
                    {
                        aParm.Value = DBNull.Value;
                    }
                    break;
                case "intervaldaytosecond":
                    aParm.OracleType = OracleType.IntervalDayToSecond;
                    if (parameterObj.ParameterObj._Value != null && parameterObj.ParameterObj._Value.ToString().Trim() != "")
                    {
                        aParm.Value = parameterObj.ParameterObj._Value;
                    }
                    else
                    {
                        aParm.Value = DBNull.Value;
                    }
                    break;
                case "intervalyeartomonth":
                    aParm.OracleType = OracleType.IntervalYearToMonth;
                    if (parameterObj.ParameterObj._Value != null && parameterObj.ParameterObj._Value.ToString().Trim() != "")
                    {
                        aParm.Value = parameterObj.ParameterObj._Value;
                    }
                    else
                    {
                        aParm.Value = DBNull.Value;
                    }
                    break;
                case "longraw":
                    aParm.OracleType = OracleType.LongRaw;
                    if (parameterObj.ParameterObj._Size != null && parameterObj.ParameterObj._Size.ToString() != "")
                        aParm.Size = System.Convert.ToInt32(parameterObj.ParameterObj._Size);
                    if (parameterObj.ParameterObj._Value != null && parameterObj.ParameterObj._Value.ToString().Trim() != "")
                    {
                        aParm.Value = parameterObj.ParameterObj._Value;
                    }
                    else
                    {
                        aParm.Value = DBNull.Value;
                    }
                    break;
                case "longvarchar":
                    aParm.OracleType = OracleType.LongVarChar;
                    if (parameterObj.ParameterObj._Size != null && parameterObj.ParameterObj._Size.ToString() != "")
                        aParm.Size = System.Convert.ToInt32(parameterObj.ParameterObj._Size);
                    if (parameterObj.ParameterObj._Value != null && parameterObj.ParameterObj._Value.ToString().Trim() != "")
                    {
                        aParm.Value = parameterObj.ParameterObj._Value;
                    }
                    else
                    {
                        aParm.Value = DBNull.Value;
                    }
                    break;
                case "nchar":
                    aParm.OracleType = OracleType.NChar;
                    if (parameterObj.ParameterObj._Size != null && parameterObj.ParameterObj._Size.ToString() != "")
                        aParm.Size = System.Convert.ToInt32(parameterObj.ParameterObj._Size);
                    if (parameterObj.ParameterObj._Value != null && parameterObj.ParameterObj._Value.ToString().Trim() != "")
                    {
                        aParm.Value = parameterObj.ParameterObj._Value;
                    }
                    else
                    {
                        aParm.Value = DBNull.Value;
                    }
                    break;
                case "nclob":
                    aParm.OracleType = OracleType.NClob;
                    if (parameterObj.ParameterObj._Size != null && parameterObj.ParameterObj._Size.ToString() != "")
                        aParm.Size = System.Convert.ToInt32(parameterObj.ParameterObj._Size);
                    break;
                case "number":
                    aParm.OracleType = OracleType.Number;
                    if (parameterObj.ParameterObj._Size != null && parameterObj.ParameterObj._Size.ToString() != "")
                        aParm.Size = System.Convert.ToInt32(parameterObj.ParameterObj._Size);
                    if (parameterObj.ParameterObj._Value != null && parameterObj.ParameterObj._Value.ToString().Trim() != "")
                    {
                        aParm.Value = parameterObj.ParameterObj._Value;
                    }
                    else
                    {
                        aParm.Value = DBNull.Value;
                    }
                    break;
                case "nvarchar":
                    aParm.OracleType = OracleType.NVarChar;
                    if (parameterObj.ParameterObj._Size != null && parameterObj.ParameterObj._Size.ToString() != "")
                        aParm.Size = System.Convert.ToInt32(parameterObj.ParameterObj._Size);
                    if (parameterObj.ParameterObj._Value != null && parameterObj.ParameterObj._Value.ToString().Trim() != "")
                    {
                        aParm.Value = parameterObj.ParameterObj._Value;
                    }
                    else
                    {
                        aParm.Value = DBNull.Value;
                    }
                    break;
                case "raw":
                    aParm.OracleType = OracleType.Raw;
                    if (parameterObj.ParameterObj._Size != null && parameterObj.ParameterObj._Size.ToString() != "")
                        aParm.Size = System.Convert.ToInt32(parameterObj.ParameterObj._Size);
                    if (parameterObj.ParameterObj._Value != null && parameterObj.ParameterObj._Value.ToString().Trim() != "")
                    {
                        aParm.Value = parameterObj.ParameterObj._Value;
                    }
                    else
                    {
                        aParm.Value = DBNull.Value;
                    }
                    break;
                case "rowid":
                    aParm.OracleType = OracleType.RowId;
                    if (parameterObj.ParameterObj._Size != null && parameterObj.ParameterObj._Size.ToString() != "")
                        aParm.Size = System.Convert.ToInt32(parameterObj.ParameterObj._Size);
                    if (parameterObj.ParameterObj._Value != null && parameterObj.ParameterObj._Value.ToString().Trim() != "")
                    {
                        aParm.Value = parameterObj.ParameterObj._Value;
                    }
                    else
                    {
                        aParm.Value = DBNull.Value;
                    }
                    break;
                case "sbyte":
                    aParm.OracleType = OracleType.SByte;
                    if (parameterObj.ParameterObj._Size != null && parameterObj.ParameterObj._Size.ToString() != "")
                        aParm.Size = System.Convert.ToInt32(parameterObj.ParameterObj._Size);
                    if (parameterObj.ParameterObj._Value != null && parameterObj.ParameterObj._Value.ToString().Trim() != "")
                    {
                        aParm.Value = parameterObj.ParameterObj._Value;
                    }
                    else
                    {
                        aParm.Value = DBNull.Value;
                    }
                    break;
                case "timestamplocal":
                    aParm.OracleType = OracleType.TimestampLocal;
                    if (parameterObj.ParameterObj._Value != null && parameterObj.ParameterObj._Value.ToString().Trim() != "")
                    {
                        aParm.Value = parameterObj.ParameterObj._Value;
                    }
                    else
                    {
                        aParm.Value = DBNull.Value;
                    }
                    break;
                case "timestampwithtz":
                    aParm.OracleType = OracleType.TimestampWithTZ;
                    if (parameterObj.ParameterObj._Value != null && parameterObj.ParameterObj._Value.ToString().Trim() != "")
                    {
                        aParm.Value = parameterObj.ParameterObj._Value;
                    }
                    else
                    {
                        aParm.Value = DBNull.Value;
                    }
                    break;
                case "uint16":
                    aParm.OracleType = OracleType.UInt16;
                    if (parameterObj.ParameterObj._Size != null && parameterObj.ParameterObj._Size.ToString() != "")
                        aParm.Size = System.Convert.ToInt32(parameterObj.ParameterObj._Size);
                    if (parameterObj.ParameterObj._Value != null && parameterObj.ParameterObj._Value.ToString().Trim() != "")
                    {
                        aParm.Value = parameterObj.ParameterObj._Value;
                    }
                    else
                    {
                        aParm.Value = DBNull.Value;
                    }
                    break;
                case "uint32":
                    aParm.OracleType = OracleType.UInt32;
                    if (parameterObj.ParameterObj._Size != null && parameterObj.ParameterObj._Size.ToString() != "")
                        aParm.Size = System.Convert.ToInt32(parameterObj.ParameterObj._Size);
                    if (parameterObj.ParameterObj._Value != null && parameterObj.ParameterObj._Value.ToString().Trim() != "")
                    {
                        aParm.Value = parameterObj.ParameterObj._Value;
                    }
                    else
                    {
                        aParm.Value = DBNull.Value;
                    }
                    break;
                case "varchar":
                    aParm.OracleType = OracleType.VarChar;
                    if (parameterObj.ParameterObj._Size != null && parameterObj.ParameterObj._Size.ToString() != "")
                        aParm.Size = System.Convert.ToInt32(parameterObj.ParameterObj._Size);
                    if (parameterObj.ParameterObj._Value != null && parameterObj.ParameterObj._Value.ToString().Trim() != "")
                    {
                        aParm.Value = parameterObj.ParameterObj._Value;
                    }
                    else
                    {
                        aParm.Value = DBNull.Value;
                    }
                    break;
                default:
                    aParm.OracleType = OracleType.VarChar;
                    if (parameterObj.ParameterObj._Size != null && parameterObj.ParameterObj._Size.ToString() != "")
                        aParm.Size = System.Convert.ToInt32(parameterObj.ParameterObj._Size);
                    if (parameterObj.ParameterObj._Value != null && parameterObj.ParameterObj._Value.ToString().Trim() != "")
                    {
                        aParm.Value = parameterObj.ParameterObj._Value;
                    }
                    else
                    {
                        aParm.Value = DBNull.Value;
                    }
                    break;
            }
            return aParm;
        }
        public static ClassR2 OracleParameter2ClassR2(OracleParameter parameterObj)
        {
            if (parameterObj == null) throw new Exception(" Oracle Parameter Can not null !");
            ClassR2 aParm = new ClassR2();
            aParm.ParameterObj = new PassParameterR();
            aParm.ParameterObj._Name = parameterObj.ParameterName;
            aParm.ParameterObj._DbType = parameterObj.DbType.ToString();
            aParm.ParameterObj._Direction = parameterObj.Direction.ToString();
            aParm.dtData = ""; // gridclass can not null add initial value
            aParm.dtAttr = ""; //gridclass can not null add initial value
            if (parameterObj.Value == DBNull.Value)
            {
                aParm.ParameterObj._Value = parameterObj.Value.ToString();
            }
            else
            {
                switch (parameterObj.OracleType.ToString().ToLower())
                {
                    case "bfile":
                        OracleBFile oraBflie = (OracleBFile)parameterObj.Value; //wait check and test how to get bfile
                        aParm.ParameterObj._Value = oraBflie.Value.ToString();
                        break;
                    case "blob":
                        OracleLob oraLob = (OracleLob)parameterObj.Value;
                        aParm.ParameterObj._Value = oraLob.Value.ToString();
                        break;
                    case "byte":
                        aParm.ParameterObj._Value = parameterObj.Value;
                        break;
                    case "char":
                        aParm.ParameterObj._Value = parameterObj.Value;
                        break;
                    case "clob":
                        oraLob = (OracleLob)parameterObj.Value;
                        aParm.ParameterObj._Value = oraLob.Value.ToString();
                        break;
                    case "cursor":
                        //wait check and test how to get cursor "Cursor" , should use fill to dataset
                        //aParm.ParameterObj._Value = parameterObj.Value.ToString();
                        break;
                    case "datetime":
                        aParm.ParameterObj._Value = parameterObj.Value;
                        break;
                    case "double":
                        aParm.ParameterObj._Value = parameterObj.Value;
                        break;
                    case "float":
                        aParm.ParameterObj._Value = parameterObj.Value;
                        break;
                    case "int16":
                        aParm.ParameterObj._Value = parameterObj.Value;
                        break;
                    case "int32":
                        aParm.ParameterObj._Value = parameterObj.Value;
                        break;
                    case "intervaldaytosecond":
                        aParm.ParameterObj._Value = parameterObj.Value;
                        break;
                    case "intervalyeartomonth":
                        aParm.ParameterObj._Value = parameterObj.Value;
                        break;
                    case "longraw":
                        //obser to lob "Longraw"
                        aParm.ParameterObj._Value = parameterObj.Value.ToString();
                        break;
                    case "longvarchar":
                        // obser to lob "LongVarchar"
                        aParm.ParameterObj._Value = parameterObj.Value.ToString();
                        break;
                    case "nchar":
                        aParm.ParameterObj._Value = parameterObj.Value;
                        break;
                    case "nclob":
                        oraLob = (OracleLob)parameterObj.Value;
                        aParm.ParameterObj._Value = oraLob.Value.ToString();
                        break;
                    case "number":
                        aParm.ParameterObj._Value = parameterObj.Value;
                        break;
                    case "nvarchar":
                        aParm.ParameterObj._Value = parameterObj.Value;
                        break;
                    case "raw":
                        OracleBinary oraBin = (OracleBinary)parameterObj.Value;
                        aParm.ParameterObj._Value = oraBin.Value.ToString();
                        break;
                    case "rowid":
                        aParm.ParameterObj._Value = parameterObj.Value;
                        break;
                    case "sbyte":
                        aParm.ParameterObj._Value = parameterObj.Value;
                        break;
                    case "timestamplocal":
                        aParm.ParameterObj._Value = parameterObj.Value;
                        break;
                    case "timestampwithtz":
                        aParm.ParameterObj._Value = parameterObj.Value;
                        break;
                    case "uint16":
                        aParm.ParameterObj._Value = parameterObj.Value;
                        break;
                    case "uint32":
                        aParm.ParameterObj._Value = parameterObj.Value;
                        break;
                    case "varchar":
                        aParm.ParameterObj._Value = parameterObj.Value;
                        break;
                    default:
                        aParm.ParameterObj._Value = parameterObj.Value;
                        break;
                }
            }
            return aParm;
        }
        public static OracleParameter NewOracleParameter(OracleConnection orcn, OracleTransaction transaction, ClassR2 parameterObj)
        {
            if (orcn == null) return NewOracleParameter(parameterObj);
            // add case for dbType direction name value ....
            if (parameterObj == null || parameterObj.ParameterObj._Name.ToString() == "" || parameterObj.ParameterObj._DbType.ToString() == "") throw new Exception(" Parameter Can not null (Name , DbType) !");
            OracleParameter aParm = new OracleParameter();
            aParm.ParameterName = parameterObj.ParameterObj._Name.ToString();
            switch (parameterObj.ParameterObj._Direction.ToString().ToLower())
            {
                case "input":
                    aParm.Direction = ParameterDirection.Input;
                    break;
                case "output":
                    aParm.Direction = ParameterDirection.Output;
                    break;
                case "inputoutput":
                    aParm.Direction = ParameterDirection.InputOutput;
                    break;
                case "returnvalue":
                    aParm.Direction = ParameterDirection.ReturnValue;
                    break;
                default:
                    aParm.Direction = ParameterDirection.Input;
                    break;
            }
            switch (parameterObj.ParameterObj._DbType.ToString().ToLower())
            {
                case "bfile":
                    aParm.OracleType = OracleType.BFile;
                    break;
                case "blob":
                    aParm.OracleType = OracleType.Blob;
                    // aParm.Value = DB.ToBlob(orcn, parameterObj.ParameterObj._Value.ToString());
                    break;
                case "byte":
                    aParm.OracleType = OracleType.Byte;
                    if (parameterObj.ParameterObj._Size != null && parameterObj.ParameterObj._Size.ToString() != "")
                        aParm.Size = System.Convert.ToInt32(parameterObj.ParameterObj._Size);
                    if (parameterObj.ParameterObj._Value != null && parameterObj.ParameterObj._Value.ToString().Trim() != "")
                    {
                        aParm.Value = parameterObj.ParameterObj._Value;
                    }
                    else
                    {
                        aParm.Value = DBNull.Value;
                    }
                    break;
                case "char":
                    aParm.OracleType = OracleType.Char;
                    if (parameterObj.ParameterObj._Size != null && parameterObj.ParameterObj._Size.ToString() != "")
                        aParm.Size = System.Convert.ToInt32(parameterObj.ParameterObj._Size);
                    if (parameterObj.ParameterObj._Value != null && parameterObj.ParameterObj._Value.ToString().Trim() != "")
                    {
                        aParm.Value = parameterObj.ParameterObj._Value;
                    }
                    else
                    {
                        aParm.Value = DBNull.Value;
                    }
                    break;
                case "clob":
                    aParm.OracleType = OracleType.Clob;
                    aParm.Value = DB.ToClob(orcn, transaction, parameterObj.ParameterObj._Value.ToString());
                    break;
                case "cursor":
                    aParm.OracleType = OracleType.Cursor;
                    break;
                case "datetime":
                    aParm.OracleType = OracleType.DateTime;
                    if (parameterObj.ParameterObj._Value != null && parameterObj.ParameterObj._Value.ToString().Trim() != "")
                    {
                        aParm.Value = parameterObj.ParameterObj._Value;
                    }
                    else
                    {
                        aParm.Value = DBNull.Value;
                    }
                    break;
                case "double":
                    aParm.OracleType = OracleType.Double;
                    if (parameterObj.ParameterObj._Size != null && parameterObj.ParameterObj._Size.ToString() != "")
                        aParm.Size = System.Convert.ToInt32(parameterObj.ParameterObj._Size);
                    if (parameterObj.ParameterObj._Value != null && parameterObj.ParameterObj._Value.ToString().Trim() != "")
                    {
                        aParm.Value = parameterObj.ParameterObj._Value;
                    }
                    else
                    {
                        aParm.Value = DBNull.Value;
                    }
                    break;
                case "float":
                    aParm.OracleType = OracleType.Float;
                    if (parameterObj.ParameterObj._Size != null && parameterObj.ParameterObj._Size.ToString() != "")
                        aParm.Size = System.Convert.ToInt32(parameterObj.ParameterObj._Size);
                    if (parameterObj.ParameterObj._Value != null && parameterObj.ParameterObj._Value.ToString().Trim() != "")
                    {
                        aParm.Value = parameterObj.ParameterObj._Value;
                    }
                    else
                    {
                        aParm.Value = DBNull.Value;
                    }
                    break;
                case "int16":
                    aParm.OracleType = OracleType.Int16;
                    if (parameterObj.ParameterObj._Size != null && parameterObj.ParameterObj._Size.ToString() != "")
                        aParm.Size = System.Convert.ToInt32(parameterObj.ParameterObj._Size);
                    if (parameterObj.ParameterObj._Value != null && parameterObj.ParameterObj._Value.ToString().Trim() != "")
                    {
                        aParm.Value = parameterObj.ParameterObj._Value;
                    }
                    else
                    {
                        aParm.Value = DBNull.Value;
                    }
                    break;
                case "int32":
                    aParm.OracleType = OracleType.Int32;
                    if (parameterObj.ParameterObj._Size != null && parameterObj.ParameterObj._Size.ToString() != "")
                        aParm.Size = System.Convert.ToInt32(parameterObj.ParameterObj._Size);
                    if (parameterObj.ParameterObj._Value != null && parameterObj.ParameterObj._Value.ToString().Trim() != "")
                    {
                        aParm.Value = parameterObj.ParameterObj._Value;
                    }
                    else
                    {
                        aParm.Value = DBNull.Value;
                    }
                    break;
                case "intervaldaytosecond":
                    aParm.OracleType = OracleType.IntervalDayToSecond;
                    if (parameterObj.ParameterObj._Value != null && parameterObj.ParameterObj._Value.ToString().Trim() != "")
                    {
                        aParm.Value = parameterObj.ParameterObj._Value;
                    }
                    else
                    {
                        aParm.Value = DBNull.Value;
                    }
                    break;
                case "intervalyeartomonth":
                    aParm.OracleType = OracleType.IntervalYearToMonth;
                    if (parameterObj.ParameterObj._Value != null && parameterObj.ParameterObj._Value.ToString().Trim() != "")
                    {
                        aParm.Value = parameterObj.ParameterObj._Value;
                    }
                    else
                    {
                        aParm.Value = DBNull.Value;
                    }
                    break;
                case "longraw":
                    aParm.OracleType = OracleType.LongRaw;
                    if (parameterObj.ParameterObj._Size != null && parameterObj.ParameterObj._Size.ToString() != "")
                        aParm.Size = System.Convert.ToInt32(parameterObj.ParameterObj._Size);
                    if (parameterObj.ParameterObj._Value != null && parameterObj.ParameterObj._Value.ToString().Trim() != "")
                    {
                        aParm.Value = parameterObj.ParameterObj._Value;
                    }
                    else
                    {
                        aParm.Value = DBNull.Value;
                    }
                    break;
                case "longvarchar":
                    aParm.OracleType = OracleType.LongVarChar;
                    if (parameterObj.ParameterObj._Size != null && parameterObj.ParameterObj._Size.ToString() != "")
                        aParm.Size = System.Convert.ToInt32(parameterObj.ParameterObj._Size);
                    if (parameterObj.ParameterObj._Value != null && parameterObj.ParameterObj._Value.ToString().Trim() != "")
                    {
                        aParm.Value = parameterObj.ParameterObj._Value;
                    }
                    else
                    {
                        aParm.Value = DBNull.Value;
                    }
                    break;
                case "nchar":
                    aParm.OracleType = OracleType.NChar;
                    if (parameterObj.ParameterObj._Size != null && parameterObj.ParameterObj._Size.ToString() != "")
                        aParm.Size = System.Convert.ToInt32(parameterObj.ParameterObj._Size);
                    if (parameterObj.ParameterObj._Value != null && parameterObj.ParameterObj._Value.ToString().Trim() != "")
                    {
                        aParm.Value = parameterObj.ParameterObj._Value;
                    }
                    else
                    {
                        aParm.Value = DBNull.Value;
                    }
                    break;
                case "nclob":
                    aParm.OracleType = OracleType.NClob;
                    if (parameterObj.ParameterObj._Size != null && parameterObj.ParameterObj._Size.ToString() != "")
                        aParm.Size = System.Convert.ToInt32(parameterObj.ParameterObj._Size);
                    break;
                case "number":
                    aParm.OracleType = OracleType.Number;
                    if (parameterObj.ParameterObj._Size != null && parameterObj.ParameterObj._Size.ToString() != "")
                        aParm.Size = System.Convert.ToInt32(parameterObj.ParameterObj._Size);
                    if (parameterObj.ParameterObj._Value != null && parameterObj.ParameterObj._Value.ToString().Trim() != "")
                    {
                        aParm.Value = parameterObj.ParameterObj._Value;
                    }
                    else
                    {
                        aParm.Value = DBNull.Value;
                    }
                    break;
                case "nvarchar":
                    aParm.OracleType = OracleType.NVarChar;
                    if (parameterObj.ParameterObj._Size != null && parameterObj.ParameterObj._Size.ToString() != "")
                        aParm.Size = System.Convert.ToInt32(parameterObj.ParameterObj._Size);
                    if (parameterObj.ParameterObj._Value != null && parameterObj.ParameterObj._Value.ToString().Trim() != "")
                    {
                        aParm.Value = parameterObj.ParameterObj._Value;
                    }
                    else
                    {
                        aParm.Value = DBNull.Value;
                    }
                    break;
                case "raw":
                    aParm.OracleType = OracleType.Raw;
                    if (parameterObj.ParameterObj._Size != null && parameterObj.ParameterObj._Size.ToString() != "")
                        aParm.Size = System.Convert.ToInt32(parameterObj.ParameterObj._Size);
                    if (parameterObj.ParameterObj._Value != null && parameterObj.ParameterObj._Value.ToString().Trim() != "")
                    {
                        aParm.Value = parameterObj.ParameterObj._Value;
                    }
                    else
                    {
                        aParm.Value = DBNull.Value;
                    }
                    break;
                case "rowid":
                    aParm.OracleType = OracleType.RowId;
                    if (parameterObj.ParameterObj._Size != null && parameterObj.ParameterObj._Size.ToString() != "")
                        aParm.Size = System.Convert.ToInt32(parameterObj.ParameterObj._Size);
                    if (parameterObj.ParameterObj._Value != null && parameterObj.ParameterObj._Value.ToString().Trim() != "")
                    {
                        aParm.Value = parameterObj.ParameterObj._Value;
                    }
                    else
                    {
                        aParm.Value = DBNull.Value;
                    }
                    break;
                case "sbyte":
                    aParm.OracleType = OracleType.SByte;
                    if (parameterObj.ParameterObj._Size != null && parameterObj.ParameterObj._Size.ToString() != "")
                        aParm.Size = System.Convert.ToInt32(parameterObj.ParameterObj._Size);
                    if (parameterObj.ParameterObj._Value != null && parameterObj.ParameterObj._Value.ToString().Trim() != "")
                    {
                        aParm.Value = parameterObj.ParameterObj._Value;
                    }
                    else
                    {
                        aParm.Value = DBNull.Value;
                    }
                    break;
                case "timestamplocal":
                    aParm.OracleType = OracleType.TimestampLocal;
                    if (parameterObj.ParameterObj._Value != null && parameterObj.ParameterObj._Value.ToString().Trim() != "")
                    {
                        aParm.Value = parameterObj.ParameterObj._Value;
                    }
                    else
                    {
                        aParm.Value = DBNull.Value;
                    }
                    break;
                case "timestampwithtz":
                    aParm.OracleType = OracleType.TimestampWithTZ;
                    if (parameterObj.ParameterObj._Value != null && parameterObj.ParameterObj._Value.ToString().Trim() != "")
                    {
                        aParm.Value = parameterObj.ParameterObj._Value;
                    }
                    else
                    {
                        aParm.Value = DBNull.Value;
                    }
                    break;
                case "uint16":
                    aParm.OracleType = OracleType.UInt16;
                    if (parameterObj.ParameterObj._Size != null && parameterObj.ParameterObj._Size.ToString() != "")
                        aParm.Size = System.Convert.ToInt32(parameterObj.ParameterObj._Size);
                    if (parameterObj.ParameterObj._Value != null && parameterObj.ParameterObj._Value.ToString().Trim() != "")
                    {
                        aParm.Value = parameterObj.ParameterObj._Value;
                    }
                    else
                    {
                        aParm.Value = DBNull.Value;
                    }
                    break;
                case "uint32":
                    aParm.OracleType = OracleType.UInt32;
                    if (parameterObj.ParameterObj._Size != null && parameterObj.ParameterObj._Size.ToString() != "")
                        aParm.Size = System.Convert.ToInt32(parameterObj.ParameterObj._Size);
                    if (parameterObj.ParameterObj._Value != null && parameterObj.ParameterObj._Value.ToString().Trim() != "")
                    {
                        aParm.Value = parameterObj.ParameterObj._Value;
                    }
                    else
                    {
                        aParm.Value = DBNull.Value;
                    }
                    break;
                case "varchar":
                    aParm.OracleType = OracleType.VarChar;
                    if (parameterObj.ParameterObj._Size != null && parameterObj.ParameterObj._Size.ToString() != "")
                        aParm.Size = System.Convert.ToInt32(parameterObj.ParameterObj._Size);
                    if (parameterObj.ParameterObj._Value != null && parameterObj.ParameterObj._Value.ToString().Trim() != "")
                    {
                        aParm.Value = parameterObj.ParameterObj._Value;
                    }
                    else
                    {
                        aParm.Value = DBNull.Value;
                    }
                    break;
                default:
                    aParm.OracleType = OracleType.VarChar;
                    if (parameterObj.ParameterObj._Size != null && parameterObj.ParameterObj._Size.ToString() != "")
                        aParm.Size = System.Convert.ToInt32(parameterObj.ParameterObj._Size);
                    if (parameterObj.ParameterObj._Value != null && parameterObj.ParameterObj._Value.ToString().Trim() != "")
                    {
                        aParm.Value = parameterObj.ParameterObj._Value;
                    }
                    else
                    {
                        aParm.Value = DBNull.Value;
                    }
                    break;
            }
            return aParm;
        }
        public static ClassR2 NewParmClassR2(string paramName, string paramDbType, string paramDirection, object paramValue, object paramSize)
        {
            ClassR2 tmpc2 = new ClassR2();
            tmpc2.ParameterObj = new PassParameterR();
            tmpc2.ParameterObj._Name = paramName; // parameter name  i_user_account
            tmpc2.ParameterObj._DbType = paramDbType; // parameter dbtype  varchar2 clob
            tmpc2.ParameterObj._Direction = paramDirection; // parameter direction input output intputoutput returnvalue 
            tmpc2.ParameterObj._Value = paramValue; // parameter value
            tmpc2.ParameterObj._Size = paramSize; // parameter size Especially for output varchar should assign the length value
            return tmpc2;
        }
        public static Dictionary<string, ClassR2> ClassR2ToDict(List<ClassR2> dbParameters)
        {
            Dictionary<string, ClassR2> tmpDict = new Dictionary<string, ClassR2>();
            for (int i = 0; i < dbParameters.Count; i++)
            {
                tmpDict.Add(dbParameters[i].ParameterObj._Name, dbParameters[i]);
            }
            return tmpDict;
        }
        public static Dictionary<string, ClassR2> ClassR2ToDict(ClassR2[] dbParameters)
        {
            Dictionary<string, ClassR2> tmpDict = new Dictionary<string, ClassR2>();
            for (int i = 0; i < dbParameters.Length; i++)
            {
                tmpDict.Add(dbParameters[i].ParameterObj._Name, dbParameters[i]);
            }
            return tmpDict;
        }
        public static Dictionary<string, IDataParameter> DBParmToDict(List<IDataParameter> dbParameters)
        {
            Dictionary<string, IDataParameter> tmpDict = new Dictionary<string, IDataParameter>();
            for (int i = 0; i < dbParameters.Count; i++)
            {
                tmpDict.Add(dbParameters[i].ParameterName, dbParameters[i]);
            }
            return tmpDict;
        }
        public static Dictionary<string, IDataParameter> DBParmToDict(IDataParameter[] dbParameters)
        {
            Dictionary<string, IDataParameter> tmpDict = new Dictionary<string, IDataParameter>();
            for (int i = 0; i < dbParameters.Length; i++)
            {
                tmpDict.Add(dbParameters[i].ParameterName, dbParameters[i]);
            }
            return tmpDict;
        }


        public static string V(DataSet ds)
        {
            string Ob;
            try
            {
                Ob = ds.Tables[0].Rows[0][0].ToString();
            }
            catch
            {
                Ob = "";
            }
            return Ob;
        }
        public static string V(DataSet ds, string fieldName)
        {
            string Ob;
            try
            {
                Ob = ds.Tables[0].Rows[0][fieldName].ToString();
            }
            catch
            {
                Ob = "";
            }
            return Ob;
        }
        public static string V(DataSet ds, string fieldName, int rowi)
        {
            string Ob;
            try
            {
                Ob = ds.Tables[0].Rows[rowi][fieldName].ToString();
            }
            catch
            {
                Ob = "";
            }
            return Ob;
        }
        public static string V(DataSet ds, int Coli, int rowi)
        {
            string Ob;
            try
            {
                Ob = ds.Tables[0].Rows[rowi][Coli].ToString();
            }
            catch
            {
                Ob = "";
            }
            return Ob;
        }
        public static string GetSingleStringFromDS(DataSet ds)
        {
            string Ob;
            try
            {
                Ob = ds.Tables[0].Rows[0][0].ToString();
            }
            catch
            {
                Ob = "";
            }
            return Ob;
        }
        public static string GetSingleStringFromDS(DataSet ds, string fieldName)
        {
            string Ob;
            try
            {
                Ob = ds.Tables[0].Rows[0][fieldName].ToString();
            }
            catch
            {
                Ob = "";
            }
            return Ob;
        }
        public static string GetSingleStringFromDS(DataSet ds, int Coli, int rowi)
        {
            string Ob;
            try
            {
                Ob = ds.Tables[0].Rows[rowi][Coli].ToString();
            }
            catch
            {
                Ob = "";
            }
            return Ob;
        }
        public static string GetSingleStringFromDS(DataSet ds, string fieldName, int rowi)
        {
            string Ob;
            try
            {
                Ob = ds.Tables[0].Rows[rowi][fieldName].ToString();
            }
            catch
            {
                Ob = "";
            }
            return Ob;
        }
        public static object GetSingleValueFromDS(DataSet ds)
        {
            object Ob;
            try
            {
                Ob = ds.Tables[0].Rows[0][0];
            }
            catch
            {
                Ob = null;
            }
            return Ob;
        }
        public static object GetSingleValueFromDS(DataSet ds, string fieldName)
        {
            object Ob;
            try
            {
                Ob = ds.Tables[0].Rows[0][fieldName];
            }
            catch
            {
                Ob = null;
            }
            return Ob;
        }
        public static object GetSingleValueFromDS(DataSet ds, int Coli, int rowi)
        {
            object Ob;
            try
            {
                Ob = ds.Tables[0].Rows[rowi][Coli];
            }
            catch
            {
                Ob = null;
            }
            return Ob;
        }
        public static object GetSingleValueFromDS(DataSet ds, string fieldName, int rowi)
        {
            object Ob;
            try
            {
                Ob = ds.Tables[0].Rows[rowi][fieldName];
            }
            catch
            {
                Ob = null;
            }
            return Ob;
        }

    }
    public class PassParameterR
    {
        public String _Name { get; set; }
        public object _DbType { get; set; }
        public Object _Direction { get; set; }
        public object _Value { get; set; }
        public object _Size { get; set; }
    }
    public class ClassR2
    {
        public string dtData { get; set; }
        public string dtAttr { get; set; }
        public PassParameterR ParameterObj { get; set; }
    }

}
