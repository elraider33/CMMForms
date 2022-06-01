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

namespace Library.Application.Data.Entity.Commands
{
    public class CreateEntity : IRequest<EntityDto>
    {
        public string Description { get; set; }
    }

    public class CreateEntityHandler : IRequestHandler<CreateEntity, EntityDto>
    {
        private readonly IEntityRepository entityRepository;
        private readonly IMapper mapper;

        public CreateEntityHandler(IEntityRepository entityRepository, IMapper mapper)
        {
            this.entityRepository = entityRepository;
            this.mapper = mapper;
        }

        public Task<EntityDto> Handle(CreateEntity request, CancellationToken cancellationToken)
        {
            var entity = mapper.Map<Library.Domain.Entities.Entity>(request);
            var entityObj = entityRepository.Add(entity);
            var entityDto = mapper.Map<EntityDto>(entityObj);
            return Task.FromResult<EntityDto>(entityDto);
        }
    }
}
