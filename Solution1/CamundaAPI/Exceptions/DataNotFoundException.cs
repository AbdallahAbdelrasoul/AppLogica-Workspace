namespace ERB.Services.EInvoice.Domain.Shared.Exceptions
{
    public class DataNotFoundException : Exception
    {
        public int StatusCode { get; set; } = 404;
        public string? HttpResponseMessage { get; set; }
        public DataNotFoundException(string message = "The data was not found") : base(message)
        {
            HttpResponseMessage = message;
        }
    }
}
