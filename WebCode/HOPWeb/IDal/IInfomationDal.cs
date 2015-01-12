using DefaultConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaweiSoftware.WQT.IDal
{
    public interface IInfomationDal : ICommonDAL
    {
        List<MKT_Infomation> GetOtherInfo(List<MKT_Infomation> srcItems);
    }
}
