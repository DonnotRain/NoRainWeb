using DefaultConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaweiSoftware.WQT.IDal
{
    public interface ISaleDal : ICommonDAL
    {
        List<MKT_Sell> GetOtherInfo(List<MKT_Sell> srcItems);
    }
}
