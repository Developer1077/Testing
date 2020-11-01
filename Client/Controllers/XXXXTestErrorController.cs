using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class XXXXTestErrorController: Controller
    {
        public XXXXTestErrorController()
        {

        }
        public IActionResult Get404Error() {

            return View();
        }
    }
}
