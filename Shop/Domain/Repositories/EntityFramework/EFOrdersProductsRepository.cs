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
        private readonly ShopContext _context;
        private int _countAddedRows = 0;
        public EFOrdersProductsRepository(ShopContext context)
        {
            _context = context;
        }

        public void AddOrderProduct(OrderProduct orderProduct)
        {
            if (!_context.OrderProducts.Any(x => x.ProductId == orderProduct.ProductId &&
                                          x.OrderId == orderProduct.OrderId &&
                                          x.ProductCount == orderProduct.ProductCount))
            {
                _context.OrderProducts.Add(orderProduct);
                _context.SaveChanges();
                _countAddedRows++;
            }

        }
       
        public string GetAddedRows()
        {
            string result = "В таблицу OrdersProducts добавлено объектов: " + _countAddedRows;
            _countAddedRows = 0;
            return result;
        }

    }
}
