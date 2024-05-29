using ERB.Services.EInvoice.Domain.Shared.Product.DTOs;
using ERB.Services.EInvoice.Domain.Shared.Product.Enums;

namespace ERB.Services.Product.Domain.Shared.Product.DTOs
{
    public class ProductDto
    {
        public int CompanyId { get; set; }
        public string? Note { get; set; }

        // GeneralInfo
        public string Id { get; set; } = string.Empty;
        public ProductType Type { get; set; }
        public int UnitTypeId { get; set; }
        public string StandardBarcode { get; set; } = string.Empty;
        public string? CustomBarcode { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? NameAr { get; set; }
        public string? Desc { get; set; }
        public string? DescAr { get; set; }

        // GovernmentalInfo
        public ETAItemCodeType ETAItemCodeType { get; set; }
        public string ETAItemCode { get; set; } = string.Empty;
        public int ETAUnitTypeId { get; set; }

        // DetailedInfo
        public int? CategoryId { get; set; }
        public string? Category { get; set; }
        public int? ManufacturerId { get; set; }
        public string? Manufacturer { get; set; }
        public int? BrandId { get; set; }
        public string? Brand { get; set; }
        public int? SizeId { get; set; }
        public string? Size { get; set; }
        public int? ColorId { get; set; }
        public string? Color { get; set; }
        public string? HSCode { get; set; }
        public int? CountryId { get; set; }
        public string? City { get; set; }
        public List<ProductTagDto> Tags { get; set; } = new();
        // DimensionInfo
        public double? LengthQty { get; set; }
        public double? WidthQty { get; set; }
        public double? HeightQty { get; set; }
        public int? DimensionsId { get; set; }
        public double? WeightQty { get; set; }
        public int? WeightId { get; set; }
        public double? AreaQty { get; set; }
        public int? AreaId { get; set; }
        public double? VolumeQty { get; set; }
        public int? VolumeId { get; set; }

        // PricingInfo
        public decimal? SellingPrice { get; set; }
        public decimal? MinSellingPrice { get; set; }

        // Configurations
        public List<ProductTaxDto> Taxes { get; set; } = new();

        // Balances 
        public double? CurrentBalance { get; set; }
        public double? MinBalance { get; set; }

        //public static ProductDto FromProduct(Domain.Products.Product product, List<TaxSubTypeSelectDto> taxesDtos) =>
        //    new ProductDto
        //    {
        //        CompanyId = product.CompanyId,
        //        Note = product.Note,

        //        Id = product.InternalId,

        //        Type = product.Type,
        //        UnitTypeId = product.UnitTypeId,
        //        StandardBarcode = product.StandardBarcode,
        //        CustomBarcode = product.CustomBarcode,
        //        IsActive = product.IsActive,
        //        Name = product.Name,
        //        NameAr = product.NameAr,
        //        Desc = product.Desc,
        //        DescAr = product.DescAr,

        //        ETAItemCodeType = product.ETAItemCodeType,
        //        ETAItemCode = product.ETAItemCode,
        //        ETAUnitTypeId = product.ETAUnitTypeId,

        //        CategoryId = product.CategoryId,
        //        Category = product.Category?.Name,
        //        ManufacturerId = product.ManufacturerId,
        //        Manufacturer = product.Manufacturer?.Name,
        //        BrandId = product.BrandId,
        //        Brand = product.Brand?.Name,
        //        SizeId = product.SizeId,
        //        Size = product.Size?.Name,
        //        ColorId = product.ColorId,
        //        Color = product.Color?.Name,
        //        HSCode = product.HSCode,
        //        CountryId = product.CountryId,
        //        City = product.City,

        //        Tags = ProductTagDto.FromProductTags(product.Tags),

        //        LengthQty = product.LengthQty,
        //        WidthQty = product.WidthQty,
        //        HeightQty = product.HeightQty,
        //        DimensionsId = product.DimensionsId,

        //        WeightQty = product.WeightQty,
        //        WeightId = product.WeightId,
        //        AreaQty = product.AreaQty,
        //        AreaId = product.AreaId,
        //        VolumeQty = product.VolumeQty,
        //        VolumeId = product.VolumeId,

        //        SellingPrice = product.SellingPrice,
        //        MinSellingPrice = product.MinSellingPrice,

        //        Taxes = ProductTaxDto.FromProductTaxes(product.Taxes.Where(x => !x.IsDeleted).ToList(), taxesDtos),

        //        CurrentBalance = product.CurrentBalance,
        //        MinBalance = product.MinBalance,
        //    };


    }
}
