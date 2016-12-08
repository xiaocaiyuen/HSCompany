/*
Author      : 肖亮
Date        : 2011-3-7
Description : 对 System.Web.Caching.Cache的扩展
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebCache = System.Web.Caching.Cache;
using System.Web.Caching;

namespace Shu.Utility.Extensions
{
    /// <summary>
    /// 对 System.Web.Caching.Cache的扩展
    /// </summary>
    public static class CacheExtension
    {
        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        static public object Insert(this WebCache cache, string key, object value, DateTime time, string dependencieFile, CacheItemPriority priority)
        {
            cache.Insert(key, value, dependencieFile == null ? null : new CacheDependency(dependencieFile), time, WebCache.NoSlidingExpiration, priority, null);
            return value;
        }


        /// <summary>
        /// 添加缓存 使用CacheItemPriority.Default等级
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        /// <param name="time">过期时间</param>
        /// <param name="dependencieFile">依赖文件</param>
        static public object Insert(this WebCache cache, string key, object value, DateTime time, string dependencieFile)
        {
            cache.Insert(key, value, time, dependencieFile, CacheItemPriority.Default);
            return value;
        }
        /// <summary>
        /// 存缓存中获取数据如果数据不存在则创建数据并将其缓存
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="key">缓存键</param>
        /// <param name="createData">缓存数据不存在时 将要执行的创建数据的方法</param>
        /// <param name="time">过期时间</param>
        /// <returns></returns>
        static public object Get(this WebCache cache, string key, Func<object> createData, DateTime time)
        {
            return cache.Get(key) ?? Insert(cache, key, createData(), time, null);
        }
        /// <summary>
        /// 存缓存中获取数据如果数据不存在则创建数据并将其缓存
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="key">缓存键</param>
        /// <param name="createData">缓存数据不存在时 将要执行的创建数据的方法</param>
        /// <param name="time">过期时间</param>
        /// <returns></returns>
        static public T Get<T>(this WebCache cache, string key, Func<T> createData, DateTime time)
        {
            return cache.Get<T>(key, createData, time, null);
        }

        /// <summary>
        /// 存缓存中获取数据如果数据不存在则创建数据并将其缓存
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="key">缓存键</param>
        /// <param name="createData">缓存数据不存在时 将要执行的创建数据的方法</param>
        /// <param name="time">过期时间</param>
        /// <param name="dependencieFile">依赖文件</param>
        /// <returns></returns>
        static public T Get<T>(this WebCache cache, string key, Func<T> createData, DateTime time, string dependencieFile)
        {
            return (T)(cache.Get(key) ?? Insert(cache, key, createData(), time, dependencieFile));
        }

        /// <summary>
        /// 存缓存中获取数据如果数据不存在则创建数据并将其缓存
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="key">缓存键</param>
        /// <param name="createData">缓存数据不存在时 将要执行的创建数据的方法</param>
        /// <param name="timeSpan">相对过期时间</param>
        /// <returns></returns>
        static public T Get<T>(this WebCache cache, string key, Func<T> createData, TimeSpan timeSpan, CacheItemPriority priority)
        {
            var oldVal = cache.Get(key);
            if (oldVal != null)
                return (T)oldVal;

            T newVal = createData();
            if (newVal != null)
            {
                cache.Insert(key, newVal, null, Cache.NoAbsoluteExpiration, timeSpan, priority, null);
            }

            return (T)newVal;
        }

        /// <summary>
        /// 存缓存中获取数据如果数据不存在则创建数据并将其缓存
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="key">缓存键</param>
        /// <param name="createData">缓存数据不存在时 将要执行的创建数据的方法</param>
        /// <param name="timeSpan">相对过期时间</param>
        /// <returns></returns>
        static public T Get<T>(this WebCache cache, string key, Func<T> createData, TimeSpan timeSpan)
        {
            return Get<T>(cache, key, createData, timeSpan, CacheItemPriority.High);
        }
    }
}
