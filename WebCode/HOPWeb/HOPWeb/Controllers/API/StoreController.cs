using DefaultConnection;
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
    public class StoreController : ApiController
    {
        private IStoreBll m_StoreBll;
        public StoreController()
        {
            m_StoreBll = DPResolver.Resolver<IStoreBll>();
        }
        public object GetAll()
        {
            var result = m_StoreBll.FindAll<SEC_Store>("WHERE CorpCode=@0", SysContext.CorpCode);

            return result;
        }

        public object GetPager(int page, int rows, int? begin, int? end, string name, string address, string belongTo)
        {

            var pageResult = m_StoreBll.GetStorePager(page, rows, begin, end, name, address, belongTo);

            return new { total = pageResult.TotalItems, rows = pageResult.Items };
        }



        public object Post(SEC_Store store)
        {
            store = m_StoreBll.Add(store);
            return store;
        }


        public object Put(SEC_Store store)
        {
            store = m_StoreBll.Edit(store);
            return store;
        }

        public void Delete([FromBody]string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var itemsStr = ids.Split(',').ToList();
                var items = new List<SEC_Store>();
                itemsStr.ForEach(m =>
                {
                    var item = m_StoreBll.Find<SEC_Store>("WHERE ID=@0", int.Parse(m));
                    if (item != null) items.Add(item);
                });
                m_StoreBll.Delete(items);
            }
            else
            {
                throw (new HttpResponseException(HttpStatusCode.NotAcceptable));
            }
        }
    }
}
