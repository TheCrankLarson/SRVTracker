$srvTrackerSource = Get-Content "SRVTracker.txt"
$raceManagerSource = Get-Content "Race Manager.txt"

$versionInfo = [System.Diagnostics.FileVersionInfo]::GetVersionInfo("$srvTrackerSource\SRVTracker.exe")
if (!$versionInfo)
{
    exit
}

$location = Get-Location
$zipFile = "$($location.Path)\SRVTracker_$($versionInfo.FileVersion).zip"

Write-Host "Compressing $srvTrackerSource\* to $zipFile"

Compress-Archive -Path "$srvTrackerSource\*" -DestinationPath $zipFile -Force
Compress-Archive -Path "$raceManagerSource\*.exe" -DestinationPath $zipFile -Update
Compress-Archive -Path "$raceManagerSource\*.exe.config" -DestinationPath $zipFile -Update
Compress-Archive -Path "$raceManagerSource\*.dll" -DestinationPath $zipFile -Update
#Copy-Item "$raceManagerSource\*" $zipFile -Include @("*.exe", "*.dll")
