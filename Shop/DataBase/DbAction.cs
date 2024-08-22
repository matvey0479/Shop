using Shop.Domain.Entites;
using Shop.XmlModel;
using Shop.Domain;

namespace Shop.DbActions
{
    public class DbAction:IDbAction
    {
        private readonly DataManager _dataManager;

        public DbAction(DataManager dataManager)
        {
            _dataManager = dataManager;

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
                            var _user = new User(null, null, user.Fio, user.Email, null);
                            _dataManager.User.AddUser(_user);
                            if (_user.Id == 0) { _user = _dataManager.User.GetUserByEmail(user.Email); }
                            var _order = new Order(order.NumberOrder, Convert.ToDateTime(order.OrderDate), _user.Id);
                            _dataManager.Order.AddOrder(_order);
                            if (_order.Id == 0) { _order = _dataManager.Order.GetOrderByNumber(order.NumberOrder); }
                            foreach (var product in order.Products)
                            {
                                var _product = new Product(null, product.ProductName, Convert.ToDouble(product.Price));
                                _dataManager.Product.AddProduct(_product);
                                if (_product.Id == 0)
                                {
                                    _product = _dataManager.Product.GetProductByNameAndPrice(product.ProductName, Convert.ToDouble(product.Price));
                                }
                                _dataManager.OrdersProduct.AddOrderProduct(new OrderProduct(_order.Id, _product.Id, Convert.ToDouble(product.Quantity)));
                            }
                        }
                    }

                }

            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }
        public void ShowResultInsert()
        {
            Console.WriteLine(_dataManager.User.GetAddedRows() + "\n" +
                              _dataManager.Product.GetAddedRows() + "\n" +
                              _dataManager.Order.GetAddedRows() + "\n" +
                              _dataManager.OrdersProduct.GetAddedRows() + "\n");
        }

        public void ShowSales()
        {
            var sales = _dataManager.Sale.GetSales();
            Console.WriteLine("ProductName\tQuantity\tAmount");
            foreach (var sale in sales)
            {
                Console.Write(sale.ProductName + "\t" + sale.Quantity + "\t\t" + sale.Amount + "\n");
            }
        }



    }
}
