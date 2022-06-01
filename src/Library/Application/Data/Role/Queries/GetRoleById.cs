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
    public class GetRoleById : IRequest<RoleDto>
    {
        public string Id { get; set; }
    }
    public class GetRoleByIdHandler : IRequestHandler<GetRoleById, RoleDto>
    {
        private readonly IRoleRepository roleRepository;
        private readonly IMapper mapper;

        public GetRoleByIdHandler(IRoleRepository roleRepository, IMapper mapper)
        {
            this.roleRepository = roleRepository;
            this.mapper = mapper;
        }

        public Task<RoleDto> Handle(GetRoleById request, CancellationToken cancellationToken)
        {
            var roles = roleRepository.Get(request.Id);
            var dtos = mapper.Map<RoleDto>(roles);
            return Task.FromResult(dtos);
        }
    }
}
