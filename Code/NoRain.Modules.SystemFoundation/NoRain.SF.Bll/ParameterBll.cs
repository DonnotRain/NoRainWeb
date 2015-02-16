using DefaultConnection;
using NoRain.Business.IBll;
using NoRain.Business.IDal;
using NoRain.Business.WebBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoRain.Business.Bll
{
    public class ParameterBll : CommonBLL, IParameterBll
    {
        public PetaPoco.Page<DefaultConnection.SysParameter> GetParameterPager(int pageIndex, int pageSize, string name, string value)
        {

            var sql = PetaPoco.Sql.Builder.Append(" select * FROM SysParameters s")
                .Append("Where 1=1 ");

            if (!string.IsNullOrEmpty(name))
            {
                name = "%" + name + "%";
                sql.Append(" And s.Name like @0", name);
            }
            if (!string.IsNullOrEmpty(value))
            {
                value = "%" + value + "%";
                sql.Append(" And s.Value like @0", value);
            }
            var page = FindAllByPage<SysParameter>(sql.SQL, pageSize, pageIndex, sql.Arguments);

            //page.Items.ForEach(m =>
            //{
            //    m.UserCount = m_dal.GetUserCount(m.Id);
            //});
            return page;
        }

        public SysParameter Add(SysParameter entity)
        {
            entity.Insert();
            return entity;
        }

        public SysParameter GetByName(string paramName)
        {
            var sql = PetaPoco.Sql.Builder.Append(" select * FROM SysParameters s")
             .Append("Where  s.Name=@0", paramName);

            return Find<SysParameter>(sql.SQL, sql.Arguments);
        }

        /// <summary>
        /// 获取参数，如果没有，设置默认值
        /// </summary>
        /// <returns></returns>
        public SysParameter GetSysName(string paramName)
        {
            //   var paramName = "系统名称";
            var sql = PetaPoco.Sql.Builder.Append(" select * FROM SysParameters s")
            .Append("Where  s.Name=@0", paramName);

            var item = Find<SysParameter>(sql.SQL, sql.Arguments);
            if (item == null)
            {
                var valueDict = new Dictionary<string, string>();
                valueDict.Add("上班时间", "08:00");
                valueDict.Add("下班时间", "17:00");

                string value = "";

                if (valueDict.ContainsKey(paramName))
                {
                    value = valueDict[paramName];
                }
                item = new SysParameter()
                {
                    IsEnabled = 1,
                    Name = paramName,
                    ValueContent = value
                };

                //插入
                Insert(item);
            }
            return item;
        }

        public SysParameter Edit(SysParameter entity)
        {
            var oldEntity = SysParameter.First("Where ID=@0", entity.Id);
            entity.IsEnabled = oldEntity.IsEnabled;
            entity.Update();
            return entity;
        }


        public SysParameter SetSysName(string value, string paramName)
        {
            var sql = PetaPoco.Sql.Builder.Append(" select * FROM SysParameters s")
            .Append("Where  s.Name=@0", paramName);

            var item = Find<SysParameter>(sql.SQL, sql.Arguments);

            item.ValueContent = value;

            //更新
            Update(item);

            return item;
        }


        public PetaPoco.Page<DefaultConnection.SysParameter> GetParameterPager(Model.Request.DataTablesRequest reqestParams)
        {
            return GetParameterPager(reqestParams.start + 1, reqestParams.length == -1 ? int.MaxValue : reqestParams.length, "", "");
        }
    }
}
