/*
Author      : 肖亮
Date        : 2011-6-15
Description : 通过 MSIL 实现的反射器工厂
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Shu.Utility
{
    /// <summary>
    /// 通过 MSIL 实现的反射器工厂
    /// </summary>
    internal class ILReflectorFactory : IReflectorFactory
    {
        public IMemberAccessor GetFieldAccessor(FieldInfo field)
        {
            return new ILFieldAccessor(field);
        }
        public IMemberAccessor GetPropertyAccessor(PropertyInfo property)
        {
            return new ILPropertyAccessor(property);
        }

        public IMethodInvoker GetMethodInvoker(MethodBase method)
        {
            return new ILMethodInvoker(method);
        }
    }
}
