using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Security_ConnectionString;

namespace iBangTeam.Business.IBll
{
    public interface IOperatorBll : ICommonSecurityBLL
    {
        Page<Operator> GetOperatorPager(int page, int rows, string name,int? roleId);
        Operator Add(Operator entity);
        Operator Edit(Operator entity);

        Operator ChangePwd(string password, string newPassword, string confrimPassword);
    }
}
