using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Library.Domain.Entities
{
    public class CMM
    {
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("requestedBy")]
        [BsonRepresentation(BsonType.String)]
        public string RequestedBy { get; set; }
        [BsonElement("entity")]
        [BsonRepresentation(BsonType.String)]
        public string Entity { get; set; }
        [BsonElement("published")]
        [BsonRepresentation(BsonType.Boolean)]
        public bool Published { get; set; }
        [BsonElement("_FILE")]
        public File FILE { get; set; }
        [BsonElement("customer")]
        [BsonRepresentation(BsonType.String)]
        public string Customer { get; set; }
        [BsonElement("manual")]
        [BsonRepresentation(BsonType.String)]
        public string CMMNumber { get; set; }
        [BsonElement("initialDate")]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime? InitialDate { get; set; }
        [BsonElement("model")]
        [BsonRepresentation(BsonType.String)]
        public string Model { get; set; }
        [BsonElement("manualRev")]
        [BsonRepresentation(BsonType.String)]
        public string ManualRev { get; set; }
        [BsonElement("revDate")]
        public DateTime? RevDate { get; set; }
        [BsonElement("aircraft")]
        [BsonRepresentation(BsonType.String)]
        public string Aircraft { get; set; }
        [BsonElement("jobNo")]
        [BsonRepresentation(BsonType.String)]
        public string JobNo { get; set; }
        [BsonElement("vendor")]
        [BsonRepresentation(BsonType.String)]
        public string vendor { get; set; }
        [BsonElement("engineer")]
        [BsonRepresentation(BsonType.String)]
        public string Engineer { get; set; }
        [BsonElement("rfq")]
        [BsonRepresentation(BsonType.String)]
        public string RFQ { get; set; }
        [BsonElement("pm")]
        [BsonRepresentation(BsonType.String)]
        public string PM { get; set; }
        [BsonElement("incorporatedSeatAssemblies")]
        public IEnumerable<string> IncorporatedSeatAssemblies { get; set; }
        [BsonElement("serviceBulletins")]
        public IEnumerable<string> ServiceBulletins { get; set; }
        [BsonElement("recerence")]
        public IEnumerable<string> Reference { get; set; }
        [BsonElement("aircraftInstallation")]
        [BsonRepresentation(BsonType.String)]
        public string AircraftInstallation { get; set; }
        [BsonElement("config")]
        [BsonRepresentation(BsonType.String)]
        public string Config { get; set; }
        [BsonElement("trimFinish")]
        [BsonRepresentation(BsonType.String)]
        public string TrimFinish { get; set; }
        [BsonElement("comments")]
        [BsonRepresentation(BsonType.String)]
        public string Comments { get; set; }
        [BsonElement("documentAvailable")]
        [BsonRepresentation(BsonType.String)]
        public string DocumentAvailable { get; set; }
        [BsonElement("companycode")]
        public IEnumerable<string> Roles{ get; set; }
        [BsonElement("seatPartNumbers")]
        public IEnumerable<string> SeatPartNumbers { get; set; }
        [BsonElement("unid")]
        [BsonRepresentation(BsonType.String)]
        public string UnId { get; set; }
        [BsonElement("convertedDate")]
        [BsonRepresentation(BsonType.DateTime)]
        [BsonIgnoreIfNull]
        public DateTime? ConvertedDate { get; set; }
        [BsonRepresentation(BsonType.String)]
        [BsonIgnoreIfNull]
        public string ConvertId { get; set; }
        [BsonElement("seatPartNumbersTSO")]
        public IEnumerable<string> SeatPartNumbersTSO { get; set; }
        [BsonElement("seatsTSO")]
        public IEnumerable<string> SeatsTSO { get; set; }
        [BsonElement("deleted")]
        [BsonRepresentation(BsonType.Boolean)]
        [BsonDefaultValue(false)]
        public bool Deleted { get; set; }
    }
}