using DownNotifier.API.Entities;
using Refit;
namespace DownNotifier.WebApp.Services
{
    public interface IDownNotifierAPIService
    {
        [Get("/api/TargetApp")]
        Task<IEnumerable<TargetApp>> GetAll();

        [Post("/api/TargetApp")]
        Task Insert(TargetApp pReq);

        [Put("/api/TargetApp/{id}")]
        Task Update(int id, TargetApp pReq);

        [Delete("/api/TargetApp/{id}")]
        Task Delete(int id);
    }
}
