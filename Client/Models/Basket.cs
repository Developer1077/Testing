using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    public class Basket
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();


    }
}
