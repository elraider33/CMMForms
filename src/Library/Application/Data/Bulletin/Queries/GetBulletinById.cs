using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using LanguageExt;
using LanguageExt.TypeClasses;
using Library.Application.Dtos;
using Library.Domain.Repositories;
using MediatR;

namespace Library.Application.Data.Bulletin.Queries
{
    public class GetBulletinById : IRequest<Option<BulletinDto>>
    {
        public string Id { get; set; }
    }

    public class GetBulletinByIdHandler : IRequestHandler<GetBulletinById, Option<BulletinDto>>
    {
        private readonly IBulletinRepository _repository;
        private readonly IMapper _mapper;
        
        public GetBulletinByIdHandler(IBulletinRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        public Task<Option<BulletinDto>> Handle(GetBulletinById request, CancellationToken cancellationToken)
        {
            var bulletin = _repository.Get(request.Id);
            var bulletinDto = _mapper.Map<BulletinDto>(bulletin);
            return Task.FromResult<Option<BulletinDto>>(bulletinDto);
        }
    }
}