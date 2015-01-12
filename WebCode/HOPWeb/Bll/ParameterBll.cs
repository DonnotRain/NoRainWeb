using DefaultConnection;
using HuaweiSoftware.WQT.IBll;
using HuaweiSoftware.WQT.IDal;
using HuaweiSoftware.WQT.WebBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaweiSoftware.WQT.Bll
{
    public class ParameterBll : CommonBLL, IParameterBll
    {
        public PetaPoco.Page<DefaultConnection.Parameter> GetParameterPager(int pageIndex, int pageSize, string name, string value)
        {

            var sql = PetaPoco.Sql.Builder.Append(" select * FROM Parameters s")
                .Append("Where s.CorpCode=@0 ", SysContext.CorpCode);


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
            var page = FindAllByPage<Parameter>(sql.SQL, pageSize, pageIndex, sql.Arguments);

            //page.Items.ForEach(m =>
            //{
            //    m.UserCount = m_dal.GetUserCount(m.Id);
            //});
            return page;
        }

        public Parameter Add(Parameter entity)
        {
            entity.CorpCode = SysContext.CorpCode;
            entity.Insert();
            return entity;
        }

        public Parameter GetByName(string paramName)
        {
            var sql = PetaPoco.Sql.Builder.Append(" select * FROM Parameters s")
             .Append("Where s.CorpCode=@0 and s.Name=@1", SysContext.CorpCode, paramName);

            return Find<Parameter>(sql.SQL, sql.Arguments);
        }

        /// <summary>
        /// 获取参数，如果没有，设置默认值
        /// </summary>
        /// <returns></returns>
        public Parameter GetSysName(string paramName)
        {
            //   var paramName = "系统名称";
            var sql = PetaPoco.Sql.Builder.Append(" select * FROM Parameters s")
            .Append("Where s.CorpCode=@0 and s.Name=@1", SysContext.CorpCode, paramName);

            var item = Find<Parameter>(sql.SQL, sql.Arguments);
            if (item == null)
            {
                var corp = Find<Corporation>("WHERE Code=@0", SysContext.CorpCode);

                var valueDict = new Dictionary<string, string>();
                valueDict.Add("系统名称", corp.Name);
                valueDict.Add("上班时间", "08:00");
                valueDict.Add("下班时间", "17:00");

                string value = "";

                if (valueDict.ContainsKey(paramName))
                {
                    value = valueDict[paramName];
                }
                item = new Parameter()
                {
                    CorpCode = SysContext.CorpCode,
                    IsDeleted = false,
                    Name = paramName,
                    Value = value
                };

                //插入
                Insert(item);
            }
            return item;
        }

        public Parameter Edit(Parameter entity)
        {
            var oldEntity = Parameter.First("Where ID=@0", entity.ID);
            entity.CorpCode = oldEntity.CorpCode;
            entity.IsDeleted = oldEntity.IsDeleted;
            entity.Update();
            return entity;
        }


        public Parameter SetSysName(string value, string paramName)
        {
            var sql = PetaPoco.Sql.Builder.Append(" select * FROM Parameters s")
            .Append("Where s.CorpCode=@0 and s.Name=@1", SysContext.CorpCode, paramName);

            var item = Find<Parameter>(sql.SQL, sql.Arguments);

            item.Value = value;

            //更新
            Update(item);

            return item;
        }
    }
}
