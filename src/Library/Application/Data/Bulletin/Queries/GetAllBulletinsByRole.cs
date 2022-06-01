using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Application.Dtos;
using Library.Domain.Repositories;
using MediatR;

namespace Library.Application.Data.Bulletin.Queries
{
    public class GetAllBulletinsByRole : IRequest<List<BulletinDto>>
    {
        public string Role { get; set; }
    }

    public class GetAllBulletinsByRoleHandler : IRequestHandler<GetAllBulletinsByRole, List<BulletinDto>>
    {
        private readonly IBulletinRepository _repository;
        private readonly IMapper _mapper;
        
        public GetAllBulletinsByRoleHandler(IBulletinRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        public Task<List<BulletinDto>> Handle(GetAllBulletinsByRole request, CancellationToken cancellationToken)
        {
            var bulletins = _mapper.Map<List<BulletinDto>>(_repository.GetByRole(request.Role));
            return Task.FromResult<List<BulletinDto>>(bulletins);
        }
    }
}