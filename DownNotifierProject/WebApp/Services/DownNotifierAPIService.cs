using Refit;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
namespace DownNotifier.WebApp.Services
{
    public class DownNotifierAPIService
    {
        public IDownNotifierAPIService Api { get; }


        public DownNotifierAPIService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            var jwtToken = httpContextAccessor.HttpContext?.Session.GetString("JwtToken");
            string apiUrl = configuration["DownNotifierApiUrl"];
            
            var httpClient = new HttpClient();
            
            if (!string.IsNullOrEmpty(jwtToken))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
            }
            httpClient.BaseAddress = new Uri(apiUrl);
            Api = RestService.For<IDownNotifierAPIService>(httpClient);
        }
    }
}
