using System;
using System.Collections.Generic;

namespace com.leo83.apis.sns.Data.v1.Entities
{
    /// <summary>
    /// 帐号实体
    /// </summary>
    public class Account
    {
        /// <summary>
        /// ID编号
        /// </summary>
        /// <value></value>
        public int AccountId { get; set; }
        /// <summary>
        /// 帐号
        /// </summary>
        /// <value></value>
        public string Username { get; set; }
        /// <summary>
        /// 密码使用Base64储存
        /// </summary>
        /// <value></value>
        public string Password { get; set; }
        /// <summary>
        /// 创建时的客户端IP
        /// </summary>
        /// <value></value>
        public string CreatedAtClientIP { get; set; }
        /// <summary>
        /// 禁用到期时间
        /// </summary>
        /// <value></value>
        public DateTime? DisabledAt { get; set; }
        /// <summary>
        /// 锁定到期时间
        /// </summary>
        /// <value></value>
        public DateTime? LockedAt { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        /// <value></value>
        public DateTime? UpdatedAt { get; set; }
        /// <summary>
        /// 帐户信息
        /// </summary>
        /// <value></value>
        public virtual AccountInfo Info { get; set; }
        /// <summary>
        /// 帐户角色
        /// </summary>
        /// <value></value>
        public virtual List<AccountRole> Roles { get; set; }

        // public virtual IList<AccountCollectionVideo> AccountCollectionVideos { get; set; }
    }
}