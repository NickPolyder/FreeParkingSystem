$dictionaryTemp =  @{
			Deployment_Environment_Name = 'Staging' 
			Deployment_LogLevel_Default = 'Error'
			Deployment_Db_Server = 'localhost,3306'
			Deployment_Db_Name = 'FreeParkingSystem.Database' 
			Deployment_Db_User = 'sa' 
			Deployment_Db_Password = 'S1mple@password' 
			Deployment_PasswordOptions_MinimumCharacters = '4'
			};

foreach($item in (Get-ChildItem env:Deployment_*))
{
	if($dictionaryTemp.ContainsKey($item.Name))
	{
		$dictionaryTemp[$item.Name] = $item.Value;	
	}else{
		$dictionaryTemp.Add($item.Name, $item.Value);	
	}
}

if($dictionaryTemp.Count -le 0)
{
	Write-Error "Dictionary is empty."
	return;
}
$environmentName = $dictionaryTemp["Deployment_Environment_Name"];

if($environmentName -ne $Env:ASPNETCORE_ENVIRONMENT)
{
	$Env:ASPNETCORE_ENVIRONMENT = $environmentName
}

& .\tools\SetupEnvironmentVariables.ps1 "./appsettings.{environment_name}.json" $dictionaryTemp

& dotnet FreeParkingSystem.Accounts.API.dll