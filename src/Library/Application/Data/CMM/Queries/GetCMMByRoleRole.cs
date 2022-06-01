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

namespace Library.Application.Data.CMM.Queries
{
    public class GetCMMByRoleRole : IRequest<Dictionary<string, List<CMMDto>>>
    {
        public string Role { get; set; }
    }
    public class GetCMMByRoleRoleHandler : IRequestHandler<GetCMMByRoleRole, Dictionary<string, List<CMMDto>>>
    {
        private readonly IRoleRepository roleRepository;
        private readonly ICMMRepository CMMRepository;
        private readonly IMapper mapper;

        public GetCMMByRoleRoleHandler(IRoleRepository roleRepository, ICMMRepository CMMRepository, IMapper mapper)
        {
            this.roleRepository = roleRepository;
            this.CMMRepository = CMMRepository;
            this.mapper = mapper;
        }

        public Task<Dictionary<string, List<CMMDto>>> Handle(GetCMMByRoleRole request, CancellationToken cancellationToken)
        {
            var role = roleRepository.GetByName(request.Role);
            var roles = new List<Library.Domain.Entities.Role>() { role };
            var CMMs = CMMRepository.GetByRole(request.Role);
            var rolesCMM = new Dictionary<string, List<CMMDto>>();
            foreach (var r in roles)
            {
                var b = CMMs.Where(b =>
                {
                    if (b.Roles != null)
                    {
                        return b.Roles.Contains(r.Name);
                    }
                    return false;
                }).ToList();
                if (!string.IsNullOrEmpty(r.Name))
                    rolesCMM.TryAdd(r.Name, mapper.Map<List<CMMDto>>(b));
            }
            return Task.FromResult(rolesCMM);
        }
    }
}
