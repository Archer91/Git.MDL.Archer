using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using NPOI;
using NPOI.HPSF;
using NPOI.HSSF;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.POIFS;
using NPOI.SS.Formula.Eval;
using NPOI.SS.UserModel;
using NPOI.Util;
using NPOI.SS;
using NPOI.DDF;
using NPOI.SS.Util;
using System.Collections;
using System.Text.RegularExpressions;
using NPOI.XSSF;
using NPOI.XSSF.UserModel;
using System.Drawing;
namespace PWW
{
     public class NPOIHelper
     {
         //private static WriteLog wl = new WriteLog();
 
 
         #region 从datatable中将数据导出到excel
         /// <summary>
         /// DataTable导出到Excel的MemoryStream
         /// </summary>
         /// <param name="dtSource">源DataTable</param>
         /// <param name="strHeaderText">表头文本</param>
         static MemoryStream ExportDT(DataTable dtSource, string strHeaderText)
         {
             HSSFWorkbook workbook = new HSSFWorkbook();
             HSSFSheet sheet = workbook.CreateSheet() as HSSFSheet;
 
             #region 右击文件 属性信息
 
             //{
             //    DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
             //    dsi.Company = "http://www.yongfa365.com/";
             //    workbook.DocumentSummaryInformation = dsi;
 
             //    SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
             //    si.Author = "柳永法"; //填加xls文件作者信息
             //    si.ApplicationName = "NPOI测试程序"; //填加xls文件创建程序信息
             //    si.LastAuthor = "柳永法2"; //填加xls文件最后保存者信息
             //    si.Comments = "说明信息"; //填加xls文件作者信息
             //    si.Title = "NPOI测试"; //填加xls文件标题信息
             //    si.Subject = "NPOI测试Demo"; //填加文件主题信息
             //    si.CreateDateTime = DateTime.Now;
             //    workbook.SummaryInformation = si;
             //}
 
             #endregion
 
             HSSFCellStyle dateStyle = workbook.CreateCellStyle() as HSSFCellStyle;
             HSSFDataFormat format = workbook.CreateDataFormat() as HSSFDataFormat;
             dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");
 
             //取得列宽
             int[] arrColWidth = new int[dtSource.Columns.Count];
             foreach (DataColumn item in dtSource.Columns)
             {
                 arrColWidth[item.Ordinal] = Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;
             }
             for (int i = 0; i < dtSource.Rows.Count; i++)
             {
                 for (int j = 0; j < dtSource.Columns.Count; j++)
                 {
                     int intTemp = Encoding.GetEncoding(936).GetBytes(dtSource.Rows[i][j].ToString()).Length;
                     if (intTemp > arrColWidth[j])
                     {
                         arrColWidth[j] = intTemp;
                     }
                 }
             }
             int rowIndex = 0;
 
             foreach (DataRow row in dtSource.Rows)
             {
                 #region 新建表，填充表头，填充列头，样式
 
                 if (rowIndex == 65535 || rowIndex == 0)
                 {
                     if (rowIndex != 0)
                     {
                         sheet = workbook.CreateSheet() as HSSFSheet;
                     }
 
                     #region 表头及样式
 
                     {
                         HSSFRow headerRow = sheet.CreateRow(0) as HSSFRow;
                         headerRow.HeightInPoints = 25;
                         headerRow.CreateCell(0).SetCellValue(strHeaderText);
 
                         HSSFCellStyle headStyle = workbook.CreateCellStyle() as HSSFCellStyle;
                         headStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                         HSSFFont font = workbook.CreateFont() as HSSFFont;
                         font.FontHeightInPoints = 20;
                         font.Boldweight = 700;
                         headStyle.SetFont(font);
 
                         headerRow.GetCell(0).CellStyle = headStyle;
 
                         sheet.AddMergedRegion(new NPOI.SS.Util.Region(0, 0, 0, dtSource.Columns.Count - 1));
                         //headerRow.Dispose();
                     }
 
                     #endregion
 
 
                     #region 列头及样式
 
                     {
                         HSSFRow headerRow = sheet.CreateRow(1) as HSSFRow;
 
 
                         HSSFCellStyle headStyle = workbook.CreateCellStyle() as HSSFCellStyle;
                         headStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                         HSSFFont font = workbook.CreateFont() as HSSFFont;
                         font.FontHeightInPoints = 10;
                         font.Boldweight = 700;
                         headStyle.SetFont(font);
 
 
                         foreach (DataColumn column in dtSource.Columns)
                         {
                             headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                             headerRow.GetCell(column.Ordinal).CellStyle = headStyle;
 
                             //设置列宽
                             sheet.SetColumnWidth(column.Ordinal, (arrColWidth[column.Ordinal] + 1) * 256);
 
                         }
                         //headerRow.Dispose();
                     }
 
                     #endregion
 
                     rowIndex = 2;
                 }
 
                 #endregion
 
                 #region 填充内容
 
                 HSSFRow dataRow = sheet.CreateRow(rowIndex) as HSSFRow;
                 foreach (DataColumn column in dtSource.Columns)
                 {
                     HSSFCell newCell = dataRow.CreateCell(column.Ordinal) as HSSFCell;
 
                     string drValue = row[column].ToString();
 
                     switch (column.DataType.ToString())
                     {
                         case "System.String": //字符串类型
                             double result;
                             if (isNumeric(drValue, out result))
                             {
 
                                 double.TryParse(drValue, out result);
                                 newCell.SetCellValue(result);
                                 break;
                             }
                             else
                             {
                                 newCell.SetCellValue(drValue);
                                 break;
                             }
 
                         case "System.DateTime": //日期类型
                             DateTime dateV;
                             DateTime.TryParse(drValue, out dateV);
                             newCell.SetCellValue(dateV);
 
                             newCell.CellStyle = dateStyle; //格式化显示
                             break;
                         case "System.Boolean": //布尔型
                             bool boolV = false;
                             bool.TryParse(drValue, out boolV);
                             newCell.SetCellValue(boolV);
                             break;
                         case "System.Int16": //整型
                         case "System.Int32":
                         case "System.Int64":
                         case "System.Byte":
                             int intV = 0;
                             int.TryParse(drValue, out intV);
                             newCell.SetCellValue(intV);
                             break;
                         case "System.Decimal": //浮点型
                         case "System.Double":
                            double doubV = 0;
                             double.TryParse(drValue, out doubV);
                             newCell.SetCellValue(doubV);
                             break;
                         case "System.DBNull": //空值处理
                             newCell.SetCellValue("");
                             break;
                         default:
                             newCell.SetCellValue("");
                             break;
                     }
 
                 }
 
                 #endregion
 
                 rowIndex++;
             }
             using (MemoryStream ms = new MemoryStream())
             {
                 workbook.Write(ms);
                 ms.Flush();
                 ms.Position = 0;
 
                 //sheet.Dispose();
                 //workbook.Dispose();
 
                 return ms;
             }
         }
 
         /// <summary>
         /// DataTable导出到Excel的MemoryStream
         /// </summary>
         /// <param name="dtSource">源DataTable</param>
         /// <param name="strHeaderText">表头文本</param>
         static void ExportDTI(DataTable dtSource, string strHeaderText, FileStream fs)
         {
             XSSFWorkbook workbook = new XSSFWorkbook();
             XSSFSheet sheet = workbook.CreateSheet() as XSSFSheet;
 
             #region 右击文件 属性信息
 
             //{
             //    DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
             //    dsi.Company = "http://www.yongfa365.com/";
             //    workbook.DocumentSummaryInformation = dsi;
 
            //    SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            //    si.Author = "柳永法"; //填加xls文件作者信息
            //    si.ApplicationName = "NPOI测试程序"; //填加xls文件创建程序信息
            //    si.LastAuthor = "柳永法2"; //填加xls文件最后保存者信息
            //    si.Comments = "说明信息"; //填加xls文件作者信息
            //    si.Title = "NPOI测试"; //填加xls文件标题信息
            //    si.Subject = "NPOI测试Demo"; //填加文件主题信息
             //    si.CreateDateTime = DateTime.Now;
             //    workbook.SummaryInformation = si;
             //}
 
             #endregion
 
             XSSFCellStyle dateStyle = workbook.CreateCellStyle() as XSSFCellStyle;
             XSSFDataFormat format = workbook.CreateDataFormat() as XSSFDataFormat;
             dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");
 
             //取得列宽
             int[] arrColWidth = new int[dtSource.Columns.Count];
             foreach (DataColumn item in dtSource.Columns)
             {
                 arrColWidth[item.Ordinal] = Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;
             }
             for (int i = 0; i < dtSource.Rows.Count; i++)
             {
                 for (int j = 0; j < dtSource.Columns.Count; j++)
                 {
                     int intTemp = Encoding.GetEncoding(936).GetBytes(dtSource.Rows[i][j].ToString()).Length;
                     if (intTemp > arrColWidth[j])
                     {
                         arrColWidth[j] = intTemp;
                     }
                 }
             }
             int rowIndex = 0;
 
             foreach (DataRow row in dtSource.Rows)
             {
                 #region 新建表，填充表头，填充列头，样式
 
                 if (rowIndex == 0)
                 {
                     #region 表头及样式
                     //{
                     //    XSSFRow headerRow = sheet.CreateRow(0) as XSSFRow;
                     //    headerRow.HeightInPoints = 25;
                     //    headerRow.CreateCell(0).SetCellValue(strHeaderText);
 
                     //    XSSFCellStyle headStyle = workbook.CreateCellStyle() as XSSFCellStyle;
                     //    headStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER;
                     //    XSSFFont font = workbook.CreateFont() as XSSFFont;
                     //    font.FontHeightInPoints = 20;
                     //    font.Boldweight = 700;
                     //    headStyle.SetFont(font);
 
                     //    headerRow.GetCell(0).CellStyle = headStyle;
 
                     //    //sheet.AddMergedRegion(new Region(0, 0, 0, dtSource.Columns.Count - 1));
                     //    //headerRow.Dispose();
                     //}
 
                     #endregion
 
 
                     #region 列头及样式
 
                     {
                         XSSFRow headerRow = sheet.CreateRow(0) as XSSFRow;
 
 
                         XSSFCellStyle headStyle = workbook.CreateCellStyle() as XSSFCellStyle;
                         headStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                         XSSFFont font = workbook.CreateFont() as XSSFFont;
                         font.FontHeightInPoints = 10;
                         font.Boldweight = 700;
                         headStyle.SetFont(font);
 
 
                         foreach (DataColumn column in dtSource.Columns)
                         {
                             headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                             headerRow.GetCell(column.Ordinal).CellStyle = headStyle;
 
                             //设置列宽
                             sheet.SetColumnWidth(column.Ordinal, (arrColWidth[column.Ordinal] + 1) * 256);
 
                         }
                         //headerRow.Dispose();
                     }
 
                     #endregion
 
                     rowIndex = 1;
                 }
 
                 #endregion
 
                 #region 填充内容
 
                 XSSFRow dataRow = sheet.CreateRow(rowIndex) as XSSFRow;
                 foreach (DataColumn column in dtSource.Columns)
                 {
                     XSSFCell newCell = dataRow.CreateCell(column.Ordinal) as XSSFCell;
 
                     string drValue = row[column].ToString();
 
                     switch (column.DataType.ToString())
                     {
                         case "System.String": //字符串类型
                            double result;
                             if (isNumeric(drValue, out result))
                             {
 
                                 double.TryParse(drValue, out result);
                                 newCell.SetCellValue(result);
                                 break;
                             }
                             else
                             {
                                 newCell.SetCellValue(drValue);
                                 break;
                             }
 
                         case "System.DateTime": //日期类型
                             DateTime dateV;
                             DateTime.TryParse(drValue, out dateV);
                             newCell.SetCellValue(dateV);
 
                             newCell.CellStyle = dateStyle; //格式化显示
                             break;
                         case "System.Boolean": //布尔型
                             bool boolV = false;
                             bool.TryParse(drValue, out boolV);
                             newCell.SetCellValue(boolV);
                             break;
                         case "System.Int16": //整型
                         case "System.Int32":
                         case "System.Int64":
                         case "System.Byte":
                             int intV = 0;
                             int.TryParse(drValue, out intV);
                             newCell.SetCellValue(intV);
                             break;
                         case "System.Decimal": //浮点型
                         case "System.Double":
                             double doubV = 0;
                             double.TryParse(drValue, out doubV);
                             newCell.SetCellValue(doubV);
                             break;
                         case "System.DBNull": //空值处理
                             newCell.SetCellValue("");
                             break;
                         default:
                             newCell.SetCellValue("");
                             break;
                     }
 
                 }
 
                #endregion
 
                 rowIndex++;
             }
             workbook.Write(fs);
             fs.Close();
         }
 
         /// <summary>
         /// DataTable导出到Excel文件
         /// </summary>
         /// <param name="dtSource">源DataTable</param>
         /// <param name="strHeaderText">表头文本</param>
         /// <param name="strFileName">保存位置</param>
         public static void ExportDTtoExcel(DataTable dtSource, string strHeaderText, string strFileName)
         {
             string[] temp = strFileName.Split('.');
 
             if (temp[temp.Length - 1] == "xls" && dtSource.Columns.Count < 256 && dtSource.Rows.Count < 65536)
             {
                 using (MemoryStream ms = ExportDT(dtSource, strHeaderText))
                 {
                     using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                     {
                         byte[] data = ms.ToArray();
                         fs.Write(data, 0, data.Length);
                         fs.Flush();
                     }
                 }
             }
             else
             {
                 if (temp[temp.Length - 1] == "xls")
                     strFileName = strFileName + "x";
 
                 using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                 {
                     ExportDTI(dtSource, strHeaderText, fs);
                 }
             }
         }
         #endregion
 
         #region 从excel中将数据导出到datatable
         /// <summary>
         /// 读取excel 默认第一行为标头
         /// </summary>
         /// <param name="strFileName">excel文档路径</param>
         /// <returns></returns>
         public static DataTable ImportExceltoDt(string strFileName)
         {
            DataTable dt = new DataTable();
             IWorkbook wb;
             using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
             {
                 wb = WorkbookFactory.Create(file);
             }
             ISheet sheet = wb.GetSheetAt(0);
             dt = ImportDt(sheet, 0, true);
             return dt;
         }
 
         /// <summary>
         /// 读取Excel流到DataTable
         /// </summary>
         /// <param name="stream">Excel流</param>
         /// <returns>第一个sheet中的数据</returns>
         public static DataTable ImportExceltoDt(Stream stream)
         {
             try
             {
                 DataTable dt = new DataTable();
                 IWorkbook wb;
                 using (stream)
                 {
                     wb = WorkbookFactory.Create(stream);
                 }
                 ISheet sheet = wb.GetSheetAt(0);
                 dt = ImportDt(sheet, 0, true);
                 return dt;
             }
             catch (Exception)
             {
 
                 throw;
             }
         }
 
         /// <summary>
         /// 读取Excel流到DataTable
         /// </summary>
         /// <param name="stream">Excel流</param>
         /// <param name="sheetName">表单名</param>
         /// <param name="HeaderRowIndex">列头所在行号，-1表示没有列头</param>
         /// <returns>指定sheet中的数据</returns>
         public static DataTable ImportExceltoDt(Stream stream, string sheetName, int HeaderRowIndex)
         {
             try
             {
                 DataTable dt = new DataTable();
                 IWorkbook wb;
                 using (stream)
                 {
                     wb = WorkbookFactory.Create(stream);
                 }
                 ISheet sheet = wb.GetSheet(sheetName);
                 dt = ImportDt(sheet, HeaderRowIndex, true);
                 return dt;
             }
             catch (Exception)
             {
 
                 throw;
             }
         }
 
         /// <summary>
         /// 读取Excel流到DataSet
         /// </summary>
         /// <param name="stream">Excel流</param>
         /// <returns>Excel中的数据</returns>
         public static DataSet ImportExceltoDs(Stream stream)
         {
             try
             {
                 DataSet ds = new DataSet();
                 IWorkbook wb;
                 using (stream)
                 {
                     wb = WorkbookFactory.Create(stream);
                 }
                 for (int i = 0; i < wb.NumberOfSheets; i++)
                 {
                     DataTable dt = new DataTable();
                     ISheet sheet = wb.GetSheetAt(i);
                     dt = ImportDt(sheet, 0, true);
                     ds.Tables.Add(dt);
                 }
                 return ds;
             }
             catch (Exception)
             {
 
                 throw;
             }
         }
 
         /// <summary>
         /// 读取Excel流到DataSet
         /// </summary>
         /// <param name="stream">Excel流</param>
         /// <param name="dict">字典参数，key：sheet名，value：列头所在行号，-1表示没有列头</param>
         /// <returns>Excel中的数据</returns>
         public static DataSet ImportExceltoDs(Stream stream,Dictionary<string,int> dict)
         {
             try
             {
                 DataSet ds = new DataSet();
                 IWorkbook wb;
                 using (stream)
                 {
                     wb = WorkbookFactory.Create(stream);
                 }
                 foreach (string key in dict.Keys)
                 {
                     DataTable dt = new DataTable();
                     ISheet sheet = wb.GetSheet(key);
                     dt = ImportDt(sheet, dict[key], true);
                     ds.Tables.Add(dt);
                 }
                 return ds;
             }
             catch (Exception)
             {
 
                 throw;
             }
         }
 
         /// <summary>
         /// 读取excel
         /// </summary>
         /// <param name="strFileName">excel文件路径</param>
         /// <param name="sheet">需要导出的sheet</param>
         /// <param name="HeaderRowIndex">列头所在行号，-1表示没有列头</param>
         /// <returns></returns>
         public static DataTable ImportExceltoDt(string strFileName, string SheetName, int HeaderRowIndex)
         {
             HSSFWorkbook workbook;
             IWorkbook wb;
             using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
             {
                 wb = new HSSFWorkbook(file);
             }
             ISheet sheet = wb.GetSheet(SheetName);
             DataTable table = new DataTable();
             table = ImportDt(sheet, HeaderRowIndex, true);
             //ExcelFileStream.Close();
             workbook = null;
             sheet = null;
             return table;
         }
 
         /// <summary>
         /// 读取excel
         /// </summary>
         /// <param name="strFileName">excel文件路径</param>
         /// <param name="sheet">需要导出的sheet序号</param>
         /// <param name="HeaderRowIndex">列头所在行号，-1表示没有列头</param>
         /// <returns></returns>
         public static DataTable ImportExceltoDt(string strFileName, int SheetIndex, int HeaderRowIndex)
         {
             HSSFWorkbook workbook;
             IWorkbook wb;
             using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
             {
                 wb = WorkbookFactory.Create(file);
             }
             ISheet isheet = wb.GetSheetAt(SheetIndex);
             DataTable table = new DataTable();
             table = ImportDt(isheet, HeaderRowIndex, true);
             //ExcelFileStream.Close();
             workbook = null;
             isheet = null;
             return table;
         }
 
         /// <summary>
         /// 读取excel
         /// </summary>
         /// <param name="strFileName">excel文件路径</param>
         /// <param name="sheet">需要导出的sheet</param>
         /// <param name="HeaderRowIndex">列头所在行号，-1表示没有列头</param>
         /// <returns></returns>
         public static DataTable ImportExceltoDt(string strFileName, string SheetName, int HeaderRowIndex, bool needHeader)
         {
             HSSFWorkbook workbook;
             IWorkbook wb;
             using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
             {
                 wb = WorkbookFactory.Create(file);
             }
             ISheet sheet = wb.GetSheet(SheetName);
             DataTable table = new DataTable();
             table = ImportDt(sheet, HeaderRowIndex, needHeader);
             //ExcelFileStream.Close();
             workbook = null;
             sheet = null;
             return table;
         }
 
         /// <summary>
         /// 读取excel
         /// </summary>
         /// <param name="strFileName">excel文件路径</param>
         /// <param name="sheet">需要导出的sheet序号</param>
         /// <param name="HeaderRowIndex">列头所在行号，-1表示没有列头</param>
         /// <returns></returns>
         public static DataTable ImportExceltoDt(string strFileName, int SheetIndex, int HeaderRowIndex, bool needHeader)
         {
             HSSFWorkbook workbook;
             IWorkbook wb;
             using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
             {
                 wb = WorkbookFactory.Create(file);
             }
             ISheet sheet = wb.GetSheetAt(SheetIndex);
             DataTable table = new DataTable();
             table = ImportDt(sheet, HeaderRowIndex, needHeader);
             //ExcelFileStream.Close();
             workbook = null;
             sheet = null;
             return table;
         }
 
         /// <summary>
         /// 将制定sheet中的数据导出到datatable中
         /// </summary>
         /// <param name="sheet">需要导出的sheet</param>
         /// <param name="HeaderRowIndex">列头所在行号，-1表示没有列头</param>
         /// <returns></returns>
         static DataTable ImportDt(ISheet sheet, int HeaderRowIndex, bool needHeader)
         {
             DataTable table = new DataTable();
             IRow headerRow;
             int cellCount;
             try
             {
                 if (HeaderRowIndex < 0 || !needHeader)
                 {
                     headerRow = sheet.GetRow(0);
                     cellCount = headerRow.LastCellNum;
 
                     for (int i = headerRow.FirstCellNum; i <= cellCount; i++)
                     {
                         DataColumn column = new DataColumn(Convert.ToString(i));
                         table.Columns.Add(column);
                     }
                 }
                 else
                 {
                     headerRow = sheet.GetRow(HeaderRowIndex);
                     cellCount = headerRow.LastCellNum;
 
                     for (int i = headerRow.FirstCellNum; i <= cellCount; i++)
                     {
                         if (headerRow.GetCell(i) == null)
                         {
                             if (table.Columns.IndexOf(Convert.ToString(i)) > 0)
                             {
                                 DataColumn column = new DataColumn(Convert.ToString("重复列名" + i));
                                 table.Columns.Add(column);
                             }
                             else
                             {
                                 DataColumn column = new DataColumn(Convert.ToString(i));
                                 table.Columns.Add(column);
                             }
 
                         }
                         else if (table.Columns.IndexOf(headerRow.GetCell(i).ToString()) > 0)
                         {
                             DataColumn column = new DataColumn(Convert.ToString("重复列名" + i));
                             table.Columns.Add(column);
                         }
                         else
                         {
                             DataColumn column = new DataColumn(headerRow.GetCell(i).ToString());
                             table.Columns.Add(column);
                         }
                     }
                 }
                 int rowCount = sheet.LastRowNum;
                 for (int i = (HeaderRowIndex + 1); i <= sheet.LastRowNum; i++)
                 {
                     try
                     {
                         IRow row;
                         if (sheet.GetRow(i) == null)
                         {
                             row = sheet.CreateRow(i);
                         }
                         else
                         {
                             row = sheet.GetRow(i);
                         }
 
                         DataRow dataRow = table.NewRow();

                         for (int j = row.FirstCellNum; j <= cellCount; j++)
                         {
                             try
                             {
                                 if (row.GetCell(j) != null)
                                 {
                                     switch (row.GetCell(j).CellType)
                                     {
                                         case CellType.String:
                                             string str = row.GetCell(j).StringCellValue;
                                             if (str != null && str.Length > 0)
                                             {
                                                 dataRow[j] = str.ToString();
                                             }
                                             else
                                             {
                                                 dataRow[j] = null;
                                             }
                                             break;
                                         case CellType.Numeric:
                                             if (DateUtil.IsCellDateFormatted(row.GetCell(j)))
                                             {
                                                 dataRow[j] = DateTime.FromOADate(row.GetCell(j).NumericCellValue);
                                             }
                                             else
                                             {
                                                 dataRow[j] = Convert.ToDouble(row.GetCell(j).NumericCellValue);
                                             }
                                             break;
                                         case CellType.Boolean:
                                             dataRow[j] = Convert.ToString(row.GetCell(j).BooleanCellValue);
                                             break;
                                         case CellType.Error:
                                             dataRow[j] = ErrorEval.GetText(row.GetCell(j).ErrorCellValue);
                                             break;
                                         case CellType.Formula:
                                             switch (row.GetCell(j).CachedFormulaResultType)
                                             {
                                                 case CellType.String:
                                                     string strFORMULA = row.GetCell(j).StringCellValue;
                                                     if (strFORMULA != null && strFORMULA.Length > 0)
                                                     {
                                                         dataRow[j] = strFORMULA.ToString();
                                                     }
                                                     else
                                                     {
                                                         dataRow[j] = null;
                                                     }
                                                     break;
                                                 case CellType.Numeric:
                                                     dataRow[j] = Convert.ToString(row.GetCell(j).NumericCellValue);
                                                     break;
                                                 case CellType.Boolean:
                                                     dataRow[j] = Convert.ToString(row.GetCell(j).BooleanCellValue);
                                                     break;
                                                 case CellType.Error:
                                                     dataRow[j] = ErrorEval.GetText(row.GetCell(j).ErrorCellValue);
                                                     break;
                                                 default:
                                                     dataRow[j] = "";
                                                     break;
                                             }
                                             break;
                                         default:
                                             dataRow[j] = "";
                                             break;
                                     }
                                 }
                             }
                             catch (Exception exception)
                             {
                                 //wl.WriteLogs(exception.ToString());
                             }
                         }
                         table.Rows.Add(dataRow);
                     }
                     catch (Exception exception)
                     {
                         //wl.WriteLogs(exception.ToString());
                     }
                 }
             }
             catch (Exception exception)
             {
                 //wl.WriteLogs(exception.ToString());
             }
             return table;
         }
 
         #endregion
 
 
         public static void InsertSheet(string outputFile, string sheetname, DataTable dt)
         {
             FileStream readfile = new FileStream(outputFile, FileMode.Open, FileAccess.Read);
             IWorkbook hssfworkbook = WorkbookFactory.Create(readfile);
             //HSSFWorkbook hssfworkbook = new HSSFWorkbook(readfile);
             int num = hssfworkbook.GetSheetIndex(sheetname);
             ISheet sheet1;
             if (num >= 0)
                 sheet1 = hssfworkbook.GetSheet(sheetname);
             else
             {
                 sheet1 = hssfworkbook.CreateSheet(sheetname);
             }
 
 
             try
             {
                 if (sheet1.GetRow(0) == null)
                 {
                     sheet1.CreateRow(0);
                 }
                 for (int coluid = 0; coluid < dt.Columns.Count; coluid++)
                 {
                     if (sheet1.GetRow(0).GetCell(coluid) == null)
                     {
                         sheet1.GetRow(0).CreateCell(coluid);
                     }
 
                     sheet1.GetRow(0).GetCell(coluid).SetCellValue(dt.Columns[coluid].ColumnName);
                 }
             }
             catch (Exception ex)
             {
                 //wl.WriteLogs(ex.ToString());
                 throw;
             }
 
 
             for (int i = 1; i <= dt.Rows.Count; i++)
             {
                 try
                 {
                     if (sheet1.GetRow(i) == null)
                     {
                         sheet1.CreateRow(i);
                     }
                     for (int coluid = 0; coluid < dt.Columns.Count; coluid++)
                     {
                         if (sheet1.GetRow(i).GetCell(coluid) == null)
                         {
                             sheet1.GetRow(i).CreateCell(coluid);
                         }
 
                         sheet1.GetRow(i).GetCell(coluid).SetCellValue(dt.Rows[i - 1][coluid].ToString());
                     }
                 }
                 catch (Exception ex)
                 {
                     //wl.WriteLogs(ex.ToString());
                     //throw;
                 }
             }
             try
             {
                 readfile.Close();
 
                 FileStream writefile = new FileStream(outputFile, FileMode.OpenOrCreate, FileAccess.Write);
                 hssfworkbook.Write(writefile);
                 writefile.Close();
             }
             catch (Exception ex)
             {
                 //wl.WriteLogs(ex.ToString());
             }
         }
 
         #region 更新excel中的数据
         /// <summary>
         /// 更新Excel表格
         /// </summary>
         /// <param name="outputFile">需更新的excel表格路径</param>
         /// <param name="sheetname">sheet名</param>
         /// <param name="updateData">需更新的数据</param>
         /// <param name="coluid">需更新的列号</param>
         /// <param name="rowid">需更新的开始行号</param>
         public static void UpdateExcel(string outputFile, string sheetname, string[] updateData, int coluid, int rowid)
         {
             //FileStream readfile = new FileStream(outputFile, FileMode.Open, FileAccess.Read);
             IWorkbook hssfworkbook = null;// WorkbookFactory.Create(outputFile);
             //HSSFWorkbook hssfworkbook = new HSSFWorkbook(readfile);
             ISheet sheet1 = hssfworkbook.GetSheet(sheetname);
             for (int i = 0; i < updateData.Length; i++)
             {
                 try
                 {
                     if (sheet1.GetRow(i + rowid) == null)
                     {
                         sheet1.CreateRow(i + rowid);
                     }
                     if (sheet1.GetRow(i + rowid).GetCell(coluid) == null)
                     {
                         sheet1.GetRow(i + rowid).CreateCell(coluid);
                     }
 
                     sheet1.GetRow(i + rowid).GetCell(coluid).SetCellValue(updateData[i]);
                 }
                 catch (Exception ex)
                 {
                     //wl.WriteLogs(ex.ToString());
                     throw;
                 }
             }
             try
             {
                 //readfile.Close();
                 FileStream writefile = new FileStream(outputFile, FileMode.OpenOrCreate, FileAccess.Write);
                 hssfworkbook.Write(writefile);
                 writefile.Close();
             }
             catch (Exception ex)
             {
                 //wl.WriteLogs(ex.ToString());
             }
 
         }
 
         /// <summary>
         /// 更新Excel表格
         /// </summary>
         /// <param name="outputFile">需更新的excel表格路径</param>
         /// <param name="sheetname">sheet名</param>
         /// <param name="updateData">需更新的数据</param>
         /// <param name="coluids">需更新的列号</param>
         /// <param name="rowid">需更新的开始行号</param>
         public static void UpdateExcel(string outputFile, string sheetname, string[][] updateData, int[] coluids, int rowid)
         {
             FileStream readfile = new FileStream(outputFile, FileMode.Open, FileAccess.Read);
 
             HSSFWorkbook hssfworkbook = new HSSFWorkbook(readfile);
             readfile.Close();
             ISheet sheet1 = hssfworkbook.GetSheet(sheetname);
             for (int j = 0; j < coluids.Length; j++)
             {
                 for (int i = 0; i < updateData[j].Length; i++)
                 {
                     try
                     {
                         if (sheet1.GetRow(i + rowid) == null)
                         {
                             sheet1.CreateRow(i + rowid);
                         }
                         if (sheet1.GetRow(i + rowid).GetCell(coluids[j]) == null)
                         {
                            sheet1.GetRow(i + rowid).CreateCell(coluids[j]);
                         }
                         sheet1.GetRow(i + rowid).GetCell(coluids[j]).SetCellValue(updateData[j][i]);
                     }
                     catch (Exception ex)
                     {
                         //wl.WriteLogs(ex.ToString());
                     }
                 }
             }
             try
             {
                 FileStream writefile = new FileStream(outputFile, FileMode.Create);
                 hssfworkbook.Write(writefile);
                 writefile.Close();
             }
             catch (Exception ex)
             {
                 //wl.WriteLogs(ex.ToString());
             }
         }
 
         /// <summary>
         /// 更新Excel表格
         /// </summary>
         /// <param name="outputFile">需更新的excel表格路径</param>
         /// <param name="sheetname">sheet名</param>
         /// <param name="updateData">需更新的数据</param>
         /// <param name="coluid">需更新的列号</param>
         /// <param name="rowid">需更新的开始行号</param>
         public static void UpdateExcel(string outputFile, string sheetname, double[] updateData, int coluid, int rowid)
         {
             FileStream readfile = new FileStream(outputFile, FileMode.Open, FileAccess.Read);
 
             HSSFWorkbook hssfworkbook = new HSSFWorkbook(readfile);
             ISheet sheet1 = hssfworkbook.GetSheet(sheetname);
             for (int i = 0; i < updateData.Length; i++)
             {
                 try
                 {
                     if (sheet1.GetRow(i + rowid) == null)
                     {
                         sheet1.CreateRow(i + rowid);
                     }
                     if (sheet1.GetRow(i + rowid).GetCell(coluid) == null)
                     {
                         sheet1.GetRow(i + rowid).CreateCell(coluid);
                     }
 
                     sheet1.GetRow(i + rowid).GetCell(coluid).SetCellValue(updateData[i]);
                 }
                 catch (Exception ex)
                 {
                     //wl.WriteLogs(ex.ToString());
                     throw;
                 }
             }
             try
             {
                 readfile.Close();
                 FileStream writefile = new FileStream(outputFile, FileMode.Create, FileAccess.Write);
                 hssfworkbook.Write(writefile);
                 writefile.Close();
             }
             catch (Exception ex)
             {
                 //wl.WriteLogs(ex.ToString());
             }
 
         }
 
         /// <summary>
         /// 更新Excel表格
         /// </summary>
         /// <param name="outputFile">需更新的excel表格路径</param>
         /// <param name="sheetname">sheet名</param>
         /// <param name="updateData">需更新的数据</param>
         /// <param name="coluids">需更新的列号</param>
         /// <param name="rowid">需更新的开始行号</param>
         public static void UpdateExcel(string outputFile, string sheetname, double[][] updateData, int[] coluids, int rowid)
         {
             FileStream readfile = new FileStream(outputFile, FileMode.Open, FileAccess.Read);
 
             HSSFWorkbook hssfworkbook = new HSSFWorkbook(readfile);
             readfile.Close();
             ISheet sheet1 = hssfworkbook.GetSheet(sheetname);
             for (int j = 0; j < coluids.Length; j++)
             {
                 for (int i = 0; i < updateData[j].Length; i++)
                 {
                     try
                     {
                         if (sheet1.GetRow(i + rowid) == null)
                         {
                             sheet1.CreateRow(i + rowid);
                         }
                         if (sheet1.GetRow(i + rowid).GetCell(coluids[j]) == null)
                         {
                             sheet1.GetRow(i + rowid).CreateCell(coluids[j]);
                         }
                         sheet1.GetRow(i + rowid).GetCell(coluids[j]).SetCellValue(updateData[j][i]);
                     }
                     catch (Exception ex)
                     {
                         //wl.WriteLogs(ex.ToString());
                     }
                 }
             }
             try
             {
                 FileStream writefile = new FileStream(outputFile, FileMode.Create);
                 hssfworkbook.Write(writefile);
                 writefile.Close();
             }
             catch (Exception ex)
             {
                 //wl.WriteLogs(ex.ToString());
             }
         }
 
         #endregion
 
         public static int GetSheetNumber(string outputFile)
         {
             int number = 0;
             try
             {
                 FileStream readfile = new FileStream(outputFile, FileMode.Open, FileAccess.Read);
 
                 HSSFWorkbook hssfworkbook = new HSSFWorkbook(readfile);
                 number = hssfworkbook.NumberOfSheets;
 
             }
             catch (Exception exception)
             {
                 //wl.WriteLogs(exception.ToString());
             }
             return number;
         }
 
         public static ArrayList GetSheetName(string outputFile)
         {
             ArrayList arrayList = new ArrayList();
             try
             {
                 FileStream readfile = new FileStream(outputFile, FileMode.Open, FileAccess.Read);
 
                 HSSFWorkbook hssfworkbook = new HSSFWorkbook(readfile);
                 for (int i = 0; i < hssfworkbook.NumberOfSheets; i++)
                 {
                     arrayList.Add(hssfworkbook.GetSheetName(i));
                 }
             }
             catch (Exception exception)
             {
                 //wl.WriteLogs(exception.ToString());
             }
             return arrayList;
         }
 
         public static bool isNumeric(String message, out double result)
         {
             Regex rex = new Regex(@"^[-]?\d+[.]?\d*$");
             result = -1;
             if (rex.IsMatch(message))
             {
                 result = double.Parse(message);
                 return true;
             }
             else
                 return false;
 
         }
 
 
 
         //////////  现用导出  \\\\\\\\\\  
         /// <summary>
         /// 用于Web导出                                                                                             第一步
         /// </summary>
         /// <param name="dtSource">源DataTable</param>
         /// <param name="strHeaderText">表头文本</param>
         /// <param name="strFileName">文件名</param>
         public static void ExportByWeb(DataTable dtSource, string strHeaderText, string strFileName)
         {
             HttpContext curContext = HttpContext.Current;
 
             // 设置编码和附件格式
             curContext.Response.ContentType = "application/vnd.ms-excel";
             curContext.Response.ContentEncoding = Encoding.UTF8;
             curContext.Response.Charset = "";
             curContext.Response.AppendHeader("Content-Disposition",
             "attachment;filename=" + HttpUtility.UrlEncode(strFileName, Encoding.UTF8));
 
             curContext.Response.BinaryWrite(Export(dtSource, strHeaderText).GetBuffer());
             curContext.Response.End();
         }
 
 
 
         /// <summary>
         /// DataTable导出到Excel的MemoryStream                                                                      第二步
         /// </summary>
         /// <param name="dtSource">源DataTable</param>
         /// <param name="strHeaderText">表头文本</param>
         public static MemoryStream Export(DataTable dtSource, string strHeaderText)
         {
             HSSFWorkbook workbook = new HSSFWorkbook();
             HSSFSheet sheet = workbook.CreateSheet() as HSSFSheet;
 
             #region 右击文件 属性信息
             {
                 DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                 dsi.Company = "NPOI";
                 workbook.DocumentSummaryInformation = dsi;
 
                 SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                 si.Author = "文件作者信息"; //填加xls文件作者信息
                 si.ApplicationName = "创建程序信息"; //填加xls文件创建程序信息
                 si.LastAuthor = "最后保存者信息"; //填加xls文件最后保存者信息
                 si.Comments = "作者信息"; //填加xls文件作者信息
                 si.Title = "标题信息"; //填加xls文件标题信息
                 si.Subject = "主题信息";//填加文件主题信息
 
                 si.CreateDateTime = DateTime.Now;
                 workbook.SummaryInformation = si;
             }
             #endregion
 
             HSSFCellStyle dateStyle = workbook.CreateCellStyle() as HSSFCellStyle;
             HSSFDataFormat format = workbook.CreateDataFormat() as HSSFDataFormat;
             dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");
 
             //取得列宽
             int[] arrColWidth = new int[dtSource.Columns.Count];
             foreach (DataColumn item in dtSource.Columns)
             {
                 arrColWidth[item.Ordinal] = Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;
             }
             for (int i = 0; i < dtSource.Rows.Count; i++)
             {
                 for (int j = 0; j < dtSource.Columns.Count; j++)
                 {
                     int intTemp = Encoding.GetEncoding(936).GetBytes(dtSource.Rows[i][j].ToString()).Length;
                     if (intTemp > arrColWidth[j])
                     {
                         arrColWidth[j] = intTemp;
                     }
                 }
             }
             int rowIndex = 0;
             foreach (DataRow row in dtSource.Rows)
             {
                 #region 新建表，填充表头，填充列头，样式
                 if (rowIndex == 65535 || rowIndex == 0)
                 {
                     if (rowIndex != 0)
                     {
                         sheet = workbook.CreateSheet() as HSSFSheet;
                     }
 
                     #region 表头及样式
                     {
                         if (string.IsNullOrEmpty(strHeaderText))
                         {
                             HSSFRow headerRow = sheet.CreateRow(0) as HSSFRow;
                             headerRow.HeightInPoints = 25;
                             headerRow.CreateCell(0).SetCellValue(strHeaderText);
                             HSSFCellStyle headStyle = workbook.CreateCellStyle() as HSSFCellStyle;
                             //headStyle.Alignment = CellHorizontalAlignment.CENTER;
                             HSSFFont font = workbook.CreateFont() as HSSFFont;
                             font.FontHeightInPoints = 20;
                             font.Boldweight = 700;
                             headStyle.SetFont(font);
                             headerRow.GetCell(0).CellStyle = headStyle;
                             sheet.AddMergedRegion(new NPOI.SS.Util.Region(0, 0, 0, dtSource.Columns.Count - 1));
                             //headerRow.Dispose();
                         }
                     }
                     #endregion
 
                     #region 列头及样式
                     {
                         HSSFRow headerRow = sheet.CreateRow(0) as HSSFRow;
                         HSSFCellStyle headStyle = workbook.CreateCellStyle() as HSSFCellStyle;
                         //headStyle.Alignment = CellHorizontalAlignment.CENTER;
                         HSSFFont font = workbook.CreateFont() as HSSFFont;
                         font.FontHeightInPoints = 10;
                         font.Boldweight = 700;
                         headStyle.SetFont(font);
                         foreach (DataColumn column in dtSource.Columns)
                         {
                             headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                             headerRow.GetCell(column.Ordinal).CellStyle = headStyle;
 
                             //设置列宽
                             sheet.SetColumnWidth(column.Ordinal, (arrColWidth[column.Ordinal] + 1) * 256);
                         }
                         //headerRow.Dispose();
                     }
                     #endregion
 
                     rowIndex = 1;
                 }
                 #endregion
 
 
                 #region 填充内容
                 HSSFRow dataRow = sheet.CreateRow(rowIndex) as HSSFRow;
                 foreach (DataColumn column in dtSource.Columns)
                 {
                     HSSFCell newCell = dataRow.CreateCell(column.Ordinal) as HSSFCell;
 
                     string drValue = row[column].ToString();
 
                     switch (column.DataType.ToString())
                     {
                         case "System.String"://字符串类型
                             newCell.SetCellValue(drValue);
                             break;
                         case "System.DateTime"://日期类型
                             DateTime dateV;
                             DateTime.TryParse(drValue, out dateV);
                             newCell.SetCellValue(dateV);
 
                             newCell.CellStyle = dateStyle;//格式化显示
                             break;
                         case "System.Boolean"://布尔型
                             bool boolV = false;
                             bool.TryParse(drValue, out boolV);
                             newCell.SetCellValue(boolV);
                             break;
                         case "System.Int16"://整型
                         case "System.Int32":
                         case "System.Int64":
                         case "System.Byte":
                             int intV = 0;
                             int.TryParse(drValue, out intV);
                             newCell.SetCellValue(intV);
                             break;
                         case "System.Decimal"://浮点型
                         case "System.Double":
                             double doubV = 0;
                             double.TryParse(drValue, out doubV);
                             newCell.SetCellValue(doubV);
                             break;
                         case "System.DBNull"://空值处理
                             newCell.SetCellValue("");
                             break;
                         default:
                             newCell.SetCellValue("");
                             break;
                     }
                 }
                 #endregion
 
                 rowIndex++;
             }
             using (MemoryStream ms = new MemoryStream())
             {
                 workbook.Write(ms);
                 ms.Flush();
                 ms.Position = 0;
 
                 //sheet.Dispose();
                 //workbook.Dispose();//一般只用写这一个就OK了，他会遍历并释放所有资源，但当前版本有问题所以只释放sheet
                 return ms;
             }
         }
 
         /// <summary>
         /// /注：分浏览器进行编码（IE必须编码，FireFox不能编码，Chrome可编码也可不编码）
         /// </summary>
         /// <param name="ds"></param>
         /// <param name="strHeaderText"></param>
         /// <param name="strFileName"></param>
         public static void ExportByWeb(DataSet ds, string strHeaderText, string strFileName)
         {                    
              HttpContext curContext = HttpContext.Current;
              curContext.Response.ContentType = "application/vnd.ms-excel";
              curContext.Response.Charset = "";
              if (curContext.Request.UserAgent.ToLower().IndexOf("firefox", System.StringComparison.Ordinal) > 0)
              {
                  curContext.Response.AppendHeader("Content-Disposition", "attachment;filename=" + strFileName);
              }
              else
              {
                  curContext.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(strFileName, System.Text.Encoding.UTF8));
              }
 
            //  curContext.Response.AppendHeader("Content-Disposition", "attachment;filename=" +strFileName);
              curContext.Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
              curContext.Response.BinaryWrite(ExportDataSetToExcel(ds, strHeaderText).GetBuffer());
              curContext.Response.End();         
         }
 
         /// <summary>
         /// 由DataSet导出Excel
         /// </summary>
         /// <param name="sourceTable">要导出数据的DataTable</param>
         /// <param name="sheetName">工作表名称</param>
         /// <returns>Excel工作表</returns>
         private static MemoryStream ExportDataSetToExcel(DataSet sourceDs, string sheetName)
         {
             HSSFWorkbook workbook = new HSSFWorkbook();
             MemoryStream ms = new MemoryStream();
             string[] sheetNames = sheetName.Split(',');
             for (int i = 0; i < sheetNames.Length; i++)
             {
                 ISheet sheet = workbook.CreateSheet(sheetNames[i]);
 
                 #region 列头
                 IRow headerRow = sheet.CreateRow(0);
                 HSSFCellStyle headStyle = workbook.CreateCellStyle() as HSSFCellStyle;
                 HSSFFont font = workbook.CreateFont() as HSSFFont;
                 font.FontHeightInPoints = 10;
                 font.Boldweight = 700;
                 headStyle.SetFont(font);
 
                 //取得列宽
                 int[] arrColWidth = new int[sourceDs.Tables[i].Columns.Count];
                 foreach (DataColumn item in sourceDs.Tables[i].Columns)
                 {
                     arrColWidth[item.Ordinal] = Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;
                 }
 
                 // 处理列头
                 foreach (DataColumn column in sourceDs.Tables[i].Columns)
                 {
                     headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                     headerRow.GetCell(column.Ordinal).CellStyle = headStyle;
                     //设置列宽
                     sheet.SetColumnWidth(column.Ordinal, (arrColWidth[column.Ordinal] + 1) * 256);
 
                 }
                 #endregion
 
                 #region 填充值
                 int rowIndex = 1;
                 foreach (DataRow row in sourceDs.Tables[i].Rows)
                 {
                     IRow dataRow = sheet.CreateRow(rowIndex);
                     foreach (DataColumn column in sourceDs.Tables[i].Columns)
                     {
                         dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                     }
                     rowIndex++;
                 }
                 #endregion
             }
             workbook.Write(ms);
             ms.Flush();
             ms.Position = 0;
             workbook = null;
             return ms;
         }
 
 
         /// <summary>
         /// 验证导入的Excel是否有数据
         /// </summary>
         /// <param name="excelFileStream"></param>
         /// <returns></returns>
         public static bool HasData(Stream excelFileStream)
         {
             using (excelFileStream)
             {
                 IWorkbook workBook = new HSSFWorkbook(excelFileStream);
                 if (workBook.NumberOfSheets > 0)
                 {
                     ISheet sheet = workBook.GetSheetAt(0);
                     return sheet.PhysicalNumberOfRows > 0;
                 }
             }
             return false;
         }
         #region 读取Excel文件内容转换为DataSet
         /// <summary>
         /// 读取Excel文件内容转换为DataSet,列名依次为 "c0"……c[columnlength-1]
         /// </summary>
         /// <param name="FileName">文件绝对路径</param>
         /// <param name="startRow">数据开始行数(1为第一行)</param>
         /// <param name="ColumnDataType">每列的数据类型</param>
         /// <returns></returns>
         public static DataSet ReadExcel(string FileName, int startRow, params NpoiDataType[] ColumnDataType)
         {
             int ertime = 0;
             int intime = 0;
             DataSet ds = new DataSet("ds");
             DataTable dt = new DataTable("dt");
             DataRow dr;
             StringBuilder sb = new StringBuilder();
             using (FileStream stream = new FileStream(@FileName, FileMode.Open, FileAccess.Read))
             {
                 IWorkbook workbook = WorkbookFactory.Create(stream);//使用接口，自动识别excel2003/2007格式
                 ISheet sheet = workbook.GetSheetAt(0);//得到里面第一个sheet
                 int j;
                 IRow row;
                 #region ColumnDataType赋值
                 if (ColumnDataType.Length <= 0)
                 {
                     row = sheet.GetRow(startRow - 1);//得到第i行
                     ColumnDataType = new NpoiDataType[row.LastCellNum];
                     for (int i = 0; i < row.LastCellNum; i++)
                     {
                         ICell hs = row.GetCell(i);
                         ColumnDataType[i] = GetCellDataType(hs);
                     }
                 }
                 #endregion
                 for (j = 0; j < ColumnDataType.Length; j++)
                 {
                     Type tp = GetDataTableType(ColumnDataType[j]);
                     dt.Columns.Add("c" + j, tp);
                 }
                 for (int i = startRow - 1; i <= sheet.PhysicalNumberOfRows; i++)
                 {
                     row = sheet.GetRow(i);//得到第i行
                     if (row == null) continue;
                     try
                     {
                         dr = dt.NewRow();

                         for (j = 0; j < ColumnDataType.Length; j++)
                         {
                             dr["c" + j] = GetCellData(ColumnDataType[j], row, j);
                         }
                         dt.Rows.Add(dr);
                         intime++;
                     }
                     catch (Exception er)
                     {
                         ertime++;
                         sb.Append(string.Format("第{0}行出错：{1}\r\n", i + 1, er.Message));
                         continue;
                     }
                 }
                 ds.Tables.Add(dt);
             }
             if (ds.Tables[0].Rows.Count == 0 && sb.ToString() != "") throw new Exception(sb.ToString());
             return ds;
         }
         #endregion
         static Color LevelOneColor = Color.Green;
         static Color LevelTwoColor = Color.FromArgb(201, 217, 243);
         static Color LevelThreeColor = Color.FromArgb(231, 238, 248);
         static Color LevelFourColor = Color.FromArgb(232, 230, 231);
         static Color LevelFiveColor = Color.FromArgb(250, 252, 213);

         #region 从DataSet导出到MemoryStream流2003
         /// <summary>
         /// 从DataSet导出到MemoryStream流2003
         /// </summary>
         /// <param name="SaveFileName">文件保存路径</param>
         /// <param name="SheetName">Excel文件中的Sheet名称</param>
         /// <param name="ds">存储数据的DataSet</param>
         /// <param name="startRow">从哪一行开始写入，从0开始</param>
         /// <param name="datatypes">DataSet中的各列对应的数据类型</param>
         public static bool CreateExcel2003(string SaveFileName, string SheetName, DataSet ds, int startRow, params NpoiDataType[] datatypes)
         {
             try
             {
                 if (startRow < 0) startRow = 0;
                 HSSFWorkbook wb = new HSSFWorkbook();
                 wb = new HSSFWorkbook();
                 DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                 dsi.Company = "pkm";
                 SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                 si.Title =
                 si.Subject = "automatic genereted document";
                 si.Author = "pkm";
                 wb.DocumentSummaryInformation = dsi;
                 wb.SummaryInformation = si;
                 ISheet sheet = wb.CreateSheet(SheetName);
                 //sheet.SetColumnWidth(0, 50 * 256);
                 //sheet.SetColumnWidth(1, 100 * 256);
                 IRow row;
                 ICell cell;
                 DataRow dr;
                 int j;
                 int maxLength = 0;
                 int curLength = 0;
                 object columnValue;
                 DataTable dt = ds.Tables[0];
                 if (datatypes.Length < dt.Columns.Count)
                 {
                     datatypes = new NpoiDataType[dt.Columns.Count];
                     for (int i = 0; i < dt.Columns.Count; i++)
                     {
                         string dtcolumntype = dt.Columns[i].DataType.Name.ToLower();
                         switch (dtcolumntype)
                         {
                             case "string": datatypes[i] = NpoiDataType.String;
                                 break;
                             case "datetime": datatypes[i] = NpoiDataType.Datetime;
                                 break;
                             case "boolean": datatypes[i] = NpoiDataType.Bool;
                                 break;
                             case "double": datatypes[i] = NpoiDataType.Numeric;
                                 break;
                             default: datatypes[i] = NpoiDataType.String;
                                 break;
                         }
                     }
                 }

                 #region 创建表头
                 row = sheet.CreateRow(0);//创建第i行
                 ICellStyle style1 = wb.CreateCellStyle();//样式
                 IFont font1 = wb.CreateFont();//字体

                 font1.Color = HSSFColor.White.Index;//字体颜色
                 font1.Boldweight = (short)FontBoldWeight.Bold;//字体加粗样式
                 //style1.FillBackgroundColor = HSSFColor.WHITE.index;//GetXLColour(wb, LevelOneColor);// 设置图案色
                 style1.FillForegroundColor = HSSFColor.Green.Index;//GetXLColour(wb, LevelOneColor);// 设置背景色
                 style1.FillPattern = FillPattern.SolidForeground;
                 style1.SetFont(font1);//样式里的字体设置具体的字体样式
                 style1.Alignment = HorizontalAlignment.Center;//文字水平对齐方式
                 style1.VerticalAlignment = VerticalAlignment.Center;//文字垂直对齐方式
                 row.HeightInPoints = 25;
                 for (j = 0; j < dt.Columns.Count; j++)
                 {
                     columnValue = dt.Columns[j].ColumnName;
                     curLength = Encoding.Default.GetByteCount(columnValue.ToString());
                     maxLength = (maxLength < curLength ? curLength : maxLength);
                     int colounwidth = 256 * maxLength;
                     sheet.SetColumnWidth(j, colounwidth);
                     try
                     {
                         cell = row.CreateCell(j);//创建第0行的第j列
                         cell.CellStyle = style1;//单元格式设置样式

                         try
                         {
                             cell.SetCellType(CellType.String);
                             cell.SetCellValue(columnValue.ToString());
                         }
                         catch { }

                     }
                     catch
                     {
                         continue;
                     }
                 }
                 #endregion

                 #region 创建每一行
                 for (int i = startRow; i < ds.Tables[0].Rows.Count; i++)
                 {
                     dr = ds.Tables[0].Rows[i];
                     row = sheet.CreateRow(i + 1);//创建第i行
                     for (j = 0; j < dt.Columns.Count; j++)
                     {
                         columnValue = dr[j];
                         curLength = Encoding.Default.GetByteCount(columnValue.ToString());
                         maxLength = (maxLength < curLength ? curLength : maxLength);
                         int colounwidth = 256 * maxLength;
                         sheet.SetColumnWidth(j, colounwidth);
                         try
                         {
                             cell = row.CreateCell(j);//创建第i行的第j列
                             #region 插入第j列的数据
                             try
                             {
                                 NpoiDataType dtype = datatypes[j];
                                 switch (dtype)
                                 {
                                     case NpoiDataType.String:
                                         {
                                             cell.SetCellType(CellType.String);
                                             cell.SetCellValue(columnValue.ToString());
                                         } break;
                                     case NpoiDataType.Datetime:
                                         {
                                             cell.SetCellType(CellType.String);
                                             cell.SetCellValue(columnValue.ToString());
                                         } break;
                                     case NpoiDataType.Numeric:
                                         {
                                             cell.SetCellType(CellType.Numeric);
                                             cell.SetCellValue(Convert.ToDouble(columnValue));
                                         } break;
                                     case NpoiDataType.Bool:
                                         {
                                             cell.SetCellType(CellType.Boolean);
                                             cell.SetCellValue(Convert.ToBoolean(columnValue));
                                         } break;
                                     case NpoiDataType.Richtext:
                                         {
                                             cell.SetCellType(CellType.Formula);
                                             cell.SetCellValue(columnValue.ToString());
                                         } break;
                                 }
                             }
                             catch
                             {
                                 cell.SetCellType(CellType.String);
                                 cell.SetCellValue(columnValue.ToString());
                             }
                             #endregion

                         }
                         catch
                         {
                             continue;
                         }
                     }
                 }
                 #endregion

                 //using (FileStream fs = new FileStream(@SaveFileName, FileMode.OpenOrCreate))//生成文件在服务器上
                 //{
                 //    wb.Write(fs);
                 //}
                 //string SaveFileName = "output.xls";
                 using (FileStream fs = new FileStream(@SaveFileName, FileMode.OpenOrCreate, FileAccess.Write))//生成文件在服务器上
                 {
                     wb.Write(fs);
                     //Console.WriteLine("文件保存成功！" + SaveFileName);
                 }

                 return true;
             }
             catch (Exception er)
             {
                 //Console.WriteLine("文件保存成功！" + SaveFileName);
                 return false;
             }

         }
         #endregion
         #region 从DataSet导出到MemoryStream流2003
         /// <summary>
         /// 从DataSet导出到MemoryStream流2003
         /// </summary>
         /// <param name="SaveFileName">文件保存路径</param>
         /// <param name="ds">存储数据的DataSet</param>
         /// <param name="startRow">从哪一行开始写入，从0开始</param>
         public static bool CreateExcel2003(string SaveFileName, DataSet ds, int startRow=0)
         {
             try
             {
                 if (startRow < 0) startRow = 0;
                 HSSFWorkbook wb = new HSSFWorkbook();
                 wb = new HSSFWorkbook();
                 DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                 dsi.Company = "pkm";
                 SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                 si.Title =
                 si.Subject = "automatic genereted document";
                 si.Author = "pkm";
                 wb.DocumentSummaryInformation = dsi;
                 wb.SummaryInformation = si;
                 for (int iT = 0; iT < ds.Tables.Count; iT++)
                 {
                     ISheet sheet = wb.CreateSheet(ds.Tables[iT].TableName);
                     //sheet.SetColumnWidth(0, 50 * 256);
                     //sheet.SetColumnWidth(1, 100 * 256);
                     IRow row;
                     ICell cell;
                     DataRow dr;
                     int j;
                     int maxLength = 0;
                     int curLength = 0;
                     object columnValue;
                     DataTable dt = ds.Tables[iT];

                     NpoiDataType[] datatypes = new NpoiDataType[dt.Columns.Count];

                     //if (datatypes.Length < dt.Columns.Count)
                     //{
                     datatypes = new NpoiDataType[dt.Columns.Count];
                     for (int i = 0; i < dt.Columns.Count; i++)
                     {
                         string dtcolumntype = dt.Columns[i].DataType.Name.ToLower();
                         switch (dtcolumntype)
                         {
                             case "string": datatypes[i] = NpoiDataType.String;
                                 break;
                             case "datetime": datatypes[i] = NpoiDataType.Datetime;
                                 break;
                             case "boolean": datatypes[i] = NpoiDataType.Bool;
                                 break;
                             case "double": datatypes[i] = NpoiDataType.Numeric;
                                 break;
                             default: datatypes[i] = NpoiDataType.String;
                                 break;
                         }
                     }
                     //}

                     #region 创建表头
                     row = sheet.CreateRow(0);//创建第i行
                     ICellStyle style1 = wb.CreateCellStyle();//样式
                     IFont font1 = wb.CreateFont();//字体

                     font1.Color = HSSFColor.White.Index;//字体颜色
                     font1.Boldweight = (short)FontBoldWeight.Bold;//字体加粗样式
                     //style1.FillBackgroundColor = HSSFColor.WHITE.index;//GetXLColour(wb, LevelOneColor);// 设置图案色
                     style1.FillForegroundColor = HSSFColor.Green.Index;//GetXLColour(wb, LevelOneColor);// 设置背景色
                     style1.FillPattern = FillPattern.SolidForeground;
                     style1.SetFont(font1);//样式里的字体设置具体的字体样式
                     style1.Alignment = HorizontalAlignment.Center;//文字水平对齐方式
                     style1.VerticalAlignment = VerticalAlignment.Center;//文字垂直对齐方式
                     row.HeightInPoints = 25;
                     for (j = 0; j < dt.Columns.Count; j++)
                     {
                         columnValue = dt.Columns[j].ColumnName;
                         curLength = Encoding.Default.GetByteCount(columnValue.ToString());
                         maxLength = (maxLength < curLength ? curLength : maxLength);
                         int colounwidth = 256 * maxLength;
                         sheet.SetColumnWidth(j, colounwidth);
                         try
                         {
                             cell = row.CreateCell(j);//创建第0行的第j列
                             cell.CellStyle = style1;//单元格式设置样式

                             try
                             {
                                 cell.SetCellType(CellType.String);
                                 cell.SetCellValue(columnValue.ToString());
                             }
                             catch { }

                         }
                         catch
                         {
                             continue;
                         }
                     }
                     #endregion

                     #region 创建每一行
                     for (int i = startRow; i < ds.Tables[iT].Rows.Count; i++)
                     {
                         dr = ds.Tables[iT].Rows[i];
                         row = sheet.CreateRow(i + 1);//创建第i行
                         for (j = 0; j < dt.Columns.Count; j++)
                         {
                             columnValue = dr[j];
                             curLength = Encoding.Default.GetByteCount(columnValue.ToString());
                             maxLength = (maxLength < curLength ? curLength : maxLength);
                             int colounwidth = 256 * maxLength;
                             sheet.SetColumnWidth(j, colounwidth);
                             try
                             {
                                 cell = row.CreateCell(j);//创建第i行的第j列
                                 #region 插入第j列的数据
                                 try
                                 {
                                     NpoiDataType dtype = datatypes[j];
                                     switch (dtype)
                                     {
                                         case NpoiDataType.String:
                                             {
                                                 cell.SetCellType(CellType.String);
                                                 cell.SetCellValue(columnValue.ToString());
                                             } break;
                                         case NpoiDataType.Datetime:
                                             {
                                                 cell.SetCellType(CellType.String);
                                                 cell.SetCellValue(columnValue.ToString());
                                             } break;
                                         case NpoiDataType.Numeric:
                                             {
                                                 cell.SetCellType(CellType.Numeric);
                                                 cell.SetCellValue(Convert.ToDouble(columnValue));
                                             } break;
                                         case NpoiDataType.Bool:
                                             {
                                                 cell.SetCellType(CellType.Boolean);
                                                 cell.SetCellValue(Convert.ToBoolean(columnValue));
                                             } break;
                                         case NpoiDataType.Richtext:
                                             {
                                                 cell.SetCellType(CellType.Formula);
                                                 cell.SetCellValue(columnValue.ToString());
                                             } break;
                                     }
                                 }
                                 catch
                                 {
                                     cell.SetCellType(CellType.String);
                                     cell.SetCellValue(columnValue.ToString());
                                 }
                                 #endregion

                             }
                             catch
                             {
                                 continue;
                             }
                         }
                     }
                     #endregion
                 }
                 //using (FileStream fs = new FileStream(@SaveFileName, FileMode.OpenOrCreate))//生成文件在服务器上
                 //{
                 //    wb.Write(fs);
                 //}
                 //string SaveFileName = "output.xls";
                 using (FileStream fs = new FileStream(@SaveFileName, FileMode.OpenOrCreate, FileAccess.Write))//生成文件在服务器上
                 {
                     wb.Write(fs);
                     //Console.WriteLine("文件保存成功！" + SaveFileName);
                 }

                 return true;
             }
             catch (Exception er)
             {
                 //Console.WriteLine("文件保存成功！" + SaveFileName);
                 return false;
             }

         }
         #endregion

         #region 从DataSet导出到MemoryStream流2007
         /// <summary>
         /// 从DataSet导出到MemoryStream流2007
         /// </summary>
         /// <param name="SaveFileName">文件保存路径</param>
         /// <param name="SheetName">Excel文件中的Sheet名称</param>
         /// <param name="ds">存储数据的DataSet</param>
         /// <param name="startRow">从哪一行开始写入，从0开始</param>
         /// <param name="datatypes">DataSet中的各列对应的数据类型</param>
         public static bool CreateExcel2007(string SaveFileName, string SheetName, DataSet ds, int startRow, params NpoiDataType[] datatypes)
         {
             try
             {
                 if (startRow < 0) startRow = 0;
                 XSSFWorkbook wb = new XSSFWorkbook();
                 ISheet sheet = wb.CreateSheet(SheetName);
                 //sheet.SetColumnWidth(0, 50 * 256);
                 //sheet.SetColumnWidth(1, 100 * 256);
                 IRow row;
                 ICell cell;
                 DataRow dr;
                 int j;
                 int maxLength = 0;
                 int curLength = 0;
                 object columnValue;
                 DataTable dt = ds.Tables[0];
                 if (datatypes.Length < dt.Columns.Count)
                 {
                     datatypes = new NpoiDataType[dt.Columns.Count];
                     for (int i = 0; i < dt.Columns.Count; i++)
                     {
                         string dtcolumntype = dt.Columns[i].DataType.Name.ToLower();
                         switch (dtcolumntype)
                         {
                             case "string": datatypes[i] = NpoiDataType.String;
                                 break;
                             case "datetime": datatypes[i] = NpoiDataType.Datetime;
                                 break;
                             case "boolean": datatypes[i] = NpoiDataType.Bool;
                                 break;
                             case "double": datatypes[i] = NpoiDataType.Numeric;
                                 break;
                             default: datatypes[i] = NpoiDataType.String;
                                 break;
                         }
                     }
                 }

                 #region 创建表头
                 row = sheet.CreateRow(0);//创建第i行
                 ICellStyle style1 = wb.CreateCellStyle();//样式
                 IFont font1 = wb.CreateFont();//字体

                 font1.Color = HSSFColor.White.Index;//字体颜色
                 font1.Boldweight = (short)FontBoldWeight.Bold;//字体加粗样式
                 //style1.FillBackgroundColor = HSSFColor.WHITE.index;//GetXLColour(wb, LevelOneColor);// 设置图案色
                 style1.FillForegroundColor = HSSFColor.Green.Index;//GetXLColour(wb, LevelOneColor);// 设置背景色
                 style1.FillPattern = FillPattern.SolidForeground;
                 style1.SetFont(font1);//样式里的字体设置具体的字体样式
                 style1.Alignment = HorizontalAlignment.Center;//文字水平对齐方式
                 style1.VerticalAlignment = VerticalAlignment.Center;//文字垂直对齐方式
                 row.HeightInPoints = 25;
                 for (j = 0; j < dt.Columns.Count; j++)
                 {
                     columnValue = dt.Columns[j].ColumnName;
                     curLength = Encoding.Default.GetByteCount(columnValue.ToString());
                     maxLength = (maxLength < curLength ? curLength : maxLength);
                     int colounwidth = 256 * maxLength;
                     sheet.SetColumnWidth(j, colounwidth);
                     try
                     {
                         cell = row.CreateCell(j);//创建第0行的第j列
                         cell.CellStyle = style1;//单元格式设置样式

                         try
                         {
                             //cell.SetCellType(CellType.STRING);
                             cell.SetCellValue(columnValue.ToString());
                         }
                         catch { }

                     }
                     catch
                     {
                         continue;
                     }
                 }
                 #endregion

                 #region 创建每一行
                 for (int i = startRow; i < ds.Tables[0].Rows.Count; i++)
                 {
                     dr = ds.Tables[0].Rows[i];
                     row = sheet.CreateRow(i + 1);//创建第i行
                     for (j = 0; j < dt.Columns.Count; j++)
                     {
                         columnValue = dr[j];
                         curLength = Encoding.Default.GetByteCount(columnValue.ToString());
                         maxLength = (maxLength < curLength ? curLength : maxLength);
                         int colounwidth = 256 * maxLength;
                         sheet.SetColumnWidth(j, colounwidth);
                         try
                         {
                             cell = row.CreateCell(j);//创建第i行的第j列
                             #region 插入第j列的数据
                             try
                             {
                                 NpoiDataType dtype = datatypes[j];
                                 switch (dtype)
                                 {
                                     case NpoiDataType.String:
                                         {
                                             //cell.SetCellType(CellType.STRING);
                                             cell.SetCellValue(columnValue.ToString());
                                         } break;
                                     case NpoiDataType.Datetime:
                                         {
                                             // cell.SetCellType(CellType.STRING);
                                             cell.SetCellValue(columnValue.ToString());
                                         } break;
                                     case NpoiDataType.Numeric:
                                         {
                                             //cell.SetCellType(CellType.NUMERIC);
                                             cell.SetCellValue(Convert.ToDouble(columnValue));
                                         } break;
                                     case NpoiDataType.Bool:
                                         {
                                             //cell.SetCellType(CellType.BOOLEAN);
                                             cell.SetCellValue(Convert.ToBoolean(columnValue));
                                         } break;
                                     case NpoiDataType.Richtext:
                                         {
                                             // cell.SetCellType(CellType.FORMULA);
                                             cell.SetCellValue(columnValue.ToString());
                                         } break;
                                 }
                             }
                             catch
                             {
                                 //cell.SetCellType(HSSFCell.CELL_TYPE_STRING);
                                 cell.SetCellValue(columnValue.ToString());
                             }
                             #endregion

                         }
                         catch
                         {
                             continue;
                         }
                     }
                 }
                 #endregion

                 //using (FileStream fs = new FileStream(@SaveFileName, FileMode.OpenOrCreate))//生成文件在服务器上
                 //{
                 //    wb.Write(fs);
                 //}
                 //string SaveFileName = "output.xlsx";
                 using (FileStream fs = new FileStream(SaveFileName, FileMode.OpenOrCreate, FileAccess.Write))//生成文件在服务器上
                 {
                     wb.Write(fs);
                     //Console.WriteLine("文件保存成功！" + SaveFileName);
                 }
                 return true;
             }
             catch (Exception er)
             {
                 //Console.WriteLine("文件保存失败！" + SaveFileName);
                 return false;
             }

         }
         #endregion
         #region 从DataSet导出到MemoryStream流2007
         /// <summary>
         /// 从DataSet导出到MemoryStream流2007
         /// </summary>
         /// <param name="SaveFileName">文件保存路径</param>
         /// <param name="ds">存储数据的DataSet</param>
         /// <param name="startRow">从哪一行开始写入，从0开始</param>
         public static bool CreateExcel2007(string SaveFileName, DataSet ds, int startRow=0)
         {
             try
             {
                 if (startRow < 0) startRow = 0;
                 XSSFWorkbook wb = new XSSFWorkbook();
                 for (int iT = 0; iT < ds.Tables.Count; iT++)
                 {
                     ISheet sheet = wb.CreateSheet(ds.Tables[iT].TableName);
                     //sheet.SetColumnWidth(0, 50 * 256);
                     //sheet.SetColumnWidth(1, 100 * 256);
                     IRow row;
                     ICell cell;
                     DataRow dr;
                     int j;
                     int maxLength = 0;
                     int curLength = 0;
                     object columnValue;
                     DataTable dt = ds.Tables[iT];
                     //if (datatypes.Length < dt.Columns.Count)
                     //{
                     NpoiDataType[] datatypes = new NpoiDataType[dt.Columns.Count];
                     for (int i = 0; i < dt.Columns.Count; i++)
                     {
                         string dtcolumntype = dt.Columns[i].DataType.Name.ToLower();
                         switch (dtcolumntype)
                         {
                             case "string": datatypes[i] = NpoiDataType.String;
                                 break;
                             case "datetime": datatypes[i] = NpoiDataType.Datetime;
                                 break;
                             case "boolean": datatypes[i] = NpoiDataType.Bool;
                                 break;
                             case "double": datatypes[i] = NpoiDataType.Numeric;
                                 break;
                             default: datatypes[i] = NpoiDataType.String;
                                 break;
                         }
                     }
                     //}

                     #region 创建表头
                     row = sheet.CreateRow(0);//创建第i行
                     ICellStyle style1 = wb.CreateCellStyle();//样式
                     IFont font1 = wb.CreateFont();//字体

                     font1.Color = HSSFColor.White.Index;//字体颜色
                     font1.Boldweight = (short)FontBoldWeight.Bold;//字体加粗样式
                     //style1.FillBackgroundColor = HSSFColor.WHITE.index;//GetXLColour(wb, LevelOneColor);// 设置图案色
                     style1.FillForegroundColor = HSSFColor.Green.Index;//GetXLColour(wb, LevelOneColor);// 设置背景色
                     style1.FillPattern = FillPattern.SolidForeground;
                     style1.SetFont(font1);//样式里的字体设置具体的字体样式
                     style1.Alignment = HorizontalAlignment.Center;//文字水平对齐方式
                     style1.VerticalAlignment = VerticalAlignment.Center;//文字垂直对齐方式
                     row.HeightInPoints = 25;
                     for (j = 0; j < dt.Columns.Count; j++)
                     {
                         columnValue = dt.Columns[j].ColumnName;
                         curLength = Encoding.Default.GetByteCount(columnValue.ToString());
                         maxLength = (maxLength < curLength ? curLength : maxLength);
                         int colounwidth = 256 * maxLength;
                         sheet.SetColumnWidth(j, colounwidth);
                         try
                         {
                             cell = row.CreateCell(j);//创建第0行的第j列
                             cell.CellStyle = style1;//单元格式设置样式

                             try
                             {
                                 //cell.SetCellType(CellType.STRING);
                                 cell.SetCellValue(columnValue.ToString());
                             }
                             catch { }

                         }
                         catch
                         {
                             continue;
                         }
                     }
                     #endregion

                     #region 创建每一行
                     for (int i = startRow; i < ds.Tables[iT].Rows.Count; i++)
                     {
                         dr = ds.Tables[iT].Rows[i];
                         row = sheet.CreateRow(i + 1);//创建第i行
                         for (j = 0; j < dt.Columns.Count; j++)
                         {
                             columnValue = dr[j];
                             curLength = Encoding.Default.GetByteCount(columnValue.ToString());
                             maxLength = (maxLength < curLength ? curLength : maxLength);
                             int colounwidth = 256 * maxLength;
                             sheet.SetColumnWidth(j, colounwidth);
                             try
                             {
                                 cell = row.CreateCell(j);//创建第i行的第j列
                                 #region 插入第j列的数据
                                 try
                                 {
                                     NpoiDataType dtype = datatypes[j];
                                     switch (dtype)
                                     {
                                         case NpoiDataType.String:
                                             {
                                                 //cell.SetCellType(CellType.STRING);
                                                 cell.SetCellValue(columnValue.ToString());
                                             } break;
                                         case NpoiDataType.Datetime:
                                             {
                                                 // cell.SetCellType(CellType.STRING);
                                                 cell.SetCellValue(columnValue.ToString());
                                             } break;
                                         case NpoiDataType.Numeric:
                                             {
                                                 //cell.SetCellType(CellType.NUMERIC);
                                                 cell.SetCellValue(Convert.ToDouble(columnValue));
                                             } break;
                                         case NpoiDataType.Bool:
                                             {
                                                 //cell.SetCellType(CellType.BOOLEAN);
                                                 cell.SetCellValue(Convert.ToBoolean(columnValue));
                                             } break;
                                         case NpoiDataType.Richtext:
                                             {
                                                 // cell.SetCellType(CellType.FORMULA);
                                                 cell.SetCellValue(columnValue.ToString());
                                             } break;
                                     }
                                 }
                                 catch
                                 {
                                     //cell.SetCellType(HSSFCell.CELL_TYPE_STRING);
                                     cell.SetCellValue(columnValue.ToString());
                                 }
                                 #endregion

                             }
                             catch
                             {
                                 continue;
                             }
                         }
                     }
                     #endregion
                 }
                 //using (FileStream fs = new FileStream(@SaveFileName, FileMode.OpenOrCreate))//生成文件在服务器上
                 //{
                 //    wb.Write(fs);
                 //}
                 //string SaveFileName = "output.xlsx";
                 using (FileStream fs = new FileStream(SaveFileName, FileMode.OpenOrCreate, FileAccess.Write))//生成文件在服务器上
                 {
                     wb.Write(fs);
                     //Console.WriteLine("文件保存成功！" + SaveFileName);
                 }
                 return true;
             }
             catch (Exception er)
             {
                 //Console.WriteLine("文件保存失败！" + SaveFileName);
                 return false;
             }

         }
         #endregion

         private static short GetXLColour(HSSFWorkbook workbook, System.Drawing.Color SystemColour)
         {
             short s = 0;
             HSSFPalette XlPalette = workbook.GetCustomPalette();
             HSSFColor XlColour = XlPalette.FindColor(SystemColour.R, SystemColour.G, SystemColour.B);
             if (XlColour == null)
             {
                 if (NPOI.HSSF.Record.PaletteRecord.STANDARD_PALETTE_SIZE < 255)
                 {
                     if (NPOI.HSSF.Record.PaletteRecord.STANDARD_PALETTE_SIZE < 64)
                     {
                         //NPOI.HSSF.Record.PaletteRecord.STANDARD_PALETTE_SIZE= 64;
                         //NPOI.HSSF.Record.PaletteRecord.STANDARD_PALETTE_SIZE += 1;
                         XlColour = XlPalette.AddColor(SystemColour.R, SystemColour.G, SystemColour.B);
                     }
                     else
                     {
                         XlColour = XlPalette.FindSimilarColor(SystemColour.R, SystemColour.G, SystemColour.B);
                     }
                     s = XlColour.Indexed;//.GetIndex();
                 }
             }
             else
                 s = XlColour.Indexed;//.GetIndex();
             return s;
         }

         #region 读Excel-根据NpoiDataType创建的DataTable列的数据类型
         /// <summary>
         /// 读Excel-根据NpoiDataType创建的DataTable列的数据类型
         /// </summary>
         /// <param name="datatype"></param>
         /// <returns></returns>
         private static Type GetDataTableType(NpoiDataType datatype)
         {
             Type tp = typeof(string);//Type.GetType("System.String")
             switch (datatype)
             {
                 case NpoiDataType.Bool:
                     tp = typeof(bool);
                     break;
                 case NpoiDataType.Datetime:
                     tp = typeof(DateTime);
                     break;
                 case NpoiDataType.Numeric:
                     tp = typeof(double);
                     break;
                 case NpoiDataType.Error:
                     tp = typeof(string);
                     break;
                 case NpoiDataType.Blank:
                     tp = typeof(string);
                     break;
             }
             return tp;
         }
         #endregion

         #region 读Excel-得到不同数据类型单元格的数据
         /// <summary>
         /// 读Excel-得到不同数据类型单元格的数据
         /// </summary>
         /// <param name="datatype">数据类型</param>
         /// <param name="row">数据中的一行</param>
         /// <param name="column">哪列</param>
         /// <returns></returns>
         private static object GetCellData(NpoiDataType datatype, IRow row, int column)
         {

             switch (datatype)
             {
                 case NpoiDataType.String:
                     try
                     {
                         return row.GetCell(column).DateCellValue;
                     }
                     catch
                     {
                         try
                         {
                             return row.GetCell(column).StringCellValue;
                         }
                         catch
                         {
                             return row.GetCell(column).NumericCellValue;
                         }
                     }
                 case NpoiDataType.Bool:
                     try { return row.GetCell(column).BooleanCellValue; }
                     catch { return row.GetCell(column).StringCellValue; }
                 case NpoiDataType.Datetime:
                     try { return row.GetCell(column).DateCellValue; }
                     catch { return row.GetCell(column).StringCellValue; }
                 case NpoiDataType.Numeric:
                     try { return row.GetCell(column).NumericCellValue; }
                     catch { return row.GetCell(column).StringCellValue; }
                 case NpoiDataType.Richtext:
                     try { return row.GetCell(column).RichStringCellValue; }
                     catch { return row.GetCell(column).StringCellValue; }
                 case NpoiDataType.Error:
                     try { return row.GetCell(column).ErrorCellValue; }
                     catch { return row.GetCell(column).StringCellValue; }
                 case NpoiDataType.Blank:
                     try { return row.GetCell(column).StringCellValue; }
                     catch { return ""; }
                 default: return "";
             }
         }
         #endregion

         #region 获取单元格数据类型
         /// <summary>
         /// 获取单元格数据类型
         /// </summary>
         /// <param name="hs"></param>
         /// <returns></returns>
         private static NpoiDataType GetCellDataType(ICell hs)
         {
             NpoiDataType dtype;
             DateTime t1;
             string cellvalue = "";

             switch (hs.CellType)
             {
                 case CellType.Blank:
                     dtype = NpoiDataType.String;
                     cellvalue = hs.StringCellValue;
                     break;
                 case CellType.Boolean:
                     dtype = NpoiDataType.Bool;
                     break;
                 case CellType.Numeric:
                     dtype = NpoiDataType.Numeric;
                     cellvalue = hs.NumericCellValue.ToString();
                     break;
                 case CellType.String:
                     dtype = NpoiDataType.String;
                     cellvalue = hs.StringCellValue;
                     break;
                 case CellType.Error:
                     dtype = NpoiDataType.Error;
                     break;
                 case CellType.Formula:
                 default:
                     dtype = NpoiDataType.Datetime;
                     break;
             }
             if (cellvalue != "" && DateTime.TryParse(cellvalue, out t1)) dtype = NpoiDataType.Datetime;
             return dtype;
         }
         #endregion



         #region 测试代码


         #endregion
     }

     #region 枚举(Excel单元格数据类型)
     /// <summary>
     /// 枚举(Excel单元格数据类型)
     /// </summary>
     public enum NpoiDataType
     {
         /// <summary>
         /// 字符串类型-值为1
         /// </summary>
         String,
         /// <summary>
         /// 布尔类型-值为2
         /// </summary>
         Bool,
         /// <summary>
         /// 时间类型-值为3
         /// </summary>
         Datetime,
         /// <summary>
         /// 数字类型-值为4
         /// </summary>
         Numeric,
         /// <summary>
         /// 复杂文本类型-值为5
         /// </summary>
         Richtext,
         /// <summary>
         /// 空白
         /// </summary>
         Blank,
         /// <summary>
         /// 错误
         /// </summary>
         Error
     }
     #endregion
     #region Call Example
     // public static void test1()
     // {
     //     NpoiHelper np = new NpoiHelper();
     //     DataTable dt1 = np.ReadExcel(AppDomain.CurrentDomain.BaseDirectory + "1测试数据.xls", 2).Tables[0];//读2003格式数据
     //    DataSet ds1 = new DataSet();
     //     ds1.Tables.Add(dt1.Copy());
     //     ds1.AcceptChanges();
     //     string SaveFileName = "output1.xls";
     //     np.CreateExcel2003(SaveFileName, "sheet001", ds1, 0);//写2003格式数据
     //}
     // public static void test2()
     // {
     //     NpoiHelper np = new NpoiHelper();
     //     DataTable dt1 = np.ReadExcel(AppDomain.CurrentDomain.BaseDirectory + "2测试数据.xlsx", 2).Tables[0];//读2007格式数据
     //    DataSet ds1 = new DataSet();
     //     ds1.Tables.Add(dt1.Copy());
     //     ds1.AcceptChanges();

     //     string SaveFileName = "output2.xlsx";
     //     np.CreateExcel2007(SaveFileName, "sheet001", ds1, 0);//写2007格式数据
     //    //Console.ReadKey();
     // }
     #endregion
 }
