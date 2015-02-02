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
using NoRainRights;
using MainWeb.Filters;

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

        public object GetPager(int page, int rows, string name,int? roleId)
        {
            var pageResult = m_SysUserBll.GetSysUserPager(page, rows, name, roleId);

            return new { total = pageResult.TotalItems, rows = pageResult.Items };
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
                    var item = m_SysUserBll.Find<SysUser>("WHERE ID=@0", int.Parse(m));
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
