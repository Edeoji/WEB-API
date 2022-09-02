using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIApplication.Models;
using WebAPIApplication.ViewModels;

namespace WebAPIApplication.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        [HttpPost]
        [Route("Login")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        [HttpPost]
        [Route("Register")]
        public IActionResult CreateEmployee(EmployeeViewModel employeeViewModel)
        {
            try
            {
                if (ModelState != null)
                {
                    var employee = new Employee()
                    {
                        FirstName = employeeViewModel.FirstName,
                        LastName = employeeViewModel.LastName,
                        Email = employeeViewModel.Email,
                        DateOfBirth = employeeViewModel.DateOfBirth,
                    };
                    _context.Employees.Add(employee);
                    _context.SaveChanges();
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet]
        [Route("Edit")]
        public IActionResult Edit(int id)
        {

            var GetUserById = _context.Employees.Where(x => x.Id == id).FirstOrDefault();
            if (GetUserById != null)
            {
                return Ok(GetUserById);
            }
            return BadRequest(GetUserById);
        }

        [HttpPost]
        [Route("Edit")]
        public IActionResult Edit([FromForm]Employee employee)
        {
            try
            {
                _context.Update(employee);
                _context.SaveChanges();
                return Ok(employee);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("Delete")]
        public IActionResult Delete(int id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var deleteEmployee = _context.Employees.Find(id);
            if(deleteEmployee == null)
            {
                return NotFound();
            }
            return Ok(deleteEmployee);
        }
        [HttpPost]
        [Route("Delete")]
        public IActionResult Delete(int? employeeId)
        {
            try
            {
                var deleteId = _context.Employees.Find(employeeId);
                if(deleteId != null)
                {
                    _context.Employees.Remove(deleteId);
                    _context.SaveChanges();
                    return Ok(deleteId);
                }
                return BadRequest(deleteId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        // PUT api/<AccountController>/5
        [HttpGet]
        [Route("Put")]
        public IActionResult Put(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var updateEmployee = _context.Employees.Find(id);
            if(updateEmployee == null)
            {
                return NotFound();
            }
            return Ok();
        }
        [HttpPost]
        [Route("Put")]
        public IActionResult Put(Employee id)
        {
            try
            {
                    _context.Employees.Update(id);
                    _context.SaveChanges();
                    return Ok();
                
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
