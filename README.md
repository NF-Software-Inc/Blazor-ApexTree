# Blazor-ApexTree

[![MIT](https://img.shields.io/github/license/NF-Software-Inc/Blazor-ApexTree)](https://github.com/NF-Software-Inc/Blazor-ApexTree/blob/master/LICENSE)
[![NuGet](https://img.shields.io/nuget/v/Blazor-ApexTree.svg)](https://www.nuget.org/packages/Blazor-ApexTree/)

## Getting Started

This library provides a blazor wrapper for [ApexTree.js](https://apexcharts.com/apextree/). 

Usage is quite simple, just install the package and then create any `<ApexTree />` components as desired. You will need to provide data in the `Parent` parameter; all children must be nested within this. Setting the `Id` property on nodes is highly recommended for everything to work correctly and just needs to be a unique value (using `Guid.NewGuid().ToHtmlId().ToString("N")` would work well).

There are two types for `TItem` that have full support built in; otherwise you need to provide a value for `NodeTemplate` in the options for the component. The supported types are `string?` and `ApexTree.Image`; using either of these types will produce a tree that looks very similar to the official JavaScript examples.

### Installation

To use this library: 

* Clone a copy of the repository
* Reference the [NuGet package](https://www.nuget.org/packages/Blazor-ApexTree/)

## Build Details

### Frameworks

- .NET 8.0

## Authors

* **NF Software Inc.**

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details

## Acknowledgments

Thank you to:
* [joadan](https://github.com/apexcharts/Blazor-ApexCharts) for the fantastic Blazor-ApexCharts project
