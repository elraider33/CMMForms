using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Library.Application.Dtos;
using Library.Domain.Common;
using Library.Domain.Entities;
using Library.Domain.Options;
using Library.Domain.Repositories;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Library.Infrastructure.Repositories
{
    public class EntityRepository : BaseRepository, IEntityRepository
    {
        private readonly IMongoCollection<Entity> _entities;
        private readonly ILogger<EntityRepository> logger;
        public EntityRepository(IMongoOptions options, ILogger<EntityRepository> logger) : base(options, logger)
        {
            _entities = _database.GetCollection<Entity>(Collections.Entity);
            this.logger = logger;
        }

        public List<Entity> Get()
        {
            var entities = _entities.Find(e => !string.IsNullOrEmpty(e.Description)).ToList();
            return entities;
        } 
        public Entity Get(string id)
        {
            var entity = _entities.Find(e => e.Id == id).FirstOrDefault();
            return entity;
        }

        public Entity Add(Entity manual)
        {
            _entities.InsertOne(manual);
            return manual;
        }

        public void Update(string id, Entity manual)
        {
            var entity = Get(id);
            _entities.ReplaceOne(b => b.Id == id, manual);
        }
    }
}