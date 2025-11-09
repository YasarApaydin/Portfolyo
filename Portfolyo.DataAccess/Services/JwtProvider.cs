using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Portfolyo.DataAccess.Context;
using Portfolyo.Entities.Common;
using Portfolyo.Entities.Models;
using Portfolyo.Entities.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Portfolyo.DataAccess.Services
{
    internal sealed class JwtProvider : IJwtProvider
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly Jwt jwt;

        public JwtProvider(ApplicationDbContext applicationDbContext,IOptions<Jwt> options)
        {
            this.applicationDbContext = applicationDbContext;
            jwt = options.Value;
        }

        public async Task<string> CreateTokenAsync(AppUser appUser)
        {


            Claim[] claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier,appUser.Id.ToString()),
                new Claim("NameLastname",string.Join(" ",appUser.Name,appUser.LastName)),
                new Claim("Email",appUser.Email!)
               
            };
            JwtSecurityToken securityToken = new(
                issuer: jwt.Issuer,
            audience: jwt.Audience,
            claims: claims,
            notBefore: DateTime.Now,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SecretKey)),
            SecurityAlgorithms.HmacSha512));


            JwtSecurityTokenHandler tokenHandler = new();

            string token = tokenHandler.WriteToken(securityToken);

            return token;
        
        }
    }
}
