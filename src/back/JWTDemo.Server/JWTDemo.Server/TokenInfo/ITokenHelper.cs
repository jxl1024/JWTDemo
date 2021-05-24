using JWTDemo.Server.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JWTDemo.Server.TokenInfo
{
    public interface ITokenHelper
    {
        ComplexToken CreateToken(User user);
        ComplexToken CreateToken(Claim[] claims);


        Token RefreshToken(ClaimsPrincipal claimsPrincipal);
    }
}
