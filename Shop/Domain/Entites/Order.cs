using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Entites
{
    public class Order: Entity
    {
        public Order() { }
        public Order(string numberOrder,DateTime orderDate, int userId)
        {
            NumberOrder = numberOrder;
            OrderDate = orderDate;
            UserId = userId;
            OrdersProducts = new List<OrderProduct>();
        }
        public string NumberOrder {  get; set; }
        public DateTime OrderDate { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public virtual ICollection<OrderProduct> OrdersProducts { get; set; }

    }
}
