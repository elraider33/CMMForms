using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Entities
{
    public record Reset(
        [property:BsonElement("token")]
        string Token,
        [property:BsonElement("email")]
        string Email,
        [property:BsonElement("when")]
        [property:BsonRepresentation(BsonType.DateTime)]
        DateTime When
    );
    public record VerificationTokens(
        [property:BsonElement("token")]
        string Token,
        [property:BsonElement("address")]
        string Address,
        [property:BsonElement("when")]
        [property:BsonRepresentation(BsonType.DateTime)]
        DateTime When
    );
    public record LoginTokens(
        [property:BsonElement("hashedToken")]
        string HashedToken,
        [property:BsonElement("when")]
        DateTime When
    );

    public record EmailService(
        [property:BsonElement("verificationTokens")]
        IEnumerable<VerificationTokens> VerificationTokens
    );
    public record ResumeService(
        [property:BsonElement("loginTokens")]
        IEnumerable<LoginTokens> LoginTokens
    );
    public record Password(
        [property:BsonElement("bcrypt")]
        string Bcrypt,
        [property:BsonElement("reset")]
        Reset Reset
    );
    public record Service(
        [property:BsonElement("password")]
        Password Password,
        [property:BsonElement("email")]
        EmailService Email,
        [property:BsonElement("resume")]
        ResumeService Resume
    );
    public record Profile(
        [property:BsonElement("fullname")]
        string FullName,
        [property:BsonElement("firstname")]
        string FirstName,
        [property:BsonElement("lastname")]
        string LastName,
        [property:BsonElement("company")]
        string Company,
        [property:BsonElement("legacy")]
        bool Legacy,
        [property:BsonElement("legacyPwd")]
        bool LegacyPwd,
        [property:BsonElement("jobtitle")]
        string JobTitle,
        [property:BsonElement("phone")]
        string Phone,
        [property:BsonElement("email")]
        string Email
    );
    public record Roles(
        [property:BsonElement("account_status")]
        string AccountStatus,
        [property:BsonElement("customer")]
        IEnumerable<string> Customer,
        [property:BsonElement("__global_roles__")]
        IEnumerable<string> GlobalRoles
    ); 

    public record Emails(
        [property:BsonElement("address")]
        string Address,
        [property:BsonElement("verified")]
        bool Verified);
    public class User
    {
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("username")]
        [BsonRepresentation(BsonType.String)]
        public string Username { get; set; }
        [BsonElement("createdAt")]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedAt { get; set; }
        [BsonElement("services")]
        public Service Services { get; set; }
        [BsonElement("profile")]
        public Profile Profile { get; set; }
        [BsonElement("roles")]
        public Roles Roles { get; set; }
        [BsonElement("emails")]
        public IEnumerable<Emails> Emails { get; set; }
    }
}
