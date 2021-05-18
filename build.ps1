reset-templates
nuget pack -OutputDirectory nupkg/

$files = Get-ChildItem nupkg/
foreach ($file in $files) {dotnet new -i $file.FullName}

pause