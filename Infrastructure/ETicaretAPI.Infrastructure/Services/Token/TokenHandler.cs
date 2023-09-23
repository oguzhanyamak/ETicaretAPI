using ETicaretAPI.Application.Abstraction.Token;
using ETicaretAPI.Application.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using TokenDTO = ETicaretAPI.Application.DTOs.Token;

namespace ETicaretAPI.Infrastructure.Services.Token
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TokenDTO CreateAccessToken(int minute)
        {

            TokenDTO token = new TokenDTO();
            token.Expiration = DateTime.UtcNow.AddMinutes(minute);

            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            
            JwtSecurityToken securityToken = new(
                audience: _configuration["Token:Audience"], 
                issuer: _configuration["Token:Issuer"], 
                expires: token.Expiration, 
                notBefore: DateTime.UtcNow,
                signingCredentials: credentials
                );
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            token.AccessToken = handler.WriteToken(securityToken);


            return token;
        }
    }
}
