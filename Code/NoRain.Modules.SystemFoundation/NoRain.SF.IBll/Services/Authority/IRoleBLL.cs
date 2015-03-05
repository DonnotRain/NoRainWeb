using NoRain.Business.Models.Request;
using NoRain.Business.WebBase;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NoRainRights;
using NoRain.Business.Models;

namespace NoRain.Business.IBll
{
    public interface IRoleBLL : IBaseBLL
    {
        IEnumerable<JsTreeNode> GetRoleFunctions(int roleId);

        IEnumerable<Role> GetUserRoles(Guid userId);

        Page<Role> GetRolePager(int page, int rows, string name);

        Role Edit(Role entity);

        Role Add(Role entity);

        void SetRoleFunction(RoleFunctionSetting roleFunction);

        void DeleteRoles(string roleids);
    }
}
