using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTPWebShop.API.Model
{
    public class OrderDto
    {
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public double TotalWeight { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime IsExpressDelivery { get; set; }
        public bool IsDelivered { get; set; }
    }
}
