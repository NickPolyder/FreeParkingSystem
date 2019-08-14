param(
[Parameter(Mandatory=$true)]
[string]$FilePath,
[Parameter(Mandatory=$false)]
[hashtable]$Dictionary
)

if($Dictionary -eq $null)
{
    $Dictionary = @{};
    
    foreach($item in (Get-ChildItem env:Deployment_*))
    {
        $Dictionary.Add($item.Name,$item.Value)
    }
}

$environmentName = $Dictionary["Deployment_Environment_Name"];

Write-Host "Environment: " $environmentName

if (-Not (Test-Path -Path $FilePath -PathType Leaf))
{
    Write-Error "Could not find path: " $FilePath
    return;
}

$newPath = $FilePath -replace "{environment_name}", $environmentName
Rename-Item $FilePath $newPath -Force

$KeyArray = @{}

foreach($key in $Dictionary.Keys)
{
    $keyToReplace = "{{"+ $key + "}}" -replace "\.", "_"
    $keyArray.Add($keyToReplace, $Dictionary[$key]);
}

(Get-Content $newPath) | Foreach-Object {
    $item = $_
	foreach($key in $KeyArray.Keys)
    {
         $item = $item -replace $key, $KeyArray[$key]
    }
    Write-Output $item
} | Set-Content $newPath	