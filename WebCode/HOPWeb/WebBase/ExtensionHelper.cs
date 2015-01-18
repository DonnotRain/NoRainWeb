using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Linq.Expressions;

namespace NoRain.Business.WebBase
{
    public static class ExtensionHelper
    {
        /// <summary>
        /// 判断集合是否为空或没有元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ie"></param>
        /// <returns>true：集合不为空且至少有一个元素；false：集合为空或没有元素</returns>
        public static bool HasItems<T>(this IEnumerable<T> ie)
        {
            if (ie == null || !ie.Any())
            {
                return false;
            }
            return true;
        }

        public static void CopyTo(this Stream input, Stream destination)
        {
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (!input.CanRead)
            {
                throw new NotSupportedException("UnreadableStream");
            }
            if (!destination.CanWrite) 
            {
                throw new NotSupportedException("UnwritableStream");
            }
            byte[] array = new byte[1024 * 5];
            int count;
            while ((count = input.Read(array, 0, array.Length)) != 0)
            {
                destination.Write(array, 0, count);
            }
        }

        /// <summary>
        /// 将指定分隔符的字符串转换为List<int>
        /// </summary>
        /// <param name="str"></param>
        /// <param name="seperators"></param>
        /// <returns></returns>
        public static List<int> ToIntList(this String str, string[] seperators)
        {
            List<int> ints = new List<int>();
            if (!string.IsNullOrEmpty(str))
            {
                int i;
                string[] cs = str.Split(seperators, StringSplitOptions.RemoveEmptyEntries);
                foreach (var c in cs)
                {
                    if (int.TryParse(c, out i))
                    {
                        ints.Add(i);
                    }
                }
            }

            return ints;
        }

        /// <summary>
        /// 将指定分隔符的字符串转换为List<string>
        /// </summary>
        /// <param name="str"></param>
        /// <param name="seperators"></param>
        /// <returns></returns>
        public static List<string> ToStringList(this string str, string[] seperators)
        {
            List<string> strings = new List<string>();
            if (!string.IsNullOrEmpty(str))
            {
                string[] cs = str.Split(seperators, StringSplitOptions.RemoveEmptyEntries);
                foreach (var c in cs)
                {
                    strings.Add(c);
                }
            }

            return strings;
        }

        /// <summary>
        /// 根据日期判断是否相等
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        public static bool EqualsByDate(this DateTime? d1, DateTime? d2) 
        {
            if (!d1.HasValue && !d2.HasValue)
            {
                return true;
            }

            if (d1.HasValue && d2.HasValue)
            {
                return d1.Value.Date == d2.Value.Date;
            }

            return false;
        }

        /// <summary>
        /// 动态排序扩展
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="propertyName">属性名称</param>
        /// <param name="ascending">是否升序</param>
        /// <returns></returns>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName, bool ascending) where T : class
        {
            Type type = typeof(T);

            PropertyInfo property = type.GetProperty(propertyName);
            if (property == null)
                throw new ArgumentException("propertyName", "Not Exist");

            ParameterExpression param = Expression.Parameter(type, "p");
            Expression propertyAccessExpression = Expression.MakeMemberAccess(param, property);
            LambdaExpression orderByExpression = Expression.Lambda(propertyAccessExpression, param);

            string methodName = ascending ? "OrderBy" : "OrderByDescending";

            MethodCallExpression resultExp = Expression.Call(typeof(Queryable), methodName, new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExpression));

            return source.Provider.CreateQuery<T>(resultExp);
        }

        /// <summary>
        /// 动态排序扩展
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="propertyName">属性名称</param>
        /// <param name="order">"asc"或"desc"</param>
        /// <returns></returns>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName, string order) where T : class
        {
            order = order.ToUpper();

            Type type = typeof(T);

            PropertyInfo property = type.GetProperty(propertyName);
            if (property == null)
                throw new ArgumentException("propertyName", "Not Exist");

            ParameterExpression param = Expression.Parameter(type, "p");
            Expression propertyAccessExpression = Expression.MakeMemberAccess(param, property);
            LambdaExpression orderByExpression = Expression.Lambda(propertyAccessExpression, param);

            string methodName = order=="ASC" ? "OrderBy" : "OrderByDescending";

            MethodCallExpression resultExp = Expression.Call(typeof(Queryable), methodName, new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExpression));

            return source.Provider.CreateQuery<T>(resultExp);
        }

    }
}
