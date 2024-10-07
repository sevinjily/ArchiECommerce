using Core.Entities.Concrete;
using Core.Utilities.Security.Abstract;
using Microsoft.AspNetCore.Identity;
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

namespace Core.Utilities.Security.Concrete
{
    public class TokenManager : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;

        public TokenManager(IConfiguration configuration, UserManager<AppUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<Token> CreateAccessToken(AppUser appUser, List<string> roles)
        {
           Token token = new();
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier,appUser.Id),

            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role,role));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
            token.ExpiredDate=DateTime.Now.AddMinutes(2);
            JwtSecurityToken securityToken=new(
                
                issuer: _configuration["Token:Issuer"],
                audience: _configuration["Token:Audience"],
                claims:claims,
                notBefore:DateTime.Now,
                expires:token.ExpiredDate,
                signingCredentials:new SigningCredentials(key,SecurityAlgorithms.HmacSha256));
            JwtSecurityTokenHandler tokenHandler=new();
            token.AccessToken=tokenHandler.WriteToken(securityToken);
            token.RefreshToken = CreateRefreshToken();
           await _userManager.AddClaimsAsync(appUser, claims);
            return token;
        }

        public string CreateRefreshToken()
        {
            byte[] number=new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }
    }
}
