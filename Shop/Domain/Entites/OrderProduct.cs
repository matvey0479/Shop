using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Shop.Domain.Entites
{
    [PrimaryKey(nameof(OrderId), nameof(ProductId))]
    public class OrderProduct
    {
        public OrderProduct() { }
        public OrderProduct(int orderId, int productId, double productCount)
        {
            OrderId = orderId;
            ProductId = productId;
            this.productCount = productCount;
        }

        

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None), Key]
        public int OrderId { get; set; }
        public Order order { get; set; }

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None), Key]
        public int ProductId { get; set; }
        public Product product { get; set; }
        public double productCount { get; set; }
    }
}
