using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Repositories
{
    public interface IRoleRepository
    {
        List<Role> Get();
        Role Get(string id);
        Role GetByName(string role);
        Role Add(Role role);
        void Update(string id, Role role);
        void Delete(string id);
    }
}
