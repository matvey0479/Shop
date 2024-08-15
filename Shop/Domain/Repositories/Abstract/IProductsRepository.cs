using Shop.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Repositories.Abstract
{
    public interface IProductsRepository:IEntityRepository
    {
        public void AddProduct(Product product);
        public Product GetProductByNameAndPrice(string productName,double price);
    }
}
