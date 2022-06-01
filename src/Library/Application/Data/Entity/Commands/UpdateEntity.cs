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
    public class UpdateEntity : IRequest
    {
        public string Id { get; set; }
        public EntityRequest Entity { get; set; }
    }
    public class UpdateEntityHandler : IRequestHandler<UpdateEntity>
    {
        private readonly IEntityRepository entityRepository;
        private readonly IMapper mapper;

        public UpdateEntityHandler(IEntityRepository entityRepository, IMapper mapper)
        {
            this.entityRepository = entityRepository;
            this.mapper = mapper;
        }

        public Task<Unit> Handle(UpdateEntity request, CancellationToken cancellationToken)
        {
            var entity = mapper.Map<Library.Domain.Entities.Entity>(request.Entity);
            entityRepository.Update(request.Id, entity);
            return Task.FromResult(Unit.Value);
        }
    }
}
