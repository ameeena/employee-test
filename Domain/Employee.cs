using MongoDB.Bson.Serialization.Attributes;

namespace Domain
{
    // Employee Domain Entities
    public class Employee
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Address { get; set; }
        public long PhoneNumber { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
