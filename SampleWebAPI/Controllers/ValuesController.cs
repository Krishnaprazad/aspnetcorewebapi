using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace SampleWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class EmpDetailController : Controller
    {
        private AppDbContext _dbContext;

        public EmpDetailController(AppDbContext dbContext)
        {
            this._dbContext = dbContext;

            if (_dbContext.Employees.Count() == 0)
            {
                _dbContext.Employees.Add(new Employee { Name="Krishna", Age=25});
                _dbContext.SaveChanges();
            }
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Employee> GetAll()
        {
           return  _dbContext.Employees.AsNoTracking().ToList();
        }

        // GET api/values/5
        [HttpGet("{id}",Name ="empinfo")]
        public IActionResult GetEmployeeById(int id)
        {
            var emp = _dbContext.Employees.FirstOrDefault(x=>x.Id == id);

            return new ObjectResult(emp);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Create([FromBody]Employee employee)
        {
            _dbContext.Employees.Add(employee);
            _dbContext.SaveChanges();
            return CreatedAtRoute("empinfo", new { id = employee.Id }, employee);
     
            
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Employee employee)
        {
            var emp = _dbContext.Employees.Find(id);
            if (emp == null)
            {
                return NotFound();
            }
            emp.Id = employee.Id;
            emp.Name = employee.Name;
            emp.Age = employee.Age;

            _dbContext.Update(emp);
            _dbContext.SaveChanges();
            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var emp = _dbContext.Employees.Find(id);
            if (emp == null)
            {
                return NotFound();
            }
            _dbContext.Employees.Remove(emp);
            _dbContext.SaveChanges();
            return NoContent();
        }
    }
}
