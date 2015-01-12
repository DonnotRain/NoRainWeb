using DefaultConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaweiSoftware.WQT.IDal
{
    public interface IReturnDal : ICommonDAL
    {
        List<MKT_Return> GetOtherInfo(List<MKT_Return> srcItems);
    }
}
