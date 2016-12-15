using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Shu.Comm
{
    public static class FormToModelHelper<T> where T : new()
    {
        public static T ConvertToModel(HttpContext context)
        {
            T t = new T();
            PropertyInfo[] propertys = t.GetType().GetProperties();
            foreach (PropertyInfo pi in propertys)
            {
                if (!pi.CanWrite)
                    continue;
                string value = context.Request.Form[pi.Name];
                try
                {
                    if (!string.IsNullOrEmpty(value))
                        SetPropertyValue(pi, context.Request.Form[pi.Name], t);
                    //pi.SetValue(t, Convert.ChangeType(value, pi.PropertyType), null);//这一步很重要，用于类型转换
                    else
                        pi.SetValue(t, value, null);
                }
                catch
                { }
            }

            return t;
        }

        //public static T ConvertToModel(HttpContext context,T t)
        //{
        //    PropertyInfo[] propertys = t.GetType().GetProperties();
        //    foreach (PropertyInfo pi in propertys)
        //    {
        //        if (!pi.CanWrite)
        //            continue;
        //        object value = context.Request[pi.Name];
        //        if (value != null && value != DBNull.Value)
        //        {
        //            try
        //            {
        //                if (value.ToString() != "")
        //                    pi.SetValue(t, Convert.ChangeType(value, pi.PropertyType), null);//这一步很重要，用于类型转换
        //                else
        //                    pi.SetValue(t, value, null);
        //            }
        //            catch (Exception ex)
        //            {
        //                CMMLog.Error(ex.ToString());
        //            }
        //        }
        //    }

        //    return t;
        //}

        /// <summary>
        /// 取得form数据
        /// </summary>
        public static T ConvertToModel(HttpContext context, T entity)
        {
            foreach (PropertyInfo p in typeof(T).GetProperties())
            {
                string keyName = p.Name;
                if (context.Request.Form[keyName] != "")
                    SetPropertyValue(p, context.Request.Form[keyName], entity);
            }
            return entity;
        }

        /// <summary>
        /// form转成model相关数据（加前缀）
        /// </summary>
        /// <param name="context"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static T ConvertToModel(HttpContext context, string prefix)
        {
            T t = new T();
            PropertyInfo[] propertys = t.GetType().GetProperties();
            foreach (PropertyInfo pi in propertys)
            {
                if (!pi.CanWrite)
                    continue;
                object value = context.Request[prefix + pi.Name];
                if (value != null && value != DBNull.Value)
                {
                    try
                    {
                        if (value.ToString() != "")
                            pi.SetValue(t, Convert.ChangeType(value, pi.PropertyType), null);//这一步很重要，用于类型转换
                        else
                            pi.SetValue(t, value, null);
                    }
                    catch
                    { }
                }
            }

            return t;
        }

        /// <summary>
        /// form转成model相关数据（加前缀）
        /// </summary>
        /// <param name="context"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static T ConvertToModel(HttpContext context, T t, string prefix)
        {
            PropertyInfo[] propertys = t.GetType().GetProperties();
            foreach (PropertyInfo pi in propertys)
            {
                if (!pi.CanWrite)
                    continue;
                object value = context.Request[prefix + pi.Name];
                if (value != null && value != DBNull.Value)
                {
                    try
                    {
                        if (value.ToString() != "")
                            pi.SetValue(t, Convert.ChangeType(value, pi.PropertyType), null);//这一步很重要，用于类型转换
                        else
                            pi.SetValue(t, value, null);
                    }
                    catch
                    { }
                }
            }

            return t;
        }

        private static T SetPropertyValue(PropertyInfo p, string value, T entity)
        {
            if (p == null) return entity;
            if (value == null) return entity;
            if (entity == null) return entity;
            //if (value.Trim() == string.Empty) return entity;

            if (p.PropertyType == typeof(string))
            {
                p.SetValue(entity, value, null);
                return entity;
            }
            if (p.PropertyType == typeof(char?))
            {
                if (value != "")
                {
                    char charValue = Convert.ToChar(value);
                    p.SetValue(entity, charValue, null);
                }
                return entity;
            }
            else if (p.PropertyType == typeof(int))
            {
                p.SetValue(entity, int.Parse(value), null);
                return entity;
            }
            else if (p.PropertyType == typeof(int?))
            {
                if (value == "")
                    p.SetValue(entity, null, null);
                else
                    p.SetValue(entity, int.Parse(value), null);
                return entity;
            }
            else if (p.PropertyType == typeof(float))
            {
                p.SetValue(entity, float.Parse(value), null);
                return entity;
            }
            else if (p.PropertyType == typeof(float?))
            {
                if (value == "")
                    p.SetValue(entity, null, null);
                else
                    p.SetValue(entity, float.Parse(value), null);
                return entity;
            }
            else if (p.PropertyType == typeof(double))
            {
                p.SetValue(entity, double.Parse(value), null);
                return entity;
            }
            else if (p.PropertyType == typeof(double?))
            {
                if (value == "")
                    p.SetValue(entity, null, null);
                else
                    p.SetValue(entity, double.Parse(value), null);
                return entity;
            }
            else if (p.PropertyType == typeof(decimal))
            {
                p.SetValue(entity, decimal.Parse(value), null);
                return entity;
            }
            else if (p.PropertyType == typeof(decimal?))
            {
                if (value == "")
                {
                    p.SetValue(entity, null, null);
                }
                else p.SetValue(entity, decimal.Parse(value), null);
                return entity;
            }
            else if (p.PropertyType == typeof(DateTime))
            {
                p.SetValue(entity, DateTime.Parse(value), null);
                return entity;
            }
            else if (p.PropertyType == typeof(DateTime?))
            {
                if (value != "")
                    p.SetValue(entity, DateTime.Parse(value), null);
                else
                    p.SetValue(entity, null, null);
                return entity;
            }
            else if (p.PropertyType == typeof(bool))
            {
                bool b = false;
                if (Array.IndexOf(new string[] { "on", "checked", "true", "True", "1" }, value) >= 0)
                    b = true;
                p.SetValue(entity, b, null);
                return entity;
            }
            return entity;
        }
        //Model.Users model = new Model.Users();
        //model = FormToModelHelper<Model.Users>.ConvertToModel(context);//context 为ashx里的HttpContext
    }
}
