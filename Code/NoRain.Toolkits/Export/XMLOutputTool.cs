using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace NoRain.Toolkits
{
    public class XMLOutputTool
    {

        private static Type[] _valueTypes = new[] { typeof(bool),typeof(byte),typeof(sbyte),typeof(char) ,typeof(decimal),
            typeof(double),typeof(float),typeof(int),typeof(uint),typeof(long),typeof(ulong),typeof(object),typeof(short),typeof(ushort),typeof(string)};
        private static Type[] _numTypes = new[] {typeof(byte),typeof(sbyte) ,typeof(decimal),
            typeof(double),typeof(float),typeof(int),typeof(uint),typeof(long),typeof(ulong),typeof(short),typeof(ushort)};
        /// <summary>
        /// 导出数据为Excel文件
        /// </summary>
        /// <param name="items">数据源</param>
        public static void HandleItems<T>(IEnumerable<T> items, string tabelName) where T : class
        {
            var response = HttpContext.Current.Response;
            string filename = "test.xlsx";

            response.Clear();
            response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));

            XSSFWorkbook workbook = new XSSFWorkbook();
            ISheet sheet1 = workbook.CreateSheet("Sheet1");

            if (items == null || items.Count() == 0)
            {
                response.Write("无相关记录");
                response.Flush();
                response.End();
            }

            var item = items.ToList()[0];
            Type t = item.GetType();
            var fields = t.GetFields();
            var properties = t.GetProperties();
            ICellStyle style = workbook.CreateCellStyle();
            style.Alignment = HorizontalAlignment.Center;
            IFont font = workbook.CreateFont();
            font.FontHeightInPoints = 12;
            font.FontHeight = 20;
            font.Boldweight = 600;
            style.SetFont(font);


            //标题行
            var titleCell = sheet1.CreateRow(0).CreateCell(0);
            titleCell.SetCellValue(tabelName ?? "统计报表");
            titleCell.CellStyle = style;

            //合并首行标题
            sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, fields.Length + properties.Length - 1));

            //列名
            var headRow = sheet1.CreateRow(1);
            foreach (var f in fields)
            {
                var cell = headRow.CreateCell(fields.ToList().IndexOf(f));
                cell.SetCellValue(f.Name);
            }
            foreach (var f in properties)
            {
                var cell = headRow.CreateCell(properties.ToList().IndexOf(f));
                cell.SetCellValue(f.Name);
            }

            foreach (var obj in items)
            {
                var itemRow = sheet1.CreateRow(2 + items.ToList().IndexOf(obj));

                foreach (var f in fields)
                {  //判断单元格类型
                    var cellType = CellType.Blank;
                    //数字
                    if (_numTypes.Contains(f.FieldType)) cellType = CellType.Numeric;
                    var itemCell = itemRow.CreateCell(fields.ToList().IndexOf(f), cellType);
                    if (_valueTypes.Contains(f.FieldType)) itemCell.SetCellValue(f.GetValue(obj).ToString());
                    else if (f.GetType() == typeof(DateTime) || f.GetType() == typeof(DateTime?)) itemCell.SetCellValue(f.GetValue(obj).ToString());
                }
                foreach (var f in properties)
                {
                    //判断单元格类型
                    var cellType = CellType.Blank;
                    //数字
                    if (_numTypes.Contains(f.PropertyType)) cellType = CellType.Numeric;
                    ////日期
                    //else if (f.GetType() == typeof(DateTime) || f.GetType() == typeof(DateTime?)) cellType=CellType.d;
                    var itemCell = itemRow.CreateCell(fields.Length + properties.ToList().IndexOf(f), cellType);
                    if (_valueTypes.Contains(f.PropertyType)) itemCell.SetCellValue(f.GetValue(obj, null).ToString());
                    else if (f.GetType() == typeof(DateTime) || f.GetType() == typeof(DateTime?)) itemCell.SetCellValue(f.GetValue(obj, null).ToString());
                }
            }

            using (var f = File.Create(@"d:\test.xlsx"))
            {
                workbook.Write(f);
            }

            response.WriteFile(@"d:\test.xlsx");
            //http://social.msdn.microsoft.com/Forums/en-US/3a7bdd79-f926-4a5e-bcb0-ef81b6c09dcf/responseoutputstreamwrite-writes-all-but-insetrs-a-char-every-64k?forum=ncl
            //workbook.Write(Response.OutputStream); cannot be used 
            //root cause: Response.OutputStream will insert unnecessary byte into the response bytes.
            response.Flush();
            response.End();

        }

        /// <summary>
        /// 导出数据为Excel文件
        /// </summary>
        /// <param name="items">数据源</param>
        /// <param name="nameFields">列对应的名称</param>
        public static void HandleItems(IEnumerable<dynamic> items, Dictionary<string, string> nameFields)
        {

        }
    }
}
