using DownNotifier.API.Entities;
using Microsoft.AspNetCore.Http;

namespace DownNotifier.WebApp.Services
{
    public class UserSessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserSessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUsername()
        {
            return _httpContextAccessor.HttpContext?.Session.GetString("Username");
        }

        public string GetUserId()
        {
            return _httpContextAccessor.HttpContext?.Session.GetString("UserId");
        }

        public string GetJwtToken()
        {
            return _httpContextAccessor.HttpContext?.Session.GetString("JwtToken");
        }

        public void SetUserId(string applicationUserId)
        {
            _httpContextAccessor.HttpContext?.Session.SetString("UserId", applicationUserId);
        }

        public void SetUserName(string Username)
        {
            _httpContextAccessor.HttpContext?.Session.SetString("Username", Username);
        }

        public void SetJwtToken(string jwtToken)
        {
            _httpContextAccessor.HttpContext?.Session.SetString("JwtToken", jwtToken);
        }
    }
}
