using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaweiSoftware.WQT.IBll
{
    public interface IStatisticsAttendenceBll
    {
        object GetPager(string time, int? userId, int pageIndex, int pageSize);
    }
}
