using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NTPWebShop.Domain
{
    public class Product
    {
        [Key]
        public  int ProductId  { get;set;}
        public string Name { get; set; }
        public double Unit { get; set; }
        public int InStock { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
