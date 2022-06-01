using System;
using System.Collections.Generic;
using System.Linq;
using Library.Domain.Common;
using Library.Domain.Entities;
using Library.Domain.Options;
using Library.Domain.Repositories;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Library.Infrastructure.Repositories
{
    public class BulletinRepository : BaseRepository, IBulletinRepository
    {
        private readonly IMongoCollection<Bulletin> _bulletins;
        private readonly ILogger<BulletinRepository> logger;
        public BulletinRepository(
            IMongoOptions options, 
            ILogger<BulletinRepository> logger) : base(options, logger)
        {
            _bulletins = _database.GetCollection<Bulletin>(Collections.BULLETIN);
            
            this.logger = logger;
        }

        public List<Bulletin> Get()
        {
            try
            {
                var fieldsBuilder = Builders<Bulletin>.Projection;
                var fields = fieldsBuilder
                                          .Exclude(b => b.CMM)
                                          .Exclude(b => b.Roles);
                var bulletins = _bulletins.Aggregate()
                            .Match(f => !f.Deleted && !string.IsNullOrEmpty(f.Sbno))
                            .Project<Bulletin>(fields)
                            .ToList();
                return bulletins.OrderBy(sb => sb.Sbno).ToList();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                var fieldsBuilder = Builders<Bulletin>.Projection;
                var fields = fieldsBuilder.Exclude(b => b.TSORequired);
                var bulletins = _bulletins.Aggregate()
                            .Match(f => !f.Deleted && f.Roles.Count()>0 && f.CMM.Count()>0)
                            //.AppendStage<Bulletin>("{$addFields: {convertedDate: {$toDate: '$revDate'}}}")?
                            .AppendStage<Bulletin>("{$addFields: {convertedDate: {$convert: {input: '$revDate', to: 'date', onError:  null}}}}")?
                            .Project<Bulletin>(fields)
                            .ToList();
                return bulletins;
            }
        } 
        public Bulletin Get(string id)
        {
            Bulletin bulletin = null;
            var fieldsBuilder = Builders<Bulletin>.Projection;
            var fields = fieldsBuilder.Exclude(b => b.TSORequired);
            try
            {
                
                var bulletinQuery = _bulletins.Aggregate()
                    .AppendStage<Bulletin>("{$addFields: {" +
                                           "convertedDate: {$convert: {input: '$revDate', to: 'date', onError:  null}}," +
                                           "ConvertId:{$toString:'$_id'} }}")?
                    .Project<Bulletin>(fields)
                    .Match(b => b.ConvertId == id);
                bulletin = bulletinQuery.FirstOrDefault();
            }
            catch (Exception)
            {
                var records = Get();
                bulletin = records.FirstOrDefault(r => r.Id == id);
            }
            return bulletin;
        }

        public Bulletin Add(Bulletin bulletin)
        {
            _bulletins.InsertOne(bulletin);
            return bulletin;
        }
        public List<Bulletin> GetByRole(string role)
        {
            var filterArray = Builders<Bulletin>.Filter.All(b => b.Roles, new[] { role });
            var fieldsBuilder = Builders<Bulletin>.Projection;
            var fields = fieldsBuilder.Exclude(b => b.TSORequired);
            var bulletin = _bulletins.Aggregate()
                .Match(f => !f.Deleted && f.Roles.Count(w => w == role) > 0 && f.CMM.Count() > 0)
                .Project<Bulletin>(fields)
                .ToList();
            return bulletin;
        }
        public void Update(string id, Bulletin bulletin)
        {
            var entity = Get(id);
            _bulletins.ReplaceOne(b => b.Id == id, bulletin);
        }

        public IEnumerable<string> GetRoles()
        {
            
            return new string[] { "1" };
        }

        public void Delete(string id)
        {
            var entity = Get(id);
            entity.Deleted = true;
            Update(id, entity);
        }

        public List<Bulletin> GetWithRole()
        {
            var fieldsBuilder = Builders<Bulletin>.Projection;
            var fields = fieldsBuilder.Exclude(b => b.TSORequired)
                                      .Exclude(b => b.CMM);
            var bulletins = _bulletins.Aggregate()
                        .Match(f => !f.Deleted && f.Roles.Count() > 0)
                        .Project<Bulletin>(fields)
                        .ToList();
            return bulletins.OrderBy(sb => sb.Sbno).ToList();
        }
    }
}