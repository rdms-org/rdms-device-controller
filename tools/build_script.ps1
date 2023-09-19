# --------------------------------------------------
# RDMS Builder
# Copyright (c) 2023 handbros all rights reserved.
# --------------------------------------------------

#Requires -Version 7

[CmdletBinding(PositionalBinding = $false)]
Param(
    [string][Alias('v')]$verbosity = "minimal",
    [string][Alias('s')]$source = "",
    [bool][Alias('e')]$excludeSymbols = $true,
    [switch]$noLogo,
    [switch]$help,
	
	[string]$productName = "Unknown Product",
	[string]$productVersion = "1.0.0.0",
	[string]$fileDesc = "Unknown File Description",
	[string]$fileVersion = "1.0.0.0",
	[string]$company = "Unknown Corporation",
	[string]$copyright = "Unknown Copyright",
	
	[Parameter(ValueFromRemainingArguments = $true)][String[]]$properties
)

$Script:BuildPath = ""

function Invoke-ExitWithExitCode([int] $exitCode) {
    if ($ci -and $prepareMachine) {
        Stop-Processes
    }

    exit $exitCode
}

function Invoke-Help {
    Write-Host "Common settings:"
	Write-Host "  -verbosity <value>         Msbuild verbosity: q[uiet], m[inimal], n[ormal], d[etailed], and diag[nostic] (short: -v)"
	Write-Host "  -source <value>            Name of a solution or project file to build (short: -s)"
	Write-Host "  -excludeSymbols <value>    If it is true, exclude debug symbols(*.pdb) (short: -e)"
    Write-Host "  -noLogo                    Doesn't display the startup banner or the copyright message"
    Write-Host "  -help                      Print help and exit"
    Write-Host ""
	
	Write-Host "Descriptions:"
	Write-Host "  -productName <value>       Product name"
	Write-Host "  -productVersion <value>    Product version"
	Write-Host "  -fileDesc <value>          File description"
	Write-Host "  -fileVersion <value>       File version"
	Write-Host "  -company <value>           Company name"
	Write-Host "  -copyright <value>         Copyright information"
	Write-Host ""
}

function Invoke-Hello {
    if ($nologo) {
        return
    }
	
	Write-Host "RDMS Builder" -ForegroundColor White
	Write-Host "Copyright (c) 2023 handbros all rights reserved." -ForegroundColor White
    Write-Host ""
}

function Initialize-Script {
	# Check the target
	if ([string]::IsNullOrEmpty($source) -eq $True) {
		Write-Host "Please specify a target file(solution or project)." -ForegroundColor Red
		Invoke-ExitWithExitCode 1
	}

    if ((Test-Path "$($PSScriptRoot)\..\src\$($source)") -eq $False) {
        Write-Host "Target $($PSScriptRoot)\..\src\$($source) not found." -ForegroundColor Red
        Invoke-ExitWithExitCode 1
    }

    $Script:targetPath = (Resolve-Path -Path "$($PSScriptRoot)\..\src\$($source)").ToString()
}

function Invoke-Restore {
    dotnet restore $Script:targetPath --verbosity $verbosity

    if ($lastExitCode -ne 0) {
        Write-Host "Restoration has failed." -ForegroundColor Red

        Invoke-ExitWithExitCode $LastExitCode
    }
}

function Invoke-Publish {
	if ($excludeSymbols -eq $true) {
        dotnet publish $Script:targetPath -p:DebugType=None -p:DebugSymbols=false -p:Product=$productName -p:Version=$productVersion -p:AssemblyTitle=$fileDesc -p:AssemblyVersion=$fileVersion -p:Company=$company -p:Copyright=$copyright --verbosity $verbosity --no-restore --nologo $properties
    } else {
		dotnet publish $Script:targetPath -p:Product=$productName -p:Version=$productVersion -p:AssemblyTitle=$fileDesc -p:AssemblyVersion=$fileVersion -p:Company=$company -p:Copyright=$copyright --verbosity $verbosity --no-restore --nologo $properties
	}

    if ($lastExitCode -ne 0) {
        Write-Host "Publishing has failed." -ForegroundColor Red
        Invoke-ExitWithExitCode $LastExitCode
    }
}


if ($help) {
    Invoke-Help

    exit 0
}

[timespan]$execTime = Measure-Command {
    Invoke-Hello | Out-Default
    Initialize-Script | Out-Default
	Invoke-Restore | Out-Default
    Invoke-Publish | Out-Default
}

Write-Host "Finished in " -NoNewline
Write-Host "$($execTime.Minutes) min, $($execTime.Seconds) sec, $($execTime.Milliseconds) ms." -ForegroundColor Cyan

Write-Host "Finished at " -NoNewline
Write-Host "$(Get-Date -UFormat "%Y. %m. %d. %R")." -ForegroundColor Cyan