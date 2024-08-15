using Shop.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Repositories.Abstract
{
    public interface IOrdersProductsRepository:IEntityRepository
    {
        public void AddOrderProduct(OrderProduct product);

    }
}
