using ERB.Services.EInvoice.Domain.Shared.Exceptions;
using Newtonsoft.Json;

namespace ERB.Services.EInvoice.Domain.Shared.Product
{
    public class ProductUtilities
    {
        public const string BaseUrl = "ProductService:BaseUrl";
        public const string GetProduct = "ProductService:GetProduct";

        public static void HandleResponse(HttpResponseMessage response, string result)
        {
            ErpException erpException = JsonConvert.DeserializeObject<ErpException>(result)!;
            string msg = erpException.Message!;
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                throw new DataNotValidException(msg);

            if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                throw new DataDuplicateException(msg);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                throw new DataNotFoundException(msg);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException(msg);

            throw new BusinessException(msg);
        }
    }
}