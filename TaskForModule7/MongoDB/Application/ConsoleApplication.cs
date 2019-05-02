using System;
using DataGenerator;
using MongoDB.Models;
using Newtonsoft.Json;

namespace MongoDB
{
    internal sealed class ConsoleApplication : IApplication
    {
        private readonly ILogger _logger;
        private readonly IDataProvider<Employee> _dataProvider;

        public ConsoleApplication(ILogger logger, IDataProvider<Employee> dataProvider)
        {
            _logger = logger;
            _dataProvider = dataProvider;
        }

        public void Start()
        {
            bool stopInput = false;
            while (!stopInput)
            {
                try
                {
                    Console.WriteLine("Select operation:");
                    Console.WriteLine("1) Get all employees");
                    Console.WriteLine("2) Get employee by id");
                    Console.WriteLine("3) Create employee with random data");
                    Console.WriteLine("4) Update employee by id with random data");
                    Console.WriteLine("5) Delete employee by id");
                    Console.WriteLine("q) Quit");
                    string input = Console.ReadLine();
                    _logger.Log($"Entered input parameter: {input}");
                    ProcessInput(input, ref stopInput);
                }
                catch (Exception e)
                {
                    _logger.Log(e.Message);
                }
            }
        }

        private void ProcessInput(string input, ref bool stopProcessing)
        {
            if (input.ToUpperInvariant() == "Q")
            {
                stopProcessing = true;
                _logger.Log($"Quit parameter determined: {input}");
                return;
            }

            int value = int.Parse(input);
            ProcessInputInternal(value);
        }

        private void ProcessInputInternal(int input)
        {
            switch (input)
            {
                case 1:
                    var employees = _dataProvider.GetAll();
                    Console.WriteLine(JsonConvert.SerializeObject(employees));
                    break;
                case 2:
                    var getId = GetId();
                    var getEmployee = _dataProvider.GetItem(getId);
                    Console.WriteLine(JsonConvert.SerializeObject(getEmployee));
                    break;
                case 3:
                    var employee = Generator.Default.Single<Employee>();
                    _dataProvider.Create(employee);
                    break;
                case 4:
                    var updateId = GetId();
                    var updateEmployee = Generator.Default.Single<Employee>();
                    updateEmployee.Id = updateId;
                    _dataProvider.Update(updateEmployee);
                    break;
                case 5:
                    var deleteId = GetId();
                    _dataProvider.Delete(deleteId);
                    break;
            }
        }

        private string GetId()
        {
            Console.WriteLine("Enter Id:");
            var id = Console.ReadLine();
            return id;
        }
     }
}