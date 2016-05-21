using System;
using System.Windows.Input;
using SimpleMvvmSamples.UWP.Models;
using SimpleMvvmSamples.UWP.Services;
using SimpleMvvmToolkit.Express;

namespace SimpleMvvmSamples.UWP.ViewModels
{
    public class CustomerViewModel : ViewModelDetailBase<CustomerViewModel, Customer>
    {
        // Add a member for ICustomerServiceAgent
        private readonly ICustomerService _serviceAgent;

        // Ctor that accepts ICustomerServiceAgent
        public CustomerViewModel(ICustomerService serviceAgent)
        {
            _serviceAgent = serviceAgent;
        }

        public event EventHandler<NotificationEventArgs<Exception>> ErrorNotice;

        // Set the model to a new customer
        public void NewCustomer()
        {
            Model = _serviceAgent.CreateCustomer();
        }

        public ICommand NewCustomerCommand => new DelegateCommand(NewCustomer);

        // Helper method to notify View of an error
        private void NotifyError(string message, Exception error)
        {
            ErrorNotice?.Invoke(this, new NotificationEventArgs<Exception>(message, error));
        }
    }
}