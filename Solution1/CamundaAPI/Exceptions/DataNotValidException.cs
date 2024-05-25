using FluentValidation.Results;

namespace ERB.Services.EInvoice.Domain.Shared.Exceptions;
public class DataNotValidException : Exception
{
    public int StatusCode { get; set; } = 422;
    public List<ValidationFailure>? Errors { get; set; }
    public string? HttpResponseMessage { get; set; }
    public DataNotValidException(string? message = "The data not valid", List<ValidationFailure>? errors = null) : base(message)
    {
        HttpResponseMessage = message;
        Errors = errors;
    }
}