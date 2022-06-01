using AutoMapper;
using Library.Application.Data.Entity.Commands;
using Library.Application.Dtos;
using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Profiles
{
    public class EntityProfile : AutoMapper.Profile
    {
        public EntityProfile()
        {
            CreateMap<Entity, EntityDto>();
            CreateMap<CreateEntity, Entity>()
                .ForMember(d => d.Id, opt => opt.Ignore());
            CreateMap<EntityRequest, Entity>()
                .ForMember(d => d.Id, opt => opt.Ignore());
        }

    }
}
