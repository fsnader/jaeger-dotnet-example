using System.Collections.Generic;
using System.Threading.Tasks;
using CustomersApi.Domain;
using CustomersApi.Repositories;
using CustomersApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomersApi.Controllers
{
    [Route("api/customers")]
    public class CustomersController : Controller
    {
        private readonly ICustomersService _customersService;

        public CustomersController(ICustomersService customersService)
        {
            _customersService = customersService;
        }
        
        [HttpGet]  
        public async Task<IActionResult> GetAsync()
        {
            var data = await _customersService.GetList();
            return Ok(data);
        }
        
        [HttpGet("{id}")] 
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var data = await _customersService.GetById(id);
            return Ok(data);
        }
        
        [HttpPost]  
        public async Task<IActionResult> PostAsync([FromBody]Customer customer)
        {
            await _customersService.Add(customer);
            return Ok();
        } 
    }
}