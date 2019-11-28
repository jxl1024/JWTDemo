using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTDemo.Server.TokenInfo
{
    public class Token
    {
        /// <summary>
        /// Token内容
        /// </summary>
        public string TokenContent { get; set; }

        /// <summary>
        /// Token过期时间
        /// </summary>
        public DateTime TokenExpiresTime { get; set; }
    }
}
