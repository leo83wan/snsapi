using System;
using System.ComponentModel.DataAnnotations;
using com.leo83.apis.sns.Data.v1.Entities;

namespace com.leo83.apis.sns.Models.v1
{
    /// <summary>
    /// 注册请求参数
    /// </summary>
    public class RegisterRequest
    {
        /// <summary>
        /// 用户名
        /// </summary>
        /// <value></value>
        [Display(Name = "用户名")]
        [Required(ErrorMessage = "{0} 是必须的")]
        [MinLength(2, ErrorMessage = "{0} 最短 {1} 位"), MaxLength(32, ErrorMessage = "{0} 最长 {1} 位")]
        public string Username { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        /// <value></value>
        [Display(Name = "用户名")]
        [Required]
        [MinLength(6, ErrorMessage = "{0} 最短 {1} 位"), MaxLength(32, ErrorMessage = "{0} 最长 {1} 位")]
        public string Password { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        /// <value></value>
        [Display(Name = "昵称")]
        [MaxLength(32, ErrorMessage = "{0} 最长 {1} 位")]
        public string Nickname { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        /// <value></value>
        [Display(Name = "生日")]
        public DateTime? Birthday { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        /// <value></value>
        [Display(Name = "性别")]
        public Gender? Gender { get; set; }
        /// <summary>
        /// 头像URL
        /// </summary>
        /// <value></value>
        public string AvatarUrl { get; set; }
    }
}