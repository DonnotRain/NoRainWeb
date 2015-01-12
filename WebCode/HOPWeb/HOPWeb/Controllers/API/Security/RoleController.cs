﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using HuaweiSoftware.WQT.WebBase;
using WQTRights;
using HuaweiSoftware.WQT.IBll;
using HuaweiSoftware.HOP.Models.Request;
using WQTWeb.Filters;

namespace WQTWeb.Controllers.API
{
    [ServiceValidate()]
    public class RoleController : ApiController
    {

        private IRoleBLL m_roleBll = DPResolver.Resolver<IRoleBLL>();

        public IEnumerable<Role> Get()
        {
            return m_roleBll.FindAll<Role>("WHERE CorpCode=@0", SysContext.CorpCode);
        }

        public object GetPager(int page, int rows, string name)
        {
            var pageResult = m_roleBll.GetRolePager(page, rows, name);

            return new { total = pageResult.TotalItems, rows = pageResult.Items };
        }
        // GET api/Role/5
        /// <summary>
        ///获取单个角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Role Get(int id)
        {
            return m_roleBll.Find<Role>("where id=@0", id);
        }

        [Route("api/RoleFunctions")]
        public IEnumerable<EasyuiTreeNode> GetRoles(int roleId)
        {
            return m_roleBll.GetRoleFunctions(roleId);
        }

        public object Post(Role entity)
        {
            entity = m_roleBll.Add(entity);
            return entity;
        }
        [Route("api/Role/SetRoleFunctions")]
        public bool PostRoles(RoleFunctionSetting roleFunction)
        {
            m_roleBll.SetRoleFunction(roleFunction);
            return true;
        }

        public object Put(Role entity)
        {
            entity = m_roleBll.Edit(entity);
            return entity;
        }

        public void Delete([FromBody]string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                m_roleBll.DeleteRoles(ids);
            }
            else
            {
                throw (new HttpResponseException(HttpStatusCode.NotAcceptable));
            }
        }
    }
}
