using DefaultConnection;
using HuaweiSoftware.HOP.Model;
using HuaweiSoftware.WQT.IBll;
using HuaweiSoftware.WQT.IDal;
using HuaweiSoftware.WQT.WebBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuaweiSoftware.WQT.Bll
{
    public class SaleBll : CommonBLL, ISaleBll
    {
        private ISaleDal m_saleDal;
        public SaleBll(ISaleDal saleDal)
        {
            m_saleDal = saleDal;
        }
        public PetaPoco.Page<DefaultConnection.MKT_Sell> GetSalePager(int pageIndex, int pageSize, DateTime? beginDate, DateTime? endDate, string name, string code, string type, string storeName)
        {

            List<object> paramaters = new List<object>();

            var sql = PetaPoco.Sql.Builder.Append("SELECT s.*,ss.StoreName,u.UserName as Creator FROM MKT_Sell s")
   .LeftJoin("SEC_User u").On("s.UserId=u.Id").LeftJoin("SEC_Store ss")
    .On("ss.ID=u.StoreId").LeftJoin("MKT_Items IT").On("S.CODE=IT.Code").Append("Where S.CorpCode=@0 ", SysContext.CorpCode);

            //开始时间条件
            if (beginDate.HasValue)
            {
                sql.Append(" And  s.Time >=@0", beginDate.Value);

            }
            //开始时间条件
            if (endDate.HasValue)
            {
                sql.Append(" And s.Time <=@0", endDate.Value.AddDays(1));
            }

            if (!string.IsNullOrEmpty(name))
            {
                name = "%" + name + "%";
                sql.Append(" And IT.Name like @0", name);
            }

            if (!string.IsNullOrEmpty(code))
            {
                code = "%" + code + "%";
                sql.Append(" And s.Code like @0", code);
            }

            if (!string.IsNullOrEmpty(storeName))
            {
                storeName = "%" + storeName + "%";
                sql.Append(" And ss.StoreName like @0", storeName);
            }
            sql.OrderBy("Time Desc");
            var page = FindAllByPage<MKT_Sell>(sql.SQL, pageSize, pageIndex, sql.Arguments);

            //获取一些其他的基础信息
            m_saleDal.GetOtherInfo(page.Items);

            return page;
        }

        public List<MKT_Sell> GetSellByName(string p, string name)
        {
            var user = Find<SEC_User>("Where UserName=@0 AND CorpCode=@1", SysContext.UserName, SysContext.CorpCode);
            var sql = PetaPoco.Sql.Builder.Append("SELECT * FROM MKT_Sell").Append("WHERE UserId=@0", user.ID);
            

            sql.Append("ORDER BY Time DESC");
            var items = FindAll<MKT_Sell>(sql.SQL, sql.Arguments).ToList();
            m_saleDal.GetOtherInfo(items);
            if (name != null)
            {
                items = items.Where(m => m.Name.ToUpper().Contains(name.ToUpper())).ToList();
            }

            return items.ToList();
        }

        public MKT_Sell AddSell(HOP.Model.Request.SellContract model)
        {
            var user = Find<SEC_User>("Where UserName=@0 AND CorpCode=@1", SysContext.UserName, SysContext.CorpCode);
            var sale = new MKT_Sell();
            sale.Code = model.code;

            sale.TotalPrice = Convert.ToDecimal(model.totalPrice);
            sale.Discount = Convert.ToDecimal(model.discount);
            sale.Amount = Convert.ToInt32(model.amount);

            sale.Time = DateTime.Now;

            sale.CorpCode = SysContext.CorpCode;
            sale.UserId = (int)user.ID;
            Insert(sale);
            return sale;
        }

        /// <summary>
        /// 获取商品类型
        /// </summary>
        /// <param name="corpCode">公司编号</param>
        /// <returns></returns>
        public List<CategoryItem> GetCategoryItems(string corpCode)
        {
            List<CategoryItem> categoryItems = FindAll<CategoryItem>("WHERE CorpCode=@0",corpCode).ToList();

            return categoryItems;
        }

        /// <summary>
        /// 获取商品集合
        /// </summary>
        /// <param name="corpCode">公司编号</param>
        /// <param name="type">商品类型</param>
        /// <param name="barCode">条形码</param>
        /// <returns></returns>
        public object GetItems(string corpCode, string type, string barCode)
        {
            List<MKT_Item> mkt_ItemList = new List<MKT_Item>();
            MKT_Item selectedItem = new MKT_Item();
            if (barCode != null)
            {
                if (FindAll<MKT_Item>("WHERE BarCode = @0", barCode).ToList().Count > 0)
                {
                    selectedItem = FindAll<MKT_Item>("WHERE BarCode = @0", barCode).FirstOrDefault();
                    mkt_ItemList = FindAll<MKT_Item>("WHERE Type=@0 AND CorpCode=@1 AND IsDeleted=0 ORDER BY CreateTime DESC", selectedItem.Type, selectedItem.CorpCode).ToList();
                }
                else
                {
                    selectedItem = null;
                    mkt_ItemList = null;
                }
            }
            else
            {
                mkt_ItemList = FindAll<MKT_Item>("WHERE Type=@0 AND CorpCode=@1 AND IsDeleted=0 ORDER BY CreateTime DESC", type, corpCode).ToList();
                selectedItem = null;
            }

            return new { Items = mkt_ItemList, SelectedItem = selectedItem };
        }
    }
}
