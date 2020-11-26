using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class CheckoutController: Controller
    {
        public IActionResult Index() {
            return View();
        }
        
    }
}
