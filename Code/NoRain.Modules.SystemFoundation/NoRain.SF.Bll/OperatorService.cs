using NoRain.Business.WebBase;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NoRain.Business.IDal;
using DefaultConnection;
using NoRain.Business.IService;

namespace NoRain.Business.Service
{
    public class SysUserService : CommonService, ISysUserService
    {
        private IRoleService m_roleService;
        private IBaseDao _dal;

        public SysUserService(IRoleService roleService, ICommonDao dal)
        {
            m_roleService = roleService; this._dal = dal;
        }
        public PetaPoco.Page<SysUser> GetSysUserPager(int pageIndex, int pageSize, string name)
        {

            var sql = PetaPoco.Sql.Builder.Append("SELECT * FROM SysOperators S Where 1=1 ");


            if (!string.IsNullOrEmpty(name))
            {
                name = "%" + name + "%";
                sql.Append(" And (s.Name like @0 or s.FullName like @0)", name);
            }

            var page = FindAllByPage<SysUser>(sql.SQL, pageSize, pageIndex, sql.Arguments);


            page.Items.ForEach(m =>
            {
                var roles = m_roleService.GetUserRoles(m.ID);
                m.Roles = roles;
                m.RoleIds = roles.Select(r => r.ID.ToString()).ToArray();
            });

            return page;
        }

        public SysUser Add(SysUser entity)
        {
            entity.Password = CommonToolkit.CommonToolkit.GetMD5Password(entity.Password);

            entity.IsExpire = false;
            entity.ID = Guid.NewGuid();
            //判断编号是否重复
            var repeatItems = FindAll<SysUser>("WHERE Name=@0", entity.Name);
            if (repeatItems.Count() != 0) throw new ApiException(ResponseCode.参数值重复, "已存在相同用户", "用户名");

            using (PetaPoco.Transaction transac = new Transaction(_dal.DB))
            {
                entity.Insert();

                //插入用户和角色的关联
                var userRoleList = new List<User_Role>();
                if (entity.RoleIds != null)
                    foreach (var roleId in entity.RoleIds)
                    {
                        userRoleList.Add(new User_Role()
                        {
                            Role_ID = int.Parse(roleId),
                            User_ID = entity.ID.ToString()
                        });
                    }

                Insert(userRoleList);
                transac.Complete();
            }
            return entity;
        }

        public SysUser Edit(SysUser entity)
        {
            var oldEntity = SysUser.First("Where ID=@0", entity.ID);
            //不需要修改的一些属性
            entity.IsExpire = false;

            //特殊处理的属性
            entity.Password = string.IsNullOrEmpty(entity.Password) ? oldEntity.Password : CommonToolkit.CommonToolkit.GetMD5Password(entity.Password);

            //  
            using (PetaPoco.Transaction transac = new Transaction(_dal.DB))
            {
                entity.Update();

                //先去除用户和角色关联

                var deleteRoleSql = PetaPoco.Sql.Builder.Append(" DELETE FROM User_Role WHERE USER_ID=@0 ", entity.ID);
                _dal.DB.Execute(deleteRoleSql);
                //插入用户和角色的关联
                var userRoleList = new List<User_Role>();
                if (entity.RoleIds != null)
                {
                    foreach (var roleId in entity.RoleIds)
                    {
                        userRoleList.Add(new User_Role()
                        {
                            Role_ID = int.Parse(roleId),
                            User_ID = entity.ID.ToString()
                        });
                    }
                }
                Insert(userRoleList);

                transac.Complete();
            }
            return entity;
        }


        public SysUser ChangePwd(string password, string newPassword, string confrimPassword)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(newPassword) || newPassword != confrimPassword)
            {
                throw new Exception("输入信息不正确，请检查后重试");
            }

            var oldEntity = SysUser.First("Where ID=@0", SysContext.UserId);


            //不需要修改的一些属性

            //特殊处理的属性
            oldEntity.Password = string.IsNullOrEmpty(confrimPassword) ? oldEntity.Password : CommonToolkit.CommonToolkit.GetMD5Password(confrimPassword);

            oldEntity.Update();
            return oldEntity;
        }

    }
}
