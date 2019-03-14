using System.ComponentModel.DataAnnotations;

namespace sns.Models.v1.Administrator
{
    /// <summary>
    /// 用户请求
    /// </summary>
    public class UsersRequest
    {
        /// <summary>
        /// 页码
        /// </summary>
        /// <value></value>
        [Display(Name = "页码")]
        [Required(ErrorMessage = "{0} 必须")]
        public int Page { get; set; }
        /// <summary>
        /// 分页大小
        /// </summary>
        /// <value></value>
        [Display(Name = "分页大小")]
        [Required(ErrorMessage = "{0} 必须")]
        public int PageSize { get; set; }
        /// <summary>
        /// 查询字符串
        /// </summary>
        /// <value></value>
        public string findStr { get; set; }
    }
}