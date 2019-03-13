using System.ComponentModel.DataAnnotations;

namespace com.leo83.apis.sns.Models.v1
{
    /// <summary>
    /// 授权请求模型
    /// </summary>
    public class AuthRequest
    {
        /// <summary>
        /// 授权帐号
        /// </summary>
        /// <value>string</value>
        [Display(Name = "帐号")]
        [Required(ErrorMessage = "{0} 必须")]
        [MinLength(5, ErrorMessage = "{0} 最短 {1} 位"), MaxLength(12, ErrorMessage = "{0} 最长 {1} 位")]
        public string Username { get; set; }
        /// <summary>
        /// 帐号的密码
        /// </summary>
        /// <value>string</value>
        [Display(Name = "密码")]
        [Required(ErrorMessage = "{0} 必须")]
        [MinLength(5, ErrorMessage = "{0} 最短 {1} 位"), MaxLength(12, ErrorMessage = "{0} 最长 {1} 位")]
        public string Password { get; set; }
    }
}