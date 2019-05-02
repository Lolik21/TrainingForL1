using DataGenerator;
using DataGenerator.Sources;
using MongoDB.Models;

namespace MongoDB.DataGeneration
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