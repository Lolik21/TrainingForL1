using System;
using System.Collections.Generic;

namespace EmployeeWebApplication.Models
{
    public sealed class Employee
    {
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