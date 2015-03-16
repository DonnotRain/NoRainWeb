using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NoRain.Business.Models;
using DefaultConnection;

namespace NoRain.Business.IService
{
    public interface IFunctionService : IBaseService
    {
        IEnumerable<Function> GetRoleTreeFunctions(int roleId);
        IEnumerable<Function> GetFunctions();

        IEnumerable<Function> GetRoleFunctions(int roleId);


        IEnumerable<JsTreeNode> GetAllJsTreeData();

        void InsertFunction(Function func);

        void UpdateFunction(Function func);

    }
}
