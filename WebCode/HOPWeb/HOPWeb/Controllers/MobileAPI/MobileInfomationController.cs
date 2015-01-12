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
    public class MobileInformationController : ApiController
    {
        private IInfomationBll m_InformationBll;
        public MobileInformationController()
        {
            m_InformationBll = DPResolver.Resolver<IInfomationBll>();
        }
        /// <summary>
        /// 获取一个促销员的所有签到记录
        /// </summary>
        /// <param name="userName">促销员名字</param>
        /// <returns>促销员的所有签到记录</returns>
        public List<MKT_Infomation> GetInformation(string title)
        { 
            return m_InformationBll.GetInformationByName(SysContext.UserName,title);
        }

        /// <summary>
        /// 签到
        /// </summary>
        /// <param name="position">位置信息</param>
        /// <param name="type">签到类型：0表示上班</param>
        /// <returns>签到是否成功</returns>
        public MKT_Infomation PostInformation(InformationContract model)
        {
            return m_InformationBll.AddInformation(model);
        }
    }
}
