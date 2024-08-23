using Shop.Domain.Entites;
using Shop.Domain.Repositories.Abstract;

namespace Shop.Domain.Repositories.EntityFramework
{
    public class EFProductsRepository : IProductsRepository
    {
        private readonly ShopContext _context;
        private int _countAddedRows = 0;

        public EFProductsRepository(ShopContext context) 
        {
            _context = context;
        }
        public void AddProduct(Product product)
        {
            if(!_context.Products.Any(x => x.ProductName == product.ProductName && x.Price == product.Price))
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                _countAddedRows ++;
            }

        }
        public Product GetProductByNameAndPrice(string nameProduct,double price)
        {
            return _context.Products.FirstOrDefault(x=> x.ProductName ==  nameProduct && x.Price == price);
        }
        public string GetAddedRows()
        {
            string result =  "В таблицу Products добавлено объектов: " +_countAddedRows;
            _countAddedRows = 0;
            return result;
        }
    }
}
