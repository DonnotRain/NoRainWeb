using DefaultConnection;
using HuaweiSoftware.HOP.Model;
using HuaweiSoftware.HOP.Model.Request;
using HuaweiSoftware.WQT.IBll;
using HuaweiSoftware.WQT.WebBase;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaweiSoftware.WQT.Bll
{
    public class ATD_AttendenceBll : CommonBLL, IAttendenceBll
    {
        ICommonSecurityBLL m_securityBll;
       

        public ATD_AttendenceBll(ICommonSecurityBLL securityBll)
        {
            m_securityBll = securityBll;
        }
        public PetaPoco.Page<DefaultConnection.ATD_Attendence> GetAttendencePager(int pageIndex, int pageSize, DateTime? beginDate, DateTime? endDate, string userName)
        {

            var sql = Sql.Builder.Append("select a.*,u.UserName from ATD_Attendence A LEFT JOIN SEC_User u on u.ID= a.UserId Where A.CorpCode=@0 ", SysContext.CorpCode);

            //开始时间条件
            if (beginDate.HasValue)
            {
                sql.Append(" And A.Time >=@0", beginDate.Value);

            }
            //开始时间条件
            if (endDate.HasValue)
            {
                sql.Append(" And A.Time <=@0", endDate.Value.AddDays(1));

            }

            if (!string.IsNullOrEmpty(userName))
            {
                userName = "%" + userName + "%";
                sql.Append(" And U.UserName like @0", userName);
            }

            sql.OrderBy("A.Time desc");

            var page = FindAllByPage<ATD_Attendence>(sql.SQL, pageSize, pageIndex, sql.Arguments);
            page.Items.ForEach(m =>
            {
                var user = Find<SEC_User>("Where Id=@0", m.UserId);
                if (user == null)
                {
                    m.Name = "无相关用户";
                    m.StoreName = "无相关用户";
                }
                else
                {
                    var store = Find<SEC_Store>("Where ID=@0", user.StoreId);
                    m.StoreName = store != null ? store.StoreName : "无相关门店";
                    m.Name = user.UserName;
                }

                if (m.FileId > 0)
                {
                    if (m.Files == null) m.Files = new List<FileItem>();
                    var file = Find<FileItem>("Where ID=@0", m.FileId);
                    m.Files.Add(file);
                }
            });
            return page;
        }


        public List<HOP.Model.AttendenceInfo> GetAttendenceByName(string userName)
        {

            var user = Find<SEC_User>("Where UserName=@0 AND CorpCode=@1", userName, SysContext.CorpCode);
            var items = FindAll<ATD_Attendence>("Where UserId=@0 Order by Time desc", user.ID);
            return items.Select(m => new AttendenceInfo()
            {
                ID = m.ID,
                Name = user.UserName,
                Position = m.Position,
                Time = m.Time,
                Type = m.Type,
                File = Find<FileItem>("Where ID=@0", m.FileId)
            }).ToList();
        }

        public ATD_Attendence AddAttendence(AttendenceContract model)
        {
            var user = Find<SEC_User>("Where UserName=@0 AND CorpCode=@1", SysContext.UserName, SysContext.CorpCode);
            ATD_Attendence attendence = new ATD_Attendence();

            ATD_Attendence atd_Attendece = FindAll<ATD_Attendence>("WHERE UserId=@0 ORDER BY [Time] DESC", user.ID).FirstOrDefault();

            // 第一次签到没有记录，只能是上班记录
            if (atd_Attendece == null)
            {
                if (model.type == (int)AttendenceType.OnDuty)
                {
                    attendence = InsertAttendence(user, model);
                    attendence.IsSuccess = true;
                }
                else
                {
                    attendence.IsSuccess = false;
                    attendence.ErrorMessage = "只能进行上班记录";
                }
            }
            else
            {
                if (model.type == (int)AttendenceType.OnDuty)
                {
                    if (atd_Attendece.Type == Convert.ToBoolean(AttendenceType.OffDuty))
                    {
                        attendence = InsertAttendence(user, model);
                        attendence.IsSuccess = true;
                    }
                    else
                    {
                        string createTime = atd_Attendece.Time.ToShortDateString();
                        string nowDay = DateTime.Now.ToShortDateString();

                        if (createTime == nowDay)
                        {
                            attendence.IsSuccess = false;
                            attendence.ErrorMessage = "当天已有上班记录，无法继续上班";
                        }
                        else
                        {
                            attendence = InsertAttendence(user, model);
                            attendence.IsSuccess = true;
                        }
                    }
                }
                else
                {
                    if (atd_Attendece.Type == Convert.ToBoolean(AttendenceType.OnDuty))
                    {
                        model.mark = atd_Attendece.Mark;
                        attendence = InsertAttendence(user, model);
                        attendence.IsSuccess = true;
                    }
                    else
                    {
                        attendence.IsSuccess = false;
                        attendence.ErrorMessage = "没有上班记录，无法下班";
                    }
                }
            }

            return attendence;
        }

        private ATD_Attendence InsertAttendence(SEC_User user,AttendenceContract model)
        {
            ATD_Attendence attendence = new ATD_Attendence();
            SEC_Store store = FindAll<SEC_Store>("WHERE Id=@0", user.StoreId).FirstOrDefault();

            if (store != null)
            {
                double latStore = Convert.ToDouble(store.Latitude);
                double lonStore = Convert.ToDouble(store.Longitude);
                string[] position = model.position.Split(',');
                double latPerson = Convert.ToDouble(position[1]);
                double lonPerson = Convert.ToDouble(position[0]);
                double distance = GetShortDistance(lonStore, latStore, lonPerson, latPerson);
                int range = Convert.ToInt32(Find<Parameter>("WHERE Name=@0", "允许的签到误差距离").Value);

                if (model.type == (int)AttendenceType.OnDuty)
                {
                    attendence = new ATD_Attendence()
                    {
                        Name = user.UserName,
                        UserId = (int)user.ID,
                        CorpCode = SysContext.CorpCode,
                        Position = model.position,
                        Type = Convert.ToBoolean(model.type),
                        Time = DateTime.Now,
                        FileId = model.fileId,
                        Mark = Guid.NewGuid(),
                        IsInRange = range>= Math.Round(distance)
                    };
                }
                else
                {
                    attendence = new ATD_Attendence()
                    {
                        Name = user.UserName,
                        UserId = (int)user.ID,
                        CorpCode = SysContext.CorpCode,
                        Position = model.position,
                        Type = Convert.ToBoolean(model.type),
                        Time = DateTime.Now,
                        FileId = model.fileId,
                        Mark = model.mark,
                        IsInRange = range >= Math.Round(distance)
                    };
                }
            }
            else
            {
                throw new Exception("门店不能为空");
            }

            attendence.Insert();

            return attendence;
        }

        #region 计算地图上的距离
        const double DEF_PI = 3.14159265359; // PI

        const double DEF_2PI = 6.28318530712; // 2*PI

        const double DEF_PI180 = 0.01745329252; // PI/180.0

        const double DEF_R = 6370693.5; // radius of earth

        /// <summary>
        /// 计算百度地图两点距离(单位是米)
        /// </summary>
        /// <param name="lon1"></param>
        /// <param name="lat1"></param>
        /// <param name="lon2"></param>
        /// <param name="lat2"></param>
        /// <returns></returns>
        public double GetShortDistance(double lon1, double lat1, double lon2, double lat2)
        {

            double ew1, ns1, ew2, ns2;

            double dx, dy, dew;

            double distance;

            // 角度转换为弧度

            ew1 = lon1 * DEF_PI180;

            ns1 = lat1 * DEF_PI180;

            ew2 = lon2 * DEF_PI180;

            ns2 = lat2 * DEF_PI180;

            // 经度差

            dew = ew1 - ew2;

            // 若跨东经和西经180 度，进行调整

            if (dew > DEF_PI)

                dew = DEF_2PI - dew;

            else if (dew < -DEF_PI)

                dew = DEF_2PI + dew;

            dx = DEF_R * Math.Cos(ns1) * dew; // 东西方向长度(在纬度圈上的投影长度)

            dy = DEF_R * (ns1 - ns2); // 南北方向长度(在经度圈上的投影长度)

            // 勾股定理求斜边长

            distance = Math.Sqrt(dx * dx + dy * dy);

            return distance;

        }

        #endregion 

        private enum AttendenceType
        {
            OnDuty = 0,
            OffDuty = 1
        }
    }
}
