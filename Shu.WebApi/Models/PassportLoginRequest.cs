using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shu.WebApi.Models
{

    public class PassportLoginRequest
    {
        [MaxLength(50)]
        [Required]
        [DisplayName("邮箱地址")]
        public string UserName { get; set; }

        [Required]
        [DisplayName("登录密码")]
        public string Password { get; set; }

        [MinLength(32)]
        [MaxLength(40)]
        [Required]
        [Display(Name = "应用标识")]
        public string AppKey { get; set; }

        [StringLength(4)]
        [Required]
        [Display(Name = "验证码")]
        public string Code { get; set; }

        public void Trim()
        {
            UserName = UserName.Trim();
            Password = Password.Trim();
            AppKey = AppKey.Trim();
        }
    }
}