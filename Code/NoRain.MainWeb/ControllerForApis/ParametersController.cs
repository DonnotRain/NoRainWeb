using DefaultConnection;
using NoRain.Business.IBll;
using NoRain.Business.WebBase;
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
using MainWeb.Filters;
using NoRain.Business.Model.Request;

namespace MainWeb.Controllers.API
{
    /// <summary>
    /// 系统参数相关操作
    /// </summary>
    [ServiceValidate]
    public class ParameterController : ApiController
    {
        private IParameterBll m_ParameterBll;
        public ParameterController()
        {
            m_ParameterBll = DPResolver.Resolver<IParameterBll>();
        }
        public object GetAll()
        {
            var result = m_ParameterBll.FindAll<SysParameter>("");

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
        [Route("API/Parameter/DataTablePager")]
        public object GetDataTablePager([FromUri]DataTablesRequest reqestParams)
        {

            var pageResult = m_ParameterBll.GetParameterPager(reqestParams);

            return new
            {
                draw = reqestParams.draw,
                recordsTotal = pageResult.TotalItems,
                recordsFiltered = pageResult.TotalItems,
                data = pageResult.Items
            };
        }


        public object Post(SysParameter parameter)
        {
            parameter = m_ParameterBll.Add(parameter);

            return parameter;
        }

        public object Put(SysParameter parameter)
        {
            parameter = m_ParameterBll.Edit(parameter);

            return parameter;
        }

        public void Delete([FromBody]string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var itemsStr = ids.Split(',').ToList();
                var items = new List<SysParameter>();
                itemsStr.ForEach(m =>
                {
                    var item = m_ParameterBll.Find<SysParameter>("WHERE ID=@0", int.Parse(m));
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
