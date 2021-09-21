## inventor-addin-templates

A collection of Autodesk Inventor addin templates that use the dotnet new templating engine. This means the templates are always ready to build, and are simpler to configure and create than the older visual studio style templates. Depsite using the dotnet new templating engine, the addins are targeted towards .Net Framework 4.7 & 4.8, though that can be further configured after creation.

### Installation

Install the nuget package by running the simple command [here](https://www.nuget.org/packages/InventorCode.AddinTemplates/)

### Creation

To create a new empty addin, enter the following command in a command line terminal:
```dotnet new inv-empty -n NewAddinName```

- The new addin will be created in the current directory.
- type ```dotnet new inv-empty -h``` for a list of options.
- list of templates
  - `inv-empty` Empty Inventor addin template.
  - `inv-nifty` A port of Brian Ekin's VB.net Nifty Addin template.
  - `inv-wpf` A simple [MVVM] WPF addin template.
- You can enable these templates in Visual Studio 2019 if you have "Show All .NET CLI templates" enabled in Options > General > Preview Features.

### Dependencies

- nuget.exe >= 5.2
- tested with dotnet 5, Inventor 2021
