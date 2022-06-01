using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Application.Dtos;
using Library.Domain.Repositories;
using MediatR;

namespace Library.Application.Data.CMM.Queries
{
    public class GetAllCMMs : IRequest<List<CMMDto>>
    {
        public string Id { get; set; }
    }

    public class GetAllCMMsHandler : IRequestHandler<GetAllCMMs, List<CMMDto>>
    {
        private readonly ICMMRepository _repository;
        private readonly IMapper _mapper;

        public GetAllCMMsHandler(ICMMRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<List<CMMDto>> Handle(GetAllCMMs request, CancellationToken cancellationToken)
        {
            var cmms = _mapper.Map<List<CMMDto>>(_repository.Get());
            return Task.FromResult(cmms);
        }
    }
}