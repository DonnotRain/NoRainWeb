using DefaultConnection;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaweiSoftware.WQT.IBll
{
    public interface ICorpBll : ICommonBLL
    {
        Corporation Register(Corporation corp, string realName, string adminName, string password);
    }
}
