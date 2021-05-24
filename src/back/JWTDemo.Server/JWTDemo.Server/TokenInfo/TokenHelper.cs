using JWTDemo.Server.Entity;
using Microsoft.Extensions.Options;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Linq;
using JWTDemo.Server.DatabaseContext;

namespace JWTDemo.Server.TokenInfo
{
    public class TokenHelper : ITokenHelper
    {
        private IOptions<JWTConfig> _options;
        private readonly ApplicationDbContext _dbContext;

        public TokenHelper(IOptions<JWTConfig> options, ApplicationDbContext dbContext)
        {
            _options = options;
            _dbContext = dbContext;
        }

        public Token CreateAccessToken(User user)
        {
            Claim[] claims = new Claim[] { new Claim(ClaimTypes.NameIdentifier, user.UserCode), new Claim(ClaimTypes.Name, user.UserName) };

            return CreateToken(claims, TokenType.AccessToken);
        }


        public ComplexToken CreateToken(User user)
        {
            Claim[] claims = new Claim[] { new Claim(ClaimTypes.NameIdentifier, user.UserCode), new Claim(ClaimTypes.Name, user.UserName)
            };

            return CreateToken(claims);
        }

        public ComplexToken CreateToken(Claim[] claims)
        {
            return new ComplexToken { AccessToken = CreateToken(claims, TokenType.AccessToken), RefreshToken = CreateToken(claims, TokenType.RefreshToken) };
        }

        //public Token CreateToken(User user)
        //{
        //    Claim[] claims = new Claim[] { new Claim(ClaimTypes.Name, user.UserCode), new Claim(ClaimTypes.Name, user.UserName) };

        //    return CreateToken(claims);
        //}

        //private Token CreateToken(Claim[] claims)
        //{
        //    var now = DateTime.Now;
        //    var expires = now.Add(TimeSpan.FromMinutes(_options.Value.AccessTokenExpiresMinutes));
        //    var token = new JwtSecurityToken(
        //        issuer: _options.Value.Issuer,
        //        audience: _options.Value.Audience,
        //        claims: claims,
        //        notBefore: now,
        //        expires: expires,
        //        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.IssuerSigningKey)), SecurityAlgorithms.HmacSha256));
        //    return new Token { TokenContent = new JwtSecurityTokenHandler().WriteToken(token), TokenExpiresTime = expires };
        //}

        private Token CreateToken(Claim[] claims, TokenType tokenType)
        {
            var now = DateTime.Now;
            //设置不同的过期时间
            var expires = now.Add(TimeSpan.FromMinutes(tokenType.Equals(TokenType.AccessToken) ? _options.Value.AccessTokenExpiresMinutes : _options.Value.RefreshTokenExpiresMinutes));
            var token = new JwtSecurityToken(
                issuer: _options.Value.Issuer,
                //设置不同的接受者
                audience: tokenType.Equals(TokenType.AccessToken) ? _options.Value.Audience : _options.Value.RefreshTokenAudience,
                claims: claims,
                notBefore: now,
                expires: expires,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.IssuerSigningKey)), SecurityAlgorithms.HmacSha256));
            return new Token { TokenContent = new JwtSecurityTokenHandler().WriteToken(token), TokenExpiresTime = expires };
        }

        public Token RefreshToken(ClaimsPrincipal claimsPrincipal)
        {
            var code = claimsPrincipal.Claims.FirstOrDefault(m => m.Type.Equals(ClaimTypes.NameIdentifier));
            if (null != code)
            {
                Func<User, bool> func = p =>
                {
                    return p.UserCode.Equals(code.Value.ToString());
                };
                User user = _dbContext.Users.FirstOrDefault(func);
                return CreateAccessToken(user);
            }
            else
            {
                return null;
            }
        }
    }
}
