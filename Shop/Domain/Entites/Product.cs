using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Entites
{
    public class Product: Entity
    {
        public Product(string? articleNumber,string productName,double price) 
        {
            this.articleNumber = articleNumber;
            this.productName = productName;
            this.price = price;
            ordersProducts = new  List<OrderProduct>();
        }
        public Product() { }
        public string? articleNumber { get; set; }=null;
        public string productName { get; set; }
        public double price { get; set; }
        public virtual ICollection<OrderProduct> ordersProducts { get; set; }
    }
}
