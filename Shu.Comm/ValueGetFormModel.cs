using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Diagnostics;

namespace Shu.Comm
{
    public class FormModel
    {
        private static void SetPropertyValue<T>(PropertyInfo p, string value, T entity)
        {
            if (p == null) return;
            if (value == null) return;
            if (entity == null) return;
            //if (value.Trim() == string.Empty) return;

            if (p.PropertyType == typeof(string))
            {
                p.SetValue(entity, value, null);
                return;
            }
            if (p.PropertyType == typeof(char?))
            {
                if (value!="")
                {
                    char charValue = Convert.ToChar(value);
                    p.SetValue(entity, charValue, null);
                }
                return;
            }
            else if (p.PropertyType == typeof(int))
            {
                p.SetValue(entity, int.Parse(value), null);
                return;
            }
            else if (p.PropertyType == typeof(int?))
            {
                if(value=="")
                    p.SetValue(entity, null, null);
                else
                    p.SetValue(entity, int.Parse(value), null);
                return;
            }
            else if (p.PropertyType == typeof(float))
            {
                p.SetValue(entity, float.Parse(value), null);
                return;
            }
            else if (p.PropertyType == typeof(float?))
            {
                if (value == "")
                    p.SetValue(entity, null, null);
                else
                    p.SetValue(entity, float.Parse(value), null);
                return;
            }
            else if (p.PropertyType == typeof(double))
            {
                p.SetValue(entity, double.Parse(value), null);
                return;
            }
            else if (p.PropertyType == typeof(double?))
            {
                if (value == "")
                    p.SetValue(entity, null, null);
                else
                    p.SetValue(entity, double.Parse(value), null);
                return;
            }
            else if (p.PropertyType == typeof( decimal))
            {
                p.SetValue(entity, decimal.Parse(value), null);
                return;
            }
            else if (p.PropertyType == typeof(decimal?))
            {
                if (value == "")
                {
                    p.SetValue(entity, null, null);
                }
                else p.SetValue(entity, decimal.Parse(value), null);
                return;
            }
            else if (p.PropertyType == typeof(DateTime))
            {
                p.SetValue(entity, DateTime.Parse(value), null);
                return;
            }
            else if (p.PropertyType == typeof(DateTime?))
            {
                if(value!="")
                    p.SetValue(entity, DateTime.Parse(value), null);
                else
                    p.SetValue(entity, null, null);
                return;
            }
            else if (p.PropertyType == typeof(bool))
            {
                bool b = false;
                if (Array.IndexOf(new string[] { "on", "checked", "true", "True" }, value) >= 0)
                    b = true;
                p.SetValue(entity, b, null);
                return;
            }
        }
        /// <summary>
        /// 设置表单数据
        /// </summary>
        public static void SetForm<T>(UserControl Ucontrol, T enity, string prefix)
        {
            foreach (PropertyInfo p in typeof(T).GetProperties())
            {
                Control ctrl = Ucontrol.FindControl(prefix + p.Name);
                if (ctrl != null)
                {
                    string value = string.Empty;

                    if (p.GetValue(enity, null) != null)
                    {

                      

                            value = p.GetValue(enity, null).ToString().Trim();
                        
                    }
                    else
                    {
                        continue;
                    }

                    WebControlValue.SetValue(ctrl, value);
                }
            }
        }

        /// <summary>
        /// 设置表单数据
        /// </summary>
        public static void SetForm<T>(Page Ucontrol, T enity, string prefix)
        {
            foreach (PropertyInfo p in typeof(T).GetProperties())
            {
                Control ctrl = FindControl(Ucontrol, prefix + p.Name);
                if (ctrl != null)
                {
                    string value = string.Empty;
                    if (p.GetValue(enity, null) != null)
                    {
                        if (p.PropertyType == typeof(DateTime) || p.PropertyType == typeof(DateTime?))
                        {
                            value = p.GetValue(enity, null).ToString().Trim();

                            if (!string.IsNullOrEmpty(value))
                            {
                                value = DateTime.Parse(value).ToString("yyyy-MM-dd");
                            }
                        }
                        else
                        {

                            value = p.GetValue(enity, null).ToString().Trim();
                        }
                    }
                    else
                    {
                        continue;
                    }

                    WebControlValue.SetValue(ctrl, value);
                }
            }
        }

        private static Control FindControl(Page page, string id)
        {
            if (page == null) return null;
            Control ctrl = page.FindControl(id);
            if (ctrl != null) return ctrl;
            foreach (Control cr in page.Form.Controls)
            {
                if (cr is ContentPlaceHolder)
                {
                    ctrl = (cr as ContentPlaceHolder).FindControl(id);
                    if (ctrl != null) return ctrl;
                }
            }
            return null;
        }


        /// <summary>
        /// 取得表单数据
        /// </summary>
        public static void GetForm<T>(UserControl Ucontrol, T entity, string prefix)
        {
            foreach (PropertyInfo p in typeof(T).GetProperties())
            {
                Control ctrl = Ucontrol.FindControl(prefix + p.Name);

                if (ctrl != null)
                {
                    //Trace.Write(" not null ,type = "+ ctrl.GetType().Name);
                    object value = WebControlValue.GetValue(ctrl);
                    SetPropertyValue<T>(p, value.ToString(), entity);
                }
                //Trace.WriteLine("");
            }
        }

        /// <summary>
        /// 取得表单数据
        /// </summary>
        public static void GetForm<T>(Page Ucontrol, T entity, string prefix)
        {
            foreach (PropertyInfo p in typeof(T).GetProperties())
            {
                Control ctrl = FindControl(Ucontrol, prefix + p.Name);

                if (ctrl != null)
                {
                    object value = WebControlValue.GetValue(ctrl);
                    SetPropertyValue<T>(p, value.ToString(), entity);
                }
            }
        }


        /// <summary>
        /// 取得form数据
        /// </summary>
        public static void GetFormJS<T>(HttpContext context, T entity)
        {
            foreach (PropertyInfo p in typeof(T).GetProperties())
            {
                string keyName = "txt" + p.Name;
                if (context.Request.Form[keyName] != "")
                    SetPropertyValue<T>(p, context.Request.Form[keyName], entity);
            }
        }

        public static string setFormJS<T>(string preStr, T enity)
        {
            string strReturn = "";
            foreach (PropertyInfo p in typeof(T).GetProperties())
            {
                    if (p.GetValue(enity, null) != null)
                    {
                        string name = preStr + p.Name;
                        string value = "";
                        if (p.PropertyType == typeof(DateTime) || p.PropertyType == typeof(DateTime?))
                        {
                            value = p.GetValue(enity, null).ToString().Trim();

                            if (!string.IsNullOrEmpty(value))
                            {
                                value = DateTime.Parse(value).ToString("yyyy年MM月dd日");
                            }
                        }
                        else
                        {

                            value = p.GetValue(enity, null).ToString().Trim();
                        }

                        strReturn += "|" + name + "=" + value;

                    }
                    else
                    {
                        continue;
                    }
            }
            return strReturn;
        }
    }
}
