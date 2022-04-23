param(
[string]$SolutionDir,
[string]$TargetPath,
[string]$OutDir
)

Write-Host "SolutionDir: $SolutionDir"
Write-Host "TargetPath: $TargetPath"
Write-Host "OutDir: $OutDir"

$versionInfo = [System.Diagnostics.FileVersionInfo]::GetVersionInfo($TargetPath)
if (!$versionInfo)
{
    exit
}
$packageName = "$($versionInfo.ProductName)_$($versionInfo.FileVersion)"
Write-Host "Creating package $packageName"
$deployFolder = "$($SolutionDir)Deploy\$packageName"
if (Test-Path $deployFolder)
{
    Write-Host "Removing folder $deployFolder"
    Remove-Item $deployFolder -Recurse
}
New-Item $deployFolder -ItemType Directory

# Copy and .exe and .dll from source $OutDir
Copy-Item * $deployFolder -Include @("*.exe", "*.dll", "*.config")
if (Test-Path "ReleaseNotes.txt")
{
    Copy-Item "ReleaseNotes.txt" $deployFolder
}
if (Test-Path "RaceMonitoring")
{
    Copy-Item -Path "RaceMonitoring" -Destination "$deployFolder" -Recurse
}

$deployFolder | Out-File "$($SolutionDir)Deploy\$($versionInfo.ProductName).txt" -Force