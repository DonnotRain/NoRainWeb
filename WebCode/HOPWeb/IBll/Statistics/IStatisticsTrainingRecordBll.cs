using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaweiSoftware.WQT.IBll.Statistics
{
    public interface IStatisticsTrainingBll
    {
        object GetPager(int pageIndex, int pageSize, DateTime? beginTime, DateTime? endTime, int? userID,string corpCode);
    }
}
