using DownNotifier.API.Entities;
using DownNotifier.API.Enums;

namespace DownNotifier.API.Services
{
    public interface INotificationService
    {
        Task SendNotification(TargetApp targetApp);
    }
}
