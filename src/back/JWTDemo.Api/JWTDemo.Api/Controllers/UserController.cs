using System;
using System.Collections.Generic;
using System.Linq;
using JWTDemo.Api.Context;
using JWTDemo.Api.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTDemo.Api.Controllers
{
    [Route("api/User")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public UserController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// 允许匿名访问
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<User> Get()
        {
            List<User> list = _dbContext.Users.ToList();
            return list;
        }

        [HttpPost]
        public ReturnEntity Post()
        {
            User entity = new User()
            {
                UserCode = "11",
                UserName = "普通用户",
                Password = "1234",
                Sex = 1,
                Age = 20,
                CreateUserId = "admin",
                CreateDateTime = DateTime.Now
            };

            _dbContext.Users.Add(entity);
            int result = _dbContext.SaveChanges();
            if (result > 0)
            {
                return new ReturnEntity()
                {
                    Code = "1",
                    Message = "添加成功",
                    Content = ""
                };
            }
            return new ReturnEntity()
            {
                Code = "0",
                Message = "添加失败",
                Content = ""
            };
        }
    }
}