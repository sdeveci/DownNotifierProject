using DownNotifier.API.Entities;

namespace DownNotifier.API.Services
{
    public interface IMonitoringService
    {
        Task MonitorTargetApplications(TargetApp pReq, CancellationToken cancellationToken);
    }
}
