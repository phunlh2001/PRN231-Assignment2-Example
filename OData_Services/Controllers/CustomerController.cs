using BusinessObject.Model;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace OData_Services.Controllers
{
    [Route("odata/customers")]
    public class CustomerController : ODataController
    {
        private readonly ICustomerRepository _repo;
        public CustomerController(ICustomerRepository repo) => _repo = repo;

        /**
         * [GET]
         * Get all customers
        */
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> Get()
        {
            var list = _repo.GetAll();
            return Ok(list);
        }

        /**
         * [GET]
         * Get customer by Id
        */
        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            var customer = _repo.GetById(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        /**
         * [POST]
         * Create a customer
        */
        [HttpPost("add")]
        public IActionResult Post([FromBody] Customer customer)
        {
            if (customer == null)
            {
                return BadRequest("Customer info cannot be empty.");
            }

            if (ModelState.IsValid)
            {
                _repo.AddCustomer(customer!);
            }

            return Created("customers", customer);
        }

        /**
         * [PUT]
         * Update all customer info
        */
        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] int id, [FromBody] Customer updatedCustomer)
        {
            var customer = _repo.GetById(id);

            if (customer == null)
            {
                return NotFound($"id {id} not exists.");
            }

            _repo.UpdateCustomer(updatedCustomer);

            return Ok(updatedCustomer);
        }

        /**
         * [PATCH]
         * Update a partial customer info
        */
        [HttpPatch("{id}")]
        public IActionResult Patch([FromRoute] int id, [FromBody] Delta<Customer> delta)
        {
            var customer = _repo.GetById(id);

            if (customer == null)
            {
                return NotFound();
            }

            delta.Patch(customer);

            _repo.UpdateCustomer(customer);

            return Ok(customer);
        }

        /**
         * [DELETE]
         * Delete a customer by Id
        */
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var customer = _repo.GetById(id);

            if (customer == null)
            {
                return NotFound($"id {id} not exists.");
            }

            _repo.DeleteCustomer(id);
            return Ok();
        }
    }
}
