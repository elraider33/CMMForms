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
    public class GetCMMById : IRequest<Option<CMMDto>>
    {
        public string Id { get; set; }
    }

    public class GetCMMByIdHandler : IRequestHandler<GetCMMById, Option<CMMDto>>
    {
        private readonly ICMMRepository _repository;
        private readonly IMapper _mapper;
        
        public GetCMMByIdHandler(ICMMRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        public Task<Option<CMMDto>> Handle(GetCMMById request, CancellationToken cancellationToken)
        {
            var cmm = _mapper.Map<CMMDto>(_repository.Get(request.Id));
            return Task.FromResult<Option<CMMDto>>(cmm);
        }
    }
}