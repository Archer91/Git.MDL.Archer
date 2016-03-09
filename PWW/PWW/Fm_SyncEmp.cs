using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PWW
{
    public partial class Fm_SyncEmp : Form
    {
        public Fm_SyncEmp()
        {
            InitializeComponent();
        }
        private ArrayList arrSql = new ArrayList();
        private void button1_Click(object sender, EventArgs e)
        {
            /*
            视图：modern_hr_temp
            字段：
            cPsn_Num：人员编码
            cPsn_Name：人员姓名
            cDepName：部门
            gonghao：工号
            rSex：性别： 1：男 2：女
            vCardNo：卡号
            rPersonType：人员状态：10在职、30离职
            cDeptCode 部门代码 
             */
            DataSet dsemp = getEmpSQLDataset("select * from modern_hr_temp",CommandType.Text,null);
            string strSql="";
            if (dsemp != null)
            {
                for (int i = 0; i < dsemp.Tables[0].Rows.Count; i++)
                {
                    //insert or update table ztpw_emp_employee
                    /*
                    EMP_CODE
                    EMP_CARD_NUM
                    EMP_NAME
                    EMP_DEPT_CODE // can not update no code data
                    EMP_CRAFT     // can not update no code data
                    EMP_GRADE_CODE // can not update no code data
                    EMP_STATUS
                    EMP_JOIN_ON //
                    EMP_OUT_ON  //
                    EMP_OUT_REASON //
                    EMP_REMARK 
                    EMP_BIRTHDAY //
                    EMP_BMGH  //
                    EMP_SEX  //
                    EMP_U_DEPT  //
                    EMP_U_GONGHAO  //
                   */
                    DataSet dsPWemp = DB.GetDSFromSql("select * from ZTPW_EMP_EMPLOYEE where emp_code='" + dsemp.Tables[0].Rows[i]["CPSN_NUM"]+ "'");
                    if (dsPWemp != null && dsPWemp.Tables[0].Rows.Count > 0) //update 
                    {

                        strSql="update ZTPW_EMP_EMPLOYEE set "
                            +" EMP_CARD_NUM =" +DB.sp(dsemp.Tables[0].Rows[i]["VCARDNO"].ToString())+","
                            +" EMP_NAME =" +DB.sp(dsemp.Tables[0].Rows[i]["CPSN_NAME"].ToString())+","
                            +" EMP_U_DEPT =" +DB.sp(dsemp.Tables[0].Rows[i]["CDEPNAME"].ToString())+","
                            +" EMP_U_DEPT_CODE =" + DB.sp(dsemp.Tables[0].Rows[i]["CDEPCODE"].ToString()) + ","
                            + " EMP_U_GONGHAO =" + DB.sp(dsemp.Tables[0].Rows[i]["GONGHAO"].ToString()) + ","
                            +" EMP_SEX =" +DB.sp(dsemp.Tables[0].Rows[i]["RSEX"].ToString())+","
                            +" EMP_STATUS =" +DB.sp(dsemp.Tables[0].Rows[i]["RPERSONTYPE"].ToString())
                            +" where emp_code='" + dsemp.Tables[0].Rows[i]["CPSN_NUM"]+ "'";
                        //DB.ExecuteFromSql(strSql);
                        arrSql.Add(strSql);

                    }
                    else if (dsPWemp != null) //insert 
                    {
                        strSql = "insert into ZTPW_EMP_EMPLOYEE (emp_code,emp_card_num,emp_name,emp_u_dept,emp_u_dept_code,emp_u_gonghao,emp_sex,emp_status) values("
                            + DB.sp(dsemp.Tables[0].Rows[i]["CPSN_NUM"].ToString())+","
                            + DB.sp(dsemp.Tables[0].Rows[i]["VCARDNO"].ToString())+","
                            + DB.sp(dsemp.Tables[0].Rows[i]["CPSN_NAME"].ToString())+","
                            + DB.sp(dsemp.Tables[0].Rows[i]["CDEPNAME"].ToString())+","
                            + DB.sp(dsemp.Tables[0].Rows[i]["CDEPCODE"].ToString()) + ","
                            + DB.sp(dsemp.Tables[0].Rows[i]["GONGHAO"].ToString()) + ","
                            + DB.sp(dsemp.Tables[0].Rows[i]["RSEX"].ToString())+","
                            + DB.sp(dsemp.Tables[0].Rows[i]["RPERSONTYPE"].ToString())
                            +")";
                        //DB.ExecuteFromSql(strSql);
                        arrSql.Add(strSql);
                    }
                    label1.Text = "Doing " + dsemp.Tables[0].Rows.Count.ToString()+ " of " + (i+1).ToString() + " -- " + dsemp.Tables[0].Rows[i]["CPSN_NUM"].ToString();
                    this.Text = "Fm_SyncEmp " + "Doing " + dsemp.Tables[0].Rows.Count.ToString() + " of " + (i + 1).ToString() + " -- " + dsemp.Tables[0].Rows[i]["CPSN_NUM"].ToString();
                    //batch do it
                    if (arrSql.Count == 100)
                    {
                        DB.ExeTrans(arrSql);
                        arrSql.Clear();
                    }
                }
                if ((arrSql.Count > 0)) DB.ExeTrans(arrSql);
                label1.Text = "Done OK .";
            }

        }
        private DataSet getEmpSQLDataset(string sql,CommandType cmdType,SqlParameter[] paras)  
        {   
            //string strCon ="server=192.168.1.42;uid=hruser1;pwd=Hr_user;database=UFDATA_002_2012";//20150619 modify
            string strCon = "server=192.168.1.42;uid=hruser1;pwd=Hr_user;database=UFDATA_002_2014";
            DataSet ds = new DataSet();  
            SqlConnection con = new SqlConnection(strCon); 
            SqlCommand cmd = con.CreateCommand(); 
            cmd.CommandType = cmdType; 
            cmd.CommandText = sql;  
            SqlDataAdapter adp = new SqlDataAdapter(cmd); 
            adp.SelectCommand = cmd; 
            if (paras != null) 
            {  
                foreach (SqlParameter p in paras) 
                {  
                    cmd.Parameters.Add(p); 
                } 
            } 
            try 
            {  
                con.Open(); 
                adp.Fill(ds);  
            }  
            catch (System.Exception e) 
            {  
                throw e; 
            }  
            finally 
            {  
                con.Close(); 
                con.Dispose(); 
                cmd.Dispose(); 
                adp.Dispose(); 
            }  
            return ds; 
        }

        private void Fm_SyncEmp_Load(object sender, EventArgs e)
        {

        } 
    }
}
