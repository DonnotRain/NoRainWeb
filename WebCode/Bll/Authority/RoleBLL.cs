using HuaweiSoftware.HOP.Models.Request;
using iBangTeam.Business.IBll;
using iBangTeam.Business.IDal;
using WebBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Security_ConnectionString;

namespace iBangTeam.Business.Bll
{
    public class RoleBLL : CommonSecurityBLL, IRoleBLL
    {
        private IRoleDal m_dal;
        public RoleBLL(IRoleDal dal)
        {
            m_dal = dal;
        }
        public IEnumerable<EasyuiTreeNode> GetRoleFunctions(int roleId)
        {
            var functions = new FunctionBLL().GetRoleFunctions(roleId).ToList().Select(m => m.ID).ToArray();
            var allFunction = FindAll<Function>();
            return ConvertFunctionsToTree(allFunction, functions, -1);
        }

        private IEnumerable<EasyuiTreeNode> ConvertFunctionsToTree(IEnumerable<Function> allFunctions, int[] functions, int pId)
        {
            return allFunctions.Where(m => m.PID == pId).Select(m => new EasyuiTreeNode()
            {
                id = m.ID.ToString(),
                text = m.Name,
                state = "open",
                @checked = functions.Contains(m.ID),
                iconCls = m.ImageIndex + " none",
                children = ConvertFunctionsToTree(allFunctions, functions, m.ID)
            });
        }

        public IEnumerable<Role> GetUserRoles(int userId)
        {
            var roleIds = FindAll<User_Role>("where User_ID=@0", userId);

            var items = FindAll<Role>(string.Empty).Where(m => roleIds.Select(func => func.Role_ID).Contains(m.ID)).OrderBy(m => m.ID);

            return items;
        }


        public PetaPoco.Page<Role> GetRolePager(int page, int rows, string name)
        {
            var sql = PetaPoco.Sql.Builder.Append("SELECT s.* FROM Role s")
                .Append("Where 1=1");

            if (!string.IsNullOrEmpty(name))
            {
                name = "%" + name + "%";
                sql.Append(" And (s.Name like @0 or s.FullName like @0)", name);
            }


            var pageResult = FindAllByPage<Role>(sql.SQL, rows, page, sql.Arguments);
            //page.Items.ForEach(m =>
            //{
            //    var store = Find<SEC_Store>("Where ID=@0", m.StoreId);
            //    m.StoreName = store != null ? store.StoreName : "无相关门店";
            //});
            return pageResult;
        }


        public Role Edit(Role entity)
        {
            var oldEntity = Role.First("Where ID=@0", entity.ID);

            entity.Update();
            return entity;
        }

        public Role Add(Role entity)
        {
            entity.Insert();
            return entity;
        }

        public void SetRoleFunction(RoleFunctionSetting roleFunction)
        {

            //使用事务方式提交
            using (PetaPoco.Transaction scope = new PetaPoco.Transaction(DBManage.SecurityDB))
            {
                Role_Function.repo.Execute("DELETE FROM Role_Function WHERE  Role_ID=@0", roleFunction.RoleId);

                List<Role_Function> roleFuncItems = new List<Role_Function>();
                if (roleFunction.FunctionId != null)
                {
                    roleFunction.FunctionId.ToList().ForEach(m =>
                    {

                        roleFuncItems.Add(new Role_Function()
                        {
                            Role_ID = roleFunction.RoleId,
                            Function_ID = m
                        });
                    });

                }
                var result = Insert(roleFuncItems);
                scope.Complete();
            }
        }

        public void DeleteRoles(string roleids)
        {
            var itemsStr = roleids.Split(',').ToList();
            var items = new List<Role>();

            var roleFunctions = new List<Role_Function>();
            using (PetaPoco.Transaction scope = new PetaPoco.Transaction(DBManage.SecurityDB))
            {

                itemsStr.ForEach(m =>
                {
                    var roleId = int.Parse(m);
                    var item = Find<Role>("WHERE ID=@0", roleId);
                    if (item != null) items.Add(item);

                    m_dal.DeleteRoleUser(roleId);
                    m_dal.DeleteRoleFunction(roleId);
                });

                Delete(items);
                scope.Complete();
            }
        }
    }
}
