# 2025-07-16    Day - 53    Azure Functions

## Topics
- Azure Function
    - To Generate Temproary SAS Url for Blob storage items
- VS Code Azure Functions Extension


## Notes

**Create Azure Function using VS Code Extension**

**Commands to create and deploy Azure functions in Azure**
``` sh
az group create --name hayagreevan_resources --location eastus
-------------------------------------

az storage account create --name hayagreevanstorage --location eastus --resource-group reJul25 --sku Standard_LRS

-------------------------------------

az functionapp create --resource-group "hayagreevan_resources" --consumption-plan-location "eastus" --name "hayagreevandotnetfunc" --storage-account "hayagreevanstorage" --runtime dotnet-isolated --functions-version 4

------------------------------- 

az functionapp config appsettings set  --name "hayagreevandotnetfunc" --resource-group "hayagreevan_resources" --settings AzureStorageConnectionString="connectionstring" ContainerName="blobstorage" KeyVaultUri="https://hayagreevan-vault.vault.azure.net/"
-------------------------------------

func azure functionapp publish hayagreevandotnetfunc
```
## Links
- https://learn.microsoft.com/en-us/azure/azure-functions/create-first-function-vs-code-csharp