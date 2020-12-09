using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _congif;
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration congif)
        {
            _congif = congif;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_congif["Token:Key"]));
        }

        public string createToken(AppUser user)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName,user.DisplayName)
            };

            var creds = new SigningCredentials(_key,SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor 
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
                Issuer = _congif["Token:Issuer"]
            };

            var tokenhandler = new JwtSecurityTokenHandler();

            var toke = tokenhandler.CreateToken(tokenDescriptor); 

            return tokenhandler.WriteToken(toke);
        }
    }
}