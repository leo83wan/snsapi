using System.Collections.Generic;

namespace sns.Models.v1.Administrator
{
    /// <summary>
    /// 帐户
    /// </summary>
    public class UserResponse
    {
        /// <summary>
        /// 帐户ID
        /// </summary>
        /// <value></value>
        public int AccountId { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        /// <value></value>
        public string Username { get; set; }
        /// <summary>
        /// 禁用到期时间
        /// </summary>
        /// <value></value>
        public string DisabledAt { get; set; }
        /// <summary>
        /// 锁定到期时间
        /// </summary>
        /// <value></value>
        public string LockedAt { get; set; }
        /// <summary>
        /// 注册IP
        /// </summary>
        /// <value></value>
        public string CreatedAtIp { get; set; }
        /// <summary>
        /// 注册时间
        /// </summary>
        /// <value></value>
        public string CreatedAt { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        /// <value></value>
        public string UpdatedAt { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        /// <value></value>
        public string Nickname { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        /// <value></value>
        public string Birthday { get; set; }
        /// <summary>
        /// 头像URL
        /// </summary>
        /// <value></value>
        public string AvatarUrl { get; set; }
        /// <summary>
        /// 帐户角色
        /// </summary>
        /// <value></value>
        public IList<string> Roles { get; set; }
    }
}