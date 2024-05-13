using DownNotifier.API.Entities;
using DownNotifier.API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DownNotifier.API.Controllers
{
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("api/[controller]")]
        public LoginResponse Login(ApplicationUser pReq)
        {
            var isAuthenticated = AuthenticateUser(pReq.UserName, pReq.Password);

            if (!isAuthenticated)
            {
                Response.StatusCode = 401;
                return null; 
            }

            var tokenString = GenerateJwtToken(pReq);
            var response = new LoginResponse
            {
                Token = tokenString
            };

            return response;
        }

        private string GenerateJwtToken(ApplicationUser pReq)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]);
            var tokenLifetime = TimeSpan.FromHours(_configuration.GetValue<double>("Jwt:TokenLifetimeInHours"));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,pReq.UserName),
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim(ClaimTypes.Role, "Standart")
                }),
                Expires = DateTime.UtcNow.Add(tokenLifetime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _configuration["Jwt:Audience"],
                Issuer = _configuration["Jwt:Issuer"],
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private bool AuthenticateUser(string username, string password)
        {   
            return username == "test" && password == "test";
        }

    }
}
