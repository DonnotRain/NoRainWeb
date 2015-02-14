using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NoRainRights;
using NoRain.Business.Models;

namespace NoRain.Business.IBll
{
    public interface IFunctionBLL : IBaseBLL
    {
        IEnumerable<Function> GetRoleTreeFunctions(int roleId);
        IEnumerable<Function> GetFunctions();

        IEnumerable<Function> GetRoleFunctions(int roleId);


        IEnumerable<JsTreeNode> GetAllJsTreeData();

        void InsertFunction(Function func);

        void UpdateFunction(Function func);

    }
}
