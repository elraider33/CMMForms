using AutoMapper;
using LanguageExt;
using Library.Application.Dtos;
using Library.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Data.Entity.Queries
{
    public class GetEntityById : IRequest<Option<EntityDto>>
    {
        public string Id { get; set; }
    }
    public class GetEntityByIdHandler : IRequestHandler<GetEntityById, Option<EntityDto>>
    {
        private readonly IEntityRepository entityRepository;
        private readonly IMapper mapper;

        public GetEntityByIdHandler(IEntityRepository entityRepository, IMapper mapper)
        {
            this.entityRepository = entityRepository;
            this.mapper = mapper;
        }

        public Task<Option<EntityDto>> Handle(GetEntityById request, CancellationToken cancellationToken)
        {
            var entities = entityRepository.Get(request.Id);
            var dtos = mapper.Map<EntityDto>(entities);
            return Task.FromResult<Option<EntityDto>>(dtos);
        }
    }
}
