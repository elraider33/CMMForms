using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Repositories
{
    public interface IUserRepository
    {
        List<User> Get();
        User Get(string id);
        User GetByEmail(string email);
        User GetByUsername(string username);
        User Add(User user);
        void Update(string id, User user);
    }
}
