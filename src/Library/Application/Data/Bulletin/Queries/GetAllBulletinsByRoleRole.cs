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
    public class GetAllBulletinsByRoleRole : IRequest<Dictionary<string, List<BulletinDto>>>
    {
        public string Role { get; set; }
    }
    public class GetAllBulletinsByRoleRoleHandler : IRequestHandler<GetAllBulletinsByRoleRole, Dictionary<string, List<BulletinDto>>>
    {
        private readonly IRoleRepository roleRepository;
        private readonly IBulletinRepository bulletinRepository;
        private readonly IMapper mapper;

        public GetAllBulletinsByRoleRoleHandler(IRoleRepository roleRepository, IBulletinRepository bulletinRepository, IMapper mapper)
        {
            this.roleRepository = roleRepository;
            this.bulletinRepository = bulletinRepository;
            this.mapper = mapper;
        }

        public Task<Dictionary<string, List<BulletinDto>>> Handle(GetAllBulletinsByRoleRole request, CancellationToken cancellationToken)
        {
            var role = roleRepository.GetByName(request.Role);
            var roles = new List<Domain.Entities.Role>() { role };
            var bulletins = bulletinRepository.GetByRole(request.Role);
            var rolesBulletin = new Dictionary<string, List<BulletinDto>>();
            foreach (var r in roles)
            {
                var b = bulletins.Where(b =>
                {
                    if (b.Roles != null)
                    {
                        return b.Roles.Contains(r.Name);
                    }
                    return false;
                }).ToList();
                if (!string.IsNullOrEmpty(r.Name))
                    rolesBulletin.TryAdd(r.Name, mapper.Map<List<BulletinDto>>(b));
            }
            return Task.FromResult(rolesBulletin);
        }
    }
}
