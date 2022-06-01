using Library.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Data.Files.Queries
{
    public class GetDownloadFileById : IRequest<byte[]>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class GetDownloadFileByIdHandler : IRequestHandler<GetDownloadFileById, byte[]>
    {
        private IFileRepository _fileRepository;
        private ILogger<GetDownloadFileByIdHandler> logger;
        public GetDownloadFileByIdHandler(IFileRepository fileRepository, ILogger<GetDownloadFileByIdHandler> logger)
        {
            this._fileRepository = fileRepository;
            this.logger = logger;
        }

        public async Task<byte[]> Handle(GetDownloadFileById request, CancellationToken cancellationToken)
        {
            var fileStream = await _fileRepository.DownloadStreamFileById(request.Id);
            return fileStream.ToArray();
        }
    }
}
