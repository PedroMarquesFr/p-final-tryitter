using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Tryitter.Web.Constants;
using Tryitter.Web.Models;
using System.Security.Claims;

namespace Tryitter.Web.Services
{
    public class TokenGenerator
    {
        public string Generate(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = AddClaims(user),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII
                    .GetBytes(Settings.Secret)), SecurityAlgorithms.HmacSha256Signature),
                Expires = DateTime.Now.AddHours(8)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        private static ClaimsIdentity AddClaims(User user)
        {
            var claims = new ClaimsIdentity();

            claims.AddClaim(new Claim(ClaimTypes.Name, user.UserId.ToString())); //Forma de acesso no controller: User.Identity.Name
            claims.AddClaim(new Claim(ClaimTypes.Role, "Student")); //User.IsInRole() // no futuro a gnt pode colocar isso no priprio 

            return claims;
        }
    }
}
