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

namespace Library.Application.Data.Entity.Queries
{
    public class GetAllEntities : IRequest<List<EntityDto>>
    {
    }
    public class GetAllEntitiesHandler : IRequestHandler<GetAllEntities, List<EntityDto>>
    {
        private readonly IEntityRepository entityRepository;
        private readonly IMapper mapper;

        public GetAllEntitiesHandler(IEntityRepository entityRepository, IMapper mapper)
        {
            this.entityRepository = entityRepository;
            this.mapper = mapper;
        }

        public Task<List<EntityDto>> Handle(GetAllEntities request, CancellationToken cancellationToken)
        {
            var entities = entityRepository.Get();
            var dtos = mapper.Map<List<EntityDto>>(entities);
            return Task.FromResult(dtos);
        }
    }
}
