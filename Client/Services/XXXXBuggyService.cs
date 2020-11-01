using Client.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Services
{
    public class XXXXBuggyService : XXXXIBuggyService
    {
        private readonly string _apiUrl;

        public XXXXBuggyService(IConfiguration config)
        {
            _apiUrl = config["ApiUrl"];
        }

        public Product Get404Error(ShopParams shopParams)
        {

            Product product = new Product();

            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri(_apiUrl);

            //    var response = client.GetAsync($"products/42");
            //    response.Wait();

            //    var result = response.Result;

            //    if (result.IsSuccessStatusCode)
            //    {
            //        var readTask = result.Content.ReadAsAsync<Product>();
            //        readTask.Wait();

            //        product = readTask.Result;
            //    }
            //}
            return product;
        }
    }
}
