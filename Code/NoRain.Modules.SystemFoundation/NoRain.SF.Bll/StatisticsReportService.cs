using DefaultConnection;
using NoRain.Business.IService;
using NoRain.Business.IDal;
using NoRain.Business.Model.Request;
using NoRain.Business.WebBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoRain.Business.Service
{
    public class StatisticsReportService : CommonService, IStatisticsReportService
    {
        private IBaseDao _dal;
        public StatisticsReportService(ICommonDao dal)
        {
            this._dal = dal;
        }

        public List<dynamic> GetReportResults()
        {
            return _dal.DB.Query<dynamic>("select * from role R left join User_Role UR on ur.Role_ID=r.ID").ToList();
        }
    }
}
