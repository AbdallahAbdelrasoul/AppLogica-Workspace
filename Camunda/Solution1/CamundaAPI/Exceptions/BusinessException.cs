namespace ERB.Services.EInvoice.Domain.Shared.Exceptions
{
    public class BusinessException : Exception
    {
        public int StatusCode { get; set; }
        public string? HttpResponseMessage { get; set; }
        public BusinessException(string message = "There was an error processing your request", int statusCode = 405) : base(message)
        {
            StatusCode = statusCode;
            HttpResponseMessage = message;
        }
    }
}
