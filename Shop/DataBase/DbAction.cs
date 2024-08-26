using Shop.Domain.Entites;
using Shop.XmlModel;
using Shop.Domain;
using Shop.Domain.Repositories.Abstract;

namespace Shop.DbActions
{
    public class DbAction:IDbAction
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IProductsRepository _productsRepository;
        private readonly ISalesRepository _salesRepository;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IOrdersProductsRepository _ordersProductsRepository;

        public DbAction(IUsersRepository users, IProductsRepository products, ISalesRepository sales,
                        IOrdersRepository orders, IOrdersProductsRepository ordersProducts)
        {
            _usersRepository = users;
            _productsRepository = products;
            _salesRepository = sales;
            _ordersRepository = orders;
            _ordersProductsRepository = ordersProducts;

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
                            _usersRepository.AddUser(_user);
                            if (_user.Id == 0) { _user = _usersRepository.GetUserByEmail(user.Email); }
                            var _order = new Order(order.NumberOrder, Convert.ToDateTime(order.OrderDate), _user.Id);
                            _ordersRepository.AddOrder(_order);
                            if (_order.Id == 0) { _order = _ordersRepository.GetOrderByNumber(order.NumberOrder); }
                            foreach (var product in order.Products)
                            {
                                var _product = new Product(null, product.ProductName, Convert.ToDouble(product.Price));
                                _productsRepository.AddProduct(_product);
                                if (_product.Id == 0)
                                {
                                    _product = _productsRepository.GetProductByNameAndPrice(product.ProductName, Convert.ToDouble(product.Price));
                                }
                                _ordersProductsRepository.AddOrderProduct(new OrderProduct(_order.Id, _product.Id, Convert.ToDouble(product.Quantity)));
                            }
                        }
                    }

                }

            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }
        public void ShowResultInsert()
        {
            Console.WriteLine(_usersRepository.GetAddedRows() + "\n" +
                              _productsRepository.GetAddedRows() + "\n" +
                              _ordersRepository.GetAddedRows() + "\n" +
                              _ordersProductsRepository.GetAddedRows() + "\n");
        }

        public void ShowSales()
        {
            var sales = _salesRepository.GetSales();
            Console.WriteLine("ProductName\tQuantity\tAmount");
            foreach (var sale in sales)
            {
                Console.Write(sale.ProductName + "\t" + sale.Quantity + "\t\t" + sale.Amount + "\n");
            }
        }



    }
}
