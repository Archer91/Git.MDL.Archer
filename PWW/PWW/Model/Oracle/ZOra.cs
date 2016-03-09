
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.OracleClient;
//using System.Linq;
//using PWW;
//using ZComm1;
//using ZComm1.Oracle;

//namespace ZComm1.Oracle
//{
//    public static class ZOra
//    {

//        public static string V(string sql)
//        {
//            DataSet ds = DB.GetDSFromSql(sql);
//            return ZConv.V(ds, 0, 0);
//        }

//        //public static string V(OracleCommand cmd, string sql)
//        //{
//        //    DataSet ds = DB.GetDSFromSql(cmd, sql);
//        //    return ZConv.V(ds, 0, 0);
//        //}

//        public static Int32 VI(string sql)
//        {
//            DataSet ds = DB.GetDSFromSql(sql);
//            return ZConv.VI(ds, 0, 0);
//        }

//        public static Double VD(string sql)
//        {
//            DataSet ds = DB.GetDSFromSql(sql);
//            return ZConv.VD(ds, 0, 0);
//        }

//        public static List<string> VStrings(string sql)
//        {
//            DataSet ds = DB.GetDSFromSql(sql);
//            List<string> sA = new List<string>();
//            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
//            {
//                sA.Add(ZConv.V(ds, 0, i));
//            }
//            return sA;
//        }

//        public static string GetWhere(string fieldName, string fieldValue, string CSharpType)
//        {
//            return GetWhere(new[] {fieldName}, new[] {fieldValue}, new[] {CSharpType});
//        }

//        public static string GetWhere(string[] fieldName, string[] fieldValue, string[] CSharpType)
//        {
//            List<string> ls = fieldValue.Select((t, i) => GetWhere1(fieldName[i], t, CSharpType[i])).Where(s => s != "").ToList();
//            if (ls.Count > 0)
//                return "where " + string.Join("and", ls);
//            return "";
//        }

//        public static string GetWhere1(string fieldName, string fieldValue, string cSharpType)
//        {
//            string s = "";
//            if (!string.IsNullOrEmpty(fieldValue))
//            {
//                switch (cSharpType)
//                {
//                    case "string":
//                        if (fieldValue.IndexOf('%') != -1)
//                            s = fieldName + " like " + GetValueForSQL(fieldValue, cSharpType);
//                        else
//                            s = fieldName + " =" + GetValueForSQL(fieldValue, cSharpType);
//                        break;
//                    case "DateTime":
//                        s = fieldName + " = " + GetValueForSQL(fieldValue, cSharpType);
//                        break;
//                    default:
//                        s = fieldName + " = " + GetValueForSQL(fieldValue, cSharpType);
//                        break;
//                }
//            }
//            return s;
//        }

//        public static string GetValueForSQL(string fieldValue, string CSharpType)
//        {
//            if (CSharpType.IndexOf('.') != -1)
//                CSharpType = CSharpType.Substring(CSharpType.LastIndexOf('.') + 1);
//            CSharpType = CSharpType.ToUpper();
//            switch (CSharpType)
//            {
//                case "STRING":
//                    return " '" + fieldValue + "' ";
//                case "DATETIME":
//                    if (fieldValue == "sysdate")
//                        return "  sysdate ";
//                    if (fieldValue.Length > 11)
//                        return " to_date('" + fieldValue + "','yyyy-MM-dd HH24:MI:ss') ";

//                    return " to_date('" + fieldValue + "','yyyy-MM-dd') ";
//                default:
//                    return fieldValue;
//            }
//        }


//        public static string OraV(this string fType, string fieldValue)
//        {
//            return " '" + fieldValue + "' ";
//        }

//        public static string OraV(this int fType, string fieldValue)
//        {
//            return fieldValue;
//        }

//        public static string OraV(this string fType, object fieldValue)
//        {
//            return fType.OraV(ZConv.ToStr(fieldValue));
//        }

//        public static string OraV(this DateTime fType, object fieldValue)
//        {
//            if (fieldValue.ToString() == "sysdate")
//                return "  sysdate ";
//            return " to_date('" + ZConv.ToDateTimeStr(fieldValue) + "','yyyy-MM-dd HH24:MI:ss') ";
//        }

//        public static string OraV(this int fType, object fieldValue)
//        {
//            return fType.OraV(ZConv.ToStr(fieldValue));
//        }

//        public static string OraV(this string fieldValue)
//        {
//            return " '" + fieldValue + "' ";
//        }

//        public static string OraV(this int fieldValue)
//        {
//            return ZConv.ToStr(fieldValue);
//        }

//        public static string OraV(this DateTime fieldValue)
//        {
//            if (fieldValue.ToString() == "sysdate")
//                return "  sysdate ";
//            return " to_date('" + ZConv.ToDateTimeStr(fieldValue) + "','yyyy-MM-dd HH24:MI:ss') ";
//        }





//        public static string GetValueForSQL(object fieldValue, string CSharpType)
//        {
//            if (CSharpType.IndexOf('.') != -1)
//                CSharpType = CSharpType.Substring(CSharpType.LastIndexOf('.') + 1);
//            CSharpType = CSharpType.ToUpper();
//            switch (CSharpType)
//            {
//                case "STRING":
//                    return " '" + fieldValue.ToString() + "' ";
//                case "DATETIME":
//                    if (fieldValue.ToString() == "sysdate")
//                        return "  sysdate ";

//                    return " to_date('" + ZConv.ToDateTimeStr(fieldValue) + "','yyyy-MM-dd HH24:MI:ss') ";
//                default:
//                    return fieldValue.ToString();
//            }
//        }

//        public static string GetValueForSQL(DataSet ds1, string fieldName, object oValue)
//        {
//            if (oValue == null) return null;
//            try
//            {
//                string fieldValue = "";
//                switch (ds1.Tables[0].Columns[fieldName].DataType.ToString())
//                {
//                    case "System.Decimal":
//                        fieldValue = ZConv.ToStr(oValue);
//                        break;
//                    case "System.String":
//                        if (ZConv.ToStr(oValue) == "")
//                            fieldValue = "null";
//                        else
//                            fieldValue = "'" + ZConv.ToStr(oValue).Replace("'", "''") + "'";
//                        break;
//                    case "System.DateTime":
//                        if (ZConv.ToStr(oValue).ToLower() == "sysdate")
//                            fieldValue = "sysdate";
//                        else
//                        {
//                            string sT = oValue.ToString();
//                            if (sT.Length > 11)
//                                fieldValue = "to_date('" + sT + "','yyyy-MM-dd HH24:MI:ss')";
//                            else
//                                fieldValue = "to_date('" + sT + "','yyyy-MM-dd')";
//                        }
//                        break;
//                    default:
//                        fieldValue = "'" + ZConv.ToStr(oValue).Trim().Replace("'", "''") + "'";
//                        break;
//                }
//                return fieldValue;
//            }
//            catch
//            {
//                return "";
//            }
//        }
//    }
//}