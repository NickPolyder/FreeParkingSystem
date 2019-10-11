$Path = [System.IO.Path]::Combine((Split-Path $PSScriptRoot),"Tests")

$Configuration = 'Debug';

$regexConfiguration = '^*.'+$Configuration+'*.';

$testProjects = (Get-ChildItem $Path -Include *Tests.csproj -Exclude 'FreeParkingSystem.Testing' -Recurse -Force);

$length = $testProjects.Length
$activityName = "Run coverage";

if($length -le 0)
{
    Write-Output 'No scripts to run.'
    return;
}

Write-Progress -Activity $activityName -Status 'Progress->' -PercentComplete 0 -CurrentOperation "Starting..."

for($index = 0; $index -lt $length; $index++)
{
    $item = $testProjects[$index];
    
     Write-Progress -Activity $activityName -Status 'Progress->' -PercentComplete (($index * 100) / $length) -CurrentOperation $item.Name
    $dllName = $item.Name -replace $item.Extension, '.dll';

    $dllPath = (Get-ChildItem $item.Directory.FullName -Include $dllName -Recurse -Force `
    | Where-Object { $_.Directory.FullName -Match $regexConfiguration } `
    | Select -First 1).FullName;
    
    if($dllPath -ne $null -And -Not (Test-Path $dllPath))
    {
        Write-Output ''
        Write-Warning 'Cannot find dll for:' $item.Name
        continue;
    }

    $command = '& coverlet ' + $dllPath + ' --target "dotnet" --targetargs "test ' + 
    $item.FullName + 
    ' --no-build -c '+ $Configuration + '" --merge-with .\coverage.json ';

    if($index + 1 -eq $testProjects.Length)
    {
        $command = $command + ' --format opencover';
    }
    Invoke-Expression $command | Out-File .\coverage.log -Append
}

& reportgenerator -reports:coverage.opencover.xml -targetdir:.\cover | Out-File .\coverage.log -Append

& start .\cover\index.htm