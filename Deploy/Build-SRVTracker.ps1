function Set-AssemblyVersion
{
    Param (
        [string]$Version,
        [switch]$increment
    )
    $NewVersion = 'AssemblyVersion("' + $Version + '")';
    $NewFileVersion = 'AssemblyFileVersion("' + $Version + '")';

    foreach ($o in $input) 
    {
        Write-output $o.FullName
        $TmpFile = $o.FullName + ".tmp"

        Get-Content $o.FullName -encoding utf8 |
        %{$_ -replace 'AssemblyVersion\("[0-9]+(\.([0-9]+|\*)){1,3}"\)', $NewVersion } |
        %{$_ -replace 'AssemblyFileVersion\("[0-9]+(\.([0-9]+|\*)){1,3}"\)', $NewFileVersion }  |
        Set-Content $TmpFile -encoding utf8

        move-item $TmpFile $o.FullName -force
    }
}



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

# Update version numbers of both SRVTracker and Race Manager
$SRVTrackerRoot = (Get-Item $location).Parent
$SRVTrackerAssemblyFile = "$($SRVTrackerRoot.FullName)\Properties\AssemblyInfo.cs"
$RaceManagerAssemblyFolder = "$($SRVTrackerRoot.FullName)\Race Manager\Properties\AssemblyInfo.cs"