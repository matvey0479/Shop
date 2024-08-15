using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shop.Domain.Entites;
using Shop.Domain.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Repositories.EntityFramework
{
    public class EFOrdersProductsRepository : IOrdersProductsRepository
    {
        public ShopContext context;
        public int countAddedRows = 0;
        public EFOrdersProductsRepository(ShopContext context)
        {
            this.context = context;
        }

        public void AddOrderProduct(OrderProduct orderProduct)
        {
            if (!context.orderProducts.Any(x => x.ProductId == orderProduct.ProductId &&
                                          x.OrderId == orderProduct.OrderId &&
                                          x.productCount == orderProduct.productCount))
            {
                context.orderProducts.Add(orderProduct);
                context.SaveChanges();
                countAddedRows++;
            }

        }
       
        public string GetAddedRows()
        {
            string result = "В таблицу OrdersProducts добавлено объектов: " + countAddedRows;
            countAddedRows = 0;
            return result;
        }

    }
}
