using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZComm1.UControl
{
	public partial class UWinLiveSearch : UserControl
	{
		public delegate DataSet DGetDSFromSql(string strSql);

		[Browsable(true)]
		[Category("自定义属性")]
		[Description("Parent control,such as Textbox")]
		public Control ToControl { get; set; }

		[Browsable(true)]
		[Category("自定义属性")]
		[Description("if not set,get recent, :DB.GetDSFromSql")]
		public DGetDSFromSql dGetDSFromSql { get; set; }

		private string sqlQuick;
		[Browsable(true)]
		[Category("自定义属性")]
		[Description("if not set,get recent, :select UACC_CODE V,UACC_CODE || '--' || UACC_NAME T from zt00_uacc_useraccount")]
		public string SqlQuick
		{
			get { return sqlQuick; }
			set { sqlQuick = value; }
		}

		private string sqlFull;
		[Browsable(true)]
		[Category("自定义属性")]
		[Description("if not set,get SqlQuick")]
		public string SqlFull
		{
			get
			{
				if (string.IsNullOrEmpty(sqlFull))
					return SqlQuick;
				return sqlFull;
			}
			set
			{
				sqlFull = value;
			}
		}

		private void SetFull()
		{
			if (dGetDSFromSql==null) return;
			DataTable dt = dGetDSFromSql(@"select * from (" + SqlFull + ")").Tables[0];
			foreach (DataRow item in dt.Rows)
			{
				dFull[item["V"].ToString()] = item["T"].ToString();
			}
		}

		public bool PutRecentTop { get; set; }


		private List<ValueText> lRecent = new List<ValueText>();
		Dictionary<String, string> dFull = new Dictionary<string, string>();

		public UWinLiveSearch()
		{
			InitializeComponent();
		}
		private void textBox2_KeyUp(object sender, KeyEventArgs e)
		{
			List<ValueText> lUser;
			if (e.KeyCode == Keys.Down)
			{
				ToControl.CausesValidation = false;
				if (string.IsNullOrEmpty(SqlQuick) || dGetDSFromSql == null)
				{
					lUser = lRecent;
				}
				else
				{
					if (e.Control)
					{
						lUser = GetList(SqlFull);
					}
					else
					{
						lUser = GetList(SqlQuick);
						if (lUser.Count == 0 && ToControl.Text.Trim() != "")
						{
							lUser = GetList(SqlFull);
						}
						if (PutRecentTop && ToControl.Text.Trim() == "")
						{
							foreach (ValueText valueText in lRecent)
							{
								lUser.RemoveAll(ll => ll.Value == valueText.Value);
							}
							lUser.InsertRange(0, lRecent);
						}
					}
				}
				listBox1.DataSource = null;
				listBox1.DataSource = lUser;

				this.Left = ToControl.Left;
				this.Top = ToControl.Top + ToControl.Height;
				this.Visible = true;
				//listBox1.Visible = true;
				this.BringToFront();
				//listBox1.BringToFront();
				this.Focus();
				//listBox1.Focus();
			}
			else if (e.KeyCode == Keys.Enter)
			{
				if (ToControl.Text != "")
				{
					ToControl.CausesValidation = true;
					lRecent.RemoveAll(ll => ll.Value == ToControl.Text);
					if (dFull.Count == 0)
						SetFull();
					if (dFull.ContainsKey(ToControl.Text))
						lRecent.Insert(0, new ValueText(dFull[ToControl.Text], ToControl.Text));
				}
			}
		}

		private List<ValueText> GetList(string sql)
		{
			DataTable dt;
			List<ValueText> lUser;
			dt = dGetDSFromSql(@"
select * from (" + sql + @") 
where lower(v) like '%" + ToControl.Text.ToLower() + @"%' or lower(t) like '%" + ToControl.Text.ToLower() + @"%'
order by v").Tables[0];
			lUser = ValueText.ToList1(dt);
			return lUser;
		}

		private void listBox1_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				ToControl.Focus();
				ValueText valCur = listBox1.SelectedItem as ValueText;
				if (valCur != null)
				{
					ToControl.Text = valCur.Value;

					//if (lRecent.Exists(ll=>ll.Value==valCur.Value))
					lRecent.RemoveAll(ll => ll.Value == valCur.Value);
					lRecent.Insert(0, valCur);
					ToControl.CausesValidation = true;
				}

				this.Visible = false;
			}
			else if (e.KeyCode == Keys.Escape)
			{
				ToControl.Focus();
				this.Visible = false;
				e.Handled = true;
			}
		}

		private void UWinLiveSearch_Load(object sender, EventArgs e)
		{
			if (ToControl != null)
				ToControl.KeyUp += textBox2_KeyUp;
			this.Visible = false;
		}

		private void UWinLiveSearch_Leave(object sender, EventArgs e)
		{
			ToControl.CausesValidation = true;
			this.Visible = false;
		}

		private void UWinLiveSearch_Resize(object sender, EventArgs e)
		{
			listBox1.Width = this.Width - 2;
			listBox1.Height = this.Height - 2;
		}
	}
	public class ValueText
	{
		//public string Text = "", Value = "";//可以多个
		public string Text { get; set; }
		public string Value { get; set; }
		public ValueText(string _text, string _value)
		{
			Text = _text;
			Value = _value;
		}

		public override string ToString()
		{
			return Text;
		}

		//columns must have V,T
		public static List<ValueText> ToList1(DataTable dt, bool addNull = false)
		{
			List<ValueText> lv = new List<ValueText>();
			foreach (DataRow item in dt.Rows)
			{
				lv.Add(new ValueText(item["T"].ToString(), item["V"].ToString()));
			}
			if (addNull)
			{
				lv.Add(new ValueText("", ""));
			}
			return lv;
		}
	}
}
