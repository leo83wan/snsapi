using System;
using System.ComponentModel.DataAnnotations;

namespace com.leo83.apis.sns.Models.v1
{
    /// <summary>
    /// 修改密码请求参数
    /// </summary>
    public class PasswordRequest
    {
        /// <summary>
        /// 原密码
        /// </summary>
        /// <value></value>
        [Display(Name="原密码")]
        [Required]
        [MinLength(6, ErrorMessage = "{0} 最短 {1} 位"), MaxLength(32, ErrorMessage = "{0} 最长 {1} 位")]
        public string Password { get; set; }
        /// <summary>
        /// 新密码
        /// </summary>
        /// <value></value>
        [Display(Name="新密码")]
        [Required]
        [MinLength(6, ErrorMessage = "{0} 最短 {1} 位"), MaxLength(32, ErrorMessage = "{0} 最长 {1} 位")]
        public string NewPassword { get; set; }
        /// <summary>
        /// 确认新密码
        /// </summary>
        /// <value></value>
        [Required]
        [Compare("NewPassword")]
        public string ConfirmNewPassword { get; set; }
    }
}