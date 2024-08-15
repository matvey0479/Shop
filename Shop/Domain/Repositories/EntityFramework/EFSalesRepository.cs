using Shop.Domain.Repositories.Abstract;
using Shop.Models;

namespace Shop.Domain.Repositories.EntityFramework
{
    public class EFSalesRepository: ISalesRepository
    {
        public ShopContext context;
        public Sale sale;
        public EFSalesRepository(ShopContext context)
        {
            this.context = context;
        }
        public List<Sale> GetSales()
        {
            var queri = from product in context.Products
                        join orderProduct in context.orderProducts on product.Id equals orderProduct.ProductId
                        join order in context.Order on orderProduct.OrderId equals order.Id
                        group orderProduct by product.productName into g
                        select new Sale()
                        {
                            productName = g.Key,
                            Quantity = g.Sum(product=>product.productCount),
                            Amount = g.FirstOrDefault().product.price * g.Sum(product => product.productCount)

                        };
            List<Sale> sales = queri.ToList();
            return sales;
        }
    }
}
