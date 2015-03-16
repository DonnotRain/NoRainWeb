using DefaultConnection;
using NoRain.Business.Model.Request;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoRain.Business.IService
{
    public interface IStatisticsReportService : ICommonService
    {
        List<dynamic> GetReportResults();
    }
}
