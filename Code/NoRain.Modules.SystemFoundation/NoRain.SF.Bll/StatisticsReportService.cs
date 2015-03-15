using DefaultConnection;
using NoRain.Business.IBll;
using NoRain.Business.IDal;
using NoRain.Business.Model.Request;
using NoRain.Business.WebBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoRain.Business.Bll
{
    public class StatisticsReportService : CommonService, IStatisticsReportService
    {
        private IBaseDao _dal;
        public StatisticsReportService(ICommonSecurityDao dal)
        {
            this._dal = dal;
        }

        public List<dynamic> GetReportResults()
        {
            return _dal.DB.Query<dynamic>("select * from role R left join User_Role UR on ur.Role_ID=r.ID").ToList();
        }
    }
}
