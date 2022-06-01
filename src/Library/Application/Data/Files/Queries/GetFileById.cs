using AutoMapper;
using Library.Application.Dtos;
using Library.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Data.Files.Queries
{
    public class GetFileById : IRequest<FileDto>
    {
        public string Id { get; set; }
    }

    public class GetFileByIdHandler : IRequestHandler<GetFileById, FileDto>
    {
        private IFileRepository _fileRepository;
        private IMapper _mapper;

        public GetFileByIdHandler(IFileRepository fileRepository, IMapper mapper)
        {
            _fileRepository = fileRepository;
            _mapper = mapper;
        }

        public Task<FileDto> Handle(GetFileById request, CancellationToken cancellationToken)
        {
            var file = _fileRepository.GetFile(request.Id);
            var fileDto = _mapper.Map<FileDto>(file); 
            return Task.FromResult(fileDto);
        }
    }
}
