using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTDemo.Server.Entity
{
    /// <summary>
    /// 对应数据库里面的User表
    /// </summary>
    public class User
    {
        public int Id { get; set; }

        public string UserCode { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public int Sex { get; set; }

        public int Age { get; set; }

        public string CreateUserId { get; set; }

        public DateTime CreateDateTime { get; set; }

        public string UpdateUserId { get; set; }

        public DateTime UpdateDateTime { get; set; }
    }
}
