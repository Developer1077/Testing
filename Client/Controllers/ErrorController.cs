using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class ErrorController : Controller
    {

        [Route("error/{statuscode:int}")]
        public IActionResult StatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry, The resource you requested couls not be found";
                    ViewBag.Title = "Not Found";
                     break;
                case 401:
                    ViewBag.ErrorMessage = "You're not authorized";
                    break;
                case 500:
                    ViewBag.ErrorMessage = "Server Error";
                    ViewBag.Title = "Server Error";
                    break;
                default:
                    ViewBag.ErrorMessage = "Server Error";
                    break;
            
            }
            return View("index");
        }

    }
}