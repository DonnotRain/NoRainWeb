using DefaultConnection;
namespace NoRain.Business.IBll
{
    public interface IFileItemBll : ICommonService
    {
        /// <summary>
        /// 上传
        /// </summary>
        /// <returns></returns>
        FileItem UploadFile();

        ///// <summary>
        ///// 按文件名进行文件下载
        ///// </summary>
        ///// <param name="fileName">文件名</param>
        //void GetDownloadByFileName(string fileName);

        /// <summary>
        /// 获取下载文件通过文件Id
        /// </summary>
        /// <param name="fileId">文件Id</param>
        void GetDownloadByFileId(string fileId);


        /// <summary>
        /// 获取下载根据entityId
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns>文件实体</returns>
        FileItem DeleteFileItem(string fileItemId);
    }
}
