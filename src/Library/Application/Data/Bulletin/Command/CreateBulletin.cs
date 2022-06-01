using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Application.Dtos;
using Library.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Library.Application.Data.Bulletin.Command
{
    public class CreateBulletin : IRequest<BulletinDto>
    {
        public string RequestedBy { get; set; }
        public DateTime RequestedOn { get; set; }
        public bool TSORequired { get; set; }
        public string Entity { get; set; }
        public bool Published { get; set; }
        public IFormFile FILE { get; set; }
        public string Sbno { get; set; }
        public string Type { get; set; }
        public string ModelNumber { get; set; }
        public DateTime? InitialDate { get; set; }
        public string Customer { get; set; }
        public string ManualRev { get; set; }
        public DateTime? RevDate { get; set; }
        public string Description { get; set; }
        public string Model { get; set; }
        public string Aircraft { get; set; }
       
        // Cambiar por CMM Entity
        public List<string> CMM { get; set; }
        public string JobNumber { get; set; }
        public string RepairStationNumber { get; set; }
        
        public string Writer { get; set; }
        public List<string> SeatPartNumbers { get; set; }
       
        public string Comments { get; set; }
        public List<string> Roles { get; set; }
    }

    public class CreateBulletinHandler : IRequestHandler<CreateBulletin, BulletinDto>
    {
        private readonly IBulletinRepository _repository;
        private readonly IFileRepository _fileRepository;
        private readonly IMapper _mapper;

        public CreateBulletinHandler(IBulletinRepository repository, IMapper mapper, IFileRepository fileRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _fileRepository = fileRepository;
        }

        public async Task<BulletinDto> Handle(CreateBulletin request, CancellationToken cancellationToken)
        {
            var bulletin = _mapper.Map<Domain.Entities.Bulletin>(request);
            bulletin.Roles = bulletin.Roles?.Where(w => !string.IsNullOrEmpty(w)).ToList();
            bulletin.SeatPartNumbers = bulletin.SeatPartNumbers?.Where(w => !string.IsNullOrEmpty(w)).ToList();
            bulletin.CMM = bulletin.CMM?.Where(w => !string.IsNullOrEmpty(w)).ToList();
            //TODO: GridFS HERE
            if (request.FILE != null)
            {
                var fileId = await _fileRepository.AddFile(request.FILE);
                var fileInfo = _fileRepository.GetFile(fileId);
                bulletin.FILE = fileInfo;
            }

            
            
            var entity = _repository.Add(bulletin);
            var dto = _mapper.Map<BulletinDto>(entity);
            return dto;
        }
    }
}