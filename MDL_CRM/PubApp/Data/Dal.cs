using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using System.Security.Cryptography;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace PubApp.Data
{
    public static class Dal
    {
        public static string strDataBaseTy = "";//MySQL
        public enum DateInterval
        {
            Second, Minute, Hour, Day, Week, Month, Quarter, Year
        }
        public enum DataTy
        {
            Digit = 1,
            Character = 2,
            Date = 3,
            Boolean = 4
        }
        static string encryptKey = "Oyea";    //定义密钥  
        public static string strConnect = @"Password=paper;User ID=mdltest;Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.1.41)(PORT=1521)))
                    (CONNECT_DATA=(SERVER=DEDICATED)(SID=mdlmdms)));";
        public static string sqlConnect = "Server=10.10.1.153;Database=FSPDC_IDW;Uid=root;Pwd=itd3342d2;Allow User Variables=True;Ignore Prepare=false;";
        public static OracleConnection conn = null;
        public static MySqlConnection sqlconn = null;  
        public static string gWhere = "";
        public static string sUserID = "Admin";
        public static string sDepartMentID = "";
        public static bool IsAdminGroup =false;
        public static DataSet GetDataSet(string str)
        {
            DataSet ds = null;
            ds = new DataSet();
            try
            {

                Connect();
                if (strDataBaseTy.ToUpper() == "MYSQL")
                {
                    MySqlDataAdapter dp = new MySqlDataAdapter(str, sqlconn);
                    dp.Fill(ds);
                }
                else
                {
                    OracleDataAdapter dp = new OracleDataAdapter(str, conn);
                    dp.Fill(ds);
                }

            }
            catch
            {
                ds = null;
            }
            return ds;
        }
        public static DataTable GetDataTable(string str)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                Connect();
                if (strDataBaseTy.ToUpper() == "MYSQL")
                {
                    MySqlDataAdapter dp = new MySqlDataAdapter(str, sqlconn);
                    dp.Fill(ds);
                    dp.Dispose();
                }
                else
                {
                    OracleDataAdapter dp = new OracleDataAdapter(str, conn);
                    dp.Fill(ds);
                    dp.Dispose();
                }

                dt = ds.Tables[0];

            }
            catch(Exception ex)
            {
                dt = null;
                MessageBox.Show(ex.Message);
            }
            return dt;
        }
        //public static DataTable GetDataTable(string str, string sTable, int intStart, int intEnd)
        //{
        //    DataSet ds = new DataSet();
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        Connect();
        //        OracleDataAdapter dp = new OracleDataAdapter(str, conn);
        //        dp.Fill(ds, intStart, intEnd, sTable);
        //        dt = ds.Tables[0];

        //    }
        //    catch
        //    {
        //        dt = null;
        //    }
        //    return dt;
        //}
        public static bool Connect()
        {
            bool blnOk = false;
            if (strDataBaseTy.ToUpper() == "MYSQL")
            {
                if (sqlconn == null || sqlconn.State == System.Data.ConnectionState.Broken)
                {
                    sqlconn = new MySqlConnection(sqlConnect);
                    sqlconn.Open();
                }
                if (sqlconn.State == System.Data.ConnectionState.Closed)
                {
                    sqlconn.Open();
                    
                }
            }
            else
            {
                if (conn == null || conn.State == System.Data.ConnectionState.Broken)
                {
                    conn = new OracleConnection(strConnect);
                    conn.Open();
                }
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
            }
            try
            {
                blnOk = true;
            }
            catch
            {
                blnOk = false;
            }
            return blnOk;
        }
        public static bool ChkCnSatatu()
        {
            bool blnOk = true;
            if (conn == null) { blnOk = false; }
            if (conn.State == System.Data.ConnectionState.Closed) { blnOk = false; }
            if (conn.State == System.Data.ConnectionState.Broken) { blnOk = false; }
            return blnOk;
        }
        public static bool ChkDataTable(DataTable dt)
        {
            bool blnOk = true;
            if (dt == null) { return false; }
            if (dt.Rows.Count == 0) { return false; }
            return blnOk;
        }
        public static string strGetValue(string str)
        {
            string strvalue = "";
            object t;
            Connect();
            if (strDataBaseTy.ToUpper() == "MYSQL")
            {
                MySqlCommand cm = new MySqlCommand(str, sqlconn);
                t = cm.ExecuteScalar();
            }
            else
            {
                OracleCommand cm = new OracleCommand(str, conn);
                t = cm.ExecuteScalar();
            }

            if (t != null)
            { strvalue = t.ToString(); }
            return strvalue;
        }
        public static string strGetValue(string str, params Object[] parameterValues)
        {
            string strvalue = "";
            object t;
            Connect();
            if (strDataBaseTy.ToUpper() == "MYSQL")
            {
                MySqlCommand cm = new MySqlCommand(str, sqlconn);
                for (int i = 0; i < parameterValues.Length; i++)
                {
                    cm.Parameters.Add(new MySqlParameter("?"+(i+1) , parameterValues[i] == null ? DBNull.Value : parameterValues[i]));
                }

                t = cm.ExecuteScalar();
            }
            else
            {
                OracleCommand cm = new OracleCommand(str, conn);
                for (int i = 0; i < parameterValues.Length; i++)
                {
                    cm.Parameters.Add(new OracleParameter(":" + (i + 1), parameterValues[i] == null ? DBNull.Value : parameterValues[i]));
                }
                t = cm.ExecuteScalar();
            }
            if (t != null)
            { strvalue = t.ToString(); }
            return strvalue;
        }
        public static object GetValue(string str, params Object[] parameterValues)
        {
            object t;
            Connect();
            if (strDataBaseTy.ToUpper() == "MYSQL")
            {
                MySqlCommand cm = new MySqlCommand(str, sqlconn);
                for (int i = 0; i < parameterValues.Length; i++)
                {
                    cm.Parameters.Add(new MySqlParameter(":" + (i + 1), parameterValues[i] == null ? DBNull.Value : parameterValues[i]));
                }
                t = cm.ExecuteScalar();
            }
            else
            {
                OracleCommand cm = new OracleCommand(str, conn);
                for (int i = 0; i < parameterValues.Length; i++)
                {
                    cm.Parameters.Add(new OracleParameter(":" + (i + 1), parameterValues[i] == null ? DBNull.Value : parameterValues[i]));
                }
                t = cm.ExecuteScalar();
            }
            return t;
        }
        public static object GetValue(string str)
        {
            object t;
            Connect();
            if (strDataBaseTy.ToUpper() == "MYSQL")
            {
                MySqlCommand cm = new MySqlCommand(str, sqlconn);
                t = cm.ExecuteScalar();
            }
            else
            {
                OracleCommand cm = new OracleCommand(str, conn);
                t = cm.ExecuteScalar();
            }
            return t;
        }
        public static bool ExeCommnd(string str, out string sInfo, params Object[] parameterValues)
        {
            bool blnok = false;
            int intm;
            sInfo = "";
            Connect();
            if (strDataBaseTy.ToUpper() == "MYSQL")
            {
                MySqlTransaction tr = null;
                tr = sqlconn.BeginTransaction();
                try
                {

                    MySqlCommand cm = new MySqlCommand();
                    cm.CommandType = CommandType.Text;
                    cm.Connection = sqlconn;
                    cm.Transaction = tr;
                    cm.CommandText = str;
                    //cm.CommandTimeout = 20;
                    for (int i = 0; i < parameterValues.Length; i++)
                    {
                        cm.Parameters.Add(new OracleParameter("?" + (i + 1), parameterValues[i] == null ? DBNull.Value : parameterValues[i]));
                    }
                    intm = cm.ExecuteNonQuery();
                    tr.Commit();
                    blnok = true;
                    cm.Dispose();
                    tr.Dispose();

                }
                catch (Exception ex)
                {
                    sInfo = ex.Message;
                    tr.Rollback();
                }
            }
            else
            {
                OracleTransaction tr = null;
                tr = conn.BeginTransaction();
                try
                {
                    OracleCommand cm = new OracleCommand();
                    cm.CommandType = CommandType.Text;
                    cm.Connection = conn;
                    cm.Transaction = tr;
                    cm.CommandText = str;
                    //cm.CommandTimeout = 20;
                    for (int i = 0; i < parameterValues.Length; i++)
                    {
                        cm.Parameters.Add(new OracleParameter(":" + (i + 1), parameterValues[i] == null ? DBNull.Value : parameterValues[i]));
                    }
                    intm = cm.ExecuteNonQuery();
                    tr.Commit();
                    blnok = true;
                    cm.Dispose();
                    tr.Dispose();

                }
                catch (Exception ex)
                {
                    sInfo = ex.Message;
                    tr.Rollback();
                }
            }

            return blnok;
        }
        public static bool ExeCommnd(string str, out string sInfo)
        {
            bool blnok = false;
            int intm;

            sInfo = "";
            Connect();
            if (strDataBaseTy.ToUpper() == "MYSQL")
            {
                MySqlTransaction tr = null;
                tr = sqlconn.BeginTransaction();
                try
                {
                    MySqlCommand cm = new MySqlCommand();
                    cm.CommandType = CommandType.Text;
                    cm.Connection = sqlconn;
                    cm.Transaction = tr;
                    cm.CommandText = str;
                    //cm.CommandTimeout = 20;
                    intm = cm.ExecuteNonQuery();
                    tr.Commit();
                    blnok = true;
                    cm.Dispose();
                    tr.Dispose();

                }
                catch(MySqlException t)
                {
                    int intn = t.Number;
                    switch (intn)
                    {
                        case 1062:
                            {
                                sInfo = "Duplicate PRIMARY key";    
                                break;
                            }
                        case 1452:
                            {
                                sInfo = "a foreign key constraint fails ";    
                                break;
                            }
                        default:
                            {
                                sInfo = t.Message;    
                                break;
                            }
                    }
                
                    tr.Rollback();
                }
            }
            else
            {
                OracleTransaction tr = null;
                tr = conn.BeginTransaction();
                try
                {
                    OracleCommand cm = new OracleCommand();
                    cm.CommandType = CommandType.Text;
                    cm.Connection = conn;
                    cm.Transaction = tr;
                    cm.CommandText = str;
                    //cm.CommandTimeout = 20;
                    intm = cm.ExecuteNonQuery();
                    tr.Commit();
                    blnok = true;
                    cm.Dispose();
                    tr.Dispose();

                }
                catch (Exception ex)
                {
                   
                    sInfo = ex.Message;
                    tr.Rollback();
                }
            }
            return blnok;
        }
        public static string GetServerDate(bool blnVisibleTime)
        {
            string sDate = "";
            if (strDataBaseTy.ToUpper() == "MYSQL")
            {
                sDate = strGetValue("select sysdate()");
            }
            else
            {
                if (blnVisibleTime == true)
                {
                    sDate = strGetValue("select to_char(sysdate, 'mm/dd/yyyy hh24:mi:ss') from dual");
                }
                else
                {
                    sDate = strGetValue("select to_char(sysdate, 'mm/dd/yyyy') from dual");
                }
            }
            return sDate;
        }
        public static bool BatchSave(string str, DataTable dt, out string sError)
        {
            bool bln = false;
            sError = "";
            try
            {
                Connect();
                if (strDataBaseTy.ToUpper() == "MYSQL")
                {
                    MySqlDataAdapter dp = new MySqlDataAdapter(str, sqlconn);
                    MySqlCommandBuilder cm = new MySqlCommandBuilder(dp);
                    dp.Update(dt);
                }
                else
                {
                    OracleDataAdapter dp = new OracleDataAdapter(str, conn);
                    OracleCommandBuilder cm = new OracleCommandBuilder(dp);
                    dp.Update(dt);
                }

                bln = true;
            }
            catch (Exception ex)
            {
                sError = ex.Message;
                bln = false;
            }
            return bln;
        }
        public static void BindingObject(Control.ControlCollection ctls, DataTable dt)
        {
            if (dt == null) { return; }
            string sFiled;
            try
            {

                foreach (Control o in ctls)
                {
                    if (o.Controls.Count > 0 && IfContainer(o) == true)
                    {
                        BindingObject(o.Controls, dt);
                    }
                    else
                    {
                        if (o.Tag != null)
                        {
                            sFiled = o.Tag.ToString();
                            if (sFiled != "")
                            {
                                o.DataBindings.Clear();
                                switch (o.GetType().Name)
                                {
                                    case "TextBox":
                                    case "MaskedTextBox":
                                    case "ComboBox":
                                    case "RichTextBox":
                                    case "DateTimePicker":
                                        {
                                            o.DataBindings.Add("Text", dt, sFiled);
                                            break;
                                        }
                                    case "NumericUpDown":
                                        {
                                            NumericUpDown objcon = (NumericUpDown)o;
                                            o.DataBindings.Add("Value", dt, sFiled);
                                            break;
                                        }
                                    case "CheckBox":
                                        {
                                            o.DataBindings.Add("Checked", dt, sFiled);
                                            break;
                                        }
                                    case "RadioButton":
                                        {
                                            o.DataBindings.Add("Value", dt, sFiled);
                                            break;
                                        }
 
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public static void ClearBinding(Control.ControlCollection ctls)
        {
            string sFiled;
            try
            {

                foreach (Control o in ctls)
                {
                    if (o.Controls.Count > 0 && IfContainer(o) == true)
                    {
                        ClearBinding(o.Controls);
                    }
                    else
                    {
                        if (o.Tag != null)
                        {
                            sFiled = o.Tag.ToString();
                            if (sFiled != "")
                            {
                                o.DataBindings.Clear();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public static void EnabledControl(Control.ControlCollection ctls, int intMode)
        {

            try
            {
                bool bln = false;
                if (intMode == 1 || intMode == 2)
                { bln = true; }
                if (intMode == 3)
                { bln = false; }
                Panel oPanel = new Panel();
                foreach (Control o in ctls)
                {
                    if (o.Controls.Count > 0 && IfContainer(o) == true)
                    {
                        EnabledControl(o.Controls, intMode);
                    }
                    else
                    {
                        switch (o.GetType().Name)
                        {
                            case "RichTextBox":
                                {
                                    RichTextBox tx = (RichTextBox)o;
                                    tx.ReadOnly = !bln;

                                    if (bln == true)
                                    {
                                        tx.BackColor = Color.White;
                                        tx.ForeColor = Color.Black;
                                    }
                                    else
                                    {
                                        tx.BackColor = oPanel.BackColor;
                                        tx.ForeColor = Color.Blue;
                                    }
                                    break;
                                }
                            case "TextBox":
                                {
                                    TextBox tx = (TextBox)o;
                                    tx.ReadOnly = !bln;
                                    if (bln == true)
                                    {
                                        tx.BackColor = Color.White;
                                        tx.ForeColor = Color.Black;
                                    }
                                    else
                                    {
                                        tx.BackColor = oPanel.BackColor;
                                        tx.ForeColor = Color.Blue;
                                    }
                                    break;
                                }
                            case "MaskedTextBox":
                                {
                                    MaskedTextBox tx = (MaskedTextBox)o;
                                    tx.ReadOnly = !bln;
                                    if (bln == true)
                                    {
                                        tx.BackColor = Color.White;
                                        tx.ForeColor = Color.Black;
                                    }
                                    else
                                    {
                                        tx.BackColor = oPanel.BackColor;
                                        tx.ForeColor = Color.Blue;
                                    }
                                    break;
                                }
                            case "DateTimePicker":
                                {
                                    DateTimePicker tx = (DateTimePicker)o;
                                    tx.Enabled = bln;
                                    if (bln == true)
                                    {
                                        tx.BackColor = oPanel.BackColor;
                                    }
                                    else
                                    {
                                        tx.BackColor = Color.White;
                                    }
                                    break;
                                }
                            case "CheckBox":
                            case "CheckedListBox":
                           // case "ListBox":
                            case "RadioButton":
                            case "Button":
                            case "ComboBox":
                                {
                                    o.Enabled = bln;
                                    
                                    break;
                                }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public static void BlankControl(Control.ControlCollection ctls)
        {
            try
            {
                foreach (Control o in ctls)
                {
                    if (o.Controls.Count > 0 && IfContainer(o) == true)
                    {
                        BlankControl(o.Controls);
                    }
                    else
                    {
                        switch (o.GetType().Name)
                        {
                            case "TextBox":
                            case "MaskedTextBox":
                            case "DateTimePicker":
                            case "RichTextBox":
                            case "ComboBox":
                                {
                                    o.Text = "";
                                    break;
                                }
                            case "CheckBox":
                                {
                                    CheckBox chk = (CheckBox)o;
                                    chk.Checked = false;
                                    break;
                                }
                            case "RadioButton":
                                {
                                    RadioButton chk = (RadioButton)o;
                                    chk.Checked = false;
                                    break;
                                }
                            case "ListBox":
                                {
                                    ListBox con = (ListBox)o;
                                    con.Items.Clear();
                                    break;
                                }
                            case "CheckedListBox":
                                {
                                    CheckedListBox con = (CheckedListBox)o;
                                    for (int intn1 = 0; intn1 < con.Items.Count; intn1++)
                                    {
                                        con.SetItemChecked(intn1, false);
                                    }
                                    break;
                                }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void EditKeyControl(Panel oPanel, string sKeys)
        {

            string sFiled;
            try
            {
                foreach (Control o in oPanel.Controls)
                {
                    if (o.Tag != null)
                    {
                        sFiled = o.Tag.ToString();
                        if (sFiled != "")
                        {
                            if (sKeys.IndexOf(sFiled) >= 0)
                            {
                                switch (o.GetType().Name)
                                {
                                    case "RichTextBox":
                                        {
                                            RichTextBox tx = (RichTextBox)o;
                                            tx.ReadOnly = true;
                                            tx.BackColor = oPanel.BackColor;
                                            tx.ForeColor = Color.Blue;
                                            break;
                                        }
                                    case "TextBox":
                                        {
                                            TextBox tx = (TextBox)o;
                                            tx.ReadOnly = true;
                                            tx.BackColor = oPanel.BackColor;
                                            tx.ForeColor = Color.Blue;
                                            break;
                                        }
                                    case "MaskedTextBox":
                                        {
                                            MaskedTextBox tx = (MaskedTextBox)o;
                                            tx.ReadOnly = true;
                                            tx.BackColor = oPanel.BackColor;
                                            tx.ForeColor = Color.Blue;
                                            break;
                                        }
                                    case "DateTimePicker":
                                        {
                                            DateTimePicker tx = (DateTimePicker)o;
                                            tx.Enabled = false;

                                            tx.BackColor = oPanel.BackColor;
                                            break;
                                        }
                                    case "CheckBox":

                                    case "RadioButton":
                                    case "ComboBox":
                                        {
                                            o.Enabled = false;
                                            break;
                                        }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public static int GetLength_Obsoleted(string str)
        {
            if (str == null || str.Length == 0) { return 0; }
            byte[] data = Encoding.Default.GetBytes(str);
            return data.Length;
        }
        public static int GetLength(string str)
        {
            if (str == null || str.Length == 0) { return 0; }

            int l = str.Length;
            int realLen = l;

            #region 计算长度
            int clen = 0;//当前长度
            while (clen < l)
            {
                //每遇到一个中文，则将实际长度加一。
                if ((int)str[clen] > 128) { realLen++; }
                clen++;
            }
            #endregion

            return realLen;
        }
        #region 加密字符串
        /// <summary> /// 加密字符串   
        /// </summary>  
        /// <param name="str">要加密的字符串</param>  
        /// <returns>加密后的字符串</returns>  
        public static string Encrypt(string str)
        {
            try
            {
                if (str == "") { return ""; }
                DESCryptoServiceProvider descsp = new DESCryptoServiceProvider();   //实例化加/解密类对象   

                byte[] key = Encoding.Unicode.GetBytes(encryptKey); //定义字节数组，用来存储密钥    

                byte[] data = Encoding.Unicode.GetBytes(str);//定义字节数组，用来存储要加密的字符串  

                MemoryStream MStream = new MemoryStream(); //实例化内存流对象      

                //使用内存流实例化加密流对象   
                CryptoStream CStream = new CryptoStream(MStream, descsp.CreateEncryptor(key, key), CryptoStreamMode.Write);

                CStream.Write(data, 0, data.Length);  //向加密流中写入数据      

                CStream.FlushFinalBlock();              //释放加密流      

                return Convert.ToBase64String(MStream.ToArray());//返回加密后的字符串  
            }
            catch
            { return ""; }
        }
        #endregion

        #region 解密字符串
        /// <summary>  
        /// 解密字符串   
        /// </summary>  
        /// <param name="str">要解密的字符串</param>  
        /// <returns>解密后的字符串</returns>  
        public static string Decrypt(string str)
        {
            try
            {
                if (str == "") { return ""; }
                DESCryptoServiceProvider descsp = new DESCryptoServiceProvider();   //实例化加/解密类对象    

                byte[] key = Encoding.Unicode.GetBytes(encryptKey); //定义字节数组，用来存储密钥    

                byte[] data = Convert.FromBase64String(str);//定义字节数组，用来存储要解密的字符串  

                MemoryStream MStream = new MemoryStream(); //实例化内存流对象      

                //使用内存流实例化解密流对象       
                CryptoStream CStream = new CryptoStream(MStream, descsp.CreateDecryptor(key, key), CryptoStreamMode.Write);

                CStream.Write(data, 0, data.Length);      //向解密流中写入数据     

                CStream.FlushFinalBlock();               //释放解密流      
                return Encoding.Unicode.GetString(MStream.ToArray());       //返回解密后的字符串  
            }
            catch
            { return ""; }

        }
        #endregion
        private static bool DataTableExistsField(DataTable dt, string strField)
        {
            bool blnOk = false;
            blnOk=dt.Columns.Contains(strField);
 
            return blnOk;
        }
        public static bool IfContainer(Control con)  //判断是否是容器
        {
            bool blnYn;
            switch (con.GetType().Name)
            {
                case "TabPage":
                case "TabControl":
                case "GroupBox":
                case "SplitContainer":
                case "Panel":
                case "FlowLayoutPanel":
                case "TableLayoutPanel":
                    {
                        blnYn = true;
                        break;
                    }
                default:
                    {
                        blnYn = false;
                        break;
                    }
            }
           
            return blnYn;
        }
        public static bool IsNumber(string strNumber)
        {
            string strValidRealPattern = @"^[-\+]?(([1-9]{1}[0-9]*)|([0]))(\.\d+)?$";
            Regex objNumberPattern = new Regex(strValidRealPattern);
            return objNumberPattern.IsMatch(strNumber);
        }
        public static bool IsInteger(string strNumber)
        {
            Regex regex = new Regex("[^0-9]");
            return !regex.IsMatch(strNumber);
        }
        public static DateTime DateAdd(String interval, Double number, DateTime date)
        {
            String tempInterval = interval.ToLower();

            switch (tempInterval)
            {
                //年
                case "year":
                case "yyyy":
                case "yy":
                    return date.AddYears(Convert.ToInt32(number));

                //月
                case "month":
                case "mm":
                case "m":
                    return date.AddMonths(Convert.ToInt32(number));

                //日
                case "day":
                case "dd":
                case "d":
                    return date.AddDays(number);

                //小时
                case "hour":
                case "hh":
                case "h":
                    return date.AddHours(number);

                //分钟
                case "minute":
                case "mi":
                    return date.AddMinutes(number);

                //秒
                case "second":
                case "ss":
                case "s":
                    return date.AddSeconds(number);

                //毫秒
                case "millisecond":
                case "ms":
                    return date.AddMilliseconds(number);

                default:
                    return date;

            }
        }
        public static Double DatePart(String interval, DateTime date)
        {
            String tempInterval = interval.ToLower();

            switch (tempInterval)
            {
                //年
                case "year":
                case "yyyy":
                case "yy":
                    return Convert.ToDouble(date.Year);
                //月
                case "month":
                case "mm":
                case "m":
                    return Convert.ToDouble(date.Month);
                //日
                case "day":
                case "dd":
                case "d":
                    return Convert.ToDouble(date.Day);
                //小时
                case "hour":
                case "hh":
                case "h":
                    return Convert.ToDouble(date.Hour);
                //分钟
                case "minute":
                case "mi":
                    return Convert.ToDouble(date.Minute);
                //秒
                case "second":
                case "ss":
                case "s":
                    return Convert.ToDouble(date.Second);
                //毫秒
                case "millisecond":
                case "ms":
                    return Convert.ToDouble(date.Millisecond);
                //星期
                case "dayofweek":
                    return Convert.ToDouble(date.DayOfWeek);
                //在当年第几天
                case "dayofyear":
                    return Convert.ToDouble(date.DayOfYear);
                default:
                    return 0.0000;
            }
        }
        public static long DateDiff(DateInterval Interval, System.DateTime StartDate, System.DateTime EndDate)
        {
            long lngDateDiffValue = 0;
            System.TimeSpan TS = new System.TimeSpan(EndDate.Ticks - StartDate.Ticks);
            switch (Interval)
            {
                case DateInterval.Second:
                    lngDateDiffValue = (long)TS.TotalSeconds;
                    break;
                case DateInterval.Minute:
                    lngDateDiffValue = (long)TS.TotalMinutes;
                    break;
                case DateInterval.Hour:
                    lngDateDiffValue = (long)TS.TotalHours;
                    break;
                case DateInterval.Day:
                    lngDateDiffValue = (long)TS.Days;
                    break;
                case DateInterval.Week:
                    lngDateDiffValue = (long)(TS.Days / 7.0);
                    break;
                case DateInterval.Month:
                    lngDateDiffValue = (long)(TS.Days / 30.0);
                    break;
                case DateInterval.Quarter:
                    lngDateDiffValue = (long)((TS.Days / 30.0) / 3.0);
                    break;
                case DateInterval.Year:
                    lngDateDiffValue = (long)(TS.Days / 365.0);
                    break;
            }
            return (lngDateDiffValue);
        }//end of DateDiff
        public static bool DataGridviewShowToExcel(DataGridView dgv, bool isShowExcle)
        {
            if (dgv.Rows.Count == 0)
                return false;    //建立Excel对象   
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();

            excel.Application.Workbooks.Add(true);
            excel.Visible = isShowExcle;
            //生成字段名称   
            for (int i = 0; i < dgv.ColumnCount; i++)
            {
                excel.Cells[1, i + 1] = dgv.Columns[i].HeaderText;
            }
            //填充数据   
            for (int i = 0; i < dgv.RowCount - 1; i++)
            {
                for (int j = 0; j < dgv.ColumnCount; j++)
                {
                    if (dgv[j, i].ValueType == typeof(string))
                    {
                        excel.Cells[i + 2, j + 1] = "'" + dgv[j, i].Value.ToString();
                    }
                    else
                    {
                        excel.Cells[i + 2, j + 1] = dgv[j, i].Value.ToString();
                    }
                }
            }

            return true;
        }
        #region 导出当前页DataGridView中的数据到Word中

        public static void ExportDataGridViewToWord(DataGridView srcDgv, ProgressBar progreesBar, SaveFileDialog sfile)
        {
            if (srcDgv.Rows.Count == 0)
            {
                MessageBox.Show("没有数据可供导出！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                sfile.AddExtension = true;
                sfile.DefaultExt = ".doc";
                sfile.Filter = "(*.doc)|*.doc";
                if (sfile.ShowDialog() == DialogResult.OK)
                {
                    progreesBar.Visible = true;
                    object path = sfile.FileName;
                    Object none = System.Reflection.Missing.Value;
                    Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                    Microsoft.Office.Interop.Word.Document document = wordApp.Documents.Add(ref none, ref none, ref none, ref none);
                    //建立表格
                    Microsoft.Office.Interop.Word.Table table = document.Tables.Add(document.Paragraphs.Last.Range, srcDgv.Rows.Count, srcDgv.Columns.Count, ref none, ref none);
                    try
                    {
                        for (int i = 0; i < srcDgv.Columns.Count; i++)//设置标题
                        {
                            table.Cell(0, i + 1).Range.Text = srcDgv.Columns[i].HeaderText;
                        }
                        for (int i = 1; i < srcDgv.Rows.Count; i++)//填充数据
                        {
                            for (int j = 0; j < srcDgv.Columns.Count; j++)
                            {
                                table.Cell(i + 1, j + 1).Range.Text = srcDgv[j, i - 1].Value.ToString();
                            }
                            progreesBar.Value += 100 / srcDgv.RowCount;
                        }
                        document.SaveAs(ref path, ref none, ref none, ref none, ref none, ref none, ref none, ref none, ref none, ref none, ref none, ref none, ref none, ref none, ref none, ref none);
                        document.Close(ref none, ref none, ref none);
                        progreesBar.Value = 100;
                        MessageBox.Show("数据已经成功导出到：" + sfile.FileName.ToString(), "导出完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        progreesBar.Value = 0;
                        progreesBar.Visible = false;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message, "友情提示", MessageBoxButtons.OK);
                    }
                    finally
                    {

                        wordApp.Quit(ref none, ref none, ref none);
                    }
                }
            }

        }
        #endregion
        public static void ExportDataGridViewToExcel(DataGridView dgv, ProgressBar progreesBar, SaveFileDialog sfile)
        {
            if (dgv.Rows.Count == 0)
            {
                MessageBox.Show("没有数据可供导出！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            sfile.AddExtension = true;
            sfile.DefaultExt = ".xlsx";
            sfile.Filter = "(*.xlsx)|*.xlsx";
            if (sfile.ShowDialog() == DialogResult.OK)
            {
                progreesBar.Visible = true;
                object path = sfile.FileName;
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel._Workbook wk = excel.Application.Workbooks.Add(true);

                try
                {
                    //生成字段名称   
                    for (int i = 0; i < dgv.ColumnCount; i++)
                    {
                        excel.Cells[1, i + 1] = dgv.Columns[i].HeaderText;
                    }
                    //填充数据   
                    for (int i = 0; i < dgv.RowCount; i++)
                    {
                        for (int j = 0; j < dgv.ColumnCount; j++)
                        {
                            if (dgv[j, i].ValueType == typeof(string))
                            {
                                excel.Cells[i + 2, j + 1] = "'" + dgv[j, i].Value.ToString();
                            }
                            else
                            {
                                excel.Cells[i + 2, j + 1] = dgv[j, i].Value.ToString();
                            }
                        }
                        progreesBar.Value += 100 / dgv.RowCount;

                    }
                    excel.Visible = false;
                    excel.DisplayAlerts = false;
                    excel.AlertBeforeOverwriting = false;
                    wk.Save();
                    wk.SaveAs(path);
                    excel.Quit();
                    excel = null;
                    GC.Collect();
                    progreesBar.Value = 100;
                    MessageBox.Show("数据已经成功导出到：" + sfile.FileName.ToString(), "导出完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    progreesBar.Value = 0;
                    progreesBar.Visible = false;

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "友情提示", MessageBoxButtons.OK);
                }
                finally
                {

                    GC.Collect();
                }

            }
        }
        public static bool FieldExists(DataTable dt, string sField)
        {
            bool blnok = false;
            if (dt == null) { return blnok; }
            if (dt.Columns.Contains(sField))
            {
                blnok = true;
            }
            return blnok;
        }
        public static DataTy FieldType(DataTable dt, string strField)
        {
            DataTy Dataty = DataTy.Character;
            switch (dt.Columns[strField].DataType.Name)
            {

                case "String":
                    {
                        Dataty = DataTy.Character;
                        break;
                    }
                case "DateTime":
                    {
                        Dataty = DataTy.Date;
                        break;
                    }
                case "Int16":
                case "Int32":
                case "Int64":
                case "UInt16":
                case "UInt32":
                case "UInt64":
                case "Decimal":
                case "Double":
                    {
                        Dataty = DataTy.Digit;
                        break;
                    }
                case "Boolean":
                    {
                        Dataty = DataTy.Boolean;
                        break;
                    }

            }
            return Dataty;
        }
        public static void GetControlFindSQL(Control.ControlCollection ctls, string strView)
        {
            string sDataField = "";
            string strUpdate = "";
            DataTable dt = GetDataTable("select  * from " + strView + " where 1<>1");
            foreach (Control con in ctls)
            {
                if (con.Controls.Count > 0 && IfContainer(con) == true)
                {
                    GetControlFindSQL(con.Controls, strView);
                }
                else
                {
                    if (con.Tag != null && con.Tag.ToString() != "")
                    {
                        sDataField = con.Tag.ToString();
                        if (FieldExists(dt, sDataField))
                        {
                            string strv = "";
                            DataTy dty = FieldType(dt, sDataField);
                            switch (con.GetType().Name)
                            {
                                case "ComboBox":
                                    {
                                        string str;
                                        ComboBox objcon = (ComboBox)con;
                                        if (objcon.Enabled == true)
                                        {
                                            if (objcon.SelectedValue == null)
                                            {
                                                if (objcon.Text != "")
                                                { str = objcon.Text; }
                                                else
                                                { str = ""; }
                                            }
                                            else
                                            {
                                                str = objcon.SelectedValue.ToString();
                                            }
                                            if (str != "")
                                            {
                                                if (strUpdate != "")
                                                { strUpdate = strUpdate + " and "; }
                                                strUpdate = strUpdate + sDataField + "=";
                                                strUpdate = strUpdate + "'" + str + "'";
                                            }
                                        }
                                        break;
                                    }
                                case "TextBox":
                                case "RichTextBox":
                                case "MaskedTextBox":
                                case "DateTimePicker":
                                    {
                                        strv = con.Text.Trim().Replace("'", "''");
                                        if (dty == DataTy.Digit || dty == DataTy.Date)
                                        {
                                            if (strv == "")
                                            {
                                                { strUpdate = strUpdate + " and "; }
                                                strUpdate = strUpdate + sDataField + "=";
                                                strUpdate = strUpdate + "null";

                                            }
                                            else
                                            {
                                                if (dty == DataTy.Date)
                                                {
                                                    if (strUpdate != "")
                                                    { strUpdate = strUpdate + " and "; }
                                                    string strcon = con.Name.ToUpper();
                                                   
                                                    if (strcon.IndexOf("FROMDATE")>=0)
                                                    {
                                                        strUpdate = strUpdate + sDataField + ">=";
                                                    }
                                                    if (strcon.IndexOf("TODATE")>=0 )
                                                    {
                                                        strUpdate = strUpdate + sDataField + "<=";
                                                    }
                                                    strUpdate = strUpdate + " to_date('" + strv + "','yyyy-mm-dd') ";
                                                }
                                                else
                                                {
                                                    if (strUpdate != "")
                                                    { strUpdate = strUpdate + " and "; }
                                                    strUpdate = strUpdate + sDataField + "=";
                                                    strUpdate = strUpdate + "'" + strv + "'";
                                                }
                                            }
                                        }
                                        else if (dty == DataTy.Character)
                                        {
                                            if (strv != "")
                                            {
                                                if (strUpdate != "")
                                                { strUpdate = strUpdate + " and "; }
                                                strUpdate = strUpdate + sDataField + "=";
                                                strUpdate = strUpdate + "'" + strv + "'";
                                            }
                                        }
                                        break;
                                    }
                                case "NumericUpDown":
                                    {
                                        NumericUpDown objcon = (NumericUpDown)con;
                                        if (strUpdate != "")
                                        { strUpdate = strUpdate + " and "; }
                                        strUpdate = strUpdate + sDataField + "=";
                                        strUpdate = strUpdate + objcon.Value;
                                        break;
                                    }
                                case "CheckBox":
                                    {
                                        CheckBox objcon = (CheckBox)con;
                                        if (strUpdate != "")
                                        { strUpdate = strUpdate + " and "; }
                                        strUpdate = strUpdate + sDataField + "=";
                                        strUpdate = strUpdate + Convert.ToInt32(objcon.Checked);
                                        break;
                                    }
                                case "RadioButton":
                                    {
                                        RadioButton objcon = (RadioButton)con;
                                        if (strUpdate != "")
                                        { strUpdate = strUpdate + " and "; }
                                        strUpdate = strUpdate + sDataField + "=";
                                        strUpdate = strUpdate + Convert.ToInt32(objcon.Checked);
                                        break;

                                    }
                                case "ListBox":
                                    {
                                        ListBox objcon = (ListBox)con;
                                        string svalue = "";
                                        for (int intn = 0; intn < objcon.Items.Count; intn++)
                                        {
                                            if (svalue != "")
                                            {
                                                svalue = svalue + ",";
                                            }
                                            svalue = svalue + "'" + objcon.Items[intn].ToString() + "'";
                                        }
                                        if (svalue != "")
                                        {
                                            if (strUpdate != "")
                                            { strUpdate = strUpdate + " and "; }
                                            strUpdate = strUpdate + sDataField + " in ";
                                            strUpdate = strUpdate + "(" + svalue + ")";
                                        }
                                        break;
                                    }
                                case "CheckedListBox":
                                    {
                                        CheckedListBox objcon = (CheckedListBox)con;
                                        string svalue = "";
                                        for (int intn = 0; intn < objcon.Items.Count; intn++)
                                        {
                                            if (objcon.GetItemChecked(intn))
                                            {
                                                if (svalue != "")
                                                {
                                                    svalue = svalue + ",";

                                                }
                                                svalue = svalue + "'" + objcon.GetItemText(objcon.Items[intn]) + "'";
                                            }
                                        }
                                        if (svalue != "")
                                        {
                                            if (strUpdate != "")
                                            { strUpdate = strUpdate + " and "; }
                                            strUpdate = strUpdate + sDataField + " in ";
                                            strUpdate = strUpdate + "(" + svalue + ")";
                                        }
                                        break;
                                    }
                            }
                        }
                    }
                }
            }
            if (gWhere != "" && strUpdate != "")
            {
                gWhere = gWhere + " and " + strUpdate;
            }
            else if (strUpdate != "")
            {
                gWhere = strUpdate;
            }
        }
        public static void LoadDefControlValue(Control.ControlCollection ctls, DataTable dt,int index)
        {
            if (ChkDataTable(dt)==false)
            {
                return;
            }
            foreach (Control con in ctls)
            {
                    if (con.Controls.Count > 0 && IfContainer(con) == true)
                    {
                        LoadDefControlValue(con.Controls, dt, index);
                    }
                    else
                    {
                        if (con.Tag != null)
                        {
                            string strField;
                            strField = con.Tag.ToString();
                            if (strField != "")
                            {
                                switch (con.GetType().Name)
                                {
                                    case "ComboBox":
                                        {
                                            if (strField != "" && DataTableExistsField(dt, strField))
                                            {
                                                System.Windows.Forms.ComboBox objcon = (System.Windows.Forms.ComboBox)con;

                                                objcon.Text = dt.Rows[index][strField].ToString().Trim();
                                                if (objcon.DataSource == null)
                                                {
                                                    if (objcon.Text == "")
                                                    {
                                                        objcon.Items.Add(dt.Rows[index][strField].ToString().Trim());
                                                        objcon.SelectedIndex = 0;
                                                        objcon.Text = dt.Rows[index][strField].ToString().Trim();
                                                    }
                                                }
                                            }
                                            break;
                                        }
                                    case "TextBox":
                                    case "RichTextBox":
                                    case "MaskedTextBox":
                                        {
                                            if (strField != "" && DataTableExistsField(dt, strField))
                                            {
                                                if (strField.ToLower().IndexOf("password") > 0)
                                                {
                                                    //con.Text =Dal.Decrypt(dt.Rows[index][strField].ToString().Trim());
                                                    con.Text = dt.Rows[index][strField].ToString().Trim();
                                                }
                                                else
                                                {
                                                    con.Text = dt.Rows[index][strField].ToString().Trim();
                                                }
                                            }
                                            break;
                                        }
                                    case "DateTimePicker":
                                        {
                                            DateTimePicker objn = con as DateTimePicker;
                                            if (strField != "" && DataTableExistsField(dt, strField))
                                            {
                                                objn.Value =Convert.ToDateTime( dt.Rows[index][strField]);
                                            }
                                            break;
                                        }
                                    case "NumericUpDown":
                                        {
                                            NumericUpDown objn = con as NumericUpDown;
                                            if (objn.Name == "numfMaxNum")
                                            {
                                                objn.Maximum = 3267890;
                                            }
                                            if (strField != "" && DataTableExistsField(dt, strField))
                                            {
                                                if (dt.Rows[index][strField].ToString() != "")
                                                {
                                                    objn.Value = Convert.ToDecimal(dt.Rows[index][strField]);
                                                }
                                                else
                                                {
                                                    objn.Value = 0;
                                                }
                                            }
                                            break;
                                        }
                                    case "CheckBox":
                                        {
                                            CheckBox chk = con as CheckBox;
                                            if (strField != "" && DataTableExistsField(dt, strField))
                                            {
                                                if (dt.Rows[index][strField].ToString() == "")
                                                { chk.Checked = false; }
                                                else
                                                {
                                                    chk.Checked = Convert.ToBoolean(dt.Rows[index][strField]);
                                                }
                                            }
                                            break;
                                        }
                                    case "RadioButton":
                                        {
                                            RadioButton chk = con as RadioButton;
                                            if (strField != "" && DataTableExistsField(dt, strField))
                                            {
                                                if (dt.Rows[index][strField].ToString() == "")
                                                { chk.Checked = false; }
                                                else
                                                {
                                                    chk.Checked = Convert.ToBoolean(dt.Rows[index][strField]);
                                                }
                                            }
                                            break;
                                        }
                                }
                            }


                        }
                        }
                        }
                        }
        public static void LoadControlValue(Control.ControlCollection ctls, DataRow dr)
        {
 
            foreach (Control con in ctls)
            {
                if (con.Controls.Count > 0 && IfContainer(con) == true)
                {
                    LoadControlValue(con.Controls, dr);
                }
                else
                {
                    if (con.Tag != null)
                    {
                        string strField;
                        strField = con.Tag.ToString();
                        if (strField != "")
                        {
                            switch (con.GetType().Name)
                            {
                                case "ListBox":
                                    {
                                        ListBox objcon = (ListBox)con;
                                        string svalue = "";
                                        svalue = dr[strField].ToString();
                                        objcon.Items.Clear();
                                        if (svalue != "")
                                        {
                                            string[] s;
                                            s = svalue.Split(',');
                                            for (int intn = 0; intn < s.Length; intn++)
                                            {
                                                objcon.Items.Add(s[intn]);
                                            }
                                        }
                                        else
                                        {
                                            objcon.Items.Clear();
                                        }
                                        break;
                                    }
                                case "CheckedListBox":
                                    {
                                        CheckedListBox objcon = (CheckedListBox)con;
                                        string svalue = "";
                                        svalue = dr[strField].ToString();
                                        for (int intn1 = 0; intn1 < objcon.Items.Count; intn1++)
                                        {
                                            objcon.SetItemChecked(intn1, false);
                                        }
                                        if (svalue != "")
                                        {
                                            string[] s;
                                            s = svalue.Split(',');
                                            for (int intn = 0; intn < s.Length; intn++)
                                            {
                                                for (int intn1 = 0; intn1 < objcon.Items.Count; intn1++)
                                                {
                                                    
                                                    if (objcon.GetItemText(objcon.Items[intn1]) == s[intn])
                                                    {
                                                        objcon.SetItemChecked(intn1, true);
                                                        break;
                                                    }
                                                }
                                            }
                                        }
 
                                        break;
                                    }                               
                            }
                        }


                    }
                }
            }
        }
        public static int GetCurrentRowIndex(DataGridView grid,DataTable dt)
        {
            int index=-1;
            if (grid.SelectedRows.Count==0)
            { return -1; }
            DataRowView drv = (DataRowView)grid.SelectedRows[0].DataBoundItem;
            index = dt.Rows.IndexOf(drv.Row);            
            return index;
        }
        public static string GetFldString(DataTable dtable, string strField, int row)
        {
            string t = null;
            switch (dtable.Columns[strField].DataType.Name)
            {
                case "String":
                    {

                        //if (strField.ToLower().IndexOf("password")>0)
                        //{
                            if (dtable.Rows[row][strField].ToString() != "")
                            { t = "'" + dtable.Rows[row][strField].ToString() + "'"; }
                            else
                            { t = "''"; }
                        //}
                        //else
                        //{
                        //        t = "'" + dtable.Rows[row][strField].ToString() + "'";

                        //}
                        break;
                    }
                case "DateTime":
                    {
                        if (strDataBaseTy=="MYSQL")
                        {
                            t = " '" + Convert.ToDateTime(dtable.Rows[row][strField]).ToString("yyyy-MM-dd hh:MM:ss") + "'";
                        }
                        else
                        {
                            t = " to_date('" + dtable.Rows[row][strField] + "','yyyy-mm-dd HH24:mi:ss')";
                        }
                        break;
                    }
                case "Int16":
                case "Int32":
                case "Int64":
                case "UInt16":
                case "UInt32":
                case "UInt64":
                case "Decimal":
                case "Double":
                    {
                        t = dtable.Rows[row][strField].ToString();
                        if (t == "")
                        { t = "null"; }
                        break;
                    }
                case "Boolean":
                    {
                        t = Convert.ToInt32(dtable.Rows[row][strField]).ToString();
                        break;
                    }

            }
            return t;
        }
        public static string GetDelFldString(DataTable dtable, string strField, int row)
        {
            string t = null;
            switch (dtable.Columns[strField].DataType.Name)
            {
                case "String":
                case "DateTime":
                    {
                        t = "'" + dtable.Rows[row][strField, DataRowVersion.Original] + "'";
                        break;
                    }
                case "Int16":
                case "Int32":
                case "Int64":
                case "UInt16":
                case "UInt32":
                case "UInt64":
                case "Decimal":
                case "Double":
                    {
                        t = dtable.Rows[row][strField, DataRowVersion.Original].ToString();
                        if (t == "")
                        { t = "0"; }
                        break;
                    }
                case "Boolean":
                    {
                        t = Convert.ToInt32(dtable.Rows[row][strField, DataRowVersion.Original]).ToString();
                        break;
                    }

            }
            return t;
        }
        public static string SaveSQL(string strWrite,string sTable, DataTable dt, string mainKey)
        {
            try
            {
                if (dt == null) { MessageBox.Show("无更新资料"); return ""; }
                if (dt.Rows.Count == 0) { MessageBox.Show("无更新资料"); return ""; }

                //string strKey = Dal.GetstrKey(mainKey, sTable, out scap);
                DataTable dt1 = GetDataTable("select " + strWrite + " from " + sTable + " where 1<>1");
                string[] strKey = new string[] { };
                strKey = mainKey.Split(',');
                StringBuilder str = new StringBuilder();
                string strField = "";
                string strUpdate = "";
                string strFilter = "";
                dt.TableName = sTable;
                //修改
                DataTable dtupdate = dt.GetChanges(DataRowState.Modified);
                if (dtupdate != null)
                {
                    for (int row = 0; row < dtupdate.Rows.Count; row++)
                    {
                        if (dtupdate.Rows[row][0].ToString() != "合计")
                        {
                            if (strUpdate != "")
                            { strUpdate = strUpdate + "\r\n"; }
                            strUpdate = strUpdate + " update " + sTable + " set ";
                            str.Clear();
                            for (int col = 0; col < dtupdate.Columns.Count; col++)
                            {
                                strField = dtupdate.Columns[col].ColumnName.Trim();
                                if (DataTableExistsField(dt1, strField))
                                {
                                    if (str.ToString() != "")
                                    { str.AppendLine(","); }
                                    str.AppendLine(strField + "=" + GetFldString(dtupdate, strField, row));
                                }
                            }
                            strFilter = "";
                            for (int intN = 0; intN < strKey.Length; intN++)
                            {
                                if (strFilter != "")
                                { strFilter = strFilter + " and "; }
                                strFilter = strFilter + strKey[intN] + "=" + GetFldString(dtupdate, strKey[intN].ToString(), row);
                            }
                            strUpdate = strUpdate + str.ToString() + " where " + strFilter;
                        }
                    }
                    dtupdate.Dispose();
                }
                //删除
                string strDel = "";
                DataTable dtDel = dt.GetChanges(DataRowState.Deleted);
                if (dtDel != null)
                {
                    for (int row = 0; row < dtDel.Rows.Count; row++)
                    {
                        if (strDel != "")
                        {
                            strDel = strDel + "\r\n";
                        }
                        strDel = strDel + " delete from " + sTable;
                        strFilter = "";
                        for (int intN = 0; intN < strKey.Length; intN++)
                        {
                            if (strFilter != "")
                            { strFilter = strFilter + " and "; }
                            strFilter = strFilter + strKey[intN] + "=" + GetDelFldString(dtDel, strKey[intN].ToString(), row);
                        }
                        strDel = strDel + " where " + strFilter;
                    }
                    dtDel.Dispose();
                }
                //新增

                str.Clear();
                string strinsertFlds = "";
                string strinsert = "";
                DataTable dtAdd = dt.GetChanges(DataRowState.Added);
                if (dtAdd != null)
                {

                    string strNew = "insert into " + sTable + "(";
                    for (int col = 0; col < dtAdd.Columns.Count; col++)
                    {
                        strField = dtAdd.Columns[col].ColumnName.Trim();
                        if (DataTableExistsField(dt1, strField))
                        {
                            if (strinsertFlds != "")
                            { strinsertFlds = strinsertFlds + ","; }
                            strinsertFlds = strinsertFlds + strField;
                        }
                    }
                    strNew = strNew + strinsertFlds + ") values(";
                    for (int row = 0; row < dtAdd.Rows.Count; row++)
                    {
                        if (dtAdd.Rows[row][0].ToString() != "合计")
                        {
                            if (strinsert != "")
                            {
                                { strinsert = strinsert + " union all "; }
                            }
                            strinsert = strinsert + "  ";
                            str.Clear();
                            for (int col = 0; col < dtAdd.Columns.Count; col++)
                            {
                                strField = dtAdd.Columns[col].ColumnName.Trim();
                                if (DataTableExistsField(dt1, strField))
                                {
                                    if (str.ToString() != "")
                                    { str.AppendLine(","); }
                                    str.AppendLine(GetFldString(dtAdd, strField, row));
                                }
                            }
                            strinsert = strinsert + str.ToString();
                        }
                    }
                    dtAdd.Dispose();
                    if (strinsert != "")
                    {
                        strinsert = strNew + strinsert+")";
                    }
                }
                strinsert = strDel + " " + strinsert + " " + strUpdate;
                return strinsert.Trim();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); return ""; }
        }
        public static string CheckDataRow(DataRow dr, string sMainkey = "", string sMainkeydesc = "", string strNotNull = "", string strNotNullDesc = "", string strBigZero = "", string strBigZeroDesc = "")
        {
            string blnok= "";
            string[] sBigZero;
            string[] key;
            string[] sNotNull;
            key = sMainkey.Split(',');
            sBigZero = strBigZero.Split(',');
            sNotNull = strNotNull.Split(',');
            ArrayList list1 = new ArrayList();
            for (int intn = 0; intn < key.Length; intn++)
            {
                list1.Add(key[intn]);
                
                
            }
            ArrayList list2 = new ArrayList();
            for (int intn = 0; intn < key.Length; intn++)
            {
                list1.Add(dr[key[intn]]);                
            }
            if (strNotNull!= "")
            {
                for (int i = 0; i < sNotNull.Length; i++)
                {
                    if (dr[sNotNull[i]].ToString() == "")
                    {
                        blnok = blnok + strNotNullDesc[i] + "is Empty \r\n";
                    }
                }
            }
            if (strBigZero != "")
            {
                for (int i = 0; i < sBigZero.Length; i++)
                {
                    if (dr[sBigZero[i]].ToString() == "")
                    {
                        blnok = blnok + strNotNullDesc[i] + "is Zero \r\n";
                    }
                    else if (dr[sBigZero[i]].ToString() == "0")
                    {
                        blnok = blnok + strNotNullDesc[i] + "is Zero \r\n";
                    }
                }
            }
           return blnok;
        }
        public static string ExeSQLWithPara(string strSQL, ArrayList ParaNames, ArrayList ParaValues, string strOutName = "@strResult")
        {
            object strResult = "";
            Connect();

            OracleCommand sqlc = new OracleCommand();
            try
            {

                OracleParameter p = new OracleParameter();
                sqlc.CommandText = strSQL;
                sqlc.CommandType = CommandType.StoredProcedure;
                sqlc.Connection = conn;
                if (ParaNames.Count > 0)
                {
                    for (int intm = 0; intm < ParaNames.Count; intm++)
                    {
                      
                        sqlc.Parameters.Add("@" + ParaNames[intm], ParaValues[intm]);
                        
                    }
                }
                sqlc.Parameters.Add(strOutName, OracleDbType.Varchar2, 300).Direction = ParameterDirection.Output;
                sqlc.ExecuteNonQuery();
                strResult = sqlc.Parameters[strOutName].Value;
                sqlc.Dispose();
            }
            catch 
            {
                sqlc.Dispose();
                
            }
            return strResult.ToString();
        }
        public static void FormatGrid(DataGridView Grid, DataTable dt, string sColumnIDs, string sColumnCaptions = "", string sColumnWidths = "", string CheckedColumns = "", string HideColumns="")
        {
            try
            {
                if (sColumnIDs == "" || sColumnCaptions == "") { return; }
                string[] Cap;
                string[] col;
                string[] witdh;
                col = sColumnIDs.Split(',');
                Cap = sColumnCaptions.Split(',');
                witdh = sColumnWidths.Split(',');
                Grid.Columns.Clear();
                for (int i = 0; i < col.Length; i++)
                {
                    if (CheckedColumns.IndexOf(dt.Columns[i].ColumnName) >= 0)
                    {
                        DataGridViewCheckBoxColumn colid = new DataGridViewCheckBoxColumn();
                        colid.Name = "col" + dt.Columns[i].ColumnName;
                        colid.HeaderText = Cap[i];
                        colid.DataPropertyName = dt.Columns[i].ColumnName;
                        Grid.Columns.Add(colid);
                    }
                    else
                    {
                        DataGridViewTextBoxColumn colid = new DataGridViewTextBoxColumn();
                        colid.Name = "col" + dt.Columns[i].ColumnName;
                        colid.HeaderText = Cap[i];
                        colid.DataPropertyName = dt.Columns[i].ColumnName;
                        Grid.Columns.Add(colid);
                    }
                }
                if (sColumnWidths != "")
                {
                    for (int j = 0; j < witdh.Length; j++)
                    {
                        Grid.Columns[j].Width = Int32.Parse(witdh[j]);
                    }
                }
                if (HideColumns != "")
                {
                    string[] Hide;
                    Hide = HideColumns.Split(',');
                    for (int j = 0; j < Hide.Length; j++)
                    {
                        Grid.Columns["col"+Hide[j]].Visible = false;
                        
                    }
                }
                dt.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static string Encrypt(string userID, string password)
        {
            int iRow = 0, iCol = 0, iCount = 0, iCountPwd = 0;
            int iResult = 0;
            char chChar;
            string strAsc;
            char[] chAUser;
            char[] chAPwd;
            string[] strMix;
            int[] intArr;
            string strResult;

            userID.ToUpper().Trim();
            password.Trim().ToUpper();
            iCount = userID.Length;
            if (password == "") { return ""; }

            chAUser = new char[iCount];
            for (iRow = 0; iRow < iCount; iRow++)
            {
                chChar = Convert.ToChar(userID.Substring(iRow, 1));
                chAUser[iRow] = chChar;
            }

            iCountPwd = password.Length;
            chAPwd = new char[iCountPwd];
            for (iRow = 0; iRow < iCountPwd; iRow++)
            {
                chChar = Convert.ToChar(password.Substring(iRow, 1));
                iCol = iCountPwd - iRow - 1;
                chAPwd[iCol] = chChar;
            }

            iCol = (iCount + iCountPwd) % 2;
            if (iCol == 0)
            {
                iResult = iCount + iCountPwd;
                strMix = new string[iResult];
            }
            else
            {
                iResult = iCount + iCountPwd + 1;
                strMix = new string[iResult];
                strMix[iResult - 1] = "Z";
            }

            for (iRow = 0; iRow < (iCount + iCountPwd); iRow++)
            {
                strMix[iRow] = null;
            }

            iCol = 0;
            if (iCount <= iCountPwd)
            {
                for (iRow = 0; iRow < 2 * iCount; iRow += 2)
                {
                    strMix[iRow] = Convert.ToString(chAUser[iCol]);
                    iCol += 1;
                }
                iCol = 0;
                for (iRow = 1; iRow < iCount + iCountPwd; iRow++)
                {
                    if (strMix[iRow] == null)
                    {
                        strMix[iRow] = Convert.ToString(chAPwd[iCol]);
                        iCol += 1;
                    }
                }
            }
            else
            {
                for (iRow = 0; iRow < 2 * iCountPwd; iRow += 2)
                {
                    strMix[iRow] = Convert.ToString(chAPwd[iCol]);
                    iCol += 1;
                }
                iCol = 0;
                for (iRow = 1; iRow < (iCount + iCountPwd); iRow++)
                {
                    if (strMix[iRow] == null)
                    {
                        strMix[iRow] = Convert.ToString(chAUser[iCol]);
                        iCol += 1;
                    }
                }
            }


            iRow = strMix.GetUpperBound(0);
            strMix[iRow] = "Z";

            for (iRow = 0; iRow < iResult; iRow++)
            {
                chChar = Convert.ToChar(strMix[iRow]);
                strAsc = Convert.ToByte(chChar).ToString();
                if (strAsc.Length == 1)
                {
                    strMix[iRow] = strAsc.Trim() + "0";
                }

                if (strAsc.Length == 2)
                {
                    strMix[iRow] = strAsc.Trim();
                }

                if (strAsc.Length == 3)
                {
                    strMix[iRow] = strAsc.Substring(0, 2);
                }
            }

            strResult = "";
            for (iRow = 0; iRow < iResult; iRow++)
            {
                strResult = strResult.Trim() + strMix[iRow].Trim();
            }

            iCount = iResult / 2;
            iCol = iCount;

            if ((iCount % 2) == 0)
            {
                intArr = new int[iCount];
            }
            else
            {
                iCount = iCount + 1;
                intArr = new int[iCount];
                intArr[iCount - 1] = 7546;
            }

            for (iRow = 0; iRow < iCol; iRow++)
            {
                intArr[iRow] = Convert.ToInt32(strResult.Substring(iRow * 4, 4));
            }

            iResult = 0;
            for (iRow = 0; iRow < iCount / 2; iRow++)
            {
                iResult = iResult + intArr[iRow] * intArr[iCount - iRow - 1];
            }

            if (iResult.ToString().Trim().Length < 8)
            {
                while (true)
                {
                    iResult = iResult * 2;
                    if (iResult.ToString().Trim().Length >= 8)
                    {
                        break;
                    }
                }
            }

            if (iResult.ToString().Trim().Length > 8)
            {
                strResult = iResult.ToString().Trim().Substring(1, 8);
            }

            if (iResult.ToString().Trim().Length == 8)
            {
                strResult = iResult.ToString().Trim();
            }

            return strResult;
        }
    }
}
