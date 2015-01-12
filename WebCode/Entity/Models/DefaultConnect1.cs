



















// This file was automatically generated by the PetaPoco T4 Template
// Do not make changes directly to this file - edit the template instead
// 
// The following connection settings were used to generate this file
// 
//     Connection String Name: `DefaultConnection`
//     Provider:               `System.Data.SqlClient`
//     Connection String:      `Data Source=.;Initial Catalog=TeamDB; UID=sa;PWD=123456;MultipleActiveResultSets=True`



using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetaPoco;

namespace DefaultConnection
{
	public partial class DefaultConnectionDB : Database
	{
		public DefaultConnectionDB() 
			: base("DefaultConnection")
		{
			CommonConstruct();
		}

		public DefaultConnectionDB(string connectionStringName) 
			: base(connectionStringName)
		{
			CommonConstruct();
		}
		
		partial void CommonConstruct();
		
		public interface IFactory
		{
			DefaultConnectionDB GetInstance();
		}
		
		public static IFactory Factory { get; set; }
        public static DefaultConnectionDB GetInstance()
        {
			if (_instance!=null)
				return _instance;
				
			if (Factory!=null)
				return Factory.GetInstance();
			else
				return new DefaultConnectionDB();
        }

		[ThreadStatic] static DefaultConnectionDB _instance;
		
		public override void OnBeginTransaction()
		{
			if (_instance==null)
				_instance=this;
		}
		
		public override void OnEndTransaction()
		{
			if (_instance==this)
				_instance=null;
		}
        

		public class Record<T> where T:new()
		{
			public static DefaultConnectionDB repo { get { return DefaultConnectionDB.GetInstance(); } }
			public bool IsNew() { return repo.IsNew(this); }
			public void Save() { repo.Save(this); }
			public object Insert() { return repo.Insert(this); }
			public int Update() { return repo.Update(this); }
			public int Delete() { return repo.Delete(this); }
			public static int Update(string sql, params object[] args) { return repo.Delete<T>(sql, args); }
			public static int Update(Sql sql) { return repo.Delete<T>(sql); }
			public static int Delete(string sql, params object[] args) { return repo.Delete<T>(sql, args); }
			public static int Delete(Sql sql) { return repo.Delete<T>(sql); }
			public static int Delete(object primaryKey) { return repo.Delete<T>(primaryKey); }
			public static bool Exists(object primaryKey) { return repo.Exists<T>(primaryKey); }
			public static T SingleOrDefault(object primaryKey) { return repo.SingleOrDefault<T>(primaryKey); }
			public static T SingleOrDefault(string sql, params object[] args) { return repo.SingleOrDefault<T>(sql, args); }
			public static T SingleOrDefault(Sql sql) { return repo.SingleOrDefault<T>(sql); }
			public static T FirstOrDefault(string sql, params object[] args) { return repo.FirstOrDefault<T>(sql, args); }
			public static T FirstOrDefault(Sql sql) { return repo.FirstOrDefault<T>(sql); }
			public static T Single(object primaryKey) { return repo.Single<T>(primaryKey); }
			public static T Single(string sql, params object[] args) { return repo.Single<T>(sql, args); }
			public static T Single(Sql sql) { return repo.Single<T>(sql); }
			public static T First(string sql, params object[] args) { return repo.First<T>(sql, args); }
			public static T First(Sql sql) { return repo.First<T>(sql); }
			public static List<T> Fetch(string sql, params object[] args) { return repo.Fetch<T>(sql, args); }
			public static List<T> Fetch(Sql sql) { return repo.Fetch<T>(sql); }
			public static List<T> Fetch(long page, long itemsPerPage, string sql, params object[] args) { return repo.Fetch<T>(page, itemsPerPage, sql, args); }
			public static List<T> Fetch(long page, long itemsPerPage, Sql sql) { return repo.Fetch<T>(page, itemsPerPage, sql); }
			public static Page<T> Page(long page, long itemsPerPage, string sql, params object[] args) { return repo.Page<T>(page, itemsPerPage, sql, args); }
			public static Page<T> Page(long page, long itemsPerPage, Sql sql) { return repo.Page<T>(page, itemsPerPage, sql); }
			public static IEnumerable<T> Query(string sql, params object[] args) { return repo.Query<T>(sql, args); }
			public static IEnumerable<T> Query(Sql sql) { return repo.Query<T>(sql); }
		}

	}
	


    
	[TableName("CategoryItems")]

	[PrimaryKey("Id")]

	[ExplicitColumns]
    public partial class CategoryItem : DefaultConnectionDB.Record<CategoryItem>  
    {

        [Column] public Guid Id { get; set; }

        [Column] public string Code { get; set; }

        [Column] public string Content { get; set; }

        [Column] public string CategoryTypeCode { get; set; }

        [Column] public Guid? CategoryTypeId { get; set; }

        [Column] public Guid? ParentId { get; set; }

	}


    
	[TableName("CategoryTypes")]

	[PrimaryKey("Id")]

	[ExplicitColumns]
    public partial class CategoryType : DefaultConnectionDB.Record<CategoryType>  
    {

        [Column] public Guid Id { get; set; }

        [Column] public string Code { get; set; }

        [Column] public string Name { get; set; }

        [Column] public int Sort { get; set; }

	}


    
	[TableName("ConsigneeInfoes")]

	[PrimaryKey("Id")]

	[ExplicitColumns]
    public partial class ConsigneeInfo : DefaultConnectionDB.Record<ConsigneeInfo>  
    {

        [Column] public Guid Id { get; set; }

        [Column] public Guid ProvinceId { get; set; }

        [Column] public Guid CityId { get; set; }

        [Column] public Guid CountyId { get; set; }

        [Column] public string StreetAddress { get; set; }

	}


    
	[TableName("Consumers")]

	[PrimaryKey("Id")]

	[ExplicitColumns]
    public partial class Consumer : DefaultConnectionDB.Record<Consumer>  
    {

        [Column] public Guid Id { get; set; }

        [Column] public string Name { get; set; }

        [Column] public int Age { get; set; }

        [Column] public int Sex { get; set; }

        [Column] public string Password { get; set; }

        [Column] public DateTime? LastLogin { get; set; }

	}


    
	[TableName("DiscountContents")]

	[PrimaryKey("Id")]

	[ExplicitColumns]
    public partial class DiscountContent : DefaultConnectionDB.Record<DiscountContent>  
    {

        [Column] public Guid Id { get; set; }

        [Column] public string Remark { get; set; }

        [Column] public decimal Price { get; set; }

        [Column] public string Name { get; set; }

        [Column] public string Num { get; set; }

        [Column] public string Total { get; set; }

        [Column] public string CategoryName { get; set; }

	}


    
	[TableName("Discounts")]

	[PrimaryKey("Id")]

	[ExplicitColumns]
    public partial class Discount : DefaultConnectionDB.Record<Discount>  
    {

        [Column] public Guid Id { get; set; }

        [Column] public string Name { get; set; }

        [Column] public string Remark { get; set; }

        [Column] public string BusinessHours { get; set; }

        [Column] public string KeyWord { get; set; }

        [Column] public string Description { get; set; }

        [Column] public DateTime ExpirationTime { get; set; }

        [Column] public decimal BeforePrice { get; set; }

        [Column] public decimal Price { get; set; }

        [Column] public DateTime BeginTime { get; set; }

        [Column] public Guid? Seller_Id { get; set; }

	}


    
	[TableName("EnitityFiles")]

	[PrimaryKey("EntityId")]

	[ExplicitColumns]
    public partial class EnitityFile : DefaultConnectionDB.Record<EnitityFile>  
    {

        [Column] public string EntityId { get; set; }

        [Column] public string EntityType { get; set; }

        [Column] public string FileId { get; set; }

	}


    
	[TableName("FileItems")]

	[PrimaryKey("Id")]

	[ExplicitColumns]
    public partial class FileItem : DefaultConnectionDB.Record<FileItem>  
    {

        [Column] public Guid Id { get; set; }

        [Column] public string FileName { get; set; }

        [Column] public string FilePath { get; set; }

        [Column] public string ExtensionName { get; set; }

        [Column] public double FileSize { get; set; }

	}


    
	[TableName("LoginLogs")]

	[PrimaryKey("Id")]

	[ExplicitColumns]
    public partial class LoginLog : DefaultConnectionDB.Record<LoginLog>  
    {

        [Column] public Guid Id { get; set; }

        [Column] public DateTime? LoginTime { get; set; }

        [Column] public string LoginIp { get; set; }

        [Column] public string LoginStatus { get; set; }

	}


    
	[TableName("Orders")]

	[PrimaryKey("Id")]

	[ExplicitColumns]
    public partial class Order : DefaultConnectionDB.Record<Order>  
    {

        [Column] public Guid Id { get; set; }

        [Column] public Guid ProductId { get; set; }

        [Column] public decimal Quantity { get; set; }

        [Column] public string OrderNo { get; set; }

        [Column] public decimal UnitPrice { get; set; }

        [Column] public decimal TotalPrice { get; set; }

        [Column] public Guid ConsigneeInfoId { get; set; }

	}


    
	[TableName("Products")]

	[PrimaryKey("ProductId")]

	[ExplicitColumns]
    public partial class Product : DefaultConnectionDB.Record<Product>  
    {

        [Column] public Guid ProductId { get; set; }

        [Column] public string ProductCode { get; set; }

        [Column] public string ProductName { get; set; }

        [Column] public decimal ProductPrice { get; set; }

        [Column] public Guid ProductUnitId { get; set; }

        [Column] public string CategoryCode { get; set; }

        [Column] public DateTime CreateTime { get; set; }

        [Column] public DateTime ModifyTime { get; set; }

        [Column] public bool IsDeleted { get; set; }

	}


    
	[TableName("ProductUnits")]

	[PrimaryKey("Id")]

	[ExplicitColumns]
    public partial class ProductUnit : DefaultConnectionDB.Record<ProductUnit>  
    {

        [Column] public Guid Id { get; set; }

        [Column] public string Code { get; set; }

        [Column] public string Name { get; set; }

	}


    
	[TableName("Remarks")]

	[PrimaryKey("Id")]

	[ExplicitColumns]
    public partial class Remark : DefaultConnectionDB.Record<Remark>  
    {

        [Column] public Guid Id { get; set; }

        [Column] public string Content { get; set; }

        [Column] public Guid? RemarkCategory_Id { get; set; }

	}


    
	[TableName("Sellers")]

	[PrimaryKey("Id")]

	[ExplicitColumns]
    public partial class Seller : DefaultConnectionDB.Record<Seller>  
    {

        [Column] public Guid Id { get; set; }

        [Column] public string Name { get; set; }

        [Column] public string Address { get; set; }

        [Column] public decimal MapLongitude { get; set; }

        [Column] public decimal MapLatitude { get; set; }

        [Column] public string Remark { get; set; }

        [Column] public string SellerTime { get; set; }

        [Column] public int ParentId { get; set; }

        [Column] public string Keyword { get; set; }

        [Column] public string ImgUrl { get; set; }

        [Column] public Guid? SellerType_Id { get; set; }

	}


    
	[TableName("Areas")]

	[PrimaryKey("Id")]

	[ExplicitColumns]
    public partial class Area : DefaultConnectionDB.Record<Area>  
    {

        [Column] public Guid Id { get; set; }

        [Column] public Guid ParentId { get; set; }

        [Column] public string AreaCode { get; set; }

        [Column] public string AreaName { get; set; }

        [Column] public int? Sort { get; set; }

        [Column] public string Remark { get; set; }

        [Column] public int? RecFlag { get; set; }

        [Column] public int? TransFlag { get; set; }

	}


}



