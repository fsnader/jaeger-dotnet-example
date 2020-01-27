using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomersApi.Domain;

namespace CustomersApi.Repositories
{
    public class CustomersRepository : ICustomersRepository
    {
        private List<Customer> _customers { get; set; }
        
        public CustomersRepository()
        {
            _customers = new List<Customer>
            {
                new Customer
                {
                    Id = 1,
                    Name = "Joao",
                    ZipCode = "1234"
                },
                new Customer
                {
                    Id = 2,
                    Name = "Pedro",
                    ZipCode = "32143"
                },
                new Customer
                {
                    Id = 3,
                    Name = "Marcos",
                    ZipCode = "1111"
                }
            };
        }

        public async Task<Customer> GetById(int id)
        {
            return _customers.FirstOrDefault(c => c.Id == id);
        }
        
        public async Task<IEnumerable<Customer>> GetList()
        {
            return _customers;
        }

        public async Task Add(Customer customer)
        {
            _customers.Add(customer);
        }
    }
}