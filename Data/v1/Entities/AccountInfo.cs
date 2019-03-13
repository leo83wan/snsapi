using System;
using System.ComponentModel.DataAnnotations;

namespace com.leo83.apis.sns.Data.v1.Entities
{
    /// <summary>
    /// 性别
    /// </summary>
    public enum Gender
    {
        /// 女人
        Female=0,
        /// 男人
        Male=1
    }
    /// <summary>
    /// 帐户信息
    /// </summary>
    public class AccountInfo
    {
        /// <summary>
        /// 帐户ID
        /// </summary>
        /// <value></value>
        public int AccountId { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        /// <value></value>
        public string Nickname { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        /// <value></value>
        public DateTime? Birthday { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        /// <value></value>
        public Gender? Gender { get; set; }
        /// <summary>
        /// 头像URL
        /// </summary>
        /// <value></value>
        public string AvatarUrl { get; set; }
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
        /// 帐户
        /// </summary>
        /// <value></value>
        public virtual Account Account { get; set; }
    }
}