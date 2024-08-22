using Shop.Domain.Repositories.Abstract;

namespace Shop.Domain
{
    public class DataManager
    {
        public DataManager(IUsersRepository user,IProductsRepository product,IOrdersRepository order,
                           IOrdersProductsRepository ordersProduct, ISalesRepository sale) 
        { 
            User = user;
            Product = product;
            Order = order;
            OrdersProduct = ordersProduct;
            Sale = sale;
        }
        public IUsersRepository User {  get; set; }
        public IProductsRepository Product { get; set; }
        public IOrdersRepository Order { get; set; }
        public IOrdersProductsRepository OrdersProduct { get; set; }
        public ISalesRepository Sale { get; set; }
    }
}
