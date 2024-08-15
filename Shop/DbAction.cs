using Shop.Domain.Entites;
using Shop.XmlModel;
using Shop.Domain;

namespace Shop
{
    public class DbAction
    {
        private readonly DataManager dataManager;

        public DbAction(DataManager dataManager)
        {
            this.dataManager = dataManager;

        }
        public void ExecuteInsert(List<XmlDataModel> orders)
        {
            try
            {
                if (orders != null)
                {
                    foreach (var order in orders)
                    {
                        foreach (var user in order.Users)
                        {
                            var _user = new User(null, null, user.fio, user.email, null);
                            dataManager.users.AddUser(_user);
                            if (_user.Id == 0) { _user = dataManager.users.GetUserByEmail(user.email); }
                            var _order = new Order(order.numberOrder, Convert.ToDateTime(order.OrderDate), _user.Id);
                            dataManager.orders.AddOrder(_order);
                            if (_order.Id == 0) { _order = dataManager.orders.GetOrderByNumber(order.numberOrder); }
                            foreach (var product in order.Products)
                            {
                                var _product = new Product(null, product.productName, Convert.ToDouble(product.price));
                                dataManager.products.AddProduct(_product);
                                if (_product.Id == 0)
                                {
                                    _product = dataManager.products.GetProductByNameAndPrice(product.productName, Convert.ToDouble(product.price));
                                }
                                dataManager.ordersProducts.AddOrderProduct(new OrderProduct(_order.Id, _product.Id, Convert.ToDouble(product.quantity)));
                            }
                        }
                    }

                }

            }catch (Exception e) {Console.WriteLine(e.Message);}
        }
        public void ShowResultInsert()
        {
            Console.WriteLine(dataManager.users.GetAddedRows() + "\n" +
                              dataManager.products.GetAddedRows() + "\n" +
                              dataManager.orders.GetAddedRows() + "\n" +
                              dataManager.ordersProducts.GetAddedRows() + "\n");
        }

        public void ShowSales()
        {
            var sales = dataManager.sales.GetSales();
            Console.WriteLine("ProductName\tQuantity\tAmount");
            foreach (var sale in sales)
            {
                Console.Write(sale.productName + "\t" + sale.Quantity + "\t\t" + sale.Amount + "\n");
            }
        }



    }
}
