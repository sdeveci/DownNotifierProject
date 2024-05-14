using Microsoft.IdentityModel.Tokens;
using Refit;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
namespace DownNotifier.WebApp.Services
{
    public class DownNotifierAPIService
    {
        public IDownNotifierAPIService Api { get; }
        private readonly UserSessionService _userSessionService;

        public DownNotifierAPIService(IConfiguration configuration, UserSessionService userSessionService)
        {
            _userSessionService = userSessionService;
            var jwtToken = _userSessionService.GetJwtToken();
            string apiUrl = configuration["DownNotifierApiUrl"];
            
            var httpClient = new HttpClient();
            
            if (!string.IsNullOrEmpty(jwtToken))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(jwtToken) as JwtSecurityToken;
                if (jsonToken != null)
                {                    
                    var username = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "unique_name")?.Value;
                    _userSessionService.SetUserName(username);

                    var applicationUserId = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "nameid")?.Value;
                    _userSessionService.SetUserId(applicationUserId);
                }

            }
            httpClient.BaseAddress = new Uri(apiUrl);
            Api = RestService.For<IDownNotifierAPIService>(httpClient);
            
        }
    }
}
