using NoRain.Business.IService;
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
using NoRain.Business.Model.Response;
using DefaultConnection;
using AttributeRouting.Web.Http;

namespace MainWeb.Controllers.API
{
    [ServiceValidate()]
    public class SysUserController : ApiController
    {
        private ISysUserService m_SysUserService;
        public SysUserController(ISysUserService sysUserService)
        {
            m_SysUserService = sysUserService;
        }

        public object GetPager(int page, int rows, string name)
        {
            var pageResult = m_SysUserService.GetSysUserPager(page, rows, name);

            return new { total = pageResult.TotalItems, rows = pageResult.Items };
        }
        [GET("API/SysUser/DataTablePager")]
        public object GetDataTablePager([FromUri]DataTablesRequest reqestParams, [FromUri]RolePagerCondition condition)
        {
            var pageResult = m_SysUserService.GetSysUserPager(reqestParams.start / reqestParams.length + 1,reqestParams.length, condition.Name);


            return new DataTablePager<SysUser>()
            {
                draw = reqestParams.draw,
                recordsTotal = pageResult.TotalItems,
                recordsFiltered = pageResult.TotalItems,
                data = pageResult.Items
            };
        }
        public object Post(SysUser entity)
        {
            entity = m_SysUserService.Add(entity);
            return entity;
        }


        public object Put(SysUser entity)
        {
            entity = m_SysUserService.Edit(entity);
            return entity;
        }

        public void Delete([FromBody]string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var itemsStr = ids.Split(',').ToList();
                var items = new List<SysUser>();
                itemsStr.ForEach(m =>
                {
                    var item = m_SysUserService.Find<SysUser>("WHERE ID=@0", m);
                    if (item != null) items.Add(item);
                });
                m_SysUserService.Delete(items);
            }
            else
            {
                throw (new HttpResponseException(HttpStatusCode.NotAcceptable));
            }
        }
    }
}
