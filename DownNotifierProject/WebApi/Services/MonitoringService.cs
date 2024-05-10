using DownNotifier.API.Entities;
using DownNotifier.API.Repositories;
using Polly;
using System.Net;
using System;

namespace DownNotifier.API.Services
{
    public class MonitoringService : IMonitoringService
    {
        private readonly IRepository<TargetApp> _targetAppRepository;
        private readonly INotificationService _notificationService;

        public MonitoringService(IRepository<TargetApp> targetAppRepository, INotificationService notificationService)
        {
            _targetAppRepository = targetAppRepository;
            _notificationService = notificationService;
        }

        public async Task MonitorTargetApplications()
        {
            var targetApps = await _targetAppRepository.GetAll();
            foreach (var targetApp in targetApps)
            {
                await Policy
                     .Handle<Exception>()
                     .WaitAndRetryForever(retryAttempt => TimeSpan.FromSeconds(targetApp.MonitoringInterval))
                     .Execute(async () =>
                     {
                         if (!IsUrlUp(targetApp))
                         {
                             await _notificationService.SendNotification(targetApp);
                         }
                     });
            }

            Thread.Sleep(60000); 
        }

        private bool IsUrlUp(TargetApp targetApp)
        {
            try
            {
                WebRequest request = WebRequest.Create(targetApp.Url);
                request.Method = "HEAD";
                using var response = request.GetResponse() as HttpWebResponse;
                return response.StatusCode == HttpStatusCode.OK;
            }
            catch
            {
                return false;
            }
        }
    }
}
