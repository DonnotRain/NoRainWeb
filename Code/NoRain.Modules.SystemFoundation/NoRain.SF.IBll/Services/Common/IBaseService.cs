﻿using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace NoRain.Business.IService
{
    public interface IBaseService
    {
        T Update<T>(T entity) where T : class;
        T Insert<T>(T entity) where T : class;
        List<T> Insert<T>(List<T> entitys) where T : class;
        List<T> Update<T>(List<T> entitys) where T : class;
        void Delete<T>(T entity) where T : class;
        void Delete<T>(List<T> entitys) where T : class;

        T Find<T>(string sql, params object[] keyValues) where T : class;

        IEnumerable<T> FindAll<T>(string sql, params object[] keyValues) where T : class;

        Page<T> FindAllByPage<T>(string sql, int pageSize, int pageIndex, params object[] paramaters) where T : class;
    }
}
