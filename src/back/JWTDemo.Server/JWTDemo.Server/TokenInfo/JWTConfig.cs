﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTDemo.Server.TokenInfo
{
    /// <summary>
    /// 对于appsettings.json里面的JWT
    /// </summary>
    public class JWTConfig
    {
        /// <summary>
        /// 令牌签发者
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// 令牌接收者
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// 加密key
        /// </summary>
        public string IssuerSigningKey { get; set; }

        /// <summary>
        /// 访问令牌过期时间
        /// </summary>
        public int AccessTokenExpiresMinutes { get; set; }

        public string RefreshTokenAudience { get; set; }

        /// <summary>
        /// 刷新令牌过期时间
        /// </summary>

        public int RefreshTokenExpiresMinutes { get; set; }
    }
}
