using Azure.Core;
using EasyTesting.Core.Models.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EasyTesting.Core.Service
{
    public class TokenService
    {
        public const string AuthTokenKey = "AuthToken";
        private const string TeacherIdClaim = "TeacherId";
        private readonly IConfiguration _config;

        public TokenService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateJwtToken(User user)
        {
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]!);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString()),

                // Custom claim: store TeacherId (for teachers or if its null than make it 0)
                new Claim(TeacherIdClaim, (user.Role == User.UserRole.Student ? 
                                       (user.TeacherId == null ? 0 : user.TeacherId.Value) : user.Id)
                                       .ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public int? GetUserIdFromToken(HttpRequest request)
        {
            if (!request.Cookies.TryGetValue(AuthTokenKey, out var token))
                return null;

            return GetIdFromTokenByClaimType(token, ClaimTypes.NameIdentifier);
        }

        public int? GetTeacherIdFromToken(HttpRequest request)
        {
            if (!request.Cookies.TryGetValue(AuthTokenKey, out var token))
                return null;

            return GetIdFromTokenByClaimType(token, TeacherIdClaim);
        }

        private int? GetIdFromTokenByClaimType(string token, string claimType)
        {
            if (string.IsNullOrEmpty(token))
                return null;

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            var idClaim = jwt.Claims.FirstOrDefault(c => c.Type == claimType);

            return idClaim != null ? int.Parse(idClaim.Value) : null;
        }
    }
}
