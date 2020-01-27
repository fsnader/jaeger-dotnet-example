using System.Collections.Generic;
using System.Threading.Tasks;
using CustomersApi.Domain;

namespace CustomersApi.Services
{
    public interface ICustomersService
    {
        Task<CustomerDto> GetById(int id);
        Task<IEnumerable<Customer>> GetList();
        Task Add(Customer customer);
    }
}