using System.Collections.Generic;
using System.Linq;
using DataGenerator;
using EmployeeWebApplication.DataAccess;
using EmployeeWebApplication.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmployeeWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class EmployeeController : Controller
    {
        private readonly ApplicationDataContext _dataContext;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(ILogger<EmployeeController> logger, ApplicationDataContext dataContext)
        {
            _dataContext = dataContext;
            _logger = logger;
            dataContext.Employees.AddRange(Generator.Default.List<Employee>(10));
            dataContext.SaveChanges();
        }

        [EnableQuery]
        [HttpGet]
        public ActionResult<IEnumerable<Employee>> Get()
        {
            _logger.LogInformation("Getting all data");
            var result = _dataContext.Employees;
            return Ok(result);
        }

        [EnableQuery]
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<Employee> Get(string id)
        {
            _logger.LogInformation($"Getting data by id: {id}");
            var result = _dataContext.Employees.FirstOrDefault(employee => employee.Id == id);
            return Ok(result);
        }
    }
}
