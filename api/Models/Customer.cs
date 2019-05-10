using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace api.Models
{
    public class Customer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("social_name")]
        public string SocialName { get; set; }

        [BsonElement("fantasy_name")]
        public string FantasyName { get; set; } 

        [BsonElement("email")]
        public string Email { get; set; } 

        [BsonElement("cnpj")]
        public string Cnpj { get; set; } 

        [BsonElement("phone_number")]
        public string PhoneNumber { get; set; } 
        
        [BsonElement("address")]
        public string Address { get; set; } 
    }
}