using iBangTeam.Business.IBll;
using WebBase;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Security_ConnectionString;

namespace iBangTeam.Business.Bll
{
    public class OperatorBll : CommonSecurityBLL, IOperatorBll
    {
        private IRoleBLL m_roleBll;

        public OperatorBll(IRoleBLL roleBll)
        {
            m_roleBll = roleBll;
        }
        public PetaPoco.Page<Operator> GetOperatorPager(int pageIndex, int pageSize, string name, int? roleId)
        {

            var sql = PetaPoco.Sql.Builder.Append("SELECT s.* FROM Users s")
                .Append(" Where 1=1 ");


            if (!string.IsNullOrEmpty(name))
            {
                name = "%" + name + "%";
                sql.Append(" And (s.Name like @0 or s.FullName like @0)", name);
            }

            if (roleId.HasValue)
            {
                sql.Append(" And s.ID in (SELECT  User_ID FROM User_Role WHERE Role_ID=@0)", roleId);
            }


            var page = FindAllByPage<Operator>(sql.SQL, pageSize, pageIndex, sql.Arguments);


            page.Items.ForEach(m =>
            {
                var roles = m_roleBll.GetUserRoles(m.ID);
                m.Roles = roles;
                m.RoleIds = roles.Select(r => r.ID.ToString()).ToArray();
            });

            return page;
        }

        public Operator Add(Operator entity)
        {
            entity.Password = CommonToolkit.CommonToolkit.GetMD5Password(entity.Password);

            entity.IsExpire = false;

            //判断编号是否重复
            var repeatItems = FindAll<Operator>("Name=@0" , entity.Name);
            if (repeatItems.Count() != 0) throw new ApiException(ResponseCode.SYSTEM_Request_Interval);

            using (PetaPoco.Transaction transac = new Transaction(DBManage.SecurityDB))
            {
                entity.Insert();
                transac.Complete();

                //插入用户和角色的关联
                var userRoleList = new List<User_Role>();

                foreach (var roleId in entity.RoleIds)
                {
                    userRoleList.Add(new User_Role()
                    {
                        Role_ID = int.Parse(roleId),
                        User_ID = entity.ID
                    });
                }

                Insert(userRoleList);
            }
            return entity;
        }

        public Operator Edit(Operator entity)
        {
            var oldEntity = Operator.First("Where ID=@0", entity.ID);
            //不需要修改的一些属性
            entity.IsExpire = false;

            //特殊处理的属性
            entity.Password = string.IsNullOrEmpty(entity.Password) ? oldEntity.Password : CommonToolkit.CommonToolkit.GetMD5Password(entity.Password);

            entity.Update();
            return entity;
        }


        public Operator ChangePwd(string password, string newPassword, string confrimPassword)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(newPassword) || newPassword != confrimPassword)
            {
                throw new Exception("输入信息不正确，请检查后重试");
            }

            var oldEntity = Operator.First("Where ID=@0", SysContext.UserId);


            //不需要修改的一些属性

            //特殊处理的属性
            oldEntity.Password = string.IsNullOrEmpty(confrimPassword) ? oldEntity.Password : CommonToolkit.CommonToolkit.GetMD5Password(confrimPassword);

            oldEntity.Update();
            return oldEntity;
        }

    }
}
