using ERP.Services.CRM.Domain.Shared;
using Microsoft.Extensions.Configuration;
using Platform.Services.SendGrid.API;

namespace ERP.Services.CRM.Application.Shared.Emails
{
    public class EmailsService : IEmailsService
    {
        private readonly ISendgridService _sendgridService;
        private readonly IConfiguration _configuration;
        public EmailsService(ISendgridService sendgridService, IConfiguration configuration)
        {
            _sendgridService = sendgridService;
            _configuration = configuration;
        }
        public async Task<string> SendEmail(SendEmailInput input)
        {
            if (string.IsNullOrWhiteSpace(input.FromEmail))
                input.FromEmail = _configuration.GetSection(EmailUtilities.NOREPLAY_EMAIL_SECTION_KEY).Value ??
                throw new Exception(nameof(EmailUtilities.NOREPLAY_EMAIL_SECTION_KEY) + " not exist");

            var output = await _sendgridService.SendEmail(input);

            ResponsesHandler.HandleResponse(output._statusCode);
            return "Email is sent successfully";
        }
    }
}
