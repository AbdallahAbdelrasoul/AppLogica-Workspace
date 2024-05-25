using Platform.Services.SendGrid.API;

namespace ERP.Services.CRM.Application.Shared.Emails
{
    public interface IEmailsService
    {
        Task<string> SendEmail(SendEmailInput input);
    }
}
