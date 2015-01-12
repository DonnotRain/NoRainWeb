using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using HuaweiSoftware.WQT.IBll;
using HuaweiSoftware.WQT.IDal;
using PetaPoco;

namespace HuaweiSoftware.WQT.Bll
{
    public abstract class BaseBll : IBaseBLL
    {
        internal IBaseDAL m_baseDAL;

        public BaseBll(IBaseDAL baseDAL)
        {
            m_baseDAL = baseDAL;
        }


        public T Update<T>(T entity) where T : class
        {
            return m_baseDAL.Update(entity);
        }

        public List<T> Update<T>(List<T> entitys) where T : class
        {

         
            return m_baseDAL.Update(entitys);
        }

        public T Insert<T>(T entity) where T : class
        {
            return m_baseDAL.Insert(entity);
        }

        public List<T> Insert<T>(List<T> entitys) where T : class
        {
            return m_baseDAL.Insert(entitys);
        }

        public void Delete<T>(T entity) where T : class
        {
            m_baseDAL.Delete(entity);
        }

        public void Delete<T>(List<T> entitys) where T : class
        {
            m_baseDAL.Delete(entitys);
        }

        public T Find<T>(string sql, params object[] keyValues) where T : class
        {
            return m_baseDAL.Find<T>(sql, keyValues);
        }

        public IEnumerable<T> FindAll<T>(string sql="", params object[] keyValues) where T : class
        {
            return m_baseDAL.FindAll<T>(sql, keyValues);
        }

        public Page<T> FindAllByPage<T>(string sql, int pageSize, int pageIndex, params object[] paramaters) where T : class
        {
            return m_baseDAL.FindAllByPage<T>(sql, pageSize, pageIndex, paramaters);
        }


    }
}
