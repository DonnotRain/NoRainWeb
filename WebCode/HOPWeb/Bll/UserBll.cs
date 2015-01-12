using DefaultConnection;
using HuaweiSoftware.WQT.IBll;
using HuaweiSoftware.WQT.WebBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaweiSoftware.WQT.Bll
{
    public class UserBll : CommonBLL, IUserBll
    {
        public PetaPoco.Page<DefaultConnection.SEC_User> GetUserPager(int pageIndex, int pageSize, int? begin, int? end, string name, string code, string storeName)
        {

            var sql = PetaPoco.Sql.Builder.Append("SELECT s.*,ss.StoreName FROM SEC_User s")
                .LeftJoin("SEC_Store ss").On("ss.ID=s.StoreId").Append("Where  s.IsDeleted=0 AND s.CorpCode=@0 ", SysContext.CorpCode);

            //开始时间条件
            if (begin.HasValue)
            {
                sql.Append(" And  s.YearAge >=@0", begin.Value);

            }
            //开始时间条件
            if (end.HasValue)
            {
                sql.Append(" And s.YearAge <=@0", end.Value);
            }

            if (!string.IsNullOrEmpty(name))
            {
                name = "%" + name + "%";
                sql.Append(" And s.UserName like @0", name);
            }

            if (!string.IsNullOrEmpty(code))
            {
                code = "%" + code + "%";
                sql.Append(" And s.Code like @0", code);
            }

            if (!string.IsNullOrEmpty(storeName))
            {
                storeName = "%" + storeName + "%";
                sql.Append(" And ss.StoreName like @0", storeName);
            }

            var page = FindAllByPage<SEC_User>(sql.SQL, pageSize, pageIndex, sql.Arguments);
            page.Items.ForEach(m =>
            {
                var store = Find<SEC_Store>("Where ID=@0", m.StoreId);
                m.StoreName = store != null ? store.StoreName : "无相关门店";
            });
            return page;
        }

        public SEC_User Add(SEC_User entity)
        {
            entity.Password = CommonToolkit.CommonToolkit.GetMD5Password(entity.Password);
            entity.ModifyTime = DateTime.Now;
            entity.IsDeleted = false;

            entity.CorpCode = SysContext.CorpCode;
            entity.Insert();
            return entity;
        }

        public SEC_User Edit(SEC_User entity)
        {
            var oldEntity = SEC_User.First("Where ID=@0", entity.ID);
            //不需要修改的一些属性
            entity.ModifyTime = DateTime.Now;
            entity.CorpCode = oldEntity.CorpCode;
            //设为入职时间了
            //   entity.CreateTime = oldEntity.CreateTime;
            entity.IsDeleted = false;
            //特殊处理的属性
            entity.Password = string.IsNullOrEmpty(entity.Password) ? oldEntity.Password : CommonToolkit.CommonToolkit.GetMD5Password(entity.Password);

            entity.Update();
            return entity;
        }


        /// <summary>
        ///移动端登录
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <param name="corpcode"></param>
        /// <returns></returns>
        public SEC_User Login(string name, string password, string corpcode)
        {
            //先判断有没有相关用户名

            var corp = Find<Corporation>("WHERE Code=@0", corpcode);
            if (corp == null)
            {
                throw new ApiException(ResponseCode.参数值错误, "无对应企业码", "CorpCode");
            }

            var isNameExist = Find<SEC_User>("WHERE  IsDeleted=0 AND UserName=@0 and CorpCode=@1", name, corpcode) != null;
            if (!isNameExist)
            {
                throw new ApiException(ResponseCode.参数值错误, "用户名不存在", "Name");
            }

            password = HuaweiSoftware.WQT.CommonToolkit.CommonToolkit.GetMD5Password(password);
            var user = Find<SEC_User>("WHERE IsDeleted=0 AND UserName=@0 AND Password=@1 And CorpCode=@2", name, password, corpcode);
            var userPasswordExist = user != null;

            if (!userPasswordExist)
            {
                throw new ApiException(ResponseCode.参数值错误, "密码错误", "Password");
            }
            else
            {
                return user;
            }

        }

        /// <summary>
        /// 获取所有用户信息
        /// </summary>
        /// <returns></returns>
        public List<SEC_User> GetAll()
        {
            List<SEC_User> sec_UserList = FindAll<SEC_User>("WHERE CorpCode = @0", SysContext.CorpCode).ToList();

            return sec_UserList;
        }


        public void LogicDelete(string ids)
        {

            var itemsStr = ids.Split(',').ToList();
            var items = new List<SEC_User>();
            itemsStr.ForEach(m =>
            {
                var item = Find<SEC_User>("WHERE ID=@0", int.Parse(m));
                item.IsDeleted = true;
                if (item != null) items.Add(item);
            });
            Update(items);
        }
    }
}
