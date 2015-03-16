using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using NoRain.Business.WebBase;

using NoRain.Business.IService;
using NoRain.Business.Models.Request;
using MainWeb.Filters;
using NoRain.Business.Models;
using NoRain.Business.Model.Request;
using NoRain.Business.Model.Response;
using DefaultConnection;
using AttributeRouting.Web.Http;

namespace MainWeb.Controllers.API
{
    [ServiceValidate()]
    public class RoleController : ApiController
    {

        private IRoleService m_roleService = DPResolver.Resolver<IRoleService>();

        [GET("API/Role/All")]
        public IEnumerable<Role> Get()
        {
            return m_roleService.FindAll<Role>("");
        }

        public object GetPager(int page, int rows, string name)
        {
            var pageResult = m_roleService.GetRolePager(page, rows, name);

            return new { total = pageResult.TotalItems, rows = pageResult.Items };
        }

        [GET("API/Role/DataTablePager")]
        public object GetDataTablePager([FromUri]DataTablesRequest reqestParams, [FromUri]RolePagerCondition condition)
        {
            var pageResult = m_roleService.GetRolePager(reqestParams.start / reqestParams.length + 1, reqestParams.length, condition.Name);


            return new DataTablePager<Role>()
            {
                draw = reqestParams.draw,
                recordsTotal = pageResult.TotalItems,
                recordsFiltered = pageResult.TotalItems,
                data = pageResult.Items
            };
        }

        // GET api/Role/5
        /// <summary>
        ///获取单个角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Role Get(int id)
        {
            return m_roleService.Find<Role>("where id=@0", id);
        }

        public object Post(Role entity)
        {
            entity = m_roleService.Add(entity);
            return entity;
        }
        [GET("api/Role/SetRoleFunctions")]
        public bool PostRoles(RoleFunctionSetting roleFunction)
        {
            m_roleService.SetRoleFunction(roleFunction);
            return true;
        }

        public object Put(Role entity)
        {
            entity = m_roleService.Edit(entity);
            return entity;
        }

        public void Delete([FromBody]string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                m_roleService.DeleteRoles(ids);
            }
            else
            {
                throw (new HttpResponseException(HttpStatusCode.NotAcceptable));
            }
        }
    }
}
