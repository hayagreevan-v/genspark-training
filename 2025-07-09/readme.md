# 2025-07-09    Day - 48 Azure Linux Instance, ARM

## Topics
- Azure Linux Instance 
    - ssh connection
    - docker running at Port 80

- Azure CLI

- Azure Resource Manager
    - Resorce Management using CLI (Creation)
        - Template.json
        - Parameters.json

``` sh
az deployment group create --resource-group reJul25 --template-file azuredeploy.json --parameters @parameters.json
```
## Links
- https://learn.microsoft.com/en-us/azure/azure-resource-manager/templates/deployment-script-template
- https://learn.microsoft.com/en-us/cli/azure/install-azure-cli-macos?view=azure-cli-latest
- https://learn.microsoft.com/en-us/azure/azure-resource-manager/