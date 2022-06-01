using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Repositories
{
    public interface IEntityRepository
    {
        List<Entity> Get();
        Entity Get(string id);
        Entity Add(Entity entity);
        void Update(string id, Entity entity);
    }
}
