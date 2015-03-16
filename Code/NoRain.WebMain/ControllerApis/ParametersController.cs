
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

using DefaultConnection;

using MainWeb.Filters;
using NoRain.Business.Model.Request;
using NoRain.Toolkits;
using NoRain.Business.Model.Response;
using NoRain.Business.IService;
using NoRain.Business.WebBase;
using AttributeRouting.Web.Http;

namespace MainWeb.Controllers.API
{
    /// <summary>
    /// 系统参数相关操作
    /// </summary>
    [ServiceValidate]
    public class ParameterController : ApiController
    {
        private IParameterService m_ParameterService;
        public ParameterController()
        {
            m_ParameterService = DPResolver.Resolver<IParameterService>();
        }
        public object GetAll()
        {
            var result = m_ParameterService.FindAll<SysParameter>("");

            return result;
        }

        public object GetByName(string name)
        {
            var result = m_ParameterService.GetByName(name);

            return result;
        }

        public object GetPager(int page, int rows, string name, string value)
        {

            var pageResult = m_ParameterService.GetParameterPager(page, rows, name, value);

            return new { total = pageResult.TotalItems, rows = pageResult.Items };
        }
        [GET("API/Parameter/DataTablePager")]
        public object GetDataTablePager([FromUri]DataTablesRequest reqestParams, [FromUri]ParameterPagerCondition condition)
        {
            var pageResult = m_ParameterService.GetParameterPager(reqestParams, condition);

            return new DataTablePager<SysParameter>()
            {
                draw = reqestParams.draw,
                recordsTotal = pageResult.TotalItems,
                recordsFiltered = pageResult.TotalItems,
                data = pageResult.Items
            };
        }

        [GET("API/Parameter/ExportExcel")]
        public void GetExportPager([FromUri]DataTablesRequest reqestParams, [FromUri]ParameterPagerCondition condition)
        {
            var pageResult = m_ParameterService.GetParameterPager(reqestParams, condition);

            ExcelOutputTool.HandleItems(pageResult.Items,"系统参数明细统计");
        }

        public object Post(SysParameter parameter)
        {
            parameter = m_ParameterService.Add(parameter);

            return parameter;
        }

        public object Put(SysParameter parameter)
        {
            parameter = m_ParameterService.Edit(parameter);

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
                    var item = m_ParameterService.Find<SysParameter>("WHERE ID=@0", Guid.Parse(m));
                    if (item != null) items.Add(item);
                });
                m_ParameterService.Delete(items);
            }
            else
            {
                throw (new HttpResponseException(HttpStatusCode.NotAcceptable));
            }
        }
    }
}
