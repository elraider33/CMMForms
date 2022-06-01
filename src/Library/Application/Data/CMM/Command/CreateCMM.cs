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

namespace Library.Application.Data.CMM.Command
{
    public class CreateCMM : IRequest<CMMDto>
    {
        public string RequestedBy { get; set; }
        public string Entity { get; set; }
        public bool Published { get; set; }
        public IFormFile FILE { get; set; }
        public string Customer { get; set; }
        public string CMMNumber { get; set; }
        public DateTime? InitialDate { get; set; }
        public string Model { get; set; }
        public string ManualRev { get; set; }
        public DateTime? RevDate { get; set; }
        public string Aircraft { get; set; }
        public string JobNo { get; set; }
        public IEnumerable<string> IncorporatedSeatAssemblies { get; set; }
        public IEnumerable<string> ServiceBulletins { get; set; }
        public IEnumerable<string> Reference { get; set; }
        public string AircraftInstallation { get; set; }
        public string Config { get; set; }
        public string TrimFinish { get; set; }
        public string Comments { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public IEnumerable<string> SeatPartNumbers { get; set; }
        public IEnumerable<string> SeatPartNumbersTSO { get; set; }
    }

    public class CreateCMMHandler : IRequestHandler<CreateCMM, CMMDto>
    {
        private readonly ICMMRepository _repository;
        private readonly IFileRepository _fileRepository;
        private readonly IMapper _mapper;

        public CreateCMMHandler(ICMMRepository repository, IMapper mapper, IFileRepository fileRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _fileRepository = fileRepository;
        }

        public async Task<CMMDto> Handle(CreateCMM request, CancellationToken cancellationToken)
        {
            //TODO: GridFS HERE
            var cmm = _mapper.Map<Domain.Entities.CMM>(request);
            cmm.Roles = cmm.Roles?.Where(w => !string.IsNullOrEmpty(w)).ToList();
            cmm.SeatPartNumbers = cmm.SeatPartNumbers?.Where(w => !string.IsNullOrEmpty(w)).ToList();
            cmm.SeatPartNumbersTSO = cmm.SeatPartNumbersTSO?.Where(w => !string.IsNullOrEmpty(w)).ToList();
            cmm.ServiceBulletins = cmm.ServiceBulletins?.Where(w => !string.IsNullOrEmpty(w)).ToList();
            cmm.IncorporatedSeatAssemblies = cmm.IncorporatedSeatAssemblies?.Where(w => !string.IsNullOrEmpty(w)).ToList();
            cmm.Reference = cmm.Reference?.Where(w => !string.IsNullOrEmpty(w)).ToList();
            if (request.FILE != null)
            {
                var fileId = await _fileRepository.AddFile(request.FILE);
                var fileInfo = _fileRepository.GetFile(fileId);
                cmm.FILE = fileInfo;
            }
            var entity = _repository.Add(cmm);
            var dto = _mapper.Map<CMMDto>(entity);
            return dto;
        }
    }
}