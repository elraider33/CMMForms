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

namespace Library.Application.Data.Bulletin.Queries
{
    public class GetBulletinByRole : IRequest<Dictionary<string, List<BulletinDto>>>
    {
    }
    public class GetBulletinByRoleHandler : IRequestHandler<GetBulletinByRole, Dictionary<string, List<BulletinDto>>>
    {
        private readonly IRoleRepository roleRepository;
        private readonly IBulletinRepository bulletinRepository;
        private readonly IMapper mapper;

        public GetBulletinByRoleHandler(IRoleRepository roleRepository, IBulletinRepository bulletinRepository, IMapper mapper)
        {
            this.roleRepository = roleRepository;
            this.bulletinRepository = bulletinRepository;
            this.mapper = mapper;
        }

        public Task<Dictionary<string, List<BulletinDto>>> Handle(GetBulletinByRole request, CancellationToken cancellationToken)
        {
            var roles = roleRepository.Get();
            var bulletins = bulletinRepository.GetWithRole();
            var rolesBulletin = new Dictionary<string, List<BulletinDto>>();
            foreach (var role in roles.Select(r => r.Name))
            {
                var b = bulletins.Where(b =>
                {
                    if (b.Roles != null)
                    {
                        return b.Roles.Contains(role);
                    }
                    return false;
                }).ToList();
                if (!string.IsNullOrEmpty(role) && b.Any())
                    rolesBulletin.TryAdd(role, mapper.Map<List<BulletinDto>>(b));
            }
            return Task.FromResult(rolesBulletin);
        }
    }
}
