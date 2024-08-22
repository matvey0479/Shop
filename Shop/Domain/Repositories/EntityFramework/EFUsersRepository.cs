using Shop.Domain.Entites;
using Shop.Domain.Repositories.Abstract;

namespace Shop.Domain.Repositories.EntityFramework
{
    public class EFUsersRepository : IUsersRepository
    {
        public ShopContext Context;
        public int CountAddedRows = 0;
        public EFUsersRepository(ShopContext context) 
        {
            Context = context;
        }
        public void AddUser(User user)
        {
            if(!Context.Users.Any(x => x.Email == user.Email))
            {
                Context.Users.Add(user);
                Context.SaveChanges();
                CountAddedRows++;
            }
            
        }

        public User GetUserByEmail(string email)
        {
            return Context.Users.FirstOrDefault(x=>x.Email==email);
        }

        public string GetAddedRows()
        {
            string result = "В таблицу Users добавлено объектов: " + CountAddedRows;
            CountAddedRows = 0;
            return result;
        }
    }
}
