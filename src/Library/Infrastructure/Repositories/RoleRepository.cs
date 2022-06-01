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
    public class RoleRepository : BaseRepository, IRoleRepository
    {
        private readonly IMongoCollection<Role> _roles;
        private readonly ILogger<RoleRepository> logger;
        public RoleRepository(IMongoOptions options, ILogger<RoleRepository> logger) : base(options, logger)
        {
            _roles = _database.GetCollection<Role>(Collections.Role);
            this.logger = logger;
        }

        public List<Role> Get()
        {
            var filter = Builders<Role>.Filter.Ne(r => r.Name, string.Empty);
            var roles = _roles.Find(filter).SortBy(r => r.Name).ToList();
            return roles;
        } 
        public Role Get(string id)
        {
            try
            {
                var filter = Builders<Role>.Filter.Eq(r => r.Id, id);
                var Role = _roles.Find(filter).FirstOrDefault();
                return Role;
            }
            catch (Exception)
            {

                var roles = Get();
                var role = roles.FirstOrDefault(r => r.Id == id);
                return role;
            }
            
        }
        public Role GetByName(string role)
        {
            //var Role = _roles.Find(e => e.Name == role).FirstOrDefault();
            var data = _roles.Aggregate()
                .Match(r => r.Name == role)
                .FirstOrDefault();
            return data;
        }

        public Role Add(Role role)
        {
            //role.Id = new BsonObjectId(ObjectId.GenerateNewId());
            _roles.InsertOne(role);
            return role;
        }

        public void Update(string id, Role role)
        {
            var Role = Get(id);
            _roles.ReplaceOne(b => b.Id == id, role);
        }

        public void Delete(string id)
        {
            _roles.DeleteOne(b => b.Id == id);  
        }
    }
}