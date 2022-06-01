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
    public class GetCMMByRole : IRequest<Dictionary<string, List<CMMDto>>>
    {
    }
    public class GetCMMByRoleHandler : IRequestHandler<GetCMMByRole, Dictionary<string, List<CMMDto>>>
    {
        private readonly IRoleRepository roleRepository;
        private readonly ICMMRepository CMMRepository;
        private readonly IMapper mapper;

        public GetCMMByRoleHandler(IRoleRepository roleRepository, ICMMRepository CMMRepository, IMapper mapper)
        {
            this.roleRepository = roleRepository;
            this.CMMRepository = CMMRepository;
            this.mapper = mapper;
        }

        public Task<Dictionary<string, List<CMMDto>>> Handle(GetCMMByRole request, CancellationToken cancellationToken)
        {
            var roles = roleRepository.Get();
            var CMMs = CMMRepository.Get();
            var rolesCMM = new Dictionary<string, List<CMMDto>>();
            foreach (var role in roles)
            {
                var b = CMMs.Where(b =>
                {
                    if (b.Roles != null)
                    {
                        return b.Roles.Contains(role.Name);
                    }
                    return false;
                }).ToList();
                if (!string.IsNullOrEmpty(role.Name) && b.Any())
                    rolesCMM.TryAdd(role.Name, mapper.Map<List<CMMDto>>(b));
            }
            return Task.FromResult(rolesCMM);
        }
    }
}
