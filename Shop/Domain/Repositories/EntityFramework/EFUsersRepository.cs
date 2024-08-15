using Shop.Domain.Entites;
using Shop.Domain.Repositories.Abstract;

namespace Shop.Domain.Repositories.EntityFramework
{
    public class EFUsersRepository : IUsersRepository
    {
        public ShopContext context;
        public int countAddedRows = 0;
        public EFUsersRepository(ShopContext context) 
        {
            this.context = context;
        }
        public void AddUser(User user)
        {
            if(!context.Users.Any(x => x.email == user.email))
            {
                context.Users.Add(user);
                context.SaveChanges();
                countAddedRows++;
            }
            
        }

        public User GetUserByEmail(string email)
        {
            return context.Users.FirstOrDefault(x=>x.email==email);
        }

        public string GetAddedRows()
        {
            string result = "В таблицу Users добавлено объектов: " + countAddedRows;
            countAddedRows = 0;
            return result;
        }
    }
}
