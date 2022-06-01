using System.Collections.Generic;
using Library.Domain.Entities;

namespace Library.Domain.Repositories
{
    public interface ICMMRepository
    {
        List<CMM> Get();
        CMM Get(string id);
        CMM Add(CMM manual);
        List<CMM> GetByRole(string role);
        void Update(string id, CMM manual);
        IEnumerable<string> GetRoles();

        void Delete(string id);
    }
}