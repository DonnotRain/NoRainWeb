using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using System.Configuration;
using System.Web;
using System.Threading.Tasks;
using System.Drawing;
using NoRain.Business.IBll;
using DefaultConnection;
using Newtonsoft.Json;
using NoRain.Business.WebBase;
using MainWeb.Filters;


namespace MainWeb.Controllers.API
{
    [ServiceValidate()]
    [MobileServiceMark]
    public class FileItemController : ApiController
    {
        private IFileItemBll fileItemBll;
        public FileItemController()
        {
            fileItemBll = DPResolver.Resolver<IFileItemBll>();
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns>结果</returns>
        [Route("API/FileItem/Upload")]
        public FileItem PostFormData()
        {
            return fileItemBll.UploadFile();
        }

        /// <summary>
        /// 获取下载文件通过文件Id
        /// </summary>
        /// <param name="fileId">文件Id</param>
        [Route("api/FileItem/DownloadById")]
        public void GetDownloadByFileId(string fileId)
        {
            fileItemBll.GetDownloadByFileId(fileId);
        }

        /// <summary>
        /// 删除附件
        /// </summary>
        /// <param name="fileItemId">文件表Id</param>
        /// <returns>文件实体</returns>
        [Route("api/FileItem/Delete")]
        public FileItem DeleteFileItem([FromBody]string fileItemId)
        {
            return fileItemBll.DeleteFileItem(fileItemId);
        }
    }
}