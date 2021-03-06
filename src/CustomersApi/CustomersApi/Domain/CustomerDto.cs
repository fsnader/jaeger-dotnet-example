namespace CustomersApi.Domain
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int? Number { get; set; }
    }
}