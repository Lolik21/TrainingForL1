using System;
using System.Collections;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDB.Models
{
    internal sealed class Employee
    {
        [BsonId]
        public string Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public DateTime Birthday { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public AccountInfo AccountInfo { get; set; }
        public IEnumerable<Address> Addresses { get; set; }
    }
}