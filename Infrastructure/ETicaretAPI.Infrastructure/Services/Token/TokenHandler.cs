using ETicaretAPI.Application.Abstraction.Token;
using ETicaretAPI.Application.DTOs;
using ETicaretAPI.Domain.Entities.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
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

        public TokenDTO CreateAccessToken(AppUser user,int minute = 15)
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
                signingCredentials: credentials,
                claims:new List<Claim> { new(ClaimTypes.Name,user.UserName)}
                );
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            token.AccessToken = handler.WriteToken(securityToken);
            token.refToken = CreateRefreshToken();

            return token;
        }

        public string CreateRefreshToken(int minute = 5)
        {
            byte[] number = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }
    }
}
