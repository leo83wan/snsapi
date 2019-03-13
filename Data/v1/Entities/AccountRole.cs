using System;

namespace com.leo83.apis.sns.Data.v1.Entities
{
    /// <summary>
    /// 帐户角色
    /// </summary>
    public class AccountRole
    {
        /// <summary>
        /// 帐户ID
        /// </summary>
        /// <value></value>
        public int AccountId { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        /// <value></value>
        public string Role { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        /// <value></value>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        /// <value></value>
        public DateTime? UpdatedAt { get; set; }
        /// <summary>
        /// 帐户
        /// </summary>
        /// <value></value>
        public virtual Account Account { get; set; }
    }
}