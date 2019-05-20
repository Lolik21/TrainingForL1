using DataGenerator;
using DataGenerator.Sources;
using EmployeeWebApplication.Models;

namespace EmployeeWebApplication.DataGeneration
{
    internal sealed class AddressProfile : MappingProfile<Address>
    {
        public override void Configure()
        {
            Property(address => address.PostalCode).DataSource<PostalCodeSource>();
            Property(address => address.State).DataSource<StateSource>();
            Property(address => address.Street).DataSource<StreetSource>();
        }
    }
}