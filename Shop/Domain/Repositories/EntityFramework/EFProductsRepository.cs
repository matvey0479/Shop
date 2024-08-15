using Shop.Domain.Entites;
using Shop.Domain.Repositories.Abstract;

namespace Shop.Domain.Repositories.EntityFramework
{
    public class EFProductsRepository : IProductsRepository
    {
        public ShopContext context;
        public int countAddedRows = 0;

        public EFProductsRepository(ShopContext context) 
        {
            this.context = context;
        }
        public void AddProduct(Product product)
        {
            if(!context.Products.Any(x => x.productName == product.productName && x.price == product.price))
            {
                context.Products.Add(product);
                context.SaveChanges();
                countAddedRows ++;
            }

        }
        public Product GetProductByNameAndPrice(string nameProduct,double price)
        {
            return context.Products.FirstOrDefault(x=> x.productName ==  nameProduct && x.price == price);
        }
        public string GetAddedRows()
        {
            string result =  "В таблицу Products добавлено объектов: " +countAddedRows;
            countAddedRows = 0;
            return result;
        }
    }
}
