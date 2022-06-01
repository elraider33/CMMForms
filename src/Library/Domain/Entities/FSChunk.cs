using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Entities
{
    public class FSChunk
    {
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("files_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string FilesId { get; set; }
        [BsonElement("n")]
        [BsonRepresentation(BsonType.Int32)]
        public int N { get; set; }
        [BsonElement("data")]
        [BsonRepresentation(BsonType.Binary)]
        public byte[] Data { get; set; }
    }
}
