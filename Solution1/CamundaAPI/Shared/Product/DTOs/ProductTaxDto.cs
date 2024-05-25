namespace ERB.Services.EInvoice.Domain.Shared.Product.DTOs
{
    public class ProductTaxDto
    {
        public int Id { get; set; }
        public int TaxId { get; set; }
        public double Value { get; set; }
        public string? TypeCode { get; set; }
        public string? SubTypeCode { get; set; }
        public string? EnDescription { get; set; }
        public string? ArDescription { get; set; }
        public bool? IsRated { get; set; }

        //public static ProductTaxDto FromProductTax(ProductTax productTax, TaxSubTypeSelectDto? subtypesDto) =>
        //    new ProductTaxDto()
        //    {
        //        Id = productTax.Id,
        //        TaxId = productTax.TaxId,
        //        Value = productTax.Value,
        //        TypeCode = subtypesDto?.tax?.code,
        //        SubTypeCode = subtypesDto?.code,
        //        EnDescription = subtypesDto?.tax?.enDescription,
        //        ArDescription = subtypesDto?.tax?.arDescription,
        //        IsRated = subtypesDto?.isRated,
        //    };

        //public static List<ProductTaxDto> FromProductTaxes(IList<ProductTax> productTaxes, List<TaxSubTypeSelectDto> subtypesDtos)
        //{
        //    List<ProductTaxDto> productTaxDTOs = new();
        //    Parallel.ForEach(productTaxes, tax =>
        //    {
        //        TaxSubTypeSelectDto? taxDto = subtypesDtos.Find(x => x.id == tax.Id);
        //        productTaxDTOs.Add(FromProductTax(tax, taxDto));
        //    });
        //    return productTaxDTOs;
        //}
    }
}
