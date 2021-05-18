cd empty-addin-sdk
reset-templates
nuget.exe pack .\empty-addin-template.nuspec
dotnet new -i .\jordanrobot.Template.InventorAddin48.nuspec.0.1.2.nupkg
pause
cd ..