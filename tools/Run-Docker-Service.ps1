$DockerDesktopPath = 'C:\Program Files\Docker\Docker\Docker Desktop.exe'
$DockerServiceName = 'Docker'
$DockerDesktopProcessName = 'Docker Desktop'

$dockerService = (Get-Service -Name $DockerServiceName -ErrorAction SilentlyContinue);

if($dockerService -ne $null -and $dockerService.Status -ne [System.ServiceProcess.ServiceControllerStatus]::Running)
{
    Write-Host 'Starting Service: ' $dockerService.DisplayName
    Start-Service $dockerService.ServiceName
}

Remove-Variable dockerService

$dockerDesktop = Get-Process $DockerDesktopProcessName -ErrorAction SilentlyContinue

if ($dockerDesktop -eq $null) {
    Write-Host 'Starting App: ' $DockerDesktopProcessName
    Start-Process $DockerDesktopPath -Verb runAs
}

Remove-Variable dockerDesktop

$path = (Split-Path $PSScriptRoot) +'\docker-compose.yml'

$yamlContent = Get-Content $path -Raw

$dockerComposeConfiguration = ConvertFrom-Yaml $yamlContent;

$options = New-Object -TypeName 'System.Collections.Generic.Dictionary[int,string]'

Write-Host ''

Write-Host 'Available services to start (Make sure that you built the Dockerfile): '

$index = 1;

foreach($key in $dockerComposeConfiguration.services.Keys)
{
    Write-Host "`t" $index ': ' $key
    $options.Add($index,$key);
    $index++;
}

Write-Host ''

$serviceToStart = Read-Host -Prompt 'Select the service to start'

$serviceKey = $options[$serviceToStart];

$service = $dockerComposeConfiguration.services.Item($serviceKey)

Write-Host "`r`nStarting Service: " $serviceKey

if($service.depends_on -ne $null)
{
    Write-Host "`r`n Make sure that services listed below are running: "
    
    foreach($dependentService in $service.depends_on)
    {
        Write-Host "`t - " $dependentService
    }
}

$commandBuilder = New-Object -TypeName 'System.Text.StringBuilder'

[void]$commandBuilder.Append("docker container run -it -d --rm ");

if($service.ports -ne $null)
{
    Write-Host "`r`nOpening ports: "
    foreach($port in $service.ports)
    {
        [void]$commandBuilder.Append(" -p " + $port);

        Write-Host "`t - " $port
    }
    [void]$commandBuilder.Append(" ")
}

if($service.volumes -ne $null)
{
    Write-Host "`r`nIncluding Voluming mapping: "
    foreach($volume in $service.volumes)
    {
        [void]$commandBuilder.Append(" -v " + $volume);

        Write-Host "`t - " $volume
    }
    [void]$commandBuilder.Append(" ")
}


if($service.environment -ne $null)
{
    [System.Int64]$result = $null;
    Write-Host "`r`nIncluding environment variables: "
    foreach($envItem in $service.environment)
    {
       foreach($environmentKey in $envItem.Keys)
       {
            $value = $envItem.Item($environmentKey);
            if(-Not([System.Int64]::TryParse($value, [ref]$result)))
            {
                $value = '"' + ($value  -replace '"',"'" ) + '"'
            }
            $environmentKeyValuePair =  $environmentKey + "=" + $value;
            [void]$commandBuilder.Append(" -e " + $environmentKeyValuePair);

            Write-Host "`t - " $environmentKeyValuePair
       }
    }

    [void]$commandBuilder.Append(" ")
}

[void]$commandBuilder.Append($service.image);

$command = $commandBuilder.ToString();

 Invoke-Expression $command
