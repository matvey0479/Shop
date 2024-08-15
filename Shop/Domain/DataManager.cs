using Shop.Domain.Repositories.Abstract;

namespace Shop.Domain
{
    public class DataManager
    {
        public DataManager(IUsersRepository users,IProductsRepository products,IOrdersRepository orders,
                           IOrdersProductsRepository ordersProducts, ISalesRepository sales) 
        { 
            this.users = users;
            this.products = products;
            this.orders = orders;
            this.ordersProducts = ordersProducts;
            this.sales = sales;
        }
        public IUsersRepository users {  get; set; }
        public IProductsRepository products { get; set; }
        public IOrdersRepository orders { get; set; }
        public IOrdersProductsRepository ordersProducts { get; set; }
        public ISalesRepository sales { get; set; }
    }
}
