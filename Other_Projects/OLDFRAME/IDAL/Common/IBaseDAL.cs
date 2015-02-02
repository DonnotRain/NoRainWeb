using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MPAPI.IDAL
{
    public interface IBaseDAL
    {
        T Update<T>(T entity) where T : class;
        T Insert<T>(T entity) where T : class;
        List<T> Insert<T>(List<T> entitys) where T : class;
        List<T> Update<T>(List<T> entitys) where T : class;
        void Delete<T>(T entity) where T : class;
        void Delete<T>(List<T> entitys) where T : class;
        T Find<T>(params object[] keyValues) where T : class;
        T Find<T>(Expression<Func<T, bool>> conditions) where T : class;
        List<T> FindAll<T>(Expression<Func<T, bool>> conditions = null) where T : class;
        IPagedList<T> FindAllByPage<T, S>(Expression<Func<T, bool>> conditions, Expression<Func<T, S>> orderBy, int pageSize, int pageIndex) where T : class;

        IPagedList<T> FindAllByPage<T, S>(Expression<Func<T, bool>> conditions, string sort, string order, int pageSize, int pageIndex) where T : class;
    }
}
