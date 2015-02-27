
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
            if (_instance != null)
                return _instance;

            if (Factory != null)
                return Factory.GetInstance();
            else
                return new DefaultConnectionDB();
        }

        [ThreadStatic]
        static DefaultConnectionDB _instance;

        public override void OnBeginTransaction()
        {
            if (_instance == null)
                _instance = this;
        }

        public override void OnEndTransaction()
        {
            if (_instance == this)
                _instance = null;
        }

        public class Record<T> where T : new()
        {
            public static DefaultConnectionDB repo { get { return DefaultConnectionDB.GetInstance(); } }
            public bool IsNew() { return repo.IsNew(this); }
            public object Insert() { return repo.Insert(this); }
            public void Save() { repo.Save(this); }
            public int Update() { return repo.Update(this); }
            public int Update(IEnumerable<string> columns) { return repo.Update(this, columns); }
            public static int Update(string sql, params object[] args) { return repo.Update<T>(sql, args); }
            public static int Update(Sql sql) { return repo.Update<T>(sql); }
            public int Delete() { return repo.Delete(this); }
            public static int Delete(string sql, params object[] args) { return repo.Delete<T>(sql, args); }
            public static int Delete(Sql sql) { return repo.Delete<T>(sql); }
            public static int Delete(object primaryKey) { return repo.Delete<T>(primaryKey); }
            public static bool Exists(object primaryKey) { return repo.Exists<T>(primaryKey); }
            public static bool Exists(string sql, params object[] args) { return repo.Exists<T>(sql, args); }
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
            public static List<T> SkipTake(long skip, long take, string sql, params object[] args) { return repo.SkipTake<T>(skip, take, sql, args); }
            public static List<T> SkipTake(long skip, long take, Sql sql) { return repo.SkipTake<T>(skip, take, sql); }
            public static Page<T> Page(long page, long itemsPerPage, string sql, params object[] args) { return repo.Page<T>(page, itemsPerPage, sql, args); }
            public static Page<T> Page(long page, long itemsPerPage, Sql sql) { return repo.Page<T>(page, itemsPerPage, sql); }
            public static IEnumerable<T> Query(string sql, params object[] args) { return repo.Query<T>(sql, args); }
            public static IEnumerable<T> Query(Sql sql) { return repo.Query<T>(sql); }
        }
    }



    [TableName("CategoryItems")]
    [PrimaryKey("Id", autoIncrement = false)]
    [ExplicitColumns]
    public partial class CategoryItem : DefaultConnectionDB.Record<CategoryItem>
    {
        [Column]
        public Guid Id { get; set; }
        [Column]
        public string Code { get; set; }
        [Column]
        public string ItemContent { get; set; }
        [Column]
        public Guid CategoryTypeId { get; set; }
        [Column]
        public Guid? ParentId { get; set; }
        [Column]
        public int? Sort { get; set; }
        [Column]
        public string Description { get; set; }
        [Column]
        public short IsEnabled { get; set; }
    }

    [TableName("CategoryTypes")]
    [PrimaryKey("Id", autoIncrement = false)]
    [ExplicitColumns]
    public partial class CategoryType : DefaultConnectionDB.Record<CategoryType>
    {
        [Column]
        public Guid Id { get; set; }
        [Column]
        public string Code { get; set; }
        [Column]
        public string Name { get; set; }
        [Column]
        public int? Sort { get; set; }
        [Column]
        public string Description { get; set; }
        [Column]
        public short IsEnabled { get; set; }
    }

    [TableName("SysParameters")]
    [ExplicitColumns]
    public partial class SysParameter : DefaultConnectionDB.Record<SysParameter>
    {
        [Column]
        public Guid Id { get; set; }
        [Column]
        public string Name { get; set; }
        [Column]
        public string ValueContent { get; set; }
        [Column]
        public int IsEnabled { get; set; }
        [Column]
        public string Description { get; set; }
    }

    [TableName("FileItems")]
    [PrimaryKey("ID")]
    [ExplicitColumns]
    public partial class FileItem : DefaultConnectionDB.Record<FileItem>
    {
        [Column]
        public long ID { get; set; }
        [Column]
        public string FileName { get; set; }
        [Column]
        public long Size { get; set; }
        [Column]
        public string FilePath { get; set; }
        [Column]
        public string Extension { get; set; }
        [Column]
        public DateTime? CreateTime { get; set; }
        [Column]
        public int IsDeleted { get; set; }
    }
}


