using SimpleMvvmSamples.WPF.Models;

namespace SimpleMvvmSamples.WPF.Services
{
    public class MockCustomerService : ICustomerService
    {
        // Create a fake customer
        public Customer CreateCustomer()
        {
            return new Customer
            {
                CustomerId = 1,
                CustomerName = "John Doe",
                City = "Dallas"
            };
        }
    }
}
