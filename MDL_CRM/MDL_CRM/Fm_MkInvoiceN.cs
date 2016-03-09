using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using PubApp.Data;
using System.Threading;

namespace MDL_CRM
{
    public partial class Fm_MkInvoiceN : Form
    {
        public Fm_MkInvoiceN()
        {
            InitializeComponent();
            txtCode.Focus();
        }
        public DataGridViewTextBoxEditingControl dgvTxt = null; // 声明 一个 CellEdit   
        public string strword = string.Empty;
        //定为可以编辑的首行
        public int _intRowEdit = -1;
        public bool boolEdit = false;
        private string _jobm_no = string.Empty;
        private string _invnoId = "";
        private string _currency ="";
        private string auditId = "";
        private string ls_gov_com = "";   //G  C
        private string ls_print_type = "";   //S  L
        private DataSet dsInVnoce;

         //产生发票
        //private void MakeInvoice()
        //{
        //    StringBuilder sb = new StringBuilder();
        //    string sql = "";
        //    string strEntity = "MDL_SO";
        //    string invnoId = "";
        //    sb.Append("  insert into zt10_invoice_dtl ");
        //    sb.Append(" ( invd_invno,invd_entity, invd_lineno, invd_jobno, invd_prodcode, invd_desc, invd_qty, invd_unit, invd_uprice, invd_discount, ");
        //    sb.Append("  invd_createby, invd_createdate, invd_lmodby, invd_lmoddate, invd_charge_yn, invd_group_id   ");
        //    sb.Append(" ) ");
        //    sb.Append(" select '" + invnoId + "','" + strEntity + "',schg_lineno,schg_jobm_no,  schg_prodcode, ");
        //    sb.Append(" ( select pro.prod_desc from  product pro  where pro.prod_code = jp.schg_prodcode) ");
        //    sb.Append(" prod_desc , schg_qty, schg_unit, (select fn_sp_getPrice(jp2.schg_PRO_MAT,jo2.jobm_accountid,JP2.schg_PRODCODE,ac2.ACCT_PRICEGROUP, ");
        //    sb.Append("   ac2.ACCT_JOB_TYPE,JO2.JOBM_NO,jobm_receivedate,null,trunc(sysdate),trunc(sysdate)) ");
        //    sb.Append(" from job_order jo2,zt10_so_charge_dtl jp2,account ac2 where jo2.jobm_no=jp2.schg_jobm_no and jo2.jobm_accountid=ac2.acct_id and  jo2.jobm_no=jp.schg_jobm_no ");
        //    sb.Append(" and schg_prodcode = jp.schg_prodcode) uprice, ");
        //    //sb.Append(" 1,   ");    //折扣取值
        //    sb.Append(" select distinct jobm_discount	from job_order jo where jo.jobm_no = '" + _jobm_no + "',   ");
        //    sb.Append("  schg_createby, schg_createdate, schg_lmodby, schg_lmoddate,schg_charge_yn, schg_group_id ");
        //    sb.Append("   from zt10_so_charge_dtl jp  ");
        //    sb.Append(" where jp.schg_charge_yn  in (1,3) and  jp.schg_jobm_no = '" + _jobm_no + "' ");
        //    sb.Append("  ");

        //    //接收地址  发送地址   invh_from_address, invh_shipto_address,invh_acct_remark
        //    string invh_from_address = "";
        //    string invh_shipto_address = "";
        //    string invoice_remark = "";   //invh_acct_remark
        //    string sqladd = " select t.udc_extend01,t.udc_extend02,t.udc_extend03  from zt00_udc_udcode t where t.udc_sys_code = 'MDLCRM' and t.udc_category = 'SO' and t.udc_key = 'ENTITY' and t.udc_value = 'MDIL澳门'  order by udc_key ";
        //         DataTable dtaddrFrom = DB.GetDSFromSql(sqladd).Tables[0];
        //         if (dtaddrFrom.Rows.Count > 0)
        //        {
        //            invh_from_address = dtaddrFrom.Rows[0]["udc_extend02"].ToString();
        //        }
        //    sqladd = " ";
          

        //    sb = new StringBuilder();
        //    sb.Append(" select  ac.acct_id || ' - '  ||  decode(ac.acct_name_eng,'',ac.acct_name,ac.acct_name_eng) acct_addr0, ");
        //    sb.Append(" decode(ac.acct_tel,'','','Tel: ' || ac.acct_tel)  acct_tel, ");
        //    sb.Append(" ac.acct_addr, ");
        //    sb.Append(" ac.acct_addr_2, ");
        //    sb.Append(" ac.acct_addr_3,  ");
        //    sb.Append(" ac.acct_addr_4, ");
        //     sb.Append(" ac.acct_invoice_remark ");
        //     sb.Append(" from account ac where ac.acct_id = 'EK86' ");
        //     sb.Append("  ");

        //     sqladd = sb.ToString();
        //     DataTable dtaddrShipto = DB.GetDSFromSql(sqladd).Tables[0];
            
        //    if (dtaddrShipto.Rows.Count > 0)
        //    {
        //        invh_shipto_address = dtaddrShipto.Rows[0]["acct_addr0"].ToString() + "\t\n"  +
        //            dtaddrShipto.Rows[0]["acct_addr"].ToString() + "\t\n"  +
        //            dtaddrShipto.Rows[0]["acct_addr2"].ToString() + "\t\n"  +
        //            dtaddrShipto.Rows[0]["acct_addr3"].ToString() + "\t\n"  +
        //            dtaddrShipto.Rows[0]["acct_addr4"].ToString() + "\t\n"  +
        //            dtaddrShipto.Rows[0]["acct_tel"].ToString() + "\t\n";
        //        invoice_remark = dtaddrShipto.Rows[0]["acct_invoice_remark"].ToString();
        //    }

        //}

        /// <summary>
        /// 打开界面前先设值
        /// </summary>
        public string Jobm_no
        {
            get { return _jobm_no; }
            set { _jobm_no = value; }
        }

        /// <summary>
        /// 客户货币类型
        /// </summary>
        public string Currency
        {
            get { return _currency; }
            set { _currency = value; }
        }

        /// <summary>
        /// 打开界面前先设发票号码值
        /// </summary>
        public string InvnoId
        {
            get { return _invnoId; }
            set { _invnoId = value; }
        }

        /// <summary>
        /// 产生发票
        /// </summary>
        public void InsertHeader()
        { 
        
        }
        public void InsertDetails()
        { 
        
        } 

        public void BindDataGridView(string _word)
        {
            StringBuilder sb = new StringBuilder();
            string sql ="";           
            txtCode.Text = _invnoId;
            BindInvnoteInfo(_invnoId);
            //sb.Append(" select zsd.schg_jobm_no,zsd.schg_prodcode,pro.prod_desc,zsd.schg_qty,zsd.schg_unit, ");
            //sb.Append(" (select fn_sp_getPrice(jp2.JDTL_PRO_MAT,jo2.jobm_accountid,JP2.JDTL_PRODCODE,ac2.ACCT_PRICEGROUP, ");
            //sb.Append("  ac2.ACCT_JOB_TYPE,JO2.JOBM_NO,jobm_receivedate,null,trunc(sysdate),trunc(sysdate)) ");
            //sb.Append(" from job_order jo2,job_product jp2,account ac2 where jo2.jobm_no=jp2.jobm_no and jo2.jobm_accountid=ac2.acct_id and  jo2.jobm_no= zsd.schg_jobm_no ");
            //sb.Append(" and zsd.schg_prodcode = jp2.jdtl_prodcode ");
            //sb.Append(" ) uprice,  ");
            //sb.Append(" zsd.schg_qty * (select fn_sp_getPrice(jp2.JDTL_PRO_MAT,jo2.jobm_accountid,JP2.JDTL_PRODCODE,ac2.ACCT_PRICEGROUP, ");
            //sb.Append(" ac2.ACCT_JOB_TYPE,JO2.JOBM_NO,jobm_receivedate,null,trunc(sysdate),trunc(sysdate)) ");
            //sb.Append(" from job_order jo2,job_product jp2,account ac2 where jo2.jobm_no=jp2.jobm_no and jo2.jobm_accountid=ac2.acct_id and  jo2.jobm_no= zsd.schg_jobm_no ");
            //sb.Append(" and zsd.schg_prodcode = jp2.jdtl_prodcode ");
            //sb.Append(" )  sumPrice ");
            //sb.Append(" ,zsd.schg_createby,to_char(zsd.schg_createdate,'DD/MM/YYYY HH:MI:SS')  schg_createdate,zsd.schg_lmodby,to_char(zsd.schg_lmoddate,'DD/MM/YYYY HH:MI:SS') schg_lmoddate ");
            //sb.Append(" from  ZT10_SO_CHARGE_DTL zsd,product pro ");
            //sb.Append(" where zsd.schg_prodcode = pro.prod_code ");
            ////sb.Append(" and zsd.schg_jobm_no = '" + _jobm_no + "' ");
            //sb.Append(" and zsd.schg_jobm_no in (");
            //sb.Append(" select ivd.invd_jobno from zt10_invoice_dtl  ivd where ivd.invd_invno = '" + _invnoId +  "' ");  
            //sb.Append(" ) ");            
            // invd.invd_jobno = 'JIC00070'  and
            sb.Append(" select invd.invd_jobno,invd.invd_prodcode,invd.invd_desc,invd_qty, invd_unit, invd_uprice,(invd_qty * invd_uprice) sumPrice, ");
            sb.Append(" invd_createby, invd_createdate, invd_lmodby, invd_lmoddate "); 
            sb.Append("  from  zt10_invoice_dtl  invd  where  invd.invd_invno = '" + _invnoId + "' order by invd_prodcode,invd_lineno");
            sb.Append("  ");

            sql = string.Format(sb.ToString());
            DataSet ds = DB.GetDSFromSql(sql);
            double sumPrice = 0;
            double tempPrice = 0;
            int intSum = 0;

            dsInVnoce = ds;
            if (ds.Tables[0].Rows.Count > 0)
            {
                sumPrice = 0;
                tempPrice = 0;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {   tempPrice = 0;
                    tempPrice = ds.Tables[0].Rows[i]["sumPrice"].ToString() == "" ? 0 : double.Parse(ds.Tables[0].Rows[i]["sumPrice"].ToString());
                    sumPrice = sumPrice + tempPrice;
                }
                DataRow dr = ds.Tables[0].NewRow();
                dr["sumPrice"] = sumPrice.ToString();
                ds.Tables[0].Rows.Add(dr);
               
                dataGridView1.DataSource = ds.Tables[0];
                intSum = dataGridView1.Rows.Count - 1;
                dataGridView1.Rows[intSum].ReadOnly = true;
                Color _color = Color.FromArgb(224, 224, 224);
                dataGridView1.Rows[intSum].Cells["invd_jobno"].Style.BackColor =     _color;
                dataGridView1.Rows[intSum].Cells["invd_prodcode"].Style.BackColor =  _color;
                dataGridView1.Rows[intSum].Cells["invd_desc"].Style.BackColor =      _color;
                dataGridView1.Rows[intSum].Cells["invd_unit"].Style.BackColor =      _color;

                dataGridView1.Rows[intSum].Cells["invd_qty"].Style.BackColor =     _color;
                dataGridView1.Rows[intSum].Cells["invd_uprice"].Style.BackColor =  _color;
            }
            



            sb = new StringBuilder();
            sql ="";  
            sb.Append(" select nodt.cndt_invno,nodt.cndt_jobno,nodt.cndt_prodcode,nodt.cndt_desc,nodt.cndt_qty,nodt.cndt_uprice, "); 
            sb.Append(" nodt.cndt_qty * nodt.cndt_uprice sumPrice ");
            sb.Append(" ,to_char(nodt.cndt_createdate,'DD/MM/YYYY HH:MI:SS')  cndt_createdate, ");
            sb.Append(" nodt.cndt_createby,to_char(nodt.cndt_lmoddate,'DD/MM/YYYY HH:MI:SS')  cndt_lmoddate,nodt.cndt_lmodby,cte.cnhr_status from credit_note cte,credit_note_dtl nodt ");
            sb.Append(" where cte.cnhr_no = nodt.cnhr_no and nodt.cndt_invno = '" + _invnoId + "' order by cndt_prodcode"); 
            sb.Append("  ");

            sql = string.Format(sb.ToString());
            DataSet ds2 = DB.GetDSFromSql(sql);
            double sumPrice2 = 0;
            if (ds2.Tables[0].Rows.Count > 0)
            {
                sumPrice = 0;
                for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
                {
                    sumPrice2 = sumPrice2 + double.Parse(ds2.Tables[0].Rows[i]["sumPrice"].ToString());
                }
                DataRow dr2 = ds2.Tables[0].NewRow();
                dr2["sumPrice"] = sumPrice2.ToString();
                ds2.Tables[0].Rows.Add(dr2);
                dataGridView2.DataSource = ds2.Tables[0];
                dataGridView2.Visible = true;
                label1.Visible = true;
                
            }
            else
            {
                dataGridView2.Visible = false;
                label1.Visible = false;
                //dataGridView1.Height = 800;
                System.Drawing.Size mSize = SystemInformation.WorkingArea.Size;
                dataGridView1.Height = mSize.Height -100;               
            }


            
        }         

        
        /// <summary>
        /// 显示发票的状态信息
        /// </summary>
        /// <param name="_word">输入发票号码</param>
        public void BindInvnoteInfo(string _word )
        {
            StringBuilder sb = new StringBuilder();
            string sql = "";
            sb.Append(" select i.invh_invno, to_char(i.invh_date,'DD/MM/YYYY') invh_date,i.invh_acctid,acc.acct_name,i.invh_ccy,i.invh_status,i.invh_lmodby, i.invh_lmoddate,i.invh_remark,i.invh_cfmby, to_char(i.invh_cfmdate,'DD/MM/YYYY HH:MI:SS') invh_cfmdate");
            sb.Append(" ,i.invh_lprintby,to_char(i.invh_lprintdate,'DD/MM/YYYY HH:MI:SS') invh_lprintdate,i.invh_voidby, to_char(i.invh_voiddate,'DD/MM/YYYY HH:MI:SS')   invh_voiddate ");            
            sb.Append("  from ZT10_INVOICE_MSTR i,account acc where i.invh_invno = '{0}' and i.invh_acctid = acc.acct_id ");
            sb.Append("  ");
                        
            sql = string.Format(sb.ToString(), _word);
            DataSet ds = DB.GetDSFromSql(sql);
            txtCode.Text = _word;
            if (ds.Tables[0].Rows.Count > 0)
            {
                txt_Account.Text = ds.Tables[0].Rows[0]["invh_acctid"].ToString();
                txt_Currency.Text = ds.Tables[0].Rows[0]["invh_ccy"].ToString();
                ///客户货币类型
                Currency = ds.Tables[0].Rows[0]["invh_ccy"].ToString();

                txt_State.Text = ChandWordDis(ds.Tables[0].Rows[0]["invh_status"].ToString());
                txt_State.Tag = ds.Tables[0].Rows[0]["invh_status"].ToString();
                txt_AccName.Text = ds.Tables[0].Rows[0]["acct_name"].ToString();
                txt_InvnoteDate.Text = ds.Tables[0].Rows[0]["invh_date"].ToString();
                txt_Remark.Text = ds.Tables[0].Rows[0]["invh_remark"].ToString();
                txt_ModifyUser.Text = ds.Tables[0].Rows[0]["invh_cfmby"].ToString();
                txt_ComplateDt.Text = ds.Tables[0].Rows[0]["invh_cfmdate"].ToString();

                txt_Print.Text = ds.Tables[0].Rows[0]["invh_lprintby"].ToString();
                txt_Printdate.Text = ds.Tables[0].Rows[0]["invh_lprintdate"].ToString();

                txt_Cancel.Text = ds.Tables[0].Rows[0]["invh_voidby"].ToString();
                txt_CancelDt.Text = ds.Tables[0].Rows[0]["invh_voiddate"].ToString();

                if (ds.Tables[0].Rows[0]["invh_status"].ToString() == "V")
                {
                    txt_Cancel.Text = ds.Tables[0].Rows[0]["invh_lmodby"].ToString();
                    txt_CancelDt.Text = ds.Tables[0].Rows[0]["invh_lmoddate"].ToString();
                }
                if (ds.Tables[0].Rows[0]["invh_status"].ToString() == "N")
                {
                    txt_Remark.Enabled = true;
                    txt_Remark.ReadOnly = false;
                    btn_Save.Enabled = true;
                    btnComplate.Enabled = true;
                    //btn_Xml.Enabled = true;
                    btn_Cancel.Enabled = true;
                }
                else if (ds.Tables[0].Rows[0]["invh_status"].ToString() == "C")
                {                    
                    btn_Cancel.Enabled = false;
                    txt_Remark.Enabled = false;
                    txt_Remark.ReadOnly = true;
                    btn_Save.Enabled = false;
                    btnComplate.Enabled = false;
                    //btn_Xml.Enabled = false;
                }
                else
                {
                    txt_Remark.Enabled = false;
                    txt_Remark.ReadOnly = true;
                    btn_Save.Enabled = false;
                    btnComplate.Enabled = false;
                    //btn_Xml.Enabled = false;
                    btn_Cancel.Enabled = false;
                    Btn_Print.Enabled = false;//取消后不能打印
                }
            ///对客户的GC进行判断    
                string sqlGc = "select acct_dnote_yn, acct_pricegroup, acct_job_type, acct_print_type, acct_invdate_adjust from account where  acct_id = '{0}'";                
                DataTable dtAccount = new DataTable();
                dtAccount = DB.GetDSFromSql(string.Format(sqlGc, ds.Tables[0].Rows[0]["invh_acctid"].ToString())).Tables[0];
                if (dtAccount.Rows.Count > 0)
                {
                    ls_gov_com = dtAccount.Rows[0]["acct_job_type"].ToString();
                    ls_print_type = dtAccount.Rows[0]["acct_print_type"].ToString();
                }
            }
        
        }

        private string ChandWordDis(string _en)
        {
            string _ch = string.Empty;
            switch (_en)
            {
                case "V":
                    _ch = "取消";
                    break;
                case "C":
                    _ch = "正式";
                    break;
                case "N":
                    _ch = "生成中";
                    break;               
                default:
                    _ch = _en;
                    break;
            }
            return _ch;
        }


        public void ToFocus()
        {
            try
            {
                string _stateWord = string.Empty;
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    _stateWord = dataGridView1.Rows[i].Cells["zjdtl_fda_qty"].Tag == null ? "" : dataGridView1.Rows[i].Cells["zjdtl_fda_qty"].Tag.ToString();
                    if (_stateWord == "edit")
                    {
                        this.dataGridView1.CurrentCell = this.dataGridView1.Rows[i].Cells["zjdtl_fda_qty"];
                        this.dataGridView1.BeginEdit(true);
                        break;
                    }
                }
            }
            catch (Exception)
            {
            }
        }
     
        public bool InputIsNumber(string _intstr)
        {
            try
            {
                if (string.IsNullOrEmpty(_intstr))
                {
                    return true;
                }
                //int _inta = Int32.Parse(_intstr)
                int _inta = (int)(Math.Floor(float.Parse(_intstr)));          
                return  true  ;
            }
            catch
            {
                return false;

            }
        }

        private void Fm_Load(object sender, EventArgs e)
        {
            txtCode.Focus();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView2.AutoGenerateColumns = false;
            BindDataGridView("");        
            txtCode.Text = InvnoId;

        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {            

        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
           e.Cancel = true;
        }
        
        #region 处理键盘事件

        private void dgv2_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

            dgvTxt = (DataGridViewTextBoxEditingControl)e.Control; // 赋值   
            dgvTxt.SelectAll();
            //dgvTxt.KeyPress += Cells_KeyPress; // 绑定到事件             
        }
        // 自定义事件KeyPress事件
        private void Cells_KeyPress(object sender, KeyPressEventArgs e)
        {
            keyPressXS(e, dgvTxt);  
        }     

        public static void keyPressXS(KeyPressEventArgs e, DataGridViewTextBoxEditingControl dgvTxt)
        {
                       
            if (char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;       //让操作生效
                int j = 0;
                int k = 0;
                bool flag = false;
                if (dgvTxt.Text.Length == 0)
                {
                    if (e.KeyChar == '.'){
                        e.Handled = false;             //让操作失效
                    }
                }
                for (int i = 0; i < dgvTxt.Text.Length; i++)
                {

                    if (dgvTxt.Text[i] == '.')
                    {
                        j++;
                        flag = false;
                    }
                    if (flag)
                    {
                        if (char.IsNumber(dgvTxt.Text[i]) && e.KeyChar != (char)Keys.Back)
                        {
                            k++;
                        }
                    }
                    if (j >= 1)
                    {
                        if (e.KeyChar == '.'){

                            e.Handled = false;             //让操作失效
                        }
                    }
                    if (k == 2)
                    {

                        if (char.IsNumber(dgvTxt.Text[i]) && e.KeyChar != (char)Keys.Back)
                        {

                            if (dgvTxt.Text.Length - dgvTxt.SelectionStart < 3)
                            {

                                if (dgvTxt.SelectedText != dgvTxt.Text)
                                {
                                    e.Handled = true;             ////让操作失效

                                }

                            }

                        }

                    }

                }

            }

            else
            {
                e.Handled = true;
            }  
        }

        #endregion   处理键盘事件

        private void Fm_Job_Pro_Activated(object sender, EventArgs e)
        {
            txtCode.Focus();
        }
      
        private void Btn_Print_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                string sql = " update ZT10_INVOICE_MSTR set invh_lmodby ='" + DB.loginUserName + "',invh_lmoddate = sysdate, invh_lprintby ='" + DB.loginUserName + "'"
                                      + ",invh_lprintdate=sysdate  where invh_invno = '" + _invnoId + "' ";
                DB.ExecuteFromSql(sql);
                BindInvnoteInfo(_invnoId);

                Fm_Invoice frmInvo = new Fm_Invoice();
                frmInvo.Ccy = _currency;
                //frmInvo.Ccy = "HKD";
                frmInvo.InvnoId = _invnoId;
                frmInvo.Show();
            }
           
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //hkd
            Fm_Invoice frmInvo = new Fm_Invoice();
            frmInvo.Ccy = _currency;
            frmInvo.InvnoId = _invnoId;
            frmInvo.Show();           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //EUR
            Fm_Invoice frmInvo = new Fm_Invoice();
            frmInvo.Ccy = _currency;
            frmInvo.InvnoId = _invnoId;
            frmInvo.Show();    
        }
        /// <summary>
        /// 生成正式发票
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnComplate_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否把这张发票完成?", "系统讯息-MDL_CRM", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //首先取发票号过来
                 string sqlGetInvoice = "select 'A' || to_char(sysdate,'yy') || '00' || invoice_seq.nextval  invoiceNo  from dual";
                 StringBuilder sb = new StringBuilder();

                 //按G方法取发票号   ls_gov_com = "";   //G  C
                 if (ls_gov_com == "G")
                 {
                     //正式发票号A开头 G类
                     sb.Append("  select decode(invno2,'A','A' || to_char(sysdate,'YY') || '00001',invno2) inv_no from ( ");
                     sb.Append(" select 'A' ||  to_char(substr(invno,2,length(invno)) + 1) invno2  from ( ");
                     sb.Append(" select max(decode(substr(invh_invno, 1, 1), '*', '99'||invh_invno, invh_invno))		invno ");
                     sb.Append(" from job_order, ZT10_INVOICE_MSTR i ");
                     sb.Append(" where job_order.jobm_invno = i.invh_invno(+) ) ) ");
                     sb.Append("  ");
                 }
                 else
                 {   //正式发票号数字开头 C类
                     sb.Append(" select decode(invno,'',to_char(sysdate,'YY') || '000001',invno) inv_no from ( ");
                     sb.Append(" select  max(i.invh_invno) + 1  invno  from  ZT10_INVOICE_MSTR i  where  substr(i.invh_invno,1,1) not in ('*','A') ) ");
                     sb.Append("  ");        
                 }

                 sqlGetInvoice = sb.ToString();
                 DataTable  dsInvoice = DB.GetDSFromSql(sqlGetInvoice).Tables[0];
                 string _getInvoiceId = "";
                 if (dsInvoice.Rows.Count > 0)
                 {
                     _getInvoiceId = dsInvoice.Rows[0]["inv_no"].ToString();
                 }

                 if (_getInvoiceId.IndexOf("*") > -1)
                 {
                     //生成正式发票号号码不正确
                     MessageBox.Show("生成正式发票号号码不正确！", "系统讯息-MDL_CRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                     return;
                 }
                 else               
                 {
                   
                     //_getInvoiceId = "A150010032";
                     sb = new StringBuilder();
                     ArrayList arrList = new ArrayList();
                     string sql = "";
                     sb.Append(" update ZT10_INVOICE_MSTR set invh_invno = '" + _getInvoiceId + "'");
                     sb.Append(" ,invh_lmodby ='" + DB.loginUserName + "',invh_lmoddate = sysdate,invh_status='C', invh_cfmby ='" + DB.loginUserName + "',invh_remark='");
                     sb.Append(txt_Remark.Text.Trim());                  
                     sb.Append("',invh_cfmdate=sysdate  where invh_invno = '" + _invnoId + "'");
                     sb.Append("  ");
                     sql = sb.ToString();
                     arrList.Add(sql);


                     //改变发票相对应的号码
                     sql = "";
                     sb = new StringBuilder();
                     sb.Append(" update job_order set jobm_status = 'B',jobm_invno='" + _getInvoiceId + "' where jobm_invno =");
                     sb.Append("  '" + _invnoId + "' ");
                     sb.Append("  ");
                     sql = sb.ToString();
                     arrList.Add(sql);


                     //改变新系统表，发票相对应的号码
                     sql = "";
                     sb = new StringBuilder();
                     sb.Append(" update zt10_so_sales_order set so_invno='" + _getInvoiceId + "' where so_invno =");                   
                     sb.Append("  '" + _invnoId + "' ");
                     sb.Append("  ");
                     sql = sb.ToString();
                     arrList.Add(sql);


                     sql = "";
                     sb = new StringBuilder();

                     sb.Append(" update zt10_invoice_dtl set  ");
                     sb.Append(" invd_invno ='" + _getInvoiceId + "' ");
                     sb.Append(" where invd_invno = '" + _invnoId + "'");
                     sb.Append("  ");
                     sql = sb.ToString();
                     arrList.Add(sql);
                     bool boolExec = DB.ExeTrans(arrList);
                     if (boolExec)
                     {
                         BindInvnoteInfo(_getInvoiceId);
                         _invnoId = _getInvoiceId;
                     }
                     MessageBox.Show("生成成功，正式发票号为：" + _invnoId , "系统讯息-MDL_CRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 }                 
            } 
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {

            //如果发票状态为正式发票C的情况，就要审核权限，为N临时发票的情况可以直接取消
            auditId = "";
            if (MessageBox.Show("是否把这张发票取消?", "系统讯息-MDL_CRM", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                if (txt_State.Tag.ToString() == "C")
                {
                    Fm_Check fmcheck = new Fm_Check();
                    fmcheck.ShowDialog();  
                }
                else if (txt_State.Tag.ToString() == "N")
                {
                    auditId = DB.loginUserName;                
                }                

                if (auditId.Length > 0)
                {
                    string sql = " update ZT10_INVOICE_MSTR set invh_lmodby ='" + DB.loginUserName + "',invh_status='V',invh_lmoddate = sysdate,invh_remark='" +
                        txt_Remark.Text.Trim() + "',invh_voidby ='" + auditId + "',invh_voiddate = sysdate "
                        + " where invh_invno = '" + _invnoId + "' ";

                    ArrayList arrList = new ArrayList();
                    arrList.Add(sql);
                    sql = " update job_order set jobm_status = 'N',jobm_invno=''  where jobm_invno = '" + _invnoId + "'";
                    arrList.Add(sql);

                    sql = " update zt10_so_sales_order set SO_STATUS='N',so_invno='' where so_invno = '" + _invnoId + "'";
                    arrList.Add(sql);


                    bool boolExec = DB.ExeTrans(arrList);
                    if (boolExec)
                    {
                    BindInvnoteInfo(_invnoId);
                    MessageBox.Show("取消成功！", "系统讯息-MDL_CRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    } 
                }
                else
                {
                   // MessageBox.Show("没有审核权限或密码不正确！", "系统讯息-MDL_CRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            dsInVnoce.Tables[0].AcceptChanges();
            string strEntity = pubcls.CompanyName;
            if (chkdata())
            if (MessageBox.Show("是否对这张发票保存?", "系统讯息-MDL_CRM", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sql = " update ZT10_INVOICE_MSTR set invh_lmodby ='" + DB.loginUserName + "',invh_lmoddate = sysdate,invh_remark='" +
                    txt_Remark.Text.Trim()
                    + "' where invh_invno = '" + _invnoId + "' ";

                ArrayList arrList = new ArrayList();
                arrList.Add(sql);              
                sql = "delete zt10_invoice_dtl  where invd_invno = '" + _invnoId + "'";
                arrList.Add(sql);
                StringBuilder  sb = new StringBuilder();
                for (int j = 0; j < dsInVnoce.Tables[0].Rows.Count; j++)
                {
                    sql = "";
                    sb = new StringBuilder();
                    sb.Append(" insert into zt10_invoice_dtl (invd_invno, invd_lineno,invd_jobno,invd_entity,invd_prodcode,invd_desc,invd_qty,invd_unit ,invd_uprice ,   ");
                    sb.Append("  invd_createby,  invd_createdate , invd_lmodby  ,invd_lmoddate ) ");
                    sb.Append("  values  ( '");
                    
                    sb.Append(_invnoId + "','");
                    sb.Append( j.ToString() + "','");
                    sb.Append(dsInVnoce.Tables[0].Rows[j]["invd_jobno"].ToString() +  "','");
                    sb.Append(strEntity + "','");
                    sb.Append(dsInVnoce.Tables[0].Rows[j]["invd_prodcode"].ToString() +  "','");
                    sb.Append(dsInVnoce.Tables[0].Rows[j]["invd_desc"].ToString() +  "','");
                    sb.Append(dsInVnoce.Tables[0].Rows[j]["invd_qty"].ToString() +  "','");
                    sb.Append(dsInVnoce.Tables[0].Rows[j]["invd_unit"].ToString() +  "','");
                    sb.Append(dsInVnoce.Tables[0].Rows[j]["invd_uprice"].ToString() +  "','");

                    if (dsInVnoce.Tables[0].Rows[j]["invd_createby"].ToString().Length > 1)
                    {
                         sb.Append(dsInVnoce.Tables[0].Rows[j]["invd_createby"].ToString() +  "',");
                    }
                    else
                    {
                        sb.Append(DB.loginUserName + "',");
                    }
                    

                    if (dsInVnoce.Tables[0].Rows[j]["invd_createdate"].ToString().Length > 5)
                    {
                        sb.Append("to_date('" + dsInVnoce.Tables[0].Rows[j]["invd_createdate"].ToString() + "','YYYY/MM/DD HH24:MI:SS'),'");
                    }
                    else
                    {
                        sb.Append("sysdate,'");
                    }


                    if (dsInVnoce.Tables[0].Rows[j]["invd_lmodby"].ToString().Length > 1)
                    {
                        sb.Append(dsInVnoce.Tables[0].Rows[j]["invd_lmodby"].ToString() + "',");
                    }
                    else
                    {
                        sb.Append(DB.loginUserName + "',");
                    }

                    if (dsInVnoce.Tables[0].Rows[j]["invd_lmoddate"].ToString().Length > 5)
                    {
                        sb.Append("to_date('" + dsInVnoce.Tables[0].Rows[j]["invd_lmoddate"].ToString() + "','YYYY/MM/DD HH24:MI:SS') )");
                    }
                    else
                    {
                        sb.Append("sysdate)");
                    }                  

                    sb.Append("   ");                 
                    sql = sb.ToString();

                    if (dsInVnoce.Tables[0].Rows[j]["invd_jobno"].ToString().Length > 5)
                    {
                        arrList.Add(sql);
                    }
                }
                bool boolExec = DB.ExeTrans(arrList);
                if (boolExec)
                {
                    BindInvnoteInfo(_invnoId);
                    BindDataGridView(_invnoId);
                    MessageBox.Show("保存成功！", "系统讯息-MDL_CRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }                 
            }
        }

        private void btn_Xml_Click(object sender, EventArgs e)
        {

        }

        private void insertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveControl.Name == dataGridView1.Name && dataGridView1.Rows.Count > 0)
            {
                int pos = dataGridView1.CurrentCell.RowIndex;
                DataRow dr = dsInVnoce.Tables[0].NewRow();
                dsInVnoce.Tables[0].Rows.InsertAt(dr, pos);
                boolEdit = true;              
            }
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveControl.Name == dataGridView1.Name && dataGridView1.Rows.Count > 0)
            {
                int pos = dataGridView1.Rows.Count -1;       
                DataRow dr = dsInVnoce.Tables[0].NewRow();
                dsInVnoce.Tables[0].Rows.InsertAt(dr, pos);
                boolEdit = true;
            }  
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveControl.Name == dataGridView1.Name)
            {
                if (dataGridView1.Rows.Count == 1) { return; }
                if (MessageBox.Show("确定要删除这一行资料?", "警示", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    dataGridView1.Rows.RemoveAt(dataGridView1.CurrentCell.RowIndex);
                }
                boolEdit = true;
            }
        }

        private void printStripMenuItem1_Click(object sender, EventArgs e)
        {
            Btn_Print_Click(null, null);
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (dataGrid.Columns[e.ColumnIndex].Name == "colMaterial" && dataGrid.CurrentCell != null && e.RowIndex >= 0)
            {
               // GridOpenWindow(e.RowIndex);
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "invd_qty")
            {
                if (!Dal.IsNumber(dataGridView1.Rows[e.RowIndex].Cells["invd_qty"].Value.ToString()))
                {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
                }
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "invd_uprice")
            {
                if (!Dal.IsNumber(dataGridView1.Rows[e.RowIndex].Cells["invd_uprice"].Value.ToString()))
                {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
                }
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "invd_qty" || dataGridView1.Columns[e.ColumnIndex].Name == "invd_uprice")
            {
                try
                {
                    dataGridView1.Rows[e.RowIndex].Cells["sumPrice"].Value = double.Parse(dataGridView1.Rows[e.RowIndex].Cells["invd_uprice"].Value.ToString()) * double.Parse(dataGridView1.Rows[e.RowIndex].Cells["invd_qty"].Value.ToString());
                   
                }
                catch (Exception ex)
                { 
                
                }

            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "invd_jobno")
            {
                DataTable dt = Dal.GetDataTable("select so.* from zt10_so_sales_order so where so_jobm_no ='" + dataGridView1.Rows[e.RowIndex].Cells["invd_jobno"].Value.ToString() + "'");
                if (dt.Rows.Count <1)
                {
                    this.dataGridView1.CurrentCell = this.dataGridView1.Rows[e.RowIndex].Cells["invd_jobno"];
                    MessageBox.Show("工作单(" + dataGridView1.Rows[e.RowIndex].Cells["invd_jobno"].Value.ToString()  + ")不存在！", "系统讯息-MDL_CRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.dataGridView1.CurrentCell = this.dataGridView1.Rows[e.RowIndex].Cells["invd_jobno"];                  
                }
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "invd_prodcode")
            {
                StringBuilder sb = new StringBuilder();

                sb.Append(" select PROD_CODE,PROD_DESC,PROD_DESC_CHI,PROD_UNIT,ZPROD_FDAM_CODE,PROD_PRO_MAT,PROD_OTHER_NAME, ");
                sb.Append(" (select fn_sp_getPrice(jp2.schg_PRO_MAT,jo2.jobm_accountid,JP2.schg_PRODCODE,ac2.ACCT_PRICEGROUP, ");
                sb.Append(" ac2.ACCT_JOB_TYPE,JO2.JOBM_NO,jobm_receivedate,null,trunc(sysdate),trunc(sysdate))  ");
                sb.Append(" from job_order jo2,zt10_so_charge_dtl jp2,account ac2 where jo2.jobm_no=jp2.schg_jobm_no ");
                sb.Append(" and jo2.jobm_accountid=ac2.acct_id and  jo2.jobm_no='{0}' ");
                sb.Append(" and schg_prodcode = PROD_CODE ) uprice ");
                sb.Append("  from product where PROD_CODE='{1}' ");
                sb.Append("  ");
                DataTable dt = Dal.GetDataTable(string.Format(sb.ToString(), dataGridView1.Rows[e.RowIndex].Cells["invd_jobno"].Value.ToString(), dataGridView1.Rows[e.RowIndex].Cells["invd_prodcode"].Value.ToString()));
                loadgridValue(dt, e.RowIndex);
                bool boolSame = false;
                //invd_prodcode 如果表有已有这个材料项给出提示              
                string temp_prodcode = dataGridView1.Rows[e.RowIndex].Cells["invd_prodcode"].Value.ToString();
                string find_prodcode = string.Empty;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (i != e.RowIndex)
                    {
                        find_prodcode = dataGridView1.Rows[i].Cells["invd_prodcode"].Value.ToString();
                        if (temp_prodcode == find_prodcode)
                        {
                            boolSame = true;
                            break;
                        }
                    }
                }
                if (boolSame)
                {
                    MessageBox.Show("明细资料中手工物料编号(" + temp_prodcode + ")重复存在！", "系统讯息-MDL_CRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   
                }  


            }
        }
        /// <summary>
        /// 有效性检查
        /// </summary>
        /// <returns></returns>
        private bool chkdata()
        {
            bool blnok = true;
            //明细检查
            string salter = "";
            if (dsInVnoce.Tables[0].Rows.Count == 0)
            {
                salter = "必须有一条明细记录\r\n";
                MessageBox.Show(salter, "系统讯息-MDL_CRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                blnok = false;
                return false;
            }
            DataRow[] dr = dsInVnoce.Tables[0].Select("isnull(invd_qty,0)=0");
            if (dr.Length >= 1 &&  !string.IsNullOrEmpty(dr[0]["invd_prodcode"].ToString()))
            {
                salter = "明细资料有数量为零的记录\r\n";
                MessageBox.Show(salter, "系统讯息-MDL_CRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //blnok = false;
                //return false;
            }          

            dr = dsInVnoce.Tables[0].Select("isnull(invd_prodcode,'')=''");

            if (dr.Length >= 1 && !string.IsNullOrEmpty(dr[0]["invd_jobno"].ToString()) && !string.IsNullOrEmpty(dr[0]["invd_desc"].ToString()))
            {
                salter = "明细资料有手工材料编号为空的记录";
                MessageBox.Show(salter, "系统讯息-MDL_CRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                blnok = false;
                return false;
            }
            for (int i = 0; i < dsInVnoce.Tables[0].Rows.Count; i++)
            {
                if (dsInVnoce.Tables[0].Rows[i].RowState != DataRowState.Deleted)
                {
                    if (dsInVnoce.Tables[0].Rows[i]["invd_prodcode"].ToString() != "")
                    {
                        dr = dsInVnoce.Tables[0].Select("isnull(invd_prodcode,'')='" + dsInVnoce.Tables[0].Rows[i]["invd_prodcode"].ToString() + "'");
                        if (dr.Length > 1)
                        {
                            //blnok = false;
                            //MessageBox.Show("明细资料中手工物料编号(" + dsInVnoce.Tables[0].Rows[i]["invd_prodcode"].ToString() + ")重复存在！", "系统讯息-MDL_CRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //break;
                        }
                    }
                }
            }           
            return blnok;
        }


        private void loadgridValue(DataTable dt, int row)
        {
            try
            {
                if (Dal.ChkDataTable(dt))
                {
                    dataGridView1.Rows[row].Cells["invd_prodcode"].Value = dt.Rows[0]["PROD_CODE"].ToString();
                    dataGridView1.Rows[row].Cells["invd_desc"].Value = dt.Rows[0]["PROD_DESC"].ToString();
                    dataGridView1.Rows[row].Cells["invd_unit"].Value = dt.Rows[0]["PROD_UNIT"].ToString();
                    dataGridView1.Rows[row].Cells["invd_uprice"].Value = dt.Rows[0]["uprice"].ToString();
                    dataGridView1.Rows[row].Cells["sumPrice"].Value =  double.Parse(dataGridView1.Rows[row].Cells["invd_uprice"].Value.ToString()) *  double.Parse(dataGridView1.Rows[row].Cells["invd_qty"].Value.ToString());
                }  
                else
                {
                    dataGridView1.Rows[row].Cells["invd_prodcode"].Value = "";
                    dataGridView1.Rows[row].Cells["invd_desc"].Value = "";
                    dataGridView1.Rows[row].Cells["invd_unit"].Value = "";
                    dataGridView1.Rows[row].Cells["invd_uprice"].Value = "";
                }
            }
            catch (Exception ex1)
            { 
            
            }
            dt.Dispose();
        }
        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (e.RowIndex == -1) return;
                dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[0];
            }
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Tab))
            {
                if (dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].ReadOnly)
                {
                    SendKeys.Send("{Tab}");
                }

            }
        }    
    }
}
