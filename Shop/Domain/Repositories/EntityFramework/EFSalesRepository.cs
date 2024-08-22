using Shop.Domain.Repositories.Abstract;
using Shop.Models;

namespace Shop.Domain.Repositories.EntityFramework
{
    public class EFSalesRepository: ISalesRepository
    {
        public ShopContext Context;
        public EFSalesRepository(ShopContext context)
        {
            Context = context;
        }
        public List<Sale> GetSales()
        {
            var queri = from product in Context.Products
                        join orderProduct in Context.OrderProducts on product.Id equals orderProduct.ProductId
                        join order in Context.Orders on orderProduct.OrderId equals order.Id
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
