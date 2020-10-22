using Client.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    public class PaginationWithBrandsAndTypesViewModel
    {
        public PaginationWithBrandsAndTypesViewModel()
        {

        }
        public PaginationWithBrandsAndTypesViewModel(ShopParams shopParams)
        {
            BrandIdSelected = shopParams.BrandId;

            TypeIdSelected = shopParams.TypeId;
            
            if (!String.IsNullOrEmpty(shopParams.Sort))
            {
                SortSelected = shopParams.Sort;
            }

            if (!String.IsNullOrEmpty(shopParams.Search))
            {
                SearchSelected = shopParams.Search;
            }
        }
        public Pagination Pagination { get; set; }
        public IReadOnlyList<ProductBrand> Brands { get; set; }
        public IReadOnlyList<ProductType> Types { get; set; }
        public int BrandIdSelected { get; set; } = 0;
        public int TypeIdSelected { get; set; } = 0;

        public string SortSelected { get; set; } = "name";
        public string SearchSelected { get; set; }

        public List<SelectListItem> SortOptions
        {
            get
            {
                return new List<SelectListItem>(){
                    new SelectListItem { Text = "Alphabetical", Value = "name", Selected=(SortSelected =="name") },
                    new SelectListItem { Text = "Price: Low to High", Value = "priceAsc", Selected=(SortSelected =="priceAsc")  },
                    new SelectListItem { Text = "Price: High to Low", Value = "priceDesc", Selected=(SortSelected =="priceDesc") }
                };
            }

        }
    }
}