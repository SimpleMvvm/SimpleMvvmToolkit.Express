using SimpleInjector;
using SimpleMvvmSamples.UWP.Services;
using SimpleMvvmSamples.UWP.ViewModels;

namespace SimpleMvvmSamples.UWP.Locators
{
    public class ViewModelLocator
    {
        private readonly Container _container;

        public ViewModelLocator()
        {
            // Set up DI container
            _container = new Container();
            Bootstrap();
        }

        private void Bootstrap()
        {
            // Register services
            _container.Register<ICustomerService, MockCustomerService>(Lifestyle.Transient);

            // Register view models
            _container.Register<MainPageViewModel>(Lifestyle.Transient);
            _container.Register<CustomerViewModel>(Lifestyle.Transient);
        }

        public MainPageViewModel MainPageViewModel => _container.GetInstance<MainPageViewModel>();

        public CustomerViewModel CustomerViewModel => _container.GetInstance<CustomerViewModel>();
    }
}
