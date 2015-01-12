using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WQTRights;

namespace HuaweiSoftware.WQT.IBll
{
    public interface IOperatorBll : ICommonSecurityBLL
    {
        Page<User> GetOperatorPager(int page, int rows, string name,int? roleId);
        User Add(User entity);
        User Edit(User entity);

        User ChangePwd(string password, string newPassword, string confrimPassword);
    }
}
