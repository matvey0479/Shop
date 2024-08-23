using Shop.Domain.Repositories.Abstract;
using Shop.Models;

namespace Shop.Domain.Repositories.EntityFramework
{
    public class EFSalesRepository: ISalesRepository
    {
        private readonly ShopContext _context;
        public EFSalesRepository(ShopContext context)
        {
            _context = context;
        }
        public List<Sale> GetSales()
        {
            var queri = from product in _context.Products
                        join orderProduct in _context.OrderProducts on product.Id equals orderProduct.ProductId
                        join order in _context.Orders on orderProduct.OrderId equals order.Id
                        group orderProduct by product.ProductName into g
                        select new Sale()
                        {
                            ProductName = g.Key,
                            Quantity = g.Sum(product=>product.ProductCount),
                            Amount = g.FirstOrDefault().Product.Price * g.Sum(product => product.ProductCount)

                        };
            List<Sale> sales = queri.ToList();
            return sales;
        }
    }
}
