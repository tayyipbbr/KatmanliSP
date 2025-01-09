using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using KatmanliSP.Core.DTOs.UserDTO;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
//using Microsoft.Authentication.JwtBearer;
//using System.IdentityModel.Tokens.Jwt;

namespace KatmanliSP.Service.Security
{
    public class TokenCreator : ITokenCreator
    {
        private readonly IConfiguration _configuration;

        public TokenCreator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateHashPassword(string password)
        { 
            string secretPasswordKey = _configuration.GetValue<string>("AppSettings:PasswordKey");
            byte[] keyBytes = Encoding.UTF8.GetBytes(secretPasswordKey);

            using(var hmac256 = new HMACSHA256(keyBytes))
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashedBytes = hmac256.ComputeHash(passwordBytes);

                return Convert.ToBase64String(hashedBytes);
            }
        }

        public string GenerateToken(string username, int userid, int roleid)
        { // claim, paramdan eglenleri tokena ekleyeceğim.
            string secret = _configuration.GetValue<string>("AppSettings:SecretKey");
            byte[] key = Encoding.UTF8.GetBytes(secret);

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("Username", username));
            claims.Add(new Claim(ClaimTypes.Name, username));
            claims.Add(new Claim("UserId", userid.ToString()));
            //claims.Add(new Claim(ClaimTypes.Role, rolename));
            claims.Add(new Claim("RoleId", roleid.ToString()));// ROLEID CLAIM EDILECEK.
                // Ek roller buraya eklenebilir)
            

            //foreach (var role in roles)
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            //    claims.Add(new Claim("roles", role.ToString()));
            //}
            // claims.Add(new Claim(ClaimTypes.Role, "Admin"));

            JwtSecurityToken securityToken =
                new JwtSecurityToken(
                    signingCredentials: credentials,
                    claims: claims,
                    expires: DateTime.Now.AddDays(7));

            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return token;
        }
    }
}

