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
        public ShopContext Context;
        public int CountAddedRows = 0;
        public EFOrdersProductsRepository(ShopContext context)
        {
            Context = context;
        }

        public void AddOrderProduct(OrderProduct orderProduct)
        {
            if (!Context.OrderProducts.Any(x => x.ProductId == orderProduct.ProductId &&
                                          x.OrderId == orderProduct.OrderId &&
                                          x.ProductCount == orderProduct.ProductCount))
            {
                Context.OrderProducts.Add(orderProduct);
                Context.SaveChanges();
                CountAddedRows++;
            }

        }
       
        public string GetAddedRows()
        {
            string result = "В таблицу OrdersProducts добавлено объектов: " + CountAddedRows;
            CountAddedRows = 0;
            return result;
        }

    }
}
