using Library.Application.Data.Role.Commands;
using Library.Application.Dtos;
using Library.Domain.Entities;

namespace Library.Application.Profiles
{
    public class RoleProfile : AutoMapper.Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleDto>();
            CreateMap<CreateRole, Role>()
                .ForMember(d => d.Usernames, opt => opt.Ignore())
                .ForMember(d => d.Id, opt => opt.Ignore());
            CreateMap<UpdateRole, Role>()
                .ForMember(d => d.Usernames, opt => opt.Ignore());
        }
    }
}
