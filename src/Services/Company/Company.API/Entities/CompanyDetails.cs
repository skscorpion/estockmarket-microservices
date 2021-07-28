using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Company.API.Entities
{
    public class CompanyDetails
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Code { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        public string CEO { get; set; }
        public string TurnOver { get; set; }
        public string Website { get; set; }
        public string StockExchange { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
