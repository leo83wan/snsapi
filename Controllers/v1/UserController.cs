using System;
using System.Linq;
using com.leo83.apis.sns.Data.v1;
using com.leo83.apis.sns.Models.v1;
using com.leo83.libs.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace com.leo83.apis.sns.Controllers
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    [Route("api/users")]
    [ApiController]
    [ApiVersion("1.0")]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// 系统配置
        /// </summary>
        private readonly IConfiguration _configuration;
        /// <summary>
        /// 数据上下文
        /// </summary>
        private readonly SnsContext _context;
        /// <summary>
        /// 初始化用户控制器
        /// </summary>
        /// <param name="context"></param>
        /// <param name="configuration"></param>
        public UserController(SnsContext context, IConfiguration configuration) 
        {
            _context = context;
            _configuration = configuration;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="passwordRequest">密码信息</param>
        /// <returns></returns>
        [Route("password")]
        [HttpPost]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public ActionResult Password([FromBody] PasswordRequest passwordRequest)
        {
            var account = _context.Accounts.Where(w => w.Username == User.Identity.Name).FirstOrDefault();

            if (account != null && account.Password == StringTools.Encrypt(passwordRequest.Password)) {
                account.Password = StringTools.Encrypt(passwordRequest.NewPassword);
            }

            if (0 < _context.SaveChanges()){
                return Ok("ok");
            }

            return BadRequest("Could change your passowrd");
        }
    }
}