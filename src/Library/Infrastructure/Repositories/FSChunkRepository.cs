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
    public class FSChunkRepository : BaseRepository, IFSChunkRepository
    {
        private readonly IMongoCollection<FSChunk> _fsChunk;
        private readonly ILogger<FSChunkRepository> logger;
        public FSChunkRepository(IMongoOptions options, ILogger<FSChunkRepository> logger) : base(options, logger)
        {
            _fsChunk = _database.GetCollection<FSChunk>(Collections.FSChunk);
            this.logger = logger;
        }

        public List<FSChunk> Get()
        {
            var entities = _fsChunk.Find(e => true).ToList().ToList();
            return entities;
        } 
        public FSChunk Get(string id)
        {
            var FSChunk = _fsChunk.Find(e => e.Id == id).FirstOrDefault();
            return FSChunk;
        }

        public List<FSChunk> GetByFileId(string fileId)
        {
            //var fs = _fsChunk.Find(e => true).ToList();
            logger.LogInformation("[FSChunkRepository] start getting the chunks");
            var entities = _fsChunk.Find(e => e.FilesId == fileId).ToList().ToList();
            logger.LogInformation("[FSChunkRepository] end getting the chunks");
            return entities;
        }
    }
}