using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using miniprojectE.Data;
using miniprojectE.DTO.CustomerDTOs;
using miniprojectE.Models.Entities;

namespace miniprojectE.Controllers
{
    /**[Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly AppDB appDB;

        public CustomerController(AppDB appDB)
        {
            this.appDB = appDB;
        }

        //retrieve
        [HttpGet]
        public IActionResult getAllCustomers()
        {
            return Ok(appDB.User.ToList());
        }

        //create
        [HttpPost]
        public IActionResult addCustomer(AddCustomerDTO addCustomerDTO)
        {
            var cust = new User()
            {
                Name = addCustomerDTO.Name,
                Email = addCustomerDTO.Email,
                Phone = addCustomerDTO.Phone,
                Address = addCustomerDTO.Address,
                RegistrationDate = addCustomerDTO.RegistrationDate,
                Role = addCustomerDTO.Role,
            };

            appDB.User.Add(cust);
            appDB.SaveChanges();

            return Ok(cust);
        }

        //retrieve
        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult getCustomer(Guid id)
        {
            var customer = appDB.User.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        //update
        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult updateCustomer(Guid id, UpdateCustomerDTO updateCustomerDTO)
        {
            var customer = appDB.User.Find(id);

            if (customer == null)
            {
                return NotFound();
            }

            customer.Name = updateCustomerDTO.Name;
            customer.Email = updateCustomerDTO.Email;
            customer.Phone = updateCustomerDTO.Phone;
            customer.Address = updateCustomerDTO.Address;

            appDB.SaveChanges();
            return Ok(customer);
        }

        //delete
        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult deleteCustomer(Guid id)
        {
            var customer = appDB.User.Find(id);

            if (customer == null)
            {
                return NotFound();
            }

            appDB.User.Remove(customer);
            appDB.SaveChanges();
            return Ok();
        }
    }*/
}
