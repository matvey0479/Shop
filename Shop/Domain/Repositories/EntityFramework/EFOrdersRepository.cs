using Shop.Domain.Entites;
using Shop.Domain.Repositories.Abstract;

namespace Shop.Domain.Repositories.EntityFramework
{
    public class EFOrdersRepository : IOrdersRepository
    {
        public ShopContext context;
        public int countAddedRows = 0;
        public EFOrdersRepository(ShopContext context) 
        {
            this.context = context;
        }

        public void AddOrder(Order order)
        {
            if(!context.Order.Any(x=> x.OrderDate == order.OrderDate && x.UserId == order.UserId))
            {
                context.Order.Add(order);
                context.SaveChanges();
                countAddedRows++;
            }

        }

        public Order GetOrderByNumber(string numberOrder)
        {
            return context.Order.FirstOrDefault(x => x.numberOrder == numberOrder);
        }
        public string GetAddedRows()
        {
            string result = "В таблицу Orders добавлено объектов: " + countAddedRows;
            countAddedRows = 0;
            return result;
        }
    }
}
