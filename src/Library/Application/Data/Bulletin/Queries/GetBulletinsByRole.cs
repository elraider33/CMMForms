using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Application.Dtos;
using Library.Domain.Repositories;
using MediatR;

namespace Library.Application.Data.Bulletin.Queries
{
    public class GetBulletinsByRole : IRequest<List<BulletinDto>>
    {
    }

    public class GetBulletinsByRoleHandler : IRequestHandler<GetBulletinsByRole, List<BulletinDto>>
    {
        private readonly IBulletinRepository _repository;
        private readonly IMapper _mapper;
        
        public GetBulletinsByRoleHandler(IBulletinRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        public Task<List<BulletinDto>> Handle(GetBulletinsByRole request, CancellationToken cancellationToken)
        {
            var bulletins = _repository.GetRoles();
            return Task.FromResult<List<BulletinDto>>(null);
        }
    }
}