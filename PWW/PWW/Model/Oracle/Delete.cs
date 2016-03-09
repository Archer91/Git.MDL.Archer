//using System.Collections.Generic;


//namespace ZComm1.Oracle
//{
//    public class OraDelete
//    {
//        public string TableName { get; set; }
//        List<string> SqlWhere = new List<string>();
//        public OraDelete(string tableName)
//        {
//            TableName = tableName;
//        }
//        public void AddWhere(string field, int value, string ctype)
//        {
//            SqlWhere.Add(field + " = " + ZOra.GetValueForSQL(value.ToString(), ctype));
//        }
//        public void AddWhere(string field, string value, string ctype)
//        {
//            SqlWhere.Add(field + " = " + ZOra.GetValueForSQL(value, ctype));
//        }
//        public void AddWhere(string field, string valueForSQL)
//        {
//            SqlWhere.Add(field + " = " + valueForSQL);
//        }
//        public string Sql()
//        {
//            string sql = "delete " + TableName + " where {0} ";
//            return string.Format(sql
//                , string.Join(" and ", SqlWhere)
//                );
//        }
//    }
//}
