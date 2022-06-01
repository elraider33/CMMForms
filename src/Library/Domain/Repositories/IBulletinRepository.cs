using System.Collections.Generic;
using Library.Domain.Entities;

namespace Library.Domain.Repositories
{
    public interface IBulletinRepository
    {
        List<Bulletin> Get();
        List<Bulletin> GetWithRole();
        Bulletin Get(string id);
        Bulletin Add(Bulletin bulletin);

        void Update(string id, Bulletin bulletin);
        List<Bulletin> GetByRole(string role);
        IEnumerable<string> GetRoles();
        void Delete(string id);
    }
}