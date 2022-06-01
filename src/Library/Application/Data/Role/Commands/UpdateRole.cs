using AutoMapper;
using Library.Domain.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Data.Role.Commands
{
    public class UpdateRole : IRequest<Unit>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<string>? EmailAddresses { get; set; }
    }
    public class UpdateRoleHandler : IRequestHandler<UpdateRole, Unit>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public UpdateRoleHandler(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public Task<Unit> Handle(UpdateRole request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Library.Domain.Entities.Role>(request);
            _roleRepository.Update(request.Id, entity);
            return Task.FromResult(Unit.Value);
        }
    }

}
