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
using WQTRights;
using WQTWeb.Filters;

namespace WQTWeb.Controllers.API
{
    [ServiceValidate()]
    public class OperatorController : ApiController
    {
        private IOperatorBll m_OperatorBll;
        public OperatorController()
        {
            m_OperatorBll = DPResolver.Resolver<IOperatorBll>();
        }

        public object GetPager(int page, int rows, string name,int? roleId)
        {
            var pageResult = m_OperatorBll.GetOperatorPager(page, rows, name, roleId);

            return new { total = pageResult.TotalItems, rows = pageResult.Items };
        }

        public object Post(User entity)
        {
            entity = m_OperatorBll.Add(entity);
            return entity;
        }


        public object Put(User entity)
        {
            entity = m_OperatorBll.Edit(entity);
            return entity;
        }

        public void Delete([FromBody]string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var itemsStr = ids.Split(',').ToList();
                var items = new List<User>();
                itemsStr.ForEach(m =>
                {
                    var item = m_OperatorBll.Find<User>("WHERE ID=@0", int.Parse(m));
                    if (item != null) items.Add(item);
                });
                m_OperatorBll.Delete(items);
            }
            else
            {
                throw (new HttpResponseException(HttpStatusCode.NotAcceptable));
            }
        }
    }
}
