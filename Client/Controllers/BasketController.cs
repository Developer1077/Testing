using Client.Models;
using Client.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class BasketController: Controller
    {
        private readonly ILogger<BasketController> _logger;
        private readonly IBasketService _basketService;

        public BasketController(ILogger<BasketController> logger, IBasketService basketService)
        {
            _logger = logger;
            _basketService = basketService;
        }



        public IActionResult AddItemToBasket(int productId, int quantity=1) {

            _basketService.AddItemToBasket(productId, quantity);

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Index()
        {
            Basket basket = _basketService.GetCurrentBasketValue();
        //    _logger.LogInformation(basket.Id.ToString());
            return View(basket);
        }

        public IActionResult RemoveItemFromBasket(int id)
        {
            _basketService.RemoveItemFromBasket(id);
            return RedirectToAction("Index");
        }

        public IActionResult IncrementItemQuantity(int id)
        {
            _basketService.IncrementItemQuantity(id);
            return RedirectToAction("Index");
        }

        public IActionResult DecrementItemQuantity(int id)
        {
            _basketService.DecrementItemQuantity(id);
            return RedirectToAction("Index");
        }

        
    }
}
