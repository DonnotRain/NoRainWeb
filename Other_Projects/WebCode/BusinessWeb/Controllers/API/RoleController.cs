using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using  Security_ConnectionString;
using iBangTeam.Business.IBll;
using WebBase.Models;
using WebBase;

namespace MainWeb.Controllers.API
{
    public class RoleController : ApiController
    {

        private IRoleBLL m_roleBll = DPResolver.Resolver<IRoleBLL>();

        public IEnumerable<Role> Get()
        {
            return m_roleBll.FindAll<Role>();
        }

        // GET api/Role/5
        /// <summary>
        ///获取单个角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Role Get(int id)
        {
            return m_roleBll.Find<Role>("id=@0", id);
        }

        [Route("api/RoleFunctions")]
        public IEnumerable<EasyuiTreeNode> GetRoles(int roleId)
        {
            return m_roleBll.GetRoleFunctions(roleId);
        }
    }
}
