/*
Author      : 肖亮
Date        : 2011-4-1
Description : 可以传递数据项的对象 泛型版本
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Shu.Utility
{
    /// <summary>
    /// 可以传递数据项的对象 泛型版本
    /// </summary>
    public interface ITouchable<T>
    {
        T Data { get; set; }
    }
}
