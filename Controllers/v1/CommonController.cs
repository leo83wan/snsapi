using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using com.leo83.apis.sns.Data.v1;
using com.leo83.apis.sns.Data.v1.Entities;
using com.leo83.apis.sns.Models.v1;
using com.leo83.libs.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace com.leo83.apis.sns.Controllers.v1
{
    /// <summary>
    /// 通用接口控制器
    /// </summary>
    [Route("api")]
    [AllowAnonymous]
    [ApiController]
    public class CommonController : ControllerBase
    {
        // 配置参数
        private readonly IConfiguration _configuration;
        // 数据上下文
        private readonly SnsContext _context;
        /// <summary>
        /// 通用接口
        /// </summary>
        /// <param name="context"></param>
        /// <param name="configuration"></param>
        public CommonController(SnsContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        /// <summary>
        /// 得到授权令牌
        /// </summary>
        /// <param name="request">登录所使用的凭证</param>
        /// <returns></returns>
        [ApiVersion("1.0")]
        [Route("auth")]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult Auth([FromBody] AuthRequest request)
        {
            var accounts = _context.Accounts;
            var account = accounts.Where(w => w.Username == request.Username).FirstOrDefault();
            if (accounts.Count() == 0 || account != null && StringTools.Encrypt(request.Password) == account.Password)
            {
                // push the user’s name into a claim, so we can identify the user later on.
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, request.Username)
                };
                foreach (var role in account.Roles)
                {
                    claims.Append(new Claim(ClaimTypes.Role, role.Role));
                }
                //sign the token using a secret key.This secret will be shared between your API and anything that needs to check that the token is legit.
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecurityKey"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                //.NET Core’s JwtSecurityToken class takes on the heavy lifting and actually creates the token.
                /*
                 * Claims (Payload)
                    Claims 部分包含了一些跟这个 token 有关的重要信息。 JWT 标准规定了一些字段，下面节选一些字段:

                    iss: The issuer of the token，token 是给谁的
                    sub: The subject of the token，token 主题
                    exp: Expiration Time。 token 过期时间，Unix 时间戳格式
                    iat: Issued At。 token 创建时间， Unix 时间戳格式
                    jti: JWT ID。针对当前 token 的唯一标识
                    除了规定的字段外，可以包含其他任何 JSON 兼容的字段。
                 * */
                var token = new JwtSecurityToken(
                    issuer: "api.leo83.com",
                    audience: "leo83.com",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds);

                return Ok(new
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    ExpireIn = 30 * 60
                });
            }

            return BadRequest("Could not verify username and password");
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="registerRequest">新用户信息</param>
        /// <returns></returns>
        [ApiVersion("1.0")]
        [Route("register")]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult Register([FromBody] RegisterRequest registerRequest)
        {
            var account = new Account
            {
                Username = registerRequest.Username,
                Password = StringTools.Encrypt(registerRequest.Password),
                CreatedAtClientIP = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
                UpdatedAt = DateTime.Now,
                CreatedAt = DateTime.Now,
                Info = new AccountInfo
                {
                    Nickname = registerRequest.Nickname,
                    Birthday = registerRequest.Birthday,
                    AvatarUrl = registerRequest.AvatarUrl,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }
            };

            _context.Add(account);

            try
            {
                if (0 < _context.SaveChanges())
                {
                    return Ok("ok");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return BadRequest("Could not register account");
        }
    }
}