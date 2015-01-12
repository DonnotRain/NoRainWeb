using HuaweiSoftware.WQT.CommonToolkit;
using HuaweiSoftware.WQT.IBll;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace HuaweiSoftware.WQT.Bll
{
    public class AppDownloadBll : CommonBLL, IAppDownloadBll
    {
        public void DownloadApp(string type)
        {
            string value = type.ToUpper();
            FileInfo fileInfo;
            switch (value)
            {
                case "ANDROID":
                    fileInfo = new FileInfo(ApiConfig.AndroidPath);
                    DownloadHelp(ApiConfig.AndroidPath, fileInfo.Name);
                    break;
                case "IOS":
                    fileInfo = new FileInfo(ApiConfig.iOSPath);
                    DownloadHelp(ApiConfig.AndroidPath, fileInfo.Name);
                    break;
                default :
                    return;
            }
        }

        public string GetAppVersion(string type)
        {
            string value = type.ToUpper();
            string version = "0";
            switch (value)
            {
                case "ANDROID":
                    version = ApiConfig.AndroidVersion;
                    break;
                case "IOS":
                    version = ApiConfig.iOSVersion;
                    break;
                default:
                    return "0";
            }

            return version;
        }

        private void DownloadHelp(string filePath, string fullName)
        {
            FileInfo fileInfo = new FileInfo(filePath);

            if (!fileInfo.Exists)
            {
                throw new Exception("文件不存在");
            }

            FileStream fileStream = new FileStream(filePath, FileMode.Open);
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, bytes.Length);
            fileStream.Close();

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + fullName);
            HttpContext.Current.Response.AddHeader("Content-Length", bytes.Length.ToString());
            HttpContext.Current.Response.Charset = "UTF-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default;
            HttpContext.Current.Response.ContentType = HuaweiSoftware.WQT.CommonToolkit.CommonToolkit.GetContentType(fileInfo.Extension.Remove(0, 1));
            HttpContext.Current.Response.BinaryWrite(bytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
    }
}
