using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Shu.BLL;
using Shu.Model;
using Shu.WebApi.Cache;
using System.Globalization;
using Shu.Utility.Filters;

namespace Shu.WebApi.ApiControllers
{
    /// <summary>
    /// 获取SSO验证信息
    /// </summary>
    public class PassportController : ApiController
    {
        private readonly UserAuthSessionService _authSessionService = new UserAuthSessionService();
        private readonly SSO_UserAuthOperatesBLL _userAuthOperateService = new SSO_UserAuthOperatesBLL();
        /// <summary>
        /// Get获取是否登陆成功
        /// </summary>
        /// <param name="sessionKey"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public bool Get(string sessionKey = "", string remark = "")
        {
            if (_authSessionService.GetCache(sessionKey))
            {
                //_userAuthOperateService.Add(new SSO_UserAuthOperates
                //{
                //    CreateTime = DateTime.Now,
                //    IpAddress = Request.RequestUri.Host,
                //    Remark = string.Format("验证成功-{0}", remark),
                //    SessionKey = sessionKey
                //});

                return true;
            }

            _userAuthOperateService.Add(new SSO_UserAuthOperates
            {
                CreateTime = DateTime.Now,
                IpAddress = Request.RequestUri.Host,
                Remark = string.Format("验证失败-{0}", remark),
                SessionKey = sessionKey
            });

            return false;
        }
    }

    public class UserAuthSessionService : ServiceContext
    {
        private readonly SSO_UserAuthSessionsBLL UserAuthSessionsBLL = new SSO_UserAuthSessionsBLL();
        public UserAuthSessionService()
        {
            SetCacheInstance(new EnyimMemcachedContext());
        }

        public SSO_UserAuthSessions ExistsByValid(string appKey, string userName)
        {
            var currentTime = DateTime.Now;
            return UserAuthSessionsBLL.Get(p => p.AppKey == appKey && p.UserName == userName && p.InvalidTime >= currentTime);
        }

        public void ExtendValid(string sessionkey, View_Sys_UserInfo UserInfo)
        {
            var model = UserAuthSessionsBLL.Get(p => p.SessionKey == sessionkey);
            if (model != null)
            {
                //延长一年
                model.InvalidTime = DateTime.Now.AddYears(1);

                UserAuthSessionsBLL.Update(model);

                //设置缓存
                CacheContext.Set(model.SessionKey, new SessionCacheItem
                {
                    AppKey = model.AppKey,
                    InvalidTime = model.InvalidTime,
                    UserName = model.UserName,
                    UserID = UserInfo.UserInfoID,
                    LoginUserName = UserInfo.UserInfo_LoginUserName,
                    DepartmentName = UserInfo.Department_Name,
                    DepartmentCode = UserInfo.UserInfo_DepCode,
                    PostID = UserInfo.UserInfo_Post,
                    PostName = UserInfo.UserInfo_PostName,
                    RoleID = UserInfo.UserInfo_RoleID,
                    RoleName = UserInfo.UserInfo_RoleName,
                    UserType = UserInfo.UserInfo_Type,
                    DepType = UserInfo.Department_Type
                });
            }
        }

        public void Create(SSO_UserAuthSessions model, View_Sys_UserInfo UserInfo)
        {
            //添加Session
            UserAuthSessionsBLL.Add(model);

            //设置缓存
            CacheContext.Set(model.SessionKey, new SessionCacheItem
            {
                AppKey = model.AppKey,
                InvalidTime = model.InvalidTime,
                UserName = model.UserName,
                UserID = UserInfo.UserInfoID,
                LoginUserName = UserInfo.UserInfo_LoginUserName,
                DepartmentName = UserInfo.Department_Name,
                DepartmentCode = UserInfo.UserInfo_DepCode,
                PostID = UserInfo.UserInfo_Post,
                PostName = UserInfo.UserInfo_PostName,
                RoleID = UserInfo.UserInfo_RoleID,
                RoleName = UserInfo.UserInfo_RoleName,
                UserType = UserInfo.UserInfo_Type,
                DepType = UserInfo.Department_Type
            });
        }

        public SSO_UserAuthSessions Get(string sessionKey)
        {
            return UserAuthSessionsBLL.Get(p => p.SessionKey == sessionKey);
        }

        public bool GetCache(string sessionKey)
        {
            var sessionCacheItem = CacheContext.Get<SessionCacheItem>(sessionKey);

            if (sessionCacheItem == null) return false;

            if (sessionCacheItem.InvalidTime > DateTime.Now)
            {
                return true;
            }
            //移除无效Session缓存
            CacheContext.Remove(sessionKey);

            return false;
        }
    }

    public abstract class ServiceContext : IDisposable
    {
        /// <summary>
        /// 缓存组件
        /// </summary>
        public CacheContext CacheContext { get; private set; }

        /// <summary>
        /// 动态设置缓存对象的新实例
        /// </summary>
        /// <param name="cacheContext">缓存实例对象</param>
        public void SetCacheInstance(CacheContext cacheContext)
        {
            //先释放现有的缓存组件
            if (CacheContext != null)
            {
                CacheContext.Dispose();
                CacheContext = null;
            }

            //初始化缓存组件新的实例
            CacheContext = cacheContext;
        }

        public void SetCacheInstance(Type cacheContextType)
        {
            if (cacheContextType == null)
            {
                throw new ArgumentNullException("cacheContextType");
            }

            if (!typeof(CacheContext).IsAssignableFrom(cacheContextType))
            {
                throw new ArgumentException(
                    string.Format(CultureInfo.CurrentCulture, "该类型 {0} 必须继承自抽象类CacheContext", cacheContextType),
                    "cacheContextType");
            }

            try
            {
                CacheContext = Activator.CreateInstance(cacheContextType) as CacheContext;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                        String.Format(
                            CultureInfo.CurrentCulture,
                            "创建抽象类 CacheContext 的实例 {0} 失败",
                            cacheContextType),
                        ex);
            }
        }

        public void Dispose()
        {
            if (CacheContext != null)
            {
                CacheContext.Dispose();
            }
        }
    }

}