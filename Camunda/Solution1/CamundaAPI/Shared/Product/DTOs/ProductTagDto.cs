namespace ERB.Services.EInvoice.Domain.Shared.Product.DTOs
{
    public class ProductTagDto
    {
        public int Id { get; set; }
        public string Tag { get; set; } = string.Empty;
        public int CompanyId { get; set; }
        //public static ProductTagDto FromProductTag(ProductTag productTag) =>
        // new ProductTagDto
        // {
        //     Id = productTag.Id,
        //     Tag = productTag.Tag,
        //     CompanyId = productTag.CompanyId
        // };

        //public static List<ProductTagDto> FromProductTags(IList<ProductTag> productTags)
        //{
        //    List<ProductTagDto> productTagDTOs = new();
        //    Parallel.ForEach(productTags, tag =>
        //    {
        //        productTagDTOs.Add(new ProductTagDto
        //        {
        //            Id = tag.Id,
        //            Tag = tag.Tag,
        //            CompanyId = tag.CompanyId
        //        });
        //    });
        //    return productTagDTOs;
        //}
    }
}
