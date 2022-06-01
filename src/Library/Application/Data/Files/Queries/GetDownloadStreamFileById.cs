using Library.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Data.Files.Queries
{
    public class GetDownloadStreamFileById : IRequest<MemoryStream>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class GetDownloadStreamFileByIdHandler : IRequestHandler<GetDownloadStreamFileById, MemoryStream>
    {
        private IFileRepository _fileRepository;
        private ILogger<GetDownloadStreamFileByIdHandler> logger;
        private IFSChunkRepository _chunkRepository;
        public GetDownloadStreamFileByIdHandler(IFileRepository fileRepository, ILogger<GetDownloadStreamFileByIdHandler> logger, IFSChunkRepository chunkRepository)
        {
            this._fileRepository = fileRepository;
            this.logger = logger;
            _chunkRepository = chunkRepository;
        }

        public Task<MemoryStream> Handle(GetDownloadStreamFileById request, CancellationToken cancellationToken)
        {


            var chunks = _chunkRepository.GetByFileId(request.Id);
            logger.LogInformation("[GetDownloadStreamFileByIdHandler] start concat the chunks");
            byte[] ba = new byte[chunks.Sum(c => c.Data.Length)];
            var chunksArr = chunks.Select(c => c.Data).ToArray() ;
            int offset = 0;
            foreach (byte[] ar in chunksArr)
            {
                System.Buffer.BlockCopy(ar, 0, ba, offset, ar.Length);
                offset += ar.Length;
            }
            logger.LogInformation("[GetDownloadStreamFileByIdHandler] end concat the chunks");
            logger.LogInformation("[GetDownloadStreamFileByIdHandler] start create the stream");
            MemoryStream memoryStream = new(ba);
            logger.LogInformation("[GetDownloadStreamFileByIdHandler] end create the stream");
            return Task.FromResult(memoryStream);
        }
    }
}
