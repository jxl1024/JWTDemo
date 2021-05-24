using JWTDemo.Server.TokenInfo;

namespace JWTDemo.Server.Entity
{
    public class ComplexToken
    {
        /// <summary>
        /// 访问Token
        /// </summary>
        public Token AccessToken { get; set; }

        /// <summary>
        /// 刷新Token
        /// </summary>
        public Token RefreshToken { get; set; }
    }
}
