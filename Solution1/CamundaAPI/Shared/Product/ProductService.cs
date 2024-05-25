using ERB.Services.EInvoice.Domain.Shared.Product;
using ERB.Services.Product.Domain.Shared.Product.DTOs;

namespace ERB.Services.EInvoice.Infrastructure.Product
{
    public class ProductService : IProductService
    {

        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ProductDto> GetProduct(string internalId, int companyId)
        {
            //string baseUrl = _configuration.GetSection(ProductUtilities.BaseUrl).Value ??
            //                throw new Exception(nameof(ProductUtilities.BaseUrl) + " not exist");

            //string? url = _configuration.GetSection(ProductUtilities.GetProduct).Value ??
            //    throw new Exception(nameof(ProductUtilities.GetProduct) + " not exist");
            //url = url.Replace("{companyId}", companyId.ToString());

            //using HttpClient client = new();
            //client.BaseAddress = new Uri(baseUrl);

            //string authToken = GetAuthTokenFromHttpContext();
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

            //HttpResponseMessage response = await client.GetAsync(url);
            //string result = response.Content.ReadAsStringAsync().Result;

            //if (!response.IsSuccessStatusCode)
            //{
            //    ProductUtilities.HandleResponse(response, result);
            //}

            return new ProductDto
            {
                CurrentBalance = 7
            };
        }

        private string GetAuthTokenFromHttpContext()
        {
            // Retrieve the authorization token from HttpContext headers
            string authToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];

            // Extract and return the token part (Bearer token)
            if (!string.IsNullOrEmpty(authToken) && authToken.StartsWith("Bearer "))
            {
                return authToken.Substring("Bearer ".Length).Trim();
            }

            return null; // Return null if token not found or not in the expected format
        }
    }
}
