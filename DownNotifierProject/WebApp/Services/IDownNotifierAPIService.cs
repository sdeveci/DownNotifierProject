using DownNotifier.API.Entities;
using DownNotifier.API.Model;
using Refit;
namespace DownNotifier.WebApp.Services
{

    public interface IDownNotifierAPIService
    {

        [Post("/api/Auth")]
        Task<LoginResponse> Login(ApplicationUser pReq);

        [Get("/api/TargetApp")]
        Task<IEnumerable<TargetApp>> GetAll();

        [Post("/api/TargetApp")]
        Task Insert(TargetApp pReq);

        [Put("/api/TargetApp/{id}")]
        Task Update(int id, TargetApp pReq);

        [Delete("/api/TargetApp/{id}")]
        Task Delete(int id);

        [Post("/api/Monitoring/healthCheck")]
        Task HealthCheck();

    }
}
