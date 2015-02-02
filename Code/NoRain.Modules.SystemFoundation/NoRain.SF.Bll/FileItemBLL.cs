using DefaultConnection;
using NoRain.Business.CommonToolkit;
using NoRain.Business.IBll;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace NoRain.Business.Bll
{
    public class FileItemBLL : CommonBLL, IFileItemBll
    {
        private ICommonSecurityBLL m_securityBLL;
        private readonly string uploadPath =SysConfigs.UploadPath;
        public FileItemBLL(ICommonSecurityBLL securityBLL)
        {
            m_securityBLL = securityBLL;
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns>结果</returns>
        public FileItem UploadFile()
        {
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            FileItem fileItem = new FileItem();
            HttpRequest request = System.Web.HttpContext.Current.Request;
            HttpFileCollection fileCollection = request.Files;
            System.Collections.Specialized.NameValueCollection nameValueCollection = request.Form;
            if (fileCollection.Count > 0)
            {
                for (int i = 0; i < fileCollection.Count; i++)
                {
                    HttpPostedFile httpPostedFile = fileCollection[i];
                    string filename = Path.GetFileName(httpPostedFile.FileName);

                    fileItem.FileName = HttpUtility.UrlDecode(filename);// 获取文件全名
                    fileItem.Extension = Path.GetExtension(filename);// 文件扩展名

                    var relativePath = Guid.NewGuid() + "_" + fileItem.FileName;
                    string filePath = Path.Combine(uploadPath, relativePath);

                    fileItem.FilePath = relativePath;// 文件路径
                    httpPostedFile.SaveAs(filePath);
                    fileItem.Size = httpPostedFile.InputStream.Length / 1024;
                }
            }
            //var isSingle = false;
            //if (!String.IsNullOrWhiteSpace(nameValueCollection["IsSingle"]))
            //{
            //    isSingle = bool.Parse(nameValueCollection["IsSingle"].ToString());
            //}

            //if (isSingle)
            //{
            //    var item = this.Find<FileItem>("");
            //    this.Delete(item);
            //}

            this.Insert<FileItem>(fileItem);

            return fileItem;
        }

        ///// <summary>
        ///// 按文件名进行文件下载
        ///// </summary>
        ///// <param name="fileName">文件名</param>
        //public void GetDownloadByFileName(string fileName)
        //{
        //    FileItem fileItem = new FileItem();
        //    if (!string.IsNullOrWhiteSpace(fileName))
        //    {
        //        if (this.FindAll<FileItem>(o => o.FullName.Contains(fileName)).Count() > 0)
        //        {
        //            fileItem = this.FindAll<FileItem>(o => o.FullName.Contains(fileName)).FirstOrDefault();
        //        }
        //    }
        //    string fullName = fileItem.FullName;
        //    string filePath = fileItem.RelativePath;
        //    DownloadHelp(filePath, fullName);
        //}

        /// <summary>
        /// 获取下载文件通过文件Id
        /// </summary>
        /// <param name="fileId">文件Id</param>
        public void GetDownloadByFileId(string fileId)
        {
            FileItem fileItem = string.IsNullOrWhiteSpace(fileId) ? null : this.Find<FileItem>("WHERE ID=@0", fileId);

            if (fileItem != null)
            {
                string fullName = fileItem.FileName;
                string filePath = Path.Combine(uploadPath, fileItem.FilePath); ;
                DownloadHelp(filePath, fullName);
            }
            else
            {
                throw new Exception("文件不存在");
            }
        }

        /// <summary>
        /// 删除附件
        /// </summary>
        /// <param name="fileItemId">文件表Id</param>
        /// <returns>文件实体</returns>
        public FileItem DeleteFileItem(string fileItemId)
        {
            FileItem fileItem = this.FindAll<FileItem>().FirstOrDefault();
            string filePath = Path.Combine(uploadPath, fileItem.FilePath);

            this.Delete<FileItem>(fileItem);

            // 删除对应的文件
            FileInfo fileInfo = new FileInfo(filePath);
            if (fileInfo.Exists)
            {
                File.Delete(filePath);
            }

            return fileItem;
        }

        /// <summary>
        /// 下载帮助类
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="fullName">文件名</param>
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
            // HttpContext.Current.Response.AddHeader("Content-Length", bytes.Length.ToString());
            HttpContext.Current.Response.Charset = "UTF-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default;
            HttpContext.Current.Response.ContentType = CommonToolkit.CommonToolkit.GetContentType(fileInfo.Extension.Remove(0, 1));
            HttpContext.Current.Response.BinaryWrite(bytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

    }
}
