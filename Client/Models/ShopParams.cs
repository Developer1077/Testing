using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    public class ShopParams
    {
        public int BrandId { get; set; } = 0;
        public int TypeId { get; set; } = 0;
        public string Sort { get; set; } = "name";

        public string Search { get; set; }

        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 6;

    }
}
