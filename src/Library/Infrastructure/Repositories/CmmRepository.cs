using System;
using System.Collections.Generic;
using System.Linq;
using Library.Domain.Common;
using Library.Domain.Entities;
using Library.Domain.Options;
using Library.Domain.Repositories;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Library.Infrastructure.Repositories
{
    public class CMMRepository : BaseRepository, ICMMRepository
    {
        private readonly IMongoCollection<CMM> _manuals;
        private readonly ILogger<CMMRepository> logger;
        public CMMRepository(IMongoOptions options, ILogger<CMMRepository> logger) : base(options, logger)
        {
            this.logger = logger;
            _manuals = _database.GetCollection<CMM>(Collections.CMM);
        }

        public List<CMM> Get()
        {
            var fieldsBuilder = Builders<CMM>.Projection;
            var fields = fieldsBuilder.Exclude(b => b.RevDate)
                .Exclude(c => c.SeatPartNumbersTSO);
            var bulletins = _manuals.Aggregate()
                        .Match(c => !c.Deleted)
                        .AppendStage<CMM>("{$addFields: {convertedDate: {$convert: {input: '$revDate', to: 'date', onError:  null}}}}")?
                        .Project<CMM>(fields)
                        .ToEnumerable()
                        .OrderBy(cmm => cmm.CMMNumber)
                        .ToList();
            return bulletins;
        } 
        public CMM Get(string id)
        {
            var fieldsBuilder = Builders<CMM>.Projection;
            var fields = fieldsBuilder.Exclude(b => b.RevDate);
            var bulletin = _manuals.Aggregate()
                .AppendStage<CMM>("{$addFields: {" +
                                  "convertedDate: {$convert: {input: '$revDate', to: 'date', onError:  null}}," +
                                  "ConvertId:{$toString:'$_id'} }}")?
                .Project<CMM>(fields)
                .Match(b => b.ConvertId == id)
                ;
            return bulletin.FirstOrDefault();
        }

        public List<CMM> GetByRole(string role)
        {
            var filterArray = Builders<CMM>.Filter.All(b => b.Roles, new[] { role });
            var fieldsBuilder = Builders<CMM>.Projection;
            var fields = fieldsBuilder.Exclude(b => b.RevDate);
            var bulletin = _manuals.Aggregate()
                .Match(c => !c.Deleted && c.Roles.Any(w => w == role)  && c.SeatPartNumbersTSO.Any())
                .AppendStage<CMM>("{$addFields: {convertedDate: {$convert: {input: '$revDate', to: 'date', onError:  new Date(), onNull: new Date()}}}}")?
                .Project<CMM>(fields)
                .ToList();
            return bulletin;
        }

        public CMM Add(CMM manual)
        {
            _manuals.InsertOne(manual);
            return manual;
        }

        public void Update(string id, CMM manual)
        {
            var entity = Get(id);
            _manuals.ReplaceOne(b => b.Id == id, manual);
        }

        public IEnumerable<string> GetRoles()
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            var entity = Get(id);
            entity.Deleted = true;
            Update(id, entity);
        }
    }
}