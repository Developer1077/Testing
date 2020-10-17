using Client.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Net.Http;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            Pagination pagination = new Pagination();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:5001/api/");

                var response = client.GetAsync("products?pageSize=50");
                response.Wait();

                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    //Install-Package System.Net.Http.Formatting.Extension

                    var readTask = result.Content.ReadAsAsync<Pagination>();
                    readTask.Wait();

                    pagination = readTask.Result;
                }
            }
            return View(pagination);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        
    }
}
