using DefaultConnection;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaweiSoftware.WQT.IBll
{
    public interface IUserBll : ICommonBLL
    {
        Page<SEC_User> GetUserPager(int page, int rows, int? begin, int? end, string name, string code, string storeName);
        SEC_User Add(SEC_User entity);
        SEC_User Edit(SEC_User entity);

        SEC_User Login(string name, string password, string corpcode);

        List<SEC_User> GetAll();

        void LogicDelete(string ids);
    }
}
