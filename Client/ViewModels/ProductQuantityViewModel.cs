using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Models;

namespace Client.ViewModels
{
    public class ProductQuantityViewModel
    {
        public Product Product { get; set; }
        public int Quantity { get; set; } = 1;

    }
}
