<p>
    <p align="left" valign="middle">
        <img src="https://github.com/rdms-org/rdms-device-controller/blob/main/src/RDMS/Assets/RDMS.ico" width="36" height="36">
        &nbsp;<img src="https://readme-typing-svg.demolab.com?font=Outfit&weight=200&size=22&pause=3000&color=808080&vCenter=true&random=false&width=300&height=32&lines=RDMS+Device+Controller" alt="Typing SVG" />
    </p>
    <p align="left">
        <a target="_blank" href="https://github.com/rdms-org/rdms-device-controller/actions"><img alt="GitHub Workflow Status" src="https://img.shields.io/github/actions/workflow/status/rdms-org/rdms-device-controller/build.yml?branch=main"></a>
        <a target="_blank" href="https://github.com/rdms-org/rdms-device-controller/releases/latest"><img alt="GitHub release (latest by date)" src="https://img.shields.io/github/v/release/rdms-org/rdms-device-controller"></a>
        <a target="_blank" href="https://github.com/rdms-org/rdms-device-controller/blob/main/LICENSE"><img alt="GitHub" src="https://img.shields.io/github/license/rdms-org/rdms-device-controller"></a>
    </p>
</p>

## Usage
[See the documentation](./docs/USER_GUIDE.md) for instructions on how to use it.

## Build
### Requirements
 * __OS__ : Windows 10 or higher version (server edition is also available!)
 * __Tools__
   * [PowerShell 7](https://github.com/PowerShell/PowerShell)
   * [Visual Studio 2022](https://visualstudio.microsoft.com/)(include 'Desktop development with C++' and '.NET desktop development' workload)
   * [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download)

### Guide
1. Open a PowerShell terminal from the project directory.
2. Run the commands below.
```pwsh
Set-ExecutionPolicy Unrestricted
./build_script.ps1 -target RDMS/RDMS.csproj --framework net7.0 --self-contained false --configuration Release -p:PublishReadyToRun=true -excludeSymbols $(true|false)
./build_script.ps1 -target RDMS/RDMS.Services.csproj --framework net7.0 --self-contained false --configuration Release -p:PublishReadyToRun=true -p:PublishSingleFile=true -excludeSymbols $(true|false)
```

## License
The contents are freely available under the [MIT License](http://opensource.org/licenses/MIT).

The licenses of third-party libraries can be found [here](./docs/OPENSOURCES.md).
