using FluentValidation.Results;

namespace ERB.Services.EInvoice.Domain.Shared.Exceptions
{
    public class ErpException : Exception
    {
        public string? Message { get; set; }
        public int StatusCode { get; set; }
        public string? StackTrace { get; set; }
        public List<ValidationFailure>? Validations { get; set; }
    }
}
