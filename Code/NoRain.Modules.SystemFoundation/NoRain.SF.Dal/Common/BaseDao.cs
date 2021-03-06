﻿using NoRain.Business.IDal;
using NoRain.Business.WebBase;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoRain.Business.Dao
{
    public class BaseDao : IBaseDao
    {

        public Database DB { get { return DBManage.Instance[_dbName]; } }
        private string _dbName;
        public BaseDao(string dbConnectionName)
        {
            this._dbName = dbConnectionName;
        }

        public T Update<T>(T entity) where T : class
        {
            DB.Update(entity);

            return entity;
        }

        public List<T> Update<T>(List<T> entitys) where T : class
        {
            PetaPoco.Transaction transac = new Transaction(DB);
            using (transac)
            {
                foreach (T t1 in entitys)
                {
                    DB.Update(t1);

                }
                transac.Complete();
            }

            return entitys;
        }

        public T Insert<T>(T entity) where T : class
        {
            DB.Insert(entity);

            return entity;
        }

        public List<T> Insert<T>(List<T> entitys) where T : class
        {
            PetaPoco.Transaction transac = new Transaction(DB);
            using (transac)
            {
                foreach (T t1 in entitys)
                {
                    DB.Insert(t1);
                }
                transac.Complete();
            }
            return entitys;
        }

        public void Delete<T>(T entity) where T : class
        {
            DB.Delete(entity);
        }

        public void Delete<T>(List<T> entitys) where T : class
        {
            PetaPoco.Transaction transac = new Transaction(DB);
            using (transac)
            {
                foreach (T t1 in entitys)
                {
                    DB.Delete(t1);
                }
                transac.Complete();
            }

        }

        public T Find<T>(String sql, params object[] parameters) where T : class
        {
            var item = DB.FirstOrDefault<T>(sql, parameters);
            return item;
        }

        public IEnumerable<T> FindAll<T>(String sql, params object[] parameters) where T : class
        {
            return DB.Fetch<T>(sql, parameters);
        }

        public Page<T> FindAllByPage<T>(String sql, int pageSize, int pageIndex, params object[] paramaters) where T : class
        {
            return DB.Page<T>(pageIndex, pageSize, sql, paramaters);
        }

        public void Dispose()
        {
            this.DB.Dispose();
        }
    }
}
