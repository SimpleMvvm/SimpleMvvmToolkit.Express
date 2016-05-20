# Simple MVVM Toolkit Express

## Streamlined version of Simple MVVM Toolkit targeting the .NET Platform Standard

This library will target `netstandard` version **1.3**, which is compatible with the following platforms:
- **.NET Core 1.0**
- **.NET Framework 4.6**
- **Universal Windows Platform 10.0**
- **Mono/Xamarin**

The following platforms will **not** be supported:
- *Silverlight*
- *Windows Phone*

Because cross-threading concerns are generally handled by `Task` and `async/await`, threading support will be removed.
This will allow removal of platform-specific implementations that were required for cross-thread marshalling.

Object cloning will be implemented using **Json.Net**, rather than Data Contract Serializer.
The Json.Net serializer will be configured *programmatically* to handle cyclical references.
