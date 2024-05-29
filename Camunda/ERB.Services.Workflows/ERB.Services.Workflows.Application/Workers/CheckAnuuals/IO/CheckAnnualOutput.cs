using System.Text.Json.Serialization;

namespace ERB.Services.Workflows.Application.Workers.CheckAnuuals.IO
{
    public class CheckAnnualOutput
    {
        [JsonPropertyName("AvailDays")]
        public int AvailDays { get; set; }
    }
}
