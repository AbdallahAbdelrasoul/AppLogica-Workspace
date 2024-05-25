using ERB.Services.Product.Domain.Shared.Product.DTOs;

namespace ERB.Services.EInvoice.Domain.Shared.Product
{
    public interface IProductService
    {
        Task<ProductDto> GetProduct(string internalId, int companyId);
    }
}
