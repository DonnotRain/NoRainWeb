using HuaweiSoftware.HOP.Models.Request;
using WebBase;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Security_ConnectionString;
using Security_ConnectionString;

namespace iBangTeam.Business.IBll
{
    public interface IRoleBLL : IBaseBLL
    {
        IEnumerable<EasyuiTreeNode> GetRoleFunctions(int roleId);

        IEnumerable<Role> GetUserRoles(int userId);

        Page<Role> GetRolePager(int page, int rows, string name);

        Role Edit(Role entity);

        Role Add(Role entity);

        void SetRoleFunction(RoleFunctionSetting roleFunction);

        void DeleteRoles(string roleids);
    }
}
