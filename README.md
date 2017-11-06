# Simple MVVM Toolkit Express

## Streamlined version of Simple MVVM Toolkit targeting the .NET Standard 2.0

This library is compatible with the [.NET Standard](https://docs.microsoft.com/en-us/dotnet/standard/net-standard) 2.0 specification, which supports the following platforms (or higher):
- **.NET Framework 4.6.1**
- **.NET Core 2.0**
- **Mono 5.4**
- **Xamarin.iOS 10.14**
- **Xamarin.Mac 3.8**
- **Xamarin.Android 8.0**
- **Universal Windows Platform 10.0.16299**

The following platforms will **not** be supported:
- *Windows 8.1*
- *Windows Phone 8.1*
- *Windows Phone Silverlight*
- *.NET 4.0 or earlier*
- *Silverlight*

If you require support for any of these platforms, please use the prior version of Simple MVVM Toolkit.

Because cross-threading concerns are generally handled by `Task` and `async/await`, threading support will be removed.
This will allow removal of platform-specific implementations that were required for cross-thread marshalling.

Object cloning will be implemented using **Json.Net**, rather than Data Contract Serializer.
The Json.Net serializer is configured *programmatically* to handle cyclical references.
