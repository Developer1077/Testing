using Client.Extensions;
using Client.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Client.Services
{
    public class BasketService : IBasketService
    {
        private readonly string _apiUrl;
        private readonly ILogger<BasketService> _logger;
        private readonly IMapper _mapper;
        private readonly IShopService _shopService;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public BasketService(ILogger<BasketService> logger, IMapper mapper, IShopService shopService, IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _apiUrl = config["ApiUrl"];
            _logger = logger;
            _mapper = mapper;
            _shopService = shopService;
            _httpContextAccessor = httpContextAccessor;
        }



        //private Basket _basketSource;
        //public Basket BasketSource
        //{
        //    get { return GetBasket(_httpContextAccessor.HttpContext.Request.Cookies["basket_id"]); }
        //    set
        //    {
        //        _basketSource = value;
        //    //    _httpContextAccessor.HttpContext.Session.SetObject("Basket", GetBasket(_httpContextAccessor.HttpContext.Request.Cookies["basket_id"]));
        //    }
        //    //get => _httpContextAccessor.HttpContext.Session.GetObject<Basket>("Basket");
        //    //set
        //    //{
        //    //    _httpContextAccessor.HttpContext.Session.SetObject("Basket", GetBasket(_httpContextAccessor.HttpContext.Request.Cookies["basket_id"]));           
        //    //}
        //}

        public Basket GetBasket(string id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_apiUrl);

                var response = client.GetAsync($"basket?id={id}");

                response.Wait();

                var result = response.Result;

                _logger.LogWarning(response.Result.ToString() + result.IsSuccessStatusCode.ToString());
                _logger.LogWarning($"basket/{id}");

                if (result.IsSuccessStatusCode)
                {
                    //Install-Package Microsoft.AspNet.WebApi.Client 

                    var readTask = result.Content.ReadAsAsync<Basket>();
                    readTask.Wait();

                    Basket basket = readTask.Result;
                    _logger.LogWarning("ccccccccccccccccccccccc");
                    _logger.LogWarning(result.ToString());
                    return basket;
                }
                else
                {
                    _logger.LogWarning("sssssssssssssssssssss");


                }
                return null;
            }

        }


        public Basket SetBasket(Basket basket)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_apiUrl);

                var response = client.PostAsJsonAsync($"basket/", basket);
                response.Wait();

                var result = response.Result;


                if (result.IsSuccessStatusCode)
                {
                    ////Install-Package Microsoft.AspNet.WebApi.Client 

                    //var readTask = result.Content.ReadAsAsync<Basket>();
                    //readTask.Wait();

                    //basket = readTask.Result;

                    //this.BasketSource = basket;

                    //_logger.LogInformation(response.ToString());

                    return basket;
                }
                else
                {
                    // _logger.LogInformation(result.ToString());
                }
            }
            return basket;
        }

        public Basket GetCurrentBasketValue()
        {

            //_logger.LogInformation(_httpContextAccessor.HttpContext.Request.Cookies["basket_id"]);
            Basket basket = null;
            if (_httpContextAccessor.HttpContext.Request.Cookies["basket_id"] != null)
            {
                basket = GetBasket(_httpContextAccessor.HttpContext.Request.Cookies["basket_id"]);
              //  _logger.LogInformation(basket.Id.ToString());

            }

            return basket;
        }

        public void AddItemToBasket(int productId, int quantity = 1)
        {
            Product product = _shopService.GetProduct(productId);
            if (product != null)
            {
                BasketItem itemToAdd = _mapper.Map<Product, BasketItem>(product);
                itemToAdd.Quantity = quantity;

                Basket basket = GetCurrentBasketValue() ?? CreateBasket();

                basket.Items = this.AddOrUpdateItem(basket.Items, itemToAdd, quantity);

                SetBasket(basket);
            }
        }

        private List<BasketItem> AddOrUpdateItem(List<BasketItem> items, BasketItem itemToAdd, int quantity)
        {
            int index = items.FindIndex(i => i.Id == itemToAdd.Id);

            if (index == -1)
            {
                itemToAdd.Quantity = quantity;
                items.Add(itemToAdd);
            }
            else
            {
                items[index].Quantity += quantity;
            }
            return items;
        }

        private Basket CreateBasket()
        {
            Basket basket = new Basket();

            //saving the id in a cookie
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(30);
            _httpContextAccessor.HttpContext.Response.Cookies.Append("basket_id", basket.Id.ToString(), option);

            return basket;
        }

        public BasketTotals CalculateTotals()
        {
            Basket basket = GetCurrentBasketValue();
            double subTotal = 0;
            double shipping = 0;
            foreach (var item in basket.Items)
            {
                subTotal = subTotal + item.Price * item.Quantity;
            }

            BasketTotals totals = new BasketTotals()
            {

                Shipping = shipping,
                SubTotal = subTotal,
                Total = shipping + subTotal
            };
            return totals;
        }

        public void IncrementItemQuantity(int id)
        {

            Basket basket = GetCurrentBasketValue();
            var founfItemIndex = basket.Items.FindIndex(x => x.Id == id);
            basket.Items[founfItemIndex].Quantity++;
            SetBasket(basket);
        }
        public void DecrementItemQuantity(int id)
        {

            Basket basket = GetCurrentBasketValue();
            var founfItemIndex = basket.Items.FindIndex(x => x.Id == id);
            if (basket.Items[founfItemIndex].Quantity > 1)
            {
                basket.Items[founfItemIndex].Quantity--;
            }
            else
            {
                RemoveItemFromBasket(id);
            }

            SetBasket(basket);
        }

        public void RemoveItemFromBasket(int id)
        {
            Basket basket = GetCurrentBasketValue();
            if (basket.Items.Any(x=>x.Id == id))
            {
                var foundItemIndex = basket.Items.FindIndex(x => x.Id == id);

                basket.Items.RemoveAt(foundItemIndex);
                if (basket.Items.Count > 0)
                {
                    SetBasket(basket);
                    _logger.LogInformation("A");
                }
                else {
                    DeleteBasket(basket);
                    _logger.LogInformation("C");
                }
                
            }
            _logger.LogWarning("B");
        }

        public void DeleteBasket(Basket basket)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_apiUrl);

                var deleteTask = client.DeleteAsync($"basket?id={basket.Id}");

                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    _httpContextAccessor.HttpContext.Response.Cookies.Delete("basket_id");
                }
                else
                {
                    _logger.LogWarning("sssssssssssssssssssss");


                }
            }

        }


    }
}
