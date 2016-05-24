# Simple MVVM Toolkit Express

## Streamlined version of Simple MVVM Toolkit targeting the .NET Platform Standard

This library is compatible with the following platforms:
- **Portable Class Libraries**: Profile 111 - .NET 4.5, AspNet Core 1.0, Windows 8, Windows Phone 8.1
- **.NET Framework 4.6**
- **Universal Windows Platform 10.0**
- **Mono/Xamarin**: monoandroid60, xamarinios10
- **.NET Core 1.0**: NetStandard 1.3

The following platforms will **not** be supported:
- *.NET 4.0 or earlier*
- *Silverlight*

Because cross-threading concerns are generally handled by `Task` and `async/await`, threading support will be removed.
This will allow removal of platform-specific implementations that were required for cross-thread marshalling.

Object cloning will be implemented using **Json.Net**, rather than Data Contract Serializer.
The Json.Net serializer is configured *programmatically* to handle cyclical references.
