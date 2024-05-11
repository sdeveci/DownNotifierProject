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
            var targetAppList= await _targetAppRepository.GetAll();
            foreach (var targetApp in targetAppList)
            bool isRunning = true;
            while (isRunning)
            {
                var targetAppList = await _targetAppRepository.GetAll();
                foreach (var targetApp in targetAppList)
                {
                    await Policy
                         .Handle<Exception>()
                         .WaitAndRetryForever(retryAttempt => TimeSpan.FromSeconds(targetApp.MonitoringInterval))
                         .Execute(async () =>
                         {
                             if (!IsUrlUp(targetApp))
                             {
                                 isRunning = false;
                                 await _notificationService.SendNotification(targetApp);                                 
                             }
                         });
                }
            }
            Thread.Sleep(60000); 
        }

        private bool IsUrlUp(TargetApp targetApp)
        {
            try
            {
                WebRequest request = WebRequest.Create(pReq.Url);
                var request = WebRequest.Create(pReq.Url);
                if (request == null)
                {
                    return false;
                }
                request.Method = "HEAD";

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    if (response == null || response.StatusCode != HttpStatusCode.OK)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (WebException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
