// using System;
using System.Collections.Generic;
using api.Models;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    // [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerService customerService;

        public CustomersController(CustomerService service)
        {
            customerService = service;
        }

        [HttpGet]
        public ActionResult<List<Customer>> Get()
        {
            return  customerService.Get();
        }

        [HttpGet("{id:length(24)}", Name = "getCustomer")]
        public ActionResult<Customer> Get(string id)
        {
            var customer = customerService.Get(id);

            if (customer == null)
                return NotFound();

            return customer;
        }

        [HttpPost]
        public ActionResult<Customer> Create(Customer customer)
        {
            customerService.Create(customer);
            return CreatedAtRoute("GetCustomer", new { id = customer.Id.ToString() }, customer);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Customer customerDataToUpdate)
        {
            var customer = customerService.Get(id);

            if (customer == null)
                return NotFound();

            customerService.Update(id, customerDataToUpdate);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var customer = customerService.Get(id);

            if (customer == null)
                return NotFound();

            customerService.Remove(customer.Id);

            return NoContent();
        }


    }
}