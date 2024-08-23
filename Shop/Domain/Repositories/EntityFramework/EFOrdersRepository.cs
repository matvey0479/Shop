using Shop.Domain.Entites;
using Shop.Domain.Repositories.Abstract;

namespace Shop.Domain.Repositories.EntityFramework
{
    public class EFOrdersRepository : IOrdersRepository
    {
        private readonly ShopContext _context;
        private int _countAddedRows = 0;
        public EFOrdersRepository(ShopContext context) 
        {
            _context = context;
        }

        public void AddOrder(Order order)
        {
            if(!_context.Orders.Any(x=> x.OrderDate == order.OrderDate && x.UserId == order.UserId))
            {
                _context.Orders.Add(order);
                _context.SaveChanges();
                _countAddedRows++;
            }

        }

        public Order GetOrderByNumber(string numberOrder)
        {
            return _context.Orders.FirstOrDefault(x => x.NumberOrder == numberOrder);
        }
        public string GetAddedRows()
        {
            string result = "В таблицу Orders добавлено объектов: " + _countAddedRows;
            _countAddedRows = 0;
            return result;
        }
    }
}
