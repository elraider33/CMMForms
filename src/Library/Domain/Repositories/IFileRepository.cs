using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Library.Domain.Entities;
using MongoDB.Driver.GridFS;
using System.IO;
using File = Library.Domain.Entities.File;

namespace Library.Domain.Repositories
{
    public interface IFileRepository
    {
        Task<string> AddFile(IFormFile formFile);

        File GetFile(string Id);
        Task<byte[]> DownloadFileById(string id);
        Task<byte[]> DownloadFileByName(string id);
        Task<GridFSDownloadStream> DownloadStreamFileByIdAsync(string id);
        Task<MemoryStream> DownloadStreamFileById(string id);
        void DeleteFile(string id);
    }
}