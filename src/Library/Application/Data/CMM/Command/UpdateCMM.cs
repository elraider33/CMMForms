using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Application.Dtos;
using Library.Domain.Repositories;
using MediatR;

namespace Library.Application.Data.CMM.Command
{
    public class UpdateCMM : IRequest
    {
        public string Id { get; set; }
        public CMMRequestDto CMM;
    }

    public class UpdateCMMHandler : IRequestHandler<UpdateCMM>
    {
        private readonly ICMMRepository _repository;
        private readonly IFileRepository _fileRepository;
        private readonly IMapper _mapper;

        public UpdateCMMHandler(ICMMRepository repository, IMapper mapper, IFileRepository fileRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _fileRepository = fileRepository;
        }

        public async Task<Unit> Handle(UpdateCMM request, CancellationToken cancellationToken)
        {
            var cmm = _mapper.Map<Domain.Entities.CMM>(request.CMM);
            cmm.Roles = cmm.Roles?.Where(w => !string.IsNullOrEmpty(w)).ToList();
            cmm.SeatPartNumbers = cmm.SeatPartNumbers?.Where(w => !string.IsNullOrEmpty(w)).ToList();
            cmm.SeatPartNumbersTSO = cmm.SeatPartNumbersTSO?.Where(w => !string.IsNullOrEmpty(w)).ToList();
            cmm.ServiceBulletins = cmm.ServiceBulletins?.Where(w => !string.IsNullOrEmpty(w)).ToList();
            cmm.IncorporatedSeatAssemblies = cmm.IncorporatedSeatAssemblies?.Where(w => !string.IsNullOrEmpty(w)).ToList();
            cmm.Reference = cmm.Reference?.Where(w => !string.IsNullOrEmpty(w)).ToList();
            cmm.Id = request.Id;
            if (request.CMM.FILE != null)
            {
                var entity = _repository.Get(request.Id);
                if (entity.FILE != null)
                {
                    _fileRepository.DeleteFile(entity.FILE.FilesId);
                }
                var fileId = await _fileRepository.AddFile(request.CMM.FILE);
                var fileInfo = _fileRepository.GetFile(fileId);
                cmm.FILE = fileInfo;
            }
            else
            {
                var entity = _repository.Get(request.Id);
                cmm.FILE = entity.FILE;
            }
            _repository.Update(request.Id, cmm);
            return Unit.Value;
        }
    }
}