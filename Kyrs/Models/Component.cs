using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyrs.Models
{
    public class Component
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Manufacturer { get; set; }
        public decimal PricePerUnit { get; set; }
        public string Category { get; set; }
        public string ArticleNumber { get; set; }
    }
}
