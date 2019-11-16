$path = Split-Path $PSScriptRoot
$baseName = 'free-parking-system-'

$dockerFileList = @(
	@{
		Tag = $baseName + 'accounts'
		File =  $path +'\src\Account\FreeParkingSystem.Accounts.API\Dockerfile'
	},
	@{
		Tag = $baseName + 'parking'
		File =  $path+'\src\Parking\FreeParkingSystem.Parking.API\Dockerfile'
	}#,
#    @{
#        Tag = $baseName + 'orders'
#        File =  $path+'\src\Orders\FreeParkingSystem.Orders.API\Dockerfile'
#    },

);


Write-Host ''

Write-Host 'Available Dockerfiles to build: '

$options = New-Object -TypeName 'System.Collections.Generic.Dictionary[int,System.Collections.Hashtable]'

$index = 1;

foreach($value in $dockerFileList)
{
    Write-Host "`t" $index ': ' $value.Tag
    $options.Add($index,$value);
    $index++;
}


Write-Host ''

$fileToBuild = Read-Host -Prompt 'Select the Dockerfile to build'

$dockerFile = $options[$fileToBuild];

Write-Host 'Building: ' $dockerFile.Tag
& docker build $path --file $dockerFile.File --tag $dockerFile.Tag
