using System;
using System.Collections.Generic;
using System.Linq;
using com.leo83.apis.sns.Data.v1;
using com.leo83.apis.sns.Data.v1.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using sns.Models.v1.Administrator;

namespace com.leo83.apis.sns.Controllers.v1
{
    /// <summary>
    /// 管理员接口控制器
    /// </summary>
    [Route("api/administrator")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(Roles = "Administrator")]
    public class AdministratorController : ControllerBase
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
        public AdministratorController(SnsContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="usersRequest"></param>
        /// <returns></returns>
        [Route("users")]
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public ActionResult Users([FromQuery] UsersRequest usersRequest)
        {
            IQueryable<Account> accounts = null;

            if (usersRequest.findStr != null && string.Empty != usersRequest.findStr)
            {
                accounts = _context.Accounts.Where(w => w.Username.Contains(usersRequest.findStr) || w.Info.Nickname.Contains(usersRequest.findStr));
            }
            else
            {
                accounts = _context.Accounts;
            }

            return Ok(accounts.Skip((usersRequest.Page - 1) * usersRequest.PageSize).Take(usersRequest.PageSize).Select(a => new UserResponse
            {
                AccountId = a.AccountId,
                Username = a.Username,
                DisabledAt = a.DisabledAt.HasValue ? a.DisabledAt.Value.ToString("yyyy/MM/dd HH:mm:ss") : null,
                LockedAt = a.LockedAt.HasValue ? a.LockedAt.Value.ToString("yyyy/MM/dd HH:mm:ss") : null,
                CreatedAtIp = a.CreatedAtClientIP,
                CreatedAt = a.CreatedAt.ToString("yyyy/MM/dd HH:mm:ss"),
                UpdatedAt = a.UpdatedAt.HasValue ? a.UpdatedAt.Value.ToString("yyyy/MM/dd HH:mm:ss") : null,
                Nickname = a.Info != null ? a.Info.Nickname : "",
                Birthday = a.Info != null && a.Info.Birthday.HasValue ? a.Info.Birthday.Value.ToString("yyyy/MM/dd") : null,
                AvatarUrl = a.Info != null ? a.Info.AvatarUrl : "",
                Roles = a.Roles.Select(r => r.Role).ToList()
            }));
        }
        /// <summary>
        /// 设置帐户角色
        /// </summary>
        /// <param name="setAccountRoleRequest"></param>
        /// <returns></returns>
        [Route("role2account")]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public ActionResult SetAcccountRole([FromBody] SetAccountRoleRequest setAccountRoleRequest)
        {
            string[] strRoles = new string[2]{ "User", "Administrator" };
            if (strRoles.Contains(setAccountRoleRequest.Role) && 0 < _context.Accounts.Where(w => w.AccountId == setAccountRoleRequest.AccountId).Count() && 0 == _context.AccountRoles.Where(w => w.AccountId == setAccountRoleRequest.AccountId && w.Role == setAccountRoleRequest.Role).Count())
            {
                try
                {
                    _context.Add(new AccountRole
                    {
                        AccountId = setAccountRoleRequest.AccountId,
                        Role = setAccountRoleRequest.Role,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    });
                    if (0 < _context.SaveChanges())
                    {
                        return Ok("ok");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest("could not append role of account");
        }
    }
}