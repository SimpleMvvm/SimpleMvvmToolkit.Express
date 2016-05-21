# Simple MVVM Toolkit Express: UWP Sample

1. Create a new WPF project using Visual Studio 2015 or higher
  - Target Framework: Universal Windows Platform

2. Add the SimpleMvvmToolkit.Express NuGet package

3. Use Code Snippets Manager to add the Simple Mvvm C# code snippets

4. Add Models folder
  - Add Customer class
  - Use mvvmprop snippet to add properties
    + CustomerId, CustomerName, City

5. Add Services folder
  - Interface: ICustomerService
  - Class: MockCustomerService

6. Add ViewModels folder
  - CustomerViewModel
  - MainPageViewModel

7. Add Locators folder
  - Add NuGet package: SimpleInjector
  - Add ViewModelLocator class

8. Add ViewModelLocator to app resources
  - App.xaml

9. Add Views folder
  - Add NuGet package: Microsoft.Xaml.Behaviors.Uwp.Managed
  - Add User Control: CustomerView.xaml

10. Update MainWindow
  - Add DataContext
  - Add CustomerView
