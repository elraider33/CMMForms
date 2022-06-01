using System;
using System.IO;
using System.Threading.Tasks;
using Library.Domain.Entities;
using Library.Domain.Options;
using Library.Domain.Repositories;
using Library.Infrastructure.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS; 

namespace Library.Infrastructure.Repositories
{
    public class FormFileRepository : BaseRepository, IFileRepository
    {
        private readonly GridFSBucket _bucket;
        private readonly ILogger<FormFileRepository> logger;
        public FormFileRepository(IMongoOptions options, ILogger<FormFileRepository> logger) : base(options, logger)
        {
            _bucket = new GridFSBucket(_database, new GridFSBucketOptions { 
                ChunkSizeBytes = 261120,
            });
            this.logger = logger;
        }

        public async Task<string> AddFile(IFormFile formFile)
        {
            var stream = await StreamHelper.FileToStream(formFile);
            var upload = await _bucket.UploadFromBytesAsync(formFile.FileName, stream.ToArray(), new GridFSUploadOptions()
            {
                ChunkSizeBytes = 261120,
            });
            stream.Close();
            return upload.ToString();
        }

        public void DeleteFile(string id)
        {
            try
            {

                  _bucket.Delete(ObjectId.Parse(id));

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                logger.LogError(ex.StackTrace);
                throw;
            }
        }

        public Domain.Entities.File GetFile(string Id)
        {
            // _database.GridFs
            logger.LogInformation("staring to get the file info");
            logger.LogInformation(Id);
            var filter = Builders<GridFSFileInfo>.Filter.Eq("_id",ObjectId.Parse( Id));
            var file = _bucket.Find(filter).FirstOrDefault();
            logger.LogInformation("finish to get the file info");
            logger.LogInformation(file.Id.ToString());
            logger.LogInformation(file.Filename);
            var ffile = new Domain.Entities.File()
            {
                Filename = file.Filename,
                Size = file.Length,
                FilesId = file.Id.ToString()
            };
            
            return ffile;
        }

        public async Task<MemoryStream> DownloadStreamFileById(string id)
        {
            try
            {
                logger.LogInformation("staring to download the file");
                var objectId = ObjectId.Parse(id);
                logger.LogInformation("parsing the object id");
                logger.LogInformation(objectId.ToString());
                logger.LogInformation("getting the download stream");
                await using var streamTask = await _bucket.OpenDownloadStreamAsync(objectId);
                await using var ms = new MemoryStream();
                logger.LogInformation("getting the download stream complete");
                logger.LogInformation(streamTask.FileInfo.Filename);
                streamTask.CopyTo(ms);
                return ms;
            }
            catch (System.Exception e)
            {
                logger.LogInformation("there was an error trying to get the dowloaded file");
                logger.LogError(e.Message);
                return null;
            }
        }
        public async Task<GridFSDownloadStream> DownloadStreamFileByIdAsync(string id)
        {
            try
            {
                logger.LogInformation("staring to download the file");
                var objectId = ObjectId.Parse(id);
                logger.LogInformation("parsing the object id");
                logger.LogInformation(objectId.ToString());
                logger.LogInformation("getting the download stream");
                var streamTask = await _bucket.OpenDownloadStreamAsync(objectId);
                logger.LogInformation("getting the download stream complete");
                logger.LogInformation(streamTask.FileInfo.Filename);
                return streamTask;


            }
            catch (System.Exception e)
            {
                logger.LogInformation("there was an error trying to get the dowloaded file");
                logger.LogError(e.Message);
                return null;
            }
        }
        
        public async Task<byte[]> DownloadFileById(string id)
        {
            try
            {
                logger.LogInformation("staring to download the file");
                var objectId = ObjectId.Parse(id);
                logger.LogInformation("parsing the object id");
                logger.LogInformation(objectId.ToString());
                logger.LogInformation("getting from the bucket with task");
                var foo = _bucket.DownloadAsBytesAsync(objectId);
                logger.LogInformation("getting from the bucket with task wait for it");
                Task.WaitAll(foo);
                logger.LogInformation("getting from the bucket with task wait for it...");
                if (foo.IsCompleted)
                    return foo.Result;
                //_bucket.op
                logger.LogInformation("getting from the bucket");
                var b = await _bucket.DownloadAsBytesAsync(objectId,cancellationToken: new System.Threading.CancellationToken());
                //var bytes = _bucket.DownloadAsBytes(BsonValue.Create(id));
                logger.LogInformation("finishing to download the file");
                return b;
            }
            catch (System.Exception e)
            {
                logger.LogInformation("there was an error trying to get the dowloaded file");
                logger.LogError(e.Message);
                return null;
            }
        }

        public async Task<byte[]> DownloadFileByName(string fileName)
        {
            logger.LogInformation("staring to download the file by name");
            logger.LogInformation("getting from the bucket");
            logger.LogInformation(fileName);
            var b = await _bucket.DownloadAsBytesByNameAsync(fileName);
            logger.LogInformation("finishing to download the file");
            //var bytes = _bucket.DownloadAsBytes(BsonValue.Create(id));
            return b;
        }
    }
}