//using System.Collections.Generic;


//namespace ZComm1.Oracle
//{
//    public class OraInsert
//    {
//        public string TableName { get; set; }
//        List<string> fields = new List<string>();
//        List<string> values = new List<string>();
//        public OraInsert(string tableName)
//        {
//            TableName = tableName;
//        }
//        //can't resolve which fields need add/update in Model,so no use auto product
//        public void AddFields(string field, object value, string ctype)
//        {
//            if (value == null) return;
//            fields.Add(field);
//            values.Add(ZOra.GetValueForSQL(value.ToString(), ctype));
//        }
//        public void AddFields(string field, string valueForSQ)
//        {
//            if (valueForSQ == "") return;
//            fields.Add(field);
//            values.Add(valueForSQ);
//        }
//        public string Sql()
//        {
//            string sql = "insert into " + TableName + " ({0}) values ({1}) ";
//            return string.Format(sql
//                , string.Join(",", fields)
//                , string.Join(",", values)
//                );
//        }
//        //private static List<string> GenerateInsertSQL(string TableName, DataSet ds1, IEnumerable<XElement> rows, string sUsr_Code)
//        //{
//        //    List<string> sqlList = new List<string>();
//        //    string sCRT_BYField = "";
//        //    string s1;
//        //    foreach (DataColumn dc in ds1.Tables[0].Columns)
//        //    {
//        //        s1 = dc.ColumnName;
//        //        if (s1.Length > 7 && s1.Substring(s1.Length - 7, 7) == "_CRT_BY") sCRT_BYField = s1;
//        //    }
//        //    foreach (XElement row in rows)
//        //    {
//        //        string sql = "insert into " + TableName + " ";
//        //        string sqlField = "(", sqlValue = "(";
//        //        string fieldName, fieldValue;
//        //        foreach (DataColumn dc in ds1.Tables[0].Columns)
//        //        {
//        //            fieldName = dc.ColumnName;
//        //            fieldValue = PCom.GetValueForSQL(ds1, fieldName, GetIEnumeV(row, fieldName));
//        //            if (fieldValue != "" && fieldValue != null)
//        //            {
//        //                sqlField += fieldName + ",";
//        //                sqlValue += fieldValue + ",";
//        //            }
//        //        }
//        //        if (sCRT_BYField != "")
//        //        {
//        //            sqlField += sCRT_BYField + ",";
//        //            sqlValue += "'" + sUsr_Code + "',";
//        //        }

//        //        sqlField = sqlField.Substring(0, sqlField.Length - 1) + ")";
//        //        sqlValue = sqlValue.Substring(0, sqlValue.Length - 1) + ")";
//        //        sql = sql + sqlField + " values " + sqlValue;
//        //        sqlList.Add(sql);
//        //    }
//        //    return sqlList;
//        //}
//    }
//}
