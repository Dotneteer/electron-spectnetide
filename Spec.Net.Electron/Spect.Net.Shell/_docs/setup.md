# How to Setup the Development Environment

## Prerequisits

- Visual Studio 2019
- .NET Core 3.0 SDK
- Web Compiler VS Extension

## Project Setup

1. Clone the project
2. Start Visual Studio Developer Command Prompt
3. `dotnet tool install ElectronNET.CLI -g`
4. Open Visual Studio, load the Sepc.Net.Electron.sln solution
5. Rebuild All
6. In Solution Explorer, righ-click the **Spect.Net.Shell** project and invoke open folder in File Explorer.
7. Copy the path of the project
8. Start Visual Studio Developer Command Prompt
9. `cd <project path copied in step 5>`
10. `dotnet new tool-manifest`
11. `dotnet tool install ElectronNET.CLI`
12. `dotnet electronize init`
14. Restart Visual Studio
14. 

More details: https://jimbuck.io/building-desktop-apps-with-blazor/
