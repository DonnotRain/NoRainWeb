using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaweiSoftware.WQT.IBll.Statistics
{
    public interface IStatisticsSaleBll
    {
        object GetRealTimeAmount(int page, int rows, DateTime? beginDate, DateTime? endDate, string name, string code, string type);

        object GetSalersPerformance(int page, int rows, DateTime? nullable1, DateTime? nullable2, string name, string code);
    }
}
