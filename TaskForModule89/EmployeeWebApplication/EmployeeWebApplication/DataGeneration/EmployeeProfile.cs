using DataGenerator;
using DataGenerator.Sources;
using EmployeeWebApplication.Models;

namespace EmployeeWebApplication.DataGeneration
{
    internal sealed class EmployeeProfile : MappingProfile<Employee>
    {
        public override void Configure()
        {
            Property(employee => employee.Birthday).DataSource<DateTimeSource>();
            Property(employee => employee.Name).DataSource<FirstNameSource>();
            Property(employee => employee.SurName).DataSource<LastNameSource>();
            Property(employee => employee.Email).DataSource<EmailSource>();
            Property(employee => employee.PhoneNumber).DataSource<PhoneSource>();
            Property(employee => employee.Addresses).Value(Generator.Default.List<Address>(2));
            Property(employee => employee.AccountInfo).Value(Generator.Default.Single<AccountInfo>());
        }
    }
}