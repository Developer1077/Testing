using Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Services
{
    public interface IShopService
    {
        Pagination GetProducts(ShopParams shopParams);
        Product GetProduct(int id);

        IReadOnlyList<ProductType> GetProductTypes();
        IReadOnlyList<ProductBrand> GetProductBrands();
    }
}
