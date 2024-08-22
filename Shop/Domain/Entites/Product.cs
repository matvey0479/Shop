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
            ArticleNumber = articleNumber;
            ProductName = productName;
            Price = price;
            OrdersProducts = new  List<OrderProduct>();
        }
        public Product() { }
        public string? ArticleNumber { get; set; }=null;
        public string ProductName { get; set; }
        public double Price { get; set; }
        public virtual ICollection<OrderProduct> OrdersProducts { get; set; }
    }
}
