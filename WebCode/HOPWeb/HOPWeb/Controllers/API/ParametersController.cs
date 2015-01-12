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
    public class ParameterController : ApiController
    {
        private IParameterBll m_ParameterBll;
        public ParameterController()
        {
            m_ParameterBll = DPResolver.Resolver<IParameterBll>();
        }
        public object GetAll()
        {
            var result = m_ParameterBll.FindAll<Parameter>("WHERE CorpCode=@0", SysContext.CorpCode);

            return result;
        }

        public object GetByName(string name)
        {
            var result = m_ParameterBll.GetByName(name);

            return result;
        }

        public object GetPager(int page, int rows, string name, string value)
        {

            var pageResult = m_ParameterBll.GetParameterPager(page, rows, name, value);

            return new { total = pageResult.TotalItems, rows = pageResult.Items };
        }

        public object Post(Parameter Parameter)
        {
            Parameter = m_ParameterBll.Add(Parameter);
            return Parameter;
        }

        public object Put(Parameter Parameter)
        {
            Parameter = m_ParameterBll.Edit(Parameter);
            return Parameter;
        }

        public void Delete([FromBody]string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var itemsStr = ids.Split(',').ToList();
                var items = new List<Parameter>();
                itemsStr.ForEach(m =>
                {
                    var item = m_ParameterBll.Find<Parameter>("WHERE ID=@0", int.Parse(m));
                    if (item != null) items.Add(item);
                });
                m_ParameterBll.Delete(items);
            }
            else
            {
                throw (new HttpResponseException(HttpStatusCode.NotAcceptable));
            }
        }
    }
}
