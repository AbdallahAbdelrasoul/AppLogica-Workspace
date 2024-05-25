using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace ERB.Services.Workflows.Application.Workers.SendAnnualLeaveRequest.IO
{
    public class SendAnnualLeaveRequestInput
    {
        [JsonPropertyName("UserIds")]
        public List<string> UserIds { get; set; } = [];
    }
}
