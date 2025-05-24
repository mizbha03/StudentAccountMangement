using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentAccountMangement.Helper
{
    public class AuthHelper
    {
        public static string GenerateJwtToken(string username,string role, int user_id)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, role),
                new Claim("UserId", user_id.ToString())

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("sfvuisdghvhsuihvjshiudvhusdghuivhbsuidvjsuivbguyyghgfeyfgvgsfgvuigsigyigh"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "yourdomain.com",
                audience: "yourdomain.com",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
