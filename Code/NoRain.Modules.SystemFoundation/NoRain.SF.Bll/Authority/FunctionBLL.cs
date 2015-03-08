
using NoRain.Business.IBll;
using NoRain.Business.WebBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NoRain.Business.IDal;
using NoRain.Business.Models;
using DefaultConnection;

namespace NoRain.Business.Bll
{
    public class FunctionBLL : CommonSecurityBLL, IFunctionBLL, IBaseBLL
    {
        private IBaseDao _dal;
        public FunctionBLL(ICommonSecurityDao dal)
        {
            this._dal = dal;
        }

        public IEnumerable<Function> GetFunctions()
        {
            var items = FindAll<Function>(string.Empty).OrderBy(m => m.Sort);

            //查找出父节点没有对应Function的记录
            var functionIds = items.Select(m => m.ID);

            var firstLevels = items.Where(m => !functionIds.Contains(m.PID));

            firstLevels.ToList().ForEach(m =>
            {
                m.children = GetFunctionChildren(items.ToList(), m.ID);
                m.Roles = GetFunctionRoles(m.ID);
                m.iconCls = "none " + m.ImageIndex + " icon-fixed-width";
            });

            return firstLevels;
        }

        public IEnumerable<Function> GetRoleTreeFunctions(int roleId)
        {
            var functionRoles = FindAll<Role_Function>("where Role_ID =@0 ", roleId);
            var items = FindAll<Function>("").Where(m => functionRoles.Select(func => func.Function_ID).Contains(m.ID)).OrderBy(m => m.ID);

            //查找出父节点没有对应Function的记录
            var functionIds = items.Select(m => m.ID);

            var firstLevels = items.Where(m => !functionIds.Contains(m.PID));

            firstLevels.ToList().ForEach(m =>
            {
                m.children = GetFunctionChildren(items.ToList(), m.ID);
                m.Roles = GetFunctionRoles(m.ID);
                m.iconCls = "none " + m.ImageIndex + " icon-fixed-width";
            });

            return firstLevels;
        }

        private List<Function> GetFunctionChildren(List<Function> srcItems, int pId)
        {
            var children = srcItems.Where(m => m.PID == pId).ToList();
            children.ForEach(m => srcItems.Remove(m));
            children.ForEach(m =>
            {
                m.children = GetFunctionChildren(srcItems, m.ID);
                m.Roles = GetFunctionRoles(m.ID);
                m.iconCls = "none " + m.ImageIndex + " icon-fixed-width";
            });

            return children;
        }

        private List<Role> GetFunctionRoles(int functionId)
        {
            var functionRoles = FindAll<Role_Function>("where Function_ID =@0 ", functionId).Select(funcRole => funcRole.Role_ID);

            return FindAll<Role>(string.Empty).Where(model => functionRoles.Contains(model.ID)).ToList();
        }

        public IEnumerable<Function> GetRoleFunctions(int roleId)
        {
            var functionRoles = FindAll<Role_Function>("where Role_ID =@0", roleId);
            var items = FindAll<Function>(string.Empty).Where(m => functionRoles.Select(func => func.Function_ID).Contains(m.ID)).OrderBy(m => m.ID);
            items.ToList().ForEach(m =>
            {
                var subfunctionRoles = FindAll<Role_Function>("where Function_ID =@0 ", m.ID);
                m.Roles = FindAll<Role>().Where(model => subfunctionRoles.Select(funcRole => funcRole.Role_ID).Contains(model.ID));
            });
            return items;
        }

        public void InsertFunction(Function sysFunction)
        {

            var codeItem = FindAll<Function>("where ControlID=@0", sysFunction.ControlID);
            if (codeItem.Count() > 0)
            {
                throw new ApiException(ResponseCode.SYSTEM_SERVER_ERROR, "编号重复");
            }
            var items = FindAll<Function>("where PID =@0", sysFunction.PID).OrderByDescending(m => m.ID);

            //获取最大的ID加1
            var maxId = Function.repo.ExecuteScalar<int>("select max(ID) AS ID from functions");
            sysFunction.ID = maxId + 1;

            if (string.IsNullOrEmpty(sysFunction.Path))
            {
                sysFunction.Path = string.Empty;
            }

            PetaPoco.Transaction scope = new PetaPoco.Transaction(_dal.DB);
            //使用事务方式提交
            using (scope)
            {
                Insert(sysFunction);

                List<Role_Function> roleFuncItems = new List<Role_Function>();
                if (sysFunction.RoleIds != null && sysFunction.RoleIds.Count() > 0)
                {
                    sysFunction.RoleIds.ToList().ForEach(m => roleFuncItems.Add(new Role_Function()
                    {
                        Role_ID = int.Parse(m),
                        Function_ID = sysFunction.ID
                    }));
                }
                Insert(roleFuncItems);

                scope.Complete();
            }
        }

        public void UpdateFunction(Function sysFunction)
        {

            var oldFunction = Find<Function>("where ID =@0", sysFunction.ID);
            if (string.IsNullOrEmpty(sysFunction.Path))
            {
                sysFunction.Path = string.Empty;
            }

            //使用事务方式提交
            using (PetaPoco.Transaction scope = new PetaPoco.Transaction(_dal.DB))
            {
                ///先更新实体
                Update(sysFunction);

                //再删除原来的对应关系
                Role_Function.repo.Execute("DELETE FROM Role_Function where Function_ID =@0 ", sysFunction.ID);

                List<Role_Function> roleFuncItems = new List<Role_Function>();
                if (sysFunction.RoleIds != null)
                {
                    sysFunction.RoleIds.ToList().ForEach(m =>
                    {
                        if (!string.IsNullOrEmpty(m))
                            roleFuncItems.Add(new Role_Function()
                            {
                                Role_ID = int.Parse(m),
                                Function_ID = sysFunction.ID
                            });
                    });

                }
                Insert(roleFuncItems);

                scope.Complete();
            }
        }


        public IEnumerable<Models.JsTreeNode> GetAllJsTreeData()
        {
            var items = FindAll<Function>(string.Empty).OrderBy(m => m.Sort);

            //查找出父节点没有对应Function的记录
            var functionIds = items.Select(m => m.ID);

            var firstLevels = items.Where(m => !functionIds.Contains(m.PID));

            firstLevels.ToList().ForEach(m =>
            {
                m.children = GetFunctionChildren(items.ToList(), m.ID);
                m.Roles = GetFunctionRoles(m.ID);
                m.iconCls = "none " + m.ImageIndex + " icon-fixed-width";
            });

            return firstLevels.Select(m => new JsTreeNode
            {
                id = m.ID.ToString(),
                text = m.Name,
                state = new { opened = items.Select(node => node.PID).Contains(m.ID), selected = false },
                icon = " " + m.ImageIndex,
                children = GetJsTreeFunctionChildren(items.ToList(), m.ID)
            });
        }

        private List<JsTreeNode> GetJsTreeFunctionChildren(List<Function> srcItems, int pId)
        {
            var children = srcItems.Where(m => m.PID == pId).ToList();
            children.ForEach(m => srcItems.Remove(m));


            return children.Select(m => new JsTreeNode
            {
                id = m.ID.ToString(),
                text = m.Name,
                state = new { opened = srcItems.Select(node => node.PID).Contains(m.ID), selected = false },
                icon = " " + m.ImageIndex,
                children = GetJsTreeFunctionChildren(srcItems, m.ID)
            }).ToList();
        }

    }
}
