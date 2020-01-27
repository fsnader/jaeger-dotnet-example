using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CustomersApi.Domain;
using CustomersApi.Repositories;
using Newtonsoft.Json;
using OpenTracing;

namespace CustomersApi.Services
{
    public class CustomersService : ICustomersService
    {
        private readonly ICustomersRepository _repository;
        private readonly IHttpClientFactory _httpClientfactory; 
        private readonly ITracer _tracer;
        private const string AddressesApi = "http://localhost:5002/api/addresses";

        public CustomersService(
            ICustomersRepository repository, 
            ITracer tracer, 
            IHttpClientFactory httpClientfactory)
        {
            _repository = repository;
            _tracer = tracer;
            _httpClientfactory = httpClientfactory;
        }
        
        public async Task<CustomerDto> GetById(int id)
        {
            var customer = await _repository.GetById(id);
            var address = await GetAddress(customer.ZipCode);

            return new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name,
                ZipCode = customer.ZipCode,
                Street = address?.Street,
                City = address?.City,
                Country = address?.Country,
                Number = address?.Number
            };

        }

        public async Task<IEnumerable<Customer>> GetList()
        {
            await ProcessCustomerInvoices();
            return await _repository.GetList();
        }

        private async Task ProcessCustomerInvoices()
        {
            using var scope = _tracer
                .BuildSpan($"{nameof(ICustomersService)}.{nameof(ProcessCustomerInvoices)}")
                .StartActive(true);
            
            await Task.Delay(500);
        }

        private async Task<Address> GetAddress(string zipCode)
        {
            using var scope = _tracer
                .BuildSpan($"{nameof(ICustomersService)}.{nameof(GetAddress)}")
                .StartActive(true);
            
            var client = _httpClientfactory.CreateClient("addressApi");

            var responseMsg = await client.SendAsync(
                new HttpRequestMessage(
                    HttpMethod.Get, 
                    $"{AddressesApi}/{zipCode}"));
  
            var data = await responseMsg.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Address>(data);;  
        }

        public Task Add(Customer customer)
        {
            return _repository.Add(customer);
        }
    }
}