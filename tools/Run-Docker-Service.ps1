param(
[Parameter(Mandatory = $false)]
[string]$configuration
)

Function StartService() 
{
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
    
    if ($dockerDesktop -eq $null) 
    {
        Write-Host 'Starting App: ' $DockerDesktopProcessName
        Start-Process $DockerDesktopPath -Verb runAs
        
        for($i = 1;$i -le 5; $i++)
        {
            Write-Host 'Waiting for service to start.' ([System.String]::new('.',$i))
            [System.Threading.Thread]::Sleep(1000);    
        }    
    }
    
    Remove-Variable dockerDesktop
}

Function ReplaceWithActiveConfiguration($defaultConfiguration)
{
    $pathToActiveConfig = (Split-Path $PSScriptRoot) +'\docker-compose.' + $configuration + '.yml'

    if(Test-Path $pathToActiveConfig)
    {
        $yamlActiveConfigurationContent = Get-Content $pathToActiveConfig -Raw
    
        $activeConfiguration = ConvertFrom-Yaml $yamlActiveConfigurationContent;
    
        foreach($serviceKey in $activeConfiguration.services.Keys)
        {
            $currentService = $activeConfiguration.services.Item($serviceKey)

            if($defaultConfiguration.services.ContainsKey($serviceKey))
            {
                $defaultService = $defaultConfiguration.services.Item($serviceKey)

                foreach($key in $currentService.Keys)
                {
                    if($defaultService.ContainsKey($key))
                    {
                        $defaultService.Item($key) = $currentService.Item($key)
                    }
                    else
                    {
                        $defaultService.Add($key,$currentService.Item($key))
                    }

                }
            }
            else
            {
                $defaultConfiguration.services.Add($serviceKey, $currentService)
            }
           
        }
    }
}

StartService

if([System.String]::IsNullOrWhiteSpace($configuration))
{
    $configuration = "dev";
}

$path = (Split-Path $PSScriptRoot) +'\docker-compose.yml'

$yamlContent = Get-Content $path -Raw

$dockerComposeConfiguration = ConvertFrom-Yaml $yamlContent;

ReplaceWithActiveConfiguration $dockerComposeConfiguration

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
Invoke-Expression 'docker network create -d "nat" free-parking-system' -ErrorAction Ignore

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
[void]$commandBuilder.Append(' --network free-parking-system ')

[void]$commandBuilder.Append(' --name ' + $serviceKey +' ')

[void]$commandBuilder.Append($service.image);

$command = $commandBuilder.ToString();

Invoke-Expression $command
