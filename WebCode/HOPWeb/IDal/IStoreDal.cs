using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaweiSoftware.WQT.IDal
{
    public interface IStoreDal : ICommonDAL
    {
         int GetUserCount(long StoreId);
    }
}
