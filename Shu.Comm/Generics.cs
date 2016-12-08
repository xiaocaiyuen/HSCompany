using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using Shu.Model;
using System.Linq.Expressions;
using System.Reflection;

namespace Shu.Comm
{
    public class Generics : System.Web.UI.Page
    {
        /// <summary>
        /// 动态排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Sour"></param>
        /// <param name="SortExpression"></param>
        /// <param name="Direction"></param>
        /// <returns></returns>
        public static List<T> OrderSort<T>(IQueryable<T> Sour, string SortExpression, string Direction)
        {
            string SortDirection = string.Empty;
            if (Direction == "asc")
                SortDirection = "OrderBy";
            else if (Direction == "desc")
                SortDirection = "OrderByDescending";
            ParameterExpression pe = Expression.Parameter(typeof(T), SortExpression);
            PropertyInfo pi = typeof(T).GetProperty(SortExpression);
            Type[] types = new Type[2];
            types[0] = typeof(T);
            types[1] = pi.PropertyType;
            Expression expr = Expression.Call(typeof(Queryable), SortDirection, types, Sour.Expression, Expression.Lambda(Expression.Property(pe, SortExpression), pe));
            List<T> query = Sour.Provider.CreateQuery<T>(expr) as List<T>;
            return query;
        }
    }
}
