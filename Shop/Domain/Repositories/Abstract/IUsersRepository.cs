using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Domain.Entites;

namespace Shop.Domain.Repositories.Abstract
{
    public interface IUsersRepository:IEntityRepository
    {
        public void AddUser(User user);
        public User GetUserByEmail(string email);
    }
}
