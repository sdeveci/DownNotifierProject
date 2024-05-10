using DownNotifier.API.Enums;

namespace DownNotifier.API.Entities
{
    public class TargetApp
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int MonitoringInterval { get; set; } // Min

        public NotificationType NotificationType { get; set; }
    }
}
