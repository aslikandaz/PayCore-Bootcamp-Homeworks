using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace PayCore_H2.Controllers
{
    // class defined to crud operations
    public class Staff
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public double Salary { get; set; }
    }



    [Route("api/[controller]")]
    [ApiController]
    public class StaffsController : ControllerBase
    {

        private List<Staff> list;

        //constructor that populates static list
        public StaffsController()
        {
            list = new List<Staff>();
            list.Add(new Staff
            {
                Id = 1,
                Name = "Deny",
                LastName = "Sellen",
                DateOfBirth = new DateTime(1989, 01, 01),
                Email = "deny@gmail.com",
                PhoneNumber = "+905554443366",
                Salary = 4450
            });
            list.Add(new Staff
            {
                Id = 2,
                Name = "Jerry",
                LastName = "Rice",
                DateOfBirth = new DateTime(1949, 12, 12),
                Email = "jery@gmail.com",
                PhoneNumber = "+905554443322",
                Salary = 3000
            });
        }
      
        //listing all staff object
        [HttpGet]
        public List<Staff> GetStaff()
        {
            var staffList = list.OrderBy(x => x.Id).ToList<Staff>(); //sorting object by id
            return staffList;
        }


        [HttpGet("{id}")]
        public Staff GetById(int id)
        {
            var staff = list.Where(staff => staff.Id == id).SingleOrDefault();
            return staff;
        }

        [HttpPost]
        public IActionResult AddStaff([FromBody] Staff newStaff)
        {
            var staff = list.SingleOrDefault(x => x.Id == newStaff.Id); // checking if there are any objects in the list that match this Id

            if(staff is not null) // no insertion if object matching id
                return BadRequest();
            try
            {
                StaffValidator validator = new StaffValidator();
                validator.ValidateAndThrow(newStaff); //checking the validation rules we wrote
            }

            catch(Exception ex)
            {
                return BadRequest(ex.Message); // returning the errors to the user
            }
            
            list.Add(newStaff);
            return Ok();
            

        }

        [HttpPut("{id}")]
        public IActionResult UpdateStaff(int id, [FromBody] Staff updatedStaff)
        {
            var staff = list.SingleOrDefault(x => x.Id == id); 
            if (staff is null)
                return BadRequest();
            try
            {
                StaffValidator validator = new StaffValidator();
                validator.ValidateAndThrow(updatedStaff);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            staff.Id = updatedStaff.Id != default ? updatedStaff.Id : staff.Id; // ıt is checked whether a new value is entered from the body and the property is updated
            staff.Name = updatedStaff.Name != default ? updatedStaff.Name : staff.Name;
            staff.LastName = updatedStaff.LastName != default ? updatedStaff.LastName : staff.LastName;
            staff.DateOfBirth = updatedStaff.DateOfBirth != default ? updatedStaff.DateOfBirth : staff.DateOfBirth;
            staff.Email = updatedStaff.Email != default ? updatedStaff.Email : staff.Email;
            staff.PhoneNumber = updatedStaff.PhoneNumber != default ? updatedStaff.PhoneNumber : staff.PhoneNumber;
            staff.Salary = updatedStaff.Salary != default ? updatedStaff.Salary : staff.Salary;

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStaff(int id)
        {
            var staff = list.SingleOrDefault(x => x.Id == id);
            if (staff is null)
                return BadRequest();


            list.Remove(staff);
            return Ok();
        }
    }
}
