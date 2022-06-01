using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Library.Infrastructure.Helpers
{
    public class StreamHelper
    {
        public static async Task<MemoryStream> FileToStream(IFormFile file)
        {
            var filePath = Path.GetTempFileName();
            var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            return stream;
        }
    }
}