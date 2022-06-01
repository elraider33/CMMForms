using System;
using System.Collections.Generic;
using Library.Domain.Common.Serializers;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Library.Domain.Entities
{
    public class Bulletin
    {
        [BsonElement("_id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("requestedBy")]
        [BsonRepresentation(BsonType.String)]
        public string RequestedBy { get; set; }
        [BsonElement("requestedOn")]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime RequestedOn { get; set; }
        [BsonElement("tsoRequired")]
        [BsonRepresentation(BsonType.Boolean)]
        public bool TSORequired { get; set; }
        [BsonElement("entity")]
        [BsonRepresentation(BsonType.String)]
        public string Entity { get; set; }
        [BsonElement("published")]
        [BsonRepresentation(BsonType.Boolean)]
        public bool Published { get; set; }
        [BsonElement("_FILE")]
        public File FILE { get; set; }
        [BsonElement("sbno")]
        [BsonRepresentation(BsonType.String)]
        public string Sbno { get; set; }
        [BsonElement("type")]
        [BsonRepresentation(BsonType.String)]
        public string Type { get; set; }
        [BsonElement("modelNumber")]
        [BsonRepresentation(BsonType.String)]
        public string ModelNumber { get; set; }
        [BsonElement("initialDate")]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime? InitialDate { get; set; }
        [BsonElement("customer")]
        [BsonRepresentation(BsonType.String)]
        public string Customer { get; set; }
        [BsonElement("manualRev")]
        [BsonRepresentation(BsonType.String)]
        public string ManualRev { get; set; }
        [BsonElement("revDate")]
        public DateTime? RevDate { get; set; }
        [BsonElement("description")]
        [BsonRepresentation(BsonType.String)]
        public string Description { get; set; }
        [BsonElement("model")]
        [BsonRepresentation(BsonType.String)]
        public string Model { get; set; }
        [BsonElement("aircraft")]
        [BsonRepresentation(BsonType.String)]
        public string Aircraft { get; set; }
        [BsonElement("unid")]
        [BsonRepresentation(BsonType.String)]
        public string Unid { get; set; }
        [BsonElement("manual")]
        [BsonRepresentation(BsonType.String)]
        public string Manual { get; set; }
        [BsonElement("cmmStatus")]
        [BsonRepresentation(BsonType.String)]
        public string CMMStatus { get; set; }
        // Cambiar por CMM Entity
        [BsonElement("cmm")]
        public List<string> CMM { get; set; }
        [BsonElement("jobNumber")]
        [BsonRepresentation(BsonType.String)]
        public string JobNumber { get; set; }
        [BsonElement("repairStationNumber")]
        [BsonRepresentation(BsonType.String)]
        public string RepairStationNumber { get; set; }
        [BsonElement("requiredCompletionDate")]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime RequiredCompletionDate { get; set; }
        [BsonElement("writer")]
        [BsonRepresentation(BsonType.String)]
        public string Writer { get; set; }
        [BsonElement("seatPartNumbers")]
        public List<string> SeatPartNumbers { get; set; }
        [BsonElement("onDisk")]
        [BsonRepresentation(BsonType.Boolean)]
        public bool OnDisk { get; set; }
        [BsonElement("comments")]
        [BsonRepresentation(BsonType.String)]
        public string Comments { get; set; }
        [BsonElement("convertedDate")]
        [BsonRepresentation(BsonType.DateTime)]
        [BsonIgnoreIfNull]
        public DateTime? ConvertedDate { get; set; }
        [BsonRepresentation(BsonType.String)]
        [BsonIgnoreIfNull]
        public string ConvertId { get; set; }
        [BsonElement("companycode")]
        //[BsonSerializer(typeof(StringOrArraySerializer))]
        public List<string> Roles { get; set; }
        [BsonElement("view")]
        [BsonRepresentation(BsonType.Boolean)]
        public bool View { get; set; }
        [BsonElement("deleted")]
        [BsonRepresentation(BsonType.Boolean)]
        [BsonDefaultValue(false)]
        public bool Deleted { get; set; }

    }
}