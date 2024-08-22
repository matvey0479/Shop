using Shop.Domain.Entites;
using Shop.Domain.Repositories.Abstract;

namespace Shop.Domain.Repositories.EntityFramework
{
    public class EFProductsRepository : IProductsRepository
    {
        public ShopContext Context;
        public int CountAddedRows = 0;

        public EFProductsRepository(ShopContext context) 
        {
            Context = context;
        }
        public void AddProduct(Product product)
        {
            if(!Context.Products.Any(x => x.ProductName == product.ProductName && x.Price == product.Price))
            {
                Context.Products.Add(product);
                Context.SaveChanges();
                CountAddedRows ++;
            }

        }
        public Product GetProductByNameAndPrice(string nameProduct,double price)
        {
            return Context.Products.FirstOrDefault(x=> x.ProductName ==  nameProduct && x.Price == price);
        }
        public string GetAddedRows()
        {
            string result =  "В таблицу Products добавлено объектов: " +CountAddedRows;
            CountAddedRows = 0;
            return result;
        }
    }
}
