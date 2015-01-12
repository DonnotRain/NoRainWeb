using DefaultConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaweiSoftware.WQT.IDal
{
    public interface IRequestDal : ICommonDAL
    {
        List<MKT_Request> GetOtherInfo(List<MKT_Request> srcItems);
    }
}
