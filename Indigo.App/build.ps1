Write-Host "Building UI"

$baseDir = Get-Location

Remove-Item -Path "./wwwroot/*" -Recurse -Force

Set-Location "../indigo/";

npm i
npm run build

Move-Item -Path "./build/*" -Destination "$baseDir\wwwroot\" -Force

Write-Host "Completed UI Build!"