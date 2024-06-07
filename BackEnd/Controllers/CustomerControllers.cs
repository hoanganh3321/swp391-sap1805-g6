using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using swp391_sap1805_g6.Entities;
using swp391_sap1805_g6.Reporitories;

namespace swp391_sap1805_g6.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class CustomerControllers : ControllerBase
    {
        //list cus
        [Route("/Customers")]
        [HttpGet]
        public IActionResult GetCustomers()
        {
            return Ok(CustomerRepo.GetAllCustomers());
        }
        //search cus by cusid
        [Route("/Customers/{id}")]
        [HttpGet]
        public IActionResult GetCusById(int id)
        {
            return Ok(CustomerRepo.GetCusById(id));
        }
        //add cus
        [Route("/Register")]
        [HttpPost]
        public IActionResult AddCustomer([FromBody] Customer customer)
        {
            CustomerRepo.Add(customer);

            return CreatedAtAction(nameof(GetCusById), new { id = customer.CustomerId }, customer);
        }
    }
}
