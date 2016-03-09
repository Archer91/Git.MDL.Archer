using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDL_CRM.VO
{
    public class FormSysSeqVO
    {
        public string Seq_Entity { get; set; }
        public string Seq_Site { get; set; }
        public string Seq_Type { get; set; }
        public string Seq_Name { get; set; }
        public int? Seq_Min_Val { get; set; }
        public int? Seq_Max_Val { get; set; }
        public int? Seq_Curr_Val { get; set; }
        public string Seq_Prefix { get; set; }
        public string Seq_Suffix { get; set; }
        public string Seq_Crt_By { get; set; }
        public DateTime? Seq_Crt_On { get; set; }
        public string Seq_Upd_By { get; set; }
        public DateTime? Seq_Upd_On { get; set; }
        public string Seq_YYYYMM { get; set; }
        public int? Seq_Length { get; set; }
        public string Seq_Prefix_YMD { get; set; }
        public int? Seq_Step { get; set; }
        public string Seq_Status { get; set; }

        public string Seq_NO { get; set; }//票据号
        public int Seq_Flag { get; set; }//序号标记（-1为新增，1为更新）
    }
}
