namespace ERB.Services.EInvoice.Domain.Shared.Exceptions;
public class DataDuplicateException : Exception
{
    public int StatusCode { get; set; } = 409;
    public string? HttpResponseMessage { get; set; }
    public DataDuplicateException(string message = "The data was duplicated") : base(message)
    {
        HttpResponseMessage = message;
    }
}