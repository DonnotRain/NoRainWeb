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
using NoRain.Business.Model.Response;
using DefaultConnection;
using AttributeRouting.Web.Http;

namespace MainWeb.Controllers.API
{
    [ServiceValidate()]
    public class SysUserController : ApiController
    {
        private ISysUserBll m_SysUserBll;
        public SysUserController()
        {
            m_SysUserBll = DPResolver.Resolver<ISysUserBll>();
        }

        public object GetPager(int page, int rows, string name)
        {
            var pageResult = m_SysUserBll.GetSysUserPager(page, rows, name);

            return new { total = pageResult.TotalItems, rows = pageResult.Items };
        }
        [GET("API/SysUser/DataTablePager")]
        public object GetDataTablePager([FromUri]DataTablesRequest reqestParams, [FromUri]RolePagerCondition condition)
        {
            var pageResult = m_SysUserBll.GetSysUserPager(reqestParams.start / reqestParams.length + 1,reqestParams.length, condition.Name);


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
            entity = m_SysUserBll.Add(entity);
            return entity;
        }


        public object Put(SysUser entity)
        {
            entity = m_SysUserBll.Edit(entity);
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
                    var item = m_SysUserBll.Find<SysUser>("WHERE ID=@0", m);
                    if (item != null) items.Add(item);
                });
                m_SysUserBll.Delete(items);
            }
            else
            {
                throw (new HttpResponseException(HttpStatusCode.NotAcceptable));
            }
        }
    }
}
