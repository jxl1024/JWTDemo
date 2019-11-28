using JWTDemo.Server.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTDemo.Server.TokenInfo
{
    public interface ITokenHelper
    {
        Token CreateToken(User user);
    }
}
