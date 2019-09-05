param(
[Parameter(Mandatory=$false)]
[string]$Path,
[Parameter(Mandatory=$false)]
[string]$Configuration,
[Parameter(Mandatory=$false)]
[string]$Framework
)

if([System.String]::IsNullOrWhiteSpace($Path))
{
    $Path = Split-Path $PSScriptRoot
}
 
if([System.String]::IsNullOrWhiteSpace($Configuration))
{ 
    $Configuration = 'Debug';
}
 
if([System.String]::IsNullOrWhiteSpace($Framework))
{ 
    $Framework = 'netcoreapp2.1';
}


foreach($item in (Get-ChildItem $Path -Include *Tests.csproj -Recurse -Force))
{
    $dllName = $item.Name -replace $item.Extension, '.dll';
    $dllPath = $item.Directory.FullName + '\bin\' + $Configuration + '\' + $Framework + '\' + $dllName;
    $command = '& coverlet ' + $dllPath + ' --target "dotnet" --targetargs "test ' + $item.FullName + ' --no-build" --merge-with .\coverage.opencover.xml --format opencover';
    Write-Host $command
    Invoke-Expression $command

}
#coverlet .\bin\Debug\netcoreapp2.1\FreeParkingSystem.Accounts.Tests.dll --target "dotnet" --targetargs "test .\FreeParkingSystem.Accounts.Tests.csproj --no-build"