# Blazor-ApexTree

[![MIT](https://img.shields.io/github/license/NF-Software-Inc/Blazor-ApexTree)](https://github.com/NF-Software-Inc/Blazor-ApexTree/blob/master/LICENSE)
[![NuGet](https://img.shields.io/nuget/v/Blazor-ApexTree.svg)](https://www.nuget.org/packages/Blazor-ApexTree/)
[![Build](https://img.shields.io/github/actions/workflow/status/NF-Software-Inc/Blazor-ApexTree/build.yml)](https://github.com/NF-Software-Inc/Blazor-ApexTree/actions/workflows/build.yml)
[![Publish](https://img.shields.io/github/actions/workflow/status/NF-Software-Inc/Blazor-ApexTree/publish.yml?label=publish)](https://github.com/NF-Software-Inc/Blazor-ApexTree/actions/workflows/publish.yml)

## Versioning Notice

As of version 10.0.0, Blazor-ApexTree has adopted a new versioning scheme that aligns its major version with the .NET major version it targets. This change is intended to provide better clarity and consistency with regard to supported frameworks.

Going forward, the major version of Blazor-ApexTree will correspond to the major version of .NET it targets (e.g., Blazor-ApexTree 10.x.x targets .NET 10). The minor and patch versions are used for library-specific updates and do not map to .NET's minor or SDK versions.

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
- .NET 9.0
- .NET 10.0

## Authors

* **NF Software Inc.**

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details

## Acknowledgments

Thank you to:
* [joadan](https://github.com/apexcharts/Blazor-ApexCharts) for the fantastic Blazor-ApexCharts project
