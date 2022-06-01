using AutoMapper;
using Library.Application.Dtos;
using Library.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Data.Role.Queries
{
    public class GetRoles : IRequest<List<RoleDto>>
    {
    }
    public class GetRolesHandler : IRequestHandler<GetRoles, List<RoleDto>>
    {
        private readonly IRoleRepository roleRepository;
        private readonly IMapper mapper;

        public GetRolesHandler(IRoleRepository roleRepository, IMapper mapper)
        {
            this.roleRepository = roleRepository;
            this.mapper = mapper;
        }

        public Task<List<RoleDto>> Handle(GetRoles request, CancellationToken cancellationToken)
        {
            var roles = roleRepository.Get();
            var dtos = mapper.Map<List<RoleDto>>(roles);
            return Task.FromResult(dtos);
        }
    }
}
