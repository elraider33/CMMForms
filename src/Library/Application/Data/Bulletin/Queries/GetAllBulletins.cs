using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Application.Dtos;
using Library.Domain.Repositories;
using MediatR;

namespace Library.Application.Data.Bulletin.Queries
{
    public class GetAllBulletins : IRequest<List<BulletinDto>>
    {
        public string Id { get; set; }
    }

    public class GetAllBulletinsHandler : IRequestHandler<GetAllBulletins, List<BulletinDto>>
    {
        private readonly IBulletinRepository _repository;
        private readonly IMapper _mapper;
        
        public GetAllBulletinsHandler(IBulletinRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        public Task<List<BulletinDto>> Handle(GetAllBulletins request, CancellationToken cancellationToken)
        {
            var bulletins = _mapper.Map<List<BulletinDto>>(_repository.Get());
            return Task.FromResult<List<BulletinDto>>(bulletins);
        }
    }
}