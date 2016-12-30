using Shu.Utility.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Shu.Model;
using Shu.WebApi.Models;

namespace Shu.WebApi.ApiControllers
{
    /// <summary>
    /// 菜单功能
    /// </summary>
    public class MenuController : BaseController
    {
        /// <summary>
        /// 获取菜单权限列表
        /// </summary>
        /// <param name="keyId">Key</param>
        /// <param name="userId">用户ID</param>
        /// <returns>数据实体</returns>
        [ActionName("List")]
        public WebApiResult<IQueryable<MenuModels>> GetCommentsList(string RoleId)
        {
            return null;
        }
    }
}