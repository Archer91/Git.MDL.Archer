using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PWW
{
	public partial class Fm_Special_Case_History : Form
	{
		Dictionary<string, string> colName = new Dictionary<string, string>();
		private string Keyvalue;
		public Fm_Special_Case_History(string keyValue)
		{
			InitializeComponent();

			//dgv.AutoGenerateColumns = false;
			dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

			colName.Add("SPCC_DATE", "安排日期");
			colName.Add("SPCC_JOB_NO", "工作单编号");
			colName.Add("SPCC_OWNER", "跟进人");
			colName.Add("SPCC_REASON", "原因");
			colName.Add("SPCC_REASON_CAT1", "分类");
			colName.Add("SPCC_STATUS", "状态");
			colName.Add("SPCC_REMARK", "备注");
			Keyvalue = keyValue;
		}

		private void Fm_Special_Case_History_Load(object sender, EventArgs e)
		{
			DataSet ds = DB.GetDSFromSql(@"select JMLG_CHG_FIELD,
JMLG_FROM_VALUE,
JMLG_TO_VALUE,
T.JMLG_CRT_BY||'--'||UACC_NAME JMLG_CRT_BY , JMLG_CRT_ON 
from ZTPW_JMLG_JOMELOG t,
zt00_uacc_useraccount u 
 where  UACC_CODE=JMLG_CRT_BY and jmlg_table_name = 'ZTPW_SPECIAL_CASE'
and jmlg_key_value = '" + Keyvalue + @"' 
order by JMLG_CRT_ON desc ");

			for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
			{
				string s = ds.Tables[0].Rows[i]["JMLG_CHG_FIELD"].ToString();
				ds.Tables[0].Rows[i]["JMLG_CHG_FIELD"] = colName[s];
			}
			dgv.DataSource = ds.Tables[0];
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
