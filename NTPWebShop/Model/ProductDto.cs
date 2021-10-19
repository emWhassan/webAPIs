using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTPWebShop.API.Model
{
    public class ProductDto
    {

        public int? ProductId { get; set; }
        public string Name { get; set; }
        public double Unit { get; set; }
        public int InStock { get; set; }
        public string deliveryDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
