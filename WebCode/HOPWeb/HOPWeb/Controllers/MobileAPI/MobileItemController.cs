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
    public class MobileItemController : ApiController
    {
        private IItemBll m_ItemBll;
        public MobileItemController()
        {
            m_ItemBll = DPResolver.Resolver<IItemBll>();
        }
        /// <summary>
        /// 获取一个促销员的所有签到记录
        /// </summary>
        /// <param name="userName">促销员名字</param>
        /// <returns>促销员的所有签到记录</returns>
        public List<MKT_Item> GetItem()
        {
            return m_ItemBll.FindAll<MKT_Item>("WHERE  IsDeleted=0 and CORPCODE=@0 ORDER BY CreateTime DESC", SysContext.CorpCode).ToList();
        }
    }
}
