using System;
using System.Linq;
using JWTDemo.Server.DatabaseContext;
using JWTDemo.Server.Entity;
using JWTDemo.Server.TokenInfo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTDemo.Server.Controllers
{
    [Route("api/Login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ITokenHelper _tokenHelper;
        private readonly ApplicationDbContext _dbContext;

        /// <summary>
        /// 构造函数实现依赖注入
        /// </summary>
        /// <param name="tokenHelper"></param>
        /// <param name="dbContext"></param>
        public LoginController(ITokenHelper tokenHelper, ApplicationDbContext dbContext)
        {
            _tokenHelper = tokenHelper;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Post方法
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public ReturnEntity Post([FromBody]User entity)
        {
            Func<User, bool> func = p =>
            {
                return p.UserCode.Equals(entity.UserCode);
            };
            User user = _dbContext.Users.FirstOrDefault(func);
            if (null != user && user.Password.Equals(entity.Password))
            {
                //Token token = _tokenHelper.CreateToken(user);
                ComplexToken token = _tokenHelper.CreateToken(user);
                return new ReturnEntity()
                {
                    Code = "1",
                    Message = "成功",
                    Token = token
                };
            }
            return new ReturnEntity()
            {
                Code = "0",
                Message = "失败"
            };
        }

        /// <summary>
        /// 这个方法添加了[Authorize]标识，说明调用它需要RefreshToken认证通过
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public IActionResult GetAccessToken()
        {
            return Ok(_tokenHelper.RefreshToken(Request.HttpContext.User));
        }
    }
}