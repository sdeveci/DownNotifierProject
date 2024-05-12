using Refit;
namespace DownNotifier.WebApp.Services
{
    public class DownNotifierAPIService
    {
        public IDownNotifierAPIService Api { get; }

        public DownNotifierAPIService(IConfiguration configuration)
        {
            string apiUrl = configuration["DownNotifierApiUrl"];
            Api = RestService.For<IDownNotifierAPIService>(apiUrl);
        }
    }
}
