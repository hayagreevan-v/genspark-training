# 2025-07-22    Day - 57   Azure DevOps Pipeline, Kubernetes Introduction

## Topics
- Azure DevOps Pipeline
    - Github Connectivity
    - YAML file

- Kubernetes Introduction

## Notes

**Pipeline**

Code -> Version Control -> Compilation  -> Testing -> Quality Gate -> Quality Check -> Release


Pipeline - CI/CD - the entire workflow  
Stages - A big step - Build/Test  
Job - A group of task in a stage - restore and build  
Task - A single command - Restore/Build  

Agent - A machine that executes the job  

YAML - the script  


**Kuberentes**

Pods - which host the container (1 or more)  
Node - Where the pod runs  
Cluster - collection of nodes  
Master - controls the nodes  
Service - url to expose the pod  

## Links
- https://learn.microsoft.com/en-us/azure/app-service/deploy-azure-pipelines?view=azure-devops&tabs=yaml
- https://learn.microsoft.com/en-us/azure/container-registry/container-registry-quickstart-task-cli?source=recommendations