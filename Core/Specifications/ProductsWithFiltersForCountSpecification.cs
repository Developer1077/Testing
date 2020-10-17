using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithFiltersForCountSpecification : BaseSpecification<Product>
    {
        // "||" means "or else"
        public ProductsWithFiltersForCountSpecification(ProductSpecParams productParams) : base(x =>
            (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
            (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) && //if it doesn't have value, get all with the passed brand id
            (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)//if the condition is true, do what is there after the ||
        )
        {

        }

    }
}