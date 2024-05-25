using System.Net;

namespace ERP.Services.CRM.Application.Shared
{
    public static class ResponsesHandler
    {
        public static void HandleResponse(HttpStatusCode statusCode)
        {
            if (statusCode == HttpStatusCode.Accepted) return;

            //if (statusCode == HttpStatusCode.BadRequest)
            //    throw new DataNotValidException();

            //if (statusCode == HttpStatusCode.Conflict)
            //    throw new DataDuplicateException();

            //if (statusCode == HttpStatusCode.NotFound)
            //    throw new DataNotFoundException();

            if (statusCode == HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException();

            if (statusCode == HttpStatusCode.Forbidden)
                throw new UnauthorizedAccessException();

            //throw new BusinessException();
        }
    }
}
