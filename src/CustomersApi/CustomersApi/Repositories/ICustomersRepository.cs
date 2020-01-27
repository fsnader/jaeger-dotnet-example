using System.Collections.Generic;
using System.Threading.Tasks;
using CustomersApi.Domain;

namespace CustomersApi.Repositories
{
    public interface ICustomersRepository
    {
        Task<Customer> GetById(int id);
        Task<IEnumerable<Customer>> GetList();
        Task Add(Customer customer);
    }
}