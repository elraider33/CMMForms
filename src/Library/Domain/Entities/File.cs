using MongoDB.Bson.Serialization.Attributes;

namespace Library.Domain.Entities
{
    public class File
    {
        [BsonElement("filename")]
        public string Filename { get; set; }

        [BsonElement("files_id")]
        public string FilesId { get; set; }

        [BsonElement("size")]
        public double Size { get; set; }
    }
}