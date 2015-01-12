using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DefaultConnection;

namespace HuaweiSoftware.WQT.IBll.Statistics
{
    public interface IStatisticsLeaveBll
    {
        object GetPager(int pageIndex,int pageSize,DateTime? beginTime, DateTime? endTime,int? name,string corpCode);
    }
}
