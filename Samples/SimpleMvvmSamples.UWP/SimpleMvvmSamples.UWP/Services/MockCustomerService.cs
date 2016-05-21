using SimpleMvvmSamples.UWP.Models;

namespace SimpleMvvmSamples.UWP.Services
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
