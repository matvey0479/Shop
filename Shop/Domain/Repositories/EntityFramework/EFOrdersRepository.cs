using Shop.Domain.Entites;
using Shop.Domain.Repositories.Abstract;

namespace Shop.Domain.Repositories.EntityFramework
{
    public class EFOrdersRepository : IOrdersRepository
    {
        public ShopContext Context;
        public int CountAddedRows = 0;
        public EFOrdersRepository(ShopContext context) 
        {
            Context = context;
        }

        public void AddOrder(Order order)
        {
            if(!Context.Orders.Any(x=> x.OrderDate == order.OrderDate && x.UserId == order.UserId))
            {
                Context.Orders.Add(order);
                Context.SaveChanges();
                CountAddedRows++;
            }

        }

        public Order GetOrderByNumber(string numberOrder)
        {
            return Context.Orders.FirstOrDefault(x => x.NumberOrder == numberOrder);
        }
        public string GetAddedRows()
        {
            string result = "В таблицу Orders добавлено объектов: " + CountAddedRows;
            CountAddedRows = 0;
            return result;
        }
    }
}
