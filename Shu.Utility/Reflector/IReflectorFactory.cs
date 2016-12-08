/*
Author      : 肖亮
Date        : 2011-6-15
Description : 反射器工厂
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Shu.Utility
{
    /// <summary>
    /// 反射器工厂
    /// </summary>
    public interface IReflectorFactory
    {
        IMemberAccessor GetFieldAccessor(FieldInfo field);
        IMemberAccessor GetPropertyAccessor(PropertyInfo property);
        IMethodInvoker GetMethodInvoker(MethodBase method);
    }


}
