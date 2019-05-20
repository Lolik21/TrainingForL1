using DataGenerator;
using DataGenerator.Sources;
using EmployeeWebApplication.Models;

namespace EmployeeWebApplication.DataGeneration
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