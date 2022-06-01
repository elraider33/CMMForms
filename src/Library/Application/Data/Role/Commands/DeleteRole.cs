using AutoMapper;
using Library.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Data.Role.Commands
{
    public class DeleteRole : IRequest<Unit>
    {
        public string Id { get; set; }
    }
    public class DeleteRoleHandler : IRequestHandler<DeleteRole, Unit>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public DeleteRoleHandler(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public Task<Unit> Handle(DeleteRole request, CancellationToken cancellationToken)
        {
            _roleRepository.Delete(request.Id);
            return Task.FromResult(Unit.Value);
        }
    }
}
