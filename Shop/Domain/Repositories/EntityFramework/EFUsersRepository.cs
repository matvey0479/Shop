using Shop.Domain.Entites;
using Shop.Domain.Repositories.Abstract;

namespace Shop.Domain.Repositories.EntityFramework
{
    public class EFUsersRepository : IUsersRepository
    {
        private readonly ShopContext _context;
        private int _countAddedRows = 0;
        public EFUsersRepository(ShopContext context) 
        {
            _context = context;
        }
        public void AddUser(User user)
        {
            if(!_context.Users.Any(x => x.Email == user.Email))
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                _countAddedRows++;
            }
            
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(x=>x.Email==email);
        }

        public string GetAddedRows()
        {
            string result = "В таблицу Users добавлено объектов: " + _countAddedRows;
            _countAddedRows = 0;
            return result;
        }
    }
}
