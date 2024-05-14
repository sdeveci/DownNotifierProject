using DownNotifier.API.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DownNotifier.API.Entities
{
    public class TargetApp :BaseEntity
    {
        public string? Name { get; set; }
        public string? Url { get; set; }
        public int? MonitoringInterval { get; set; } 

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public NotificationType NotificationType { get; set; }
    }
}
