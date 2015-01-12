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
    public class MobileAttendenceController : ApiController
    {
        private IAttendenceBll m_AttendenceBll;
        public MobileAttendenceController()
        {
            m_AttendenceBll = DPResolver.Resolver<IAttendenceBll>();
        }
        /// <summary>
        /// 获取一个促销员的所有签到记录
        /// </summary>
        /// <param name="userName">促销员名字</param>
        /// <returns>促销员的所有签到记录</returns>
        public List<AttendenceInfo> GetAttendence()
        {
            return m_AttendenceBll.GetAttendenceByName(SysContext.UserName);
        }

        /// <summary>
        /// 签到
        /// </summary>
        /// <param name="position">位置信息</param>
        /// <param name="type">签到类型：0表示上班</param>
        /// <returns>签到是否成功</returns>
        public ATD_Attendence PostAttendence(AttendenceContract model)
        {
            return m_AttendenceBll.AddAttendence(model);
        }
    }
}
