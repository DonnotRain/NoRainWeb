using DefaultConnection;
using HuaweiSoftware.HOP.Model;
using HuaweiSoftware.HOP.Model.Request;
using HuaweiSoftware.WQT.IBll;
using HuaweiSoftware.WQT.WebBase;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WQTWeb.Filters;

namespace WWQTWeb.Controllers.MobileAPI
{
    [MobileValidate()]
    [MobileServiceMark]
    public class MobileRequestController : ApiController
    {
        private IRequestBll m_RequestBll;
        public MobileRequestController()
        {
            m_RequestBll = DPResolver.Resolver<IRequestBll>();
        }
        /// <summary>
        /// 获取一个促销员的所有签到记录
        /// </summary>
        /// <param name="userName">促销员名字</param>
        /// <returns>促销员的所有签到记录</returns>
        public List<MKT_Request> GetRequest(string title)
        {
            return m_RequestBll.GetRequestByName(SysContext.UserName,title);
        }

        /// <summary>
        /// 签到
        /// </summary>
        /// <param name="position">位置信息</param>
        /// <param name="type">签到类型：0表示上班</param>
        /// <returns>签到是否成功</returns>
        public MKT_Request PostRequest(RequestContract model)
        {
            return m_RequestBll.AddRequest(model);
        }
    }
}
