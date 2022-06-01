using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Library.Domain.Entities
{
    public class Role
    {
        [BsonElement("_id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("name")]
        [BsonRepresentation(BsonType.String)]
        public string Name { get; set; }
        [BsonElement("description")]
        [BsonRepresentation(BsonType.String)]
        public string? Description { get; set; }
        [BsonElement("usernames")]
        public List<string>? Usernames { get; set; }
        [BsonElement("emailAddresses")]
        public List<string>? EmailAddresses { get; set; }
    }
}
