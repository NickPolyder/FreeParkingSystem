param(
[Parameter(Mandatory = $false)]
[string] $password
)

if([System.String]::IsNullOrWhiteSpace($password))
{
    $password = "P@55w0rd";
}

dotnet dev-certs https -ep ${HOME}\.aspnet\https\aspnetapp.pfx -p $password
dotnet dev-certs https --trust

$path = Split-Path $PSScriptRoot

foreach($item in (Get-ChildItem $path -Force -Recurse -Include *API*.csproj -Exclude *Common*))
{
	Write-Host 'Project: ' $item.FullName

    dotnet user-secrets set "Kestrel:Certificates:Development:Password" $password --project $item.FullName
}