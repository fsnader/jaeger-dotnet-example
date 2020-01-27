using System.Threading.Tasks;
using AddressesApi.Domain;
using Microsoft.AspNetCore.Mvc;

namespace AddressesApi.Controllers
{
    [Route("api/addresses")]
    public class AddressesController : Controller
    {
        [HttpGet("{zipCode}")]  
        public async Task<IActionResult> GetAsync(string zipCode)
        {
            return Ok(new Address
            {
                ZipCode = zipCode,
                Street = "Sergipe Street",
                Number = 1440,
                City = "Belo Horizonte",
                Country = "Brazil"
            });
        }
    }
}