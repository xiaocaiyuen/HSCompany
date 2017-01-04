using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shu.Model;
using Shu.BLL;
using Shu.WebApi.ApiControllers;
using Shu.WebApi.Models;
using Shu.Utility.Extensions;
using Shu.Comm;
using Shu.WebApi.Extendsions;
using System.Configuration;

namespace Shu.WebApi.Controllers
{
    /// <summary>
    ///  公钥：AppKey
    ///  私钥：AppSecret
    ///  会话：SessionKey
    /// </summary>
    public class PassportController : Controller
    {
        private readonly Sys_ModuleBLL _appInfoService = new Sys_ModuleBLL();
        private readonly View_Sys_UserInfoBLL _appUserService = new View_Sys_UserInfoBLL();
        private readonly UserAuthSessionService _authSessionService = new UserAuthSessionService();
        private readonly SSO_UserAuthOperatesBLL _userAuthOperateService = new SSO_UserAuthOperatesBLL();

        private const string AppInfo = "AppInfo";
        private const string SessionKey = "SessionKey";
        private const string SessionUserName = "SessionUserName";

        //默认登录界面
        public ActionResult Index(string appKey = "", string username = "")
        {
            TempData[AppInfo] = _appInfoService.Get(p => p.Id == appKey);

            var viewModel = new PassportLoginRequest
            {
                AppKey = appKey,
                UserName = username
            };

            return View(viewModel);
        }

        //授权登录
        [HttpPost]
        public ActionResult Index(PassportLoginRequest model)
        {
            //获取应用信息
            var appInfo = _appInfoService.Get(p => p.Id == model.AppKey);
            if (appInfo == null)
            {
                //应用不存在
                return Content(new Shu.Comm.JsonResult().SetError(true).SetMsg("应用不存在。").ToJson());
                //return View(model);
            }
            if (Session["shu_session_verifycode"].IsNullOrEmpty() || model.Code.ToLower().MD5Encrypt() != Session["shu_session_verifycode"].ToString())
            {
                return Content(new Shu.Comm.JsonResult().SetError(true).SetMsg("验证码错误，请重新输入").ToJson());
            }

            TempData[AppInfo] = appInfo;

            if (ModelState.IsValid == false)
            {
                //实体验证失败
                return Content(new Shu.Comm.JsonResult().SetError(true).SetMsg("实体验证失败").ToJson());
            }

            //过滤字段无效字符
            model.Trim();

            //获取用户信息
            var userInfo = _appUserService.Get(p => p.UserInfo_LoginUserName == model.UserName);
            if (userInfo == null)
            {
                //用户不存在
                return Content(new Shu.Comm.JsonResult().SetError(true).SetMsg("用户不存在").ToJson());
            }

            if (userInfo.UserInfo_LoginUserPwd != HttpUtility.UrlDecode(DESEncrypt.Encrypt(model.Password)))
            {
                //密码不正确
                return Content(new Shu.Comm.JsonResult().SetError(true).SetMsg("密码不正确").ToJson());
            }

            //获取当前未到期的Session
            var currentSession = _authSessionService.ExistsByValid(appInfo.Id, userInfo.UserInfo_LoginUserName);
            if (currentSession == null)
            {
                //构建Session
                currentSession = new SSO_UserAuthSessions
                {
                    AppKey = appInfo.Id,
                    CreateTime = DateTime.Now,
                    InvalidTime = DateTime.Now.AddYears(1),
                    IpAddress = Request.UserHostAddress,
                    SessionKey = Guid.NewGuid().ToString().ToMd5(),
                    UserName = userInfo.UserInfo_LoginUserName
                };

                //创建Session
                _authSessionService.Create(currentSession, userInfo);
            }
            else
            {
                //延长有效期，默认一年
                _authSessionService.ExtendValid(currentSession.SessionKey, userInfo);
            }

            //记录用户授权日志
            _userAuthOperateService.Add(new SSO_UserAuthOperates
            {
                CreateTime = DateTime.Now,
                IpAddress = Request.UserHostAddress,
                Remark = string.Format("{0} 登录 {1} 授权成功", currentSession.UserName, appInfo.Name),
                SessionKey = currentSession.SessionKey
            });

            string DomainUrl = string.Empty;
#if DEBUG
            DomainUrl = ConfigurationManager.AppSettings["DomainUrl"];
#else
            DomainUrl=appInfo.DomainUrl;
#endif
            var redirectUrl = string.Format("{0}?SessionKey={1}&SessionUserName={2}",
                DomainUrl,
                currentSession.SessionKey,
                userInfo.UserInfo_LoginUserName);

            return Content(new Shu.Comm.JsonResult().SetError(false).SetMsg("登录成功。").SetData(redirectUrl).ToJson());
            //跳转默认回调页面
            //return Redirect(redirectUrl);
        }

        [HttpGet]
        public ActionResult GetAuthCode()
        {
            return File(new VerifyCode().GetVerifyCode(), @"image/Gif");
        }
    }
}