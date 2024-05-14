using DownNotifier.API.Entities;
using DownNotifier.API.Repositories;
using Polly;
using System.Net;
using System;
using System.Threading;
using Serilog;

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

        public async Task MonitorTargetApplications(TargetApp pReq, CancellationToken cancellationToken)
        {
         
            bool isRunning = true;
            while (isRunning)
            {
                var targetAppList = await _targetAppRepository.GetAll();
                var targetAppUserList = targetAppList.Where(p => p.AplicationUserId == pReq.AplicationUserId).ToList();

                foreach (var targetApp in targetAppUserList)
                {
                    await Policy
                         .Handle<Exception>()
                         .WaitAndRetryForever(retryAttempt => TimeSpan.FromSeconds((double)targetApp.MonitoringInterval))
                         .Execute(async () =>
                         {
                             if (!IsUrlUp(targetApp))
                             {
                                 isRunning = false;
                                 try
                                 {
                                     await _notificationService.SendNotification(targetApp);
                                 }
                                 catch (Exception ex)
                                 {
                                     Log.Error($"Error sending notification for target app: '{targetApp.Name}'");
                                     throw;
                                 }
                             }
                         });
                }
            }
            await Task.Delay(60000, cancellationToken);
        }

        private bool IsUrlUp(TargetApp pReq)
        {
            try
            {                
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
               
                Log.Error($"An error occurred in the http request URL for target app: '{pReq.Url}'");
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
