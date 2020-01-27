using System.Collections.Generic;
using System.Threading.Tasks;
using CustomersApi.Domain;
using OpenTracing;

namespace CustomersApi.Repositories
{
    public class TracedCustomersRepository : ICustomersRepository
    {
        private readonly ICustomersRepository _customersesRepository;
        private readonly ITracer _tracer;

        public TracedCustomersRepository(
            ICustomersRepository customersesRepository,
            ITracer tracer)
        {
            _customersesRepository = customersesRepository;
            _tracer = tracer;
        }

        public Task<Customer> GetById(int id)
        {
            using var scope = _tracer
                .BuildSpan($"{nameof(ICustomersRepository)}.{nameof(GetById)}").
                StartActive(true);
            
            scope.Span.SetTag(nameof(Customer.Id), id);
            return _customersesRepository.GetById(id);
        }

        public Task<IEnumerable<Customer>> GetList()
        {
            using var scope = _tracer
                .BuildSpan($"{nameof(ICustomersRepository)}.{nameof(GetList)}").
                StartActive(true);
            return _customersesRepository.GetList();
        }

        public Task Add(Customer customer)
        {
            using var scope = _tracer
                .BuildSpan($"{nameof(ICustomersRepository)}.{nameof(Add)}").
                StartActive(true);

            scope.Span.SetTag(nameof(Customer.Id), customer.Id);
            scope.Span.SetTag(nameof(Customer.Name), customer.Name);
            scope.Span.SetTag(nameof(Customer.ZipCode), customer.ZipCode);
            
            return _customersesRepository.Add(customer);
        }
    }
}