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
    public class SellController : ApiController
    {
        private ISaleBll m_SellBll;
        public SellController()
        {
            m_SellBll = DPResolver.Resolver<ISaleBll>();
        }

        /// <summary>
        /// 获取所有商品类型
        /// </summary>
        /// <param name="corpCode">公司编号</param>
        /// <returns>商品类型集合</returns>
        [Route("api/sell/GetCategoryType")]
        public List<CategoryItem> GetCategoryType(string corpCode)
        {
            return m_SellBll.GetCategoryItems(corpCode);
        }

        [Route("api/sell/GetItems")]
        public object GetItems(string corpCode, string type, string barCode)
        {
            return m_SellBll.GetItems(corpCode, type, barCode);
        }

        /// <summary>
        /// 获取一个促销员的所有签到记录
        /// </summary>
        /// <param name="userName">促销员名字</param>
        /// <returns>促销员的所有签到记录</returns>
        public List<MKT_Sell> GetSell(string name)
        {
            return m_SellBll.GetSellByName(SysContext.UserName,name);
        }

        /// <summary>
        /// 签到
        /// </summary>
        /// <param name="position">位置信息</param>
        /// <param name="type">签到类型：0表示上班</param>
        /// <returns>签到是否成功</returns>
        public MKT_Sell PostSell(SellContract model)
        {
            return m_SellBll.AddSell(model);
        }
    }
}
