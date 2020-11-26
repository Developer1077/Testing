using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Models;
using Client.Services;
using Client.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Client.Controllers
{
    public class ShopController : Controller
    {
        private readonly IShopService _shopService;

        public ShopController(IShopService shopService)
        {
            _shopService = shopService;
        }


        public IActionResult Index(ShopParams shopParams)
        {
            return View(new PaginationWithBrandsAndTypesViewModel(shopParams)
            {
                Brands = _shopService.GetProductBrands(),
                Types = _shopService.GetProductTypes(),
                Pagination = _shopService.GetProducts(shopParams),
            });
        }

        [HttpPost]
        public IActionResult Index(PaginationWithBrandsAndTypesViewModel model, int? brandId, int? typeId)
        {
            //no need to these two lines
            ViewBag.Sort = "PostSort = " + model.SortSelected;
            ViewData["Sort"] = "PostSort = " + model.SortSelected;


            ShopParams shopParams = new ShopParams() {
                BrandId = brandId ?? 0,
                TypeId = typeId ?? 0,
                Sort = model.SortSelected,
                Search = model.SearchSelected
            };

            
            return RedirectToAction("Index", shopParams);
        }

        public IActionResult ProductDetails(int id, int quantity=1) {
            Product product = _shopService.GetProduct(id);
            if (String.IsNullOrEmpty(product.Name))
            {
               return NotFound();
            }
            ProductQuantityViewModel model = new ProductQuantityViewModel()
            {
                Product = product,
                Quantity = quantity
            };

            return View(model);
        }
        public IActionResult IncrementQuantity(int id, int quantity)
        {
            return RedirectToAction("ProductDetails", new { id = id.ToString(), quantity = (quantity+1).ToString() });
        }

        public IActionResult DecrementQuantity(int id, int quantity)
        {
            return RedirectToAction("ProductDetails", new { 
                    id = id.ToString(), 
                    quantity = (quantity > 1) ? (quantity - 1).ToString() : "1"
            });
        }
    }
}
