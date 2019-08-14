$path = Split-Path $PSScriptRoot
$baseName = 'free-parking-system-'

$dockerFileList = @(
    @{
        Tag = $baseName + 'accounts'
        File =  $path +'\src\Account\FreeParkingSystem.Accounts.API\Dockerfile'
    }#,
#    @{
#        Tag = $baseName + 'orders'
#        File =  $path+'\src\Orders\FreeParkingSystem.Orders.API\Dockerfile'
#    },
#    @{
#        Tag = $baseName + 'parking'
#        File =  $path+'\src\Parking\FreeParkingSystem.Parking.API\Dockerfile'
#    }
);

foreach($item in $dockerFileList)
{
    Write-Host 'Building: ' $item.Tag
    & docker build $path --file $item.File --tag $item.Tag
}