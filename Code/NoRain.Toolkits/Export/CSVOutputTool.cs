using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace NoRain.Toolkits
{
    public class CSVOutputTool
    {
        private CSVOutputTool()
        {
        }
        private static CSVOutputTool _instance { get; set; }
        public static CSVOutputTool Instance
        {
            get
            {
                if (_instance == null) _instance = new CSVOutputTool();
                return _instance;
            }
        }

        public static void Export<T>(List<T> entitys)
        {
            var dataset = new DataSet();
            dataset.Tables.Add(Instance.ListToDataTable(entitys));
            Instance.ExportToCsv(dataset, "CSV导出文件夹", "testCSV");
        }

        /// <summary>
        /// Export to Csv File from dataset
        /// </summary>
        /// <param name="src"></param>
        /// <param name="folderName">folderName</param>
        /// <param name="strFileName">strFileName</param>
        /// <returns></returns>
        public bool ExportToCsv(DataSet src, string folderName, string strFileName)
        {
            string csv = String.Empty;

            StreamWriter writer = null;


            var Response = HttpContext.Current.Response;
            var Server = HttpContext.Current.Server;
            string fileName = Server.MapPath("/") + folderName + "\\" + strFileName;
            try
            {
                if (src == null || src.Tables.Count == 0) throw new Exception("dataset is null or has not table in dataset");


                for (int i = 0; i < src.Tables.Count; i++)
                {

                    if (i > 0)

                        fileName = fileName.Substring(0, fileName.IndexOf('.')) + i + fileName.Substring(fileName.IndexOf("."));
                    writer = new StreamWriter(fileName);
                    DataTable dt = src.Tables[i];
                    StringBuilder sb = new StringBuilder();
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        string colName = dt.Columns[j].ColumnName;
                        if (colName.IndexOf(',') > -1)
                            colName = colName.Insert(0, "\"").Insert(colName.Length + 1, "\"");
                        sb.Append(colName);
                        if (!colName.Equals(""))
                            if (j != dt.Columns.Count - 1)
                                sb.Append(",");
                    }
                    writer.WriteLine(sb.ToString());
                    sb = new StringBuilder();
                    string temp = "";
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        DataRow dr = dt.Rows[j];
                        for (int k = 0; k < dt.Columns.Count; k++)
                        {
                            object o = dr[k];
                            if (o != null)
                                temp = o.ToString();
                            if (temp.IndexOf(',') > -1)
                                temp = temp.Insert(0, "\"").Insert(temp.Length + 1, "\"");
                            sb.Append(temp);
                            if (k != dt.Columns.Count - 1)
                                sb.Append(",");
                        }
                        writer.WriteLine(sb.ToString());
                        sb = new StringBuilder();
                        csv = sb.ToString();
                    }
                    writer.Close();
                }

                string strFilePath = Server.MapPath("/") + folderName;
                if (!Directory.Exists(strFilePath))
                {
                    Directory.CreateDirectory(strFilePath);
                }

                var strFullFileName = Server.MapPath("/") + folderName + "\\" + fileName;
                //FullFileName = Server.MapPath(FileName);
                //FileName
                FileInfo DownloadFile = new FileInfo(strFullFileName);
                if (DownloadFile.Exists)
                {
                    Response.Clear();
                    Response.ClearHeaders();
                    Response.Buffer = false;
                    Response.ContentType = "application/octet-stream";
                    //Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(DownloadFile.FullName, System.Text.Encoding.ASCII));
                    Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.ASCII));
                    Response.AppendHeader("Content-Length", DownloadFile.Length.ToString());
                    Response.WriteFile(DownloadFile.FullName);
                    Response.Flush();
                    //Response.End();
                }
                else
                {
                    //not exist
                    throw new Exception("Export csv file does not exist!");
                }
            }

            catch (Exception ex)
            {
                throw new Exception("Save csv error", ex);
            }

            finally
            {
                if (writer != null) writer.Close();
            }
            return true;
        }

        /// <summary>
        /// List to DataTable
        /// </summary>
        /// <param name="entitys">entitys list</param>
        /// <returns></returns>
        public DataTable ListToDataTable<T>(List<T> entitys)
        {
            if (entitys == null || entitys.Count < 1)
            {
                throw new Exception("list is null");
            }
            //get first Propertie
            Type entityType = entitys[0].GetType();
            PropertyInfo[] entityProperties = entityType.GetProperties();
            //DataTable structure
            //
            DataTable dt = new DataTable();
            for (int i = 0; i < entityProperties.Length; i++)
            {
                //dt.Columns.Add(entityProperties[i].Name, entityProperties[i].PropertyType);
                dt.Columns.Add(entityProperties[i].Name);
            }
            //add entity to DataTable
            foreach (object entity in entitys)
            {
                //check type
                if (entity.GetType() != entityType)
                {
                    throw new Exception("type not same");
                }
                object[] entityValues = new object[entityProperties.Length];
                for (int i = 0; i < entityProperties.Length; i++)
                {
                    entityValues[i] = entityProperties[i].GetValue(entity, null);
                }
                dt.Rows.Add(entityValues);
            }
            return dt;
        }
    }
}
