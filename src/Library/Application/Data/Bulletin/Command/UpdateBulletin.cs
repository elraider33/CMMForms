using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Application.Dtos;
using Library.Domain.Entities;
using Library.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Library.Application.Data.Bulletin.Command
{
    public class UpdateBulletin : IRequest
    {
        public string Id { get; set; }
        public BulletinRequestDto Bulletin;
    }

    public class UpdateBulletinHandler : IRequestHandler<UpdateBulletin>
    {
        private readonly IBulletinRepository _repository;
        private readonly IFileRepository _fileRepository;
        private readonly IMapper _mapper;

        public UpdateBulletinHandler(IBulletinRepository repository, IMapper mapper, IFileRepository fileRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _fileRepository = fileRepository;
        }
        
        public async Task<Unit> Handle(UpdateBulletin request, CancellationToken cancellationToken)
        {
            var bulletin = _mapper.Map<Domain.Entities.Bulletin>(request.Bulletin);
            bulletin.Roles = bulletin.Roles?.Where(w => !string.IsNullOrEmpty(w)).ToList();
            bulletin.SeatPartNumbers = bulletin.SeatPartNumbers?.Where(w => !string.IsNullOrEmpty(w)).ToList();
            bulletin.CMM = bulletin.CMM?.Where(w => !string.IsNullOrEmpty(w)).ToList();
            bulletin.Id = request.Id;
            
            if (request.Bulletin.FILE != null)
            {
                var entity = _repository.Get(request.Id);
                if (entity.FILE != null)
                {
                    _fileRepository.DeleteFile(entity.FILE.FilesId);
                }
                var fileId = await _fileRepository.AddFile(request.Bulletin.FILE);
                var fileInfo = _fileRepository.GetFile(fileId);
                bulletin.FILE = fileInfo;
            }
            else
            {
                var entity = _repository.Get(request.Id);
                bulletin.FILE = entity.FILE;
            }
            _repository.Update(request.Id, bulletin);
            return Unit.Value;
        }
    }
}