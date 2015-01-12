using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaweiSoftware.WQT.IBll
{
    public interface IAppDownloadBll : ICommonBLL
    {
        void DownloadApp(string type);

        string GetAppVersion(string type);
    }
}
