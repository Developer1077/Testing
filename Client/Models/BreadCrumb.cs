using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    public class BreadCrumb
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public bool IsActive { get; set; }
    }
}
