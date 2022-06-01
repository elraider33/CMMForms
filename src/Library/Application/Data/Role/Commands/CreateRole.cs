using AutoMapper;
using Library.Application.Dtos;
using Library.Domain.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Data.Role.Commands
{
    public class CreateRole : IRequest<RoleDto>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<string>? EmailAddresses { get; set; }
    }
    public class CreateRoleHandler : IRequestHandler<CreateRole, RoleDto>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public CreateRoleHandler(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public Task<RoleDto> Handle(CreateRole request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Domain.Entities.Role>(request);
            _roleRepository.Add(entity);
            return Task.FromResult(_mapper.Map<RoleDto>(entity));
        }
    }

}
