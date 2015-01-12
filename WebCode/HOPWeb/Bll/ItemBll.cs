using DefaultConnection;
using HuaweiSoftware.WQT.IBll;
using HuaweiSoftware.WQT.WebBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaweiSoftware.WQT.Bll
{
    public class ItemBll : CommonBLL, IItemBll
    {
        public PetaPoco.Page<DefaultConnection.MKT_Item> GetItemPager(int pageIndex, int pageSize, int? begin, int? end, string name, string code, string type)
        {

            List<object> paramaters = new List<object>();

            var sql = PetaPoco.Sql.Builder.Append("SELECT s.*,C.ID as CID FROM MKT_Items s")
                .LeftJoin("CategoryItems c").On("C.Code=S.Type")
                .Append("Where s.IsDeleted=0 and s.CorpCode=@0 ", SysContext.CorpCode);

            //开始时间条件
            if (begin.HasValue)
            {
                sql.Append(" And  s.Price >=@0", begin.Value);

            }
            //开始时间条件
            if (end.HasValue)
            {
                sql.Append(" And s.Price <=@0", end.Value);
            }

            if (!string.IsNullOrEmpty(name))
            {
                name = "%" + name + "%";
                sql.Append(" And s.Name like @0", name);
            }

            if (!string.IsNullOrEmpty(code))
            {
                code = "%" + code + "%";
                sql.Append(" And s.Code like @0", code);
            }

            if (!string.IsNullOrEmpty(type))
            {
                sql.Append(string.Format(" And C.ID in ({0})", type));
            }

            var page = FindAllByPage<MKT_Item>(sql.SQL, pageSize, pageIndex, sql.Arguments);
            page.Items.ForEach(m =>
            {
                var itemType = Find<CategoryItem>("Where Code=@0", m.Type);
                m.TypeName = itemType != null ? itemType.Content : "(未找到相关类型)";

            });
            return page;
        }


        public MKT_Item Add(MKT_Item entity)
        {
            entity.CreateTime = DateTime.Now;
            entity.ModifyTime = DateTime.Now;
            entity.IsDeleted = false;

            entity.CorpCode = SysContext.CorpCode;
            entity.Insert();
            return entity;
        }

        public MKT_Item Edit(MKT_Item entity)
        {
            var oldEntity = MKT_Item.First("Where ID=@0", entity.ID);
            //不需要修改的一些属性
            entity.ModifyTime = DateTime.Now;
            entity.CorpCode = oldEntity.CorpCode;
            entity.CreateTime = oldEntity.CreateTime;
            entity.IsDeleted = false;

            entity.Update();
            return entity;
        }


        public void LogicDelete(string ids)
        {
            var itemsStr = ids.Split(',').ToList();
            var items = new List<MKT_Item>();
            itemsStr.ForEach(m =>
            {
                var item = Find<MKT_Item>("WHERE ID=@0", int.Parse(m));
                item.IsDeleted = true;
                if (item != null) items.Add(item);
            });
            Update(items);
        }
    }
}
