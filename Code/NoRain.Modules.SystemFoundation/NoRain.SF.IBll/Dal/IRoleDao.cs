using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoRain.Business.IDal
{
    public interface IRoleDao : ICommonDao
    {
         void DeleteRoleUser(int roleId);
         void DeleteRoleFunction(int roleId);
    }
}
