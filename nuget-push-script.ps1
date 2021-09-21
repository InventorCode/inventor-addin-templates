$ApiKey = Read-Host -Prompt "Enter the Nuget API key:"
nuget push nupkg/InventorCode.AddinTemplates.0.3.0.nupkg $ApiKey -Source https://api.nuget.org/v3/index.json