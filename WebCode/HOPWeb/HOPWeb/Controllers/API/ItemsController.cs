using DefaultConnection;
using HuaweiSoftware.WQT.Bll;
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

namespace WQTWeb.Controllers.API
{
    [ServiceValidate()]
    public class ItemsController : ApiController
    {
        private IItemBll m_ItemsBll;
        public ItemsController()
        {
            m_ItemsBll = new ItemBll();//DPResolver.Resolver<IItemBll>();
        }

        public object GetPager(int page, int rows, int? begin, int? end, string name, string code, string type)
        {
            var pageResult = m_ItemsBll.GetItemPager(page, rows, begin, end, name, code, type);

            return new { total = pageResult.TotalItems, rows = pageResult.Items };
        }

        public object Post(MKT_Item entity)
        {
            entity = m_ItemsBll.Add(entity);
            return entity;
        }

        public object Put(MKT_Item entity)
        {
            entity = m_ItemsBll.Edit(entity);

            return entity;
        }

        public void Delete([FromBody]string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                m_ItemsBll.LogicDelete(ids);         
            }
            else
            {
                throw (new HttpResponseException(HttpStatusCode.NotAcceptable));
            }
        }
    }
}
