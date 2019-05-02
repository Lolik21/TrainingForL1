using DataGenerator;
using DataGenerator.Sources;
using MongoDB.Models;

namespace MongoDB.DataGeneration
{
    internal sealed class AccountInfoProfile : MappingProfile<AccountInfo>
    {
        public override void Configure()
        {
            Property(info => info.AccountNumber).DataSource<IdentifierSource>();
            Property(info => info.MoneyAmount).DataSource<MoneySource>();
        }
    }
}