using System.ComponentModel.DataAnnotations;

namespace sns.Models.v1.Administrator
{
    /// <summary>
    /// 设置帐户角色
    /// </summary>
    public class SetAccountRoleRequest
    {
        /// <summary>
        /// 帐户ID
        /// </summary>
        /// <value></value>
        [Display(Name = "帐户ID")]
        [Required(ErrorMessage = "{0} 必须")]
        public int AccountId { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        /// <value>用户:User|管理员:Administrator</value>
        [Display(Name = "角色名称")]
        [Required(ErrorMessage = "{0} 必须")]
        public string Role { get; set; }
    }
}