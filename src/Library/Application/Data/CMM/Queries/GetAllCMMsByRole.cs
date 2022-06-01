using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Application.Dtos;
using Library.Domain.Repositories;
using MediatR;

namespace Library.Application.Data.CMM.Queries
{
    public class GetAllCMMsByRole : IRequest<List<CMMDto>>
    {
        public string Role { get; set; }
    }

    public class GetAllCMMsByRoleHandler : IRequestHandler<GetAllCMMsByRole, List<CMMDto>>
    {
        private readonly ICMMRepository _repository;
        private readonly IMapper _mapper;

        public GetAllCMMsByRoleHandler(ICMMRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<List<CMMDto>> Handle(GetAllCMMsByRole request, CancellationToken cancellationToken)
        {
            var cmms = _mapper.Map<List<CMMDto>>(_repository.GetByRole(request.Role));
            return Task.FromResult(cmms);
        }
    }
}