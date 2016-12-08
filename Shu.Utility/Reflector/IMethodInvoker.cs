/*
Author      : 肖亮
Date        : 2011-10-28
Description : 方法调用器接口
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shu.Utility
{
    /// <summary>
    /// 方法调用器接口
    /// </summary>
    public interface IMethodInvoker
    {
        object Invoke(object instance, params object[] parameters);
    }
}
