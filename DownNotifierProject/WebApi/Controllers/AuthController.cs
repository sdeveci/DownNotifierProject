using DownNotifier.API.Entities;
using DownNotifier.API.Model;
using DownNotifier.API.Repositories;
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
        private readonly IRepository<ApplicationUser> _applicationUserRepository;
        public AuthController(IConfiguration configuration, IRepository<ApplicationUser> applicationUserRepository)
        {
            _configuration = configuration;
            _applicationUserRepository = applicationUserRepository;
        }

        [HttpPost]
        [Route("api/[controller]")]
        public async Task<LoginResponse> Login(ApplicationUser pReq)
        {
            var applicationUser = await AuthenticateUser(pReq.UserName, pReq.Password);
            var response = new LoginResponse();
            if (applicationUser==null)
            {               
                return new LoginResponse
                {
                    Message = "Invalid username or password."
                };
            }
            else
            {

                var tokenString = GenerateJwtToken(applicationUser);
                 response = new LoginResponse
                {
                    Token = tokenString
                };
            }

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
                    new Claim(ClaimTypes.NameIdentifier, pReq.Id.ToString()),
                    new Claim(ClaimTypes.Name, pReq.UserName)
                }),
                Expires = DateTime.UtcNow.Add(tokenLifetime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _configuration["Jwt:Audience"],
                Issuer = _configuration["Jwt:Issuer"],
            };
            
           

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private async Task<ApplicationUser> AuthenticateUser(string username, string password)
        {
            var applicationUserList = await _applicationUserRepository.GetAll();
            return applicationUserList.FirstOrDefault(user => user.UserName == username && user.Password == password);
        }

    }
}
