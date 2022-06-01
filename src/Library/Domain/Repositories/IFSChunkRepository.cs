using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Repositories
{
    public interface IFSChunkRepository
    {
        List<FSChunk> Get();
        List<FSChunk> GetByFileId(string fileId);
        FSChunk Get(string id);
    }
}
