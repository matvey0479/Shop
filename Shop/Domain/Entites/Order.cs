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
            this.numberOrder = numberOrder;
            OrderDate = orderDate;
            UserId = userId;
            ordersProducts = new List<OrderProduct>();
        }
        public string numberOrder {  get; set; }
        public DateTime OrderDate { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public virtual ICollection<OrderProduct> ordersProducts { get; set; }

    }
}
