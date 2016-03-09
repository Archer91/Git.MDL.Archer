//using System;
//using System.Collections.Generic;
//using System.Reflection;
//using PWW;


//namespace ZComm1.Oracle
//{
//    public class OraEdit
//    {
//        public string TableName { get; set; }
//        List<string> fieldsvalues = new List<string>();
//        List<string> SqlWhere = new List<string>();
//        private string FieldPrefix;
//        public OraEdit(string tableName)
//        {
//            TableName = tableName;
//        }
//        public OraEdit(string tablePrefix, object ob, string fields, string wherefields, string fieldPrefix = "")
//        {
//            FieldPrefix = fieldPrefix;
//            SetField(tablePrefix, ob, fields, true);
//            if (wherefields.Length < 2) throw new Exception("Must input WhereFields");

//            Type type = ob.GetType();
//            string[] sAWhere = wherefields.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
//            foreach (string s in sAWhere)
//            {
//                PropertyInfo propertyInfo = type.GetProperty(s);
//                string sPName = propertyInfo.PropertyType.Name;
//                object ovalue = propertyInfo.GetValue(ob, null);

//                SqlWhere.Add(s + " = " + ZOra.GetValueForSQL(ovalue, sPName));
//            }
//        }
//        public OraEdit(string tablePrefix, object ob, string fields, string whereSql, bool autoAddUpdOnBy)
//        {
//            SetField(tablePrefix, ob, fields, autoAddUpdOnBy);
//            if (whereSql.Length < 2) throw new Exception("Must input WhereFields");
//            SqlWhere.Add(whereSql);
//        }

//        private void SetField(string tablePrefix, object ob, string fields, bool autoAddUpdOnBy)
//        {
//            fields = "," + fields + ",";
//            fields = fields.Replace(",,", ",");

//            //string className = ob.ToString();
//            string className = ob.GetType().Name;
//            TableName = tablePrefix + className.Substring(className.LastIndexOf('.') + 1);
//            Type type = ob.GetType();

//            string[] sA = fields.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
//            foreach (string s in sA)
//            {
//                PropertyInfo propertyInfo = type.GetProperty(s);
//                string sPName = propertyInfo.PropertyType.Name;
//                object ovalue = propertyInfo.GetValue(ob, null);

//                fieldsvalues.Add(s + " = " + ZOra.GetValueForSQL(ovalue, sPName));
//            }
//            if (autoAddUpdOnBy)
//            {
//                if (fields.IndexOf("," + FieldPrefix + "Upd_On,") == -1) fieldsvalues.Add(FieldPrefix + "Upd_On = sysdate");
//                if (fields.IndexOf("," + FieldPrefix + "Upd_By,") == -1) fieldsvalues.Add(FieldPrefix + "Upd_By = '" + DB.loginUserName + "'");
//            }
//        }

//        //can't resolve which fields need add/update in Model,so no use auto product
//        public void AddFields(string field, string value, string ctype)
//        {
//            fieldsvalues.Add(field + " = " + ZOra.GetValueForSQL(value, ctype));
//        }
//        public void AddFields(string field, string value)
//        {
//            fieldsvalues.Add(field + " = " + value);
//        }
//        public void AddWhere(string field, string value, string ctype)
//        {
//            SqlWhere.Add(field + " = " + ZOra.GetValueForSQL(value, ctype));
//        }
//        public void AddWhere(string field, string value)
//        {
//            SqlWhere.Add(field + " = " + value);
//        }
//        public string Sql()
//        {
//            string sql = "update " + TableName + " set {0} where {1} ";
//            //if (fieldsvalues.Contains())
//            return string.Format(sql
//                , string.Join(",", fieldsvalues)
//                , string.Join(" and ", SqlWhere)
//                );
//        }
//        //        string sql = "update " + TableName + " set ";
//        //        string fieldName, fieldValue, SqlWhere = " where 1=1 ";
//        //        if (sKeyF.Count == 0) throw new Exception("Not find Key in table: " + TableName + "!");
//        //        foreach (string sKey in sKeyF)
//        //        {
//        //            fieldValue = PCom.GetValueForSQL(ds1, sKey, GetIEnumeV(row, sKey));
//        //            if (fieldValue == null) throw new Exception("Not find Key value of " + sKey + "!");
//        //            SqlWhere += " and " + sKey + "=" + fieldValue;
//        //        }

//        //        string sqlSet = "";
//        //        foreach (DataColumn dc in ds1.Tables[0].Columns)
//        //        {
//        //            int iF = sKeyF.FindIndex(delegate(string sD) { return sD == dc.ColumnName; });
//        //            if (iF == -1)
//        //            {
//        //                fieldName = dc.ColumnName;
//        //                fieldValue = PCom.GetValueForSQL(ds1, fieldName, GetIEnumeV(row, fieldName));
//        //                if (fieldValue != "" && fieldValue != null)
//        //                    sqlSet += fieldName + "=" + fieldValue + ",";
//        //            }
//        //        }
//        //        if (sUPD_BYField != "") sqlSet += sUPD_BYField + "='" + sUsr_Code + "',";
//        //        sqlSet = PCom.StrRTrim(sqlSet, ",");
//        //        sqlSet = sqlSet.Replace("=null", "=''");
//        //        sql = sql + sqlSet + " " + SqlWhere;
//    }
//}
