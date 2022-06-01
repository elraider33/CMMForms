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
    public class UserRepository : BaseRepository, IUserRepository
    {
        private readonly IMongoCollection<User> _users;
        private readonly ILogger<UserRepository> logger;

        public UserRepository(IMongoOptions options, ILogger<UserRepository> logger) : base(options, logger)
        {
            this.logger = logger;
            logger.LogInformation("User Repository");
            logger.LogInformation("Database options", options.ConnectionString, options.DatabaseName);
            _users = _database.GetCollection<User>(Collections.User);
        }

        public List<User> Get()
        {
            var users = _users.Find(e => true).ToList();
            return users;
        } 
        public User Get(string id)
        {
            var User = _users.Find(e => e.Id == id).FirstOrDefault();
            return User;
        }

        public User Add(User role)
        {
            _users.InsertOne(role);
            return role;
        }

        public void Update(string id, User role)
        {
            var User = Get(id);
            _users.ReplaceOne(b => b.Id == id, role);
        }

        public User GetByEmail(string email)
        {
            //var User = _users.Find(e => e.Emails.Where(e => e.Address == email)).FirstOrDefault();
            return null;
        }

        public User GetByUsername(string username)
        {
            var User = _users.Find(e => e.Username == username).FirstOrDefault();
            return User;
        }
    }
}