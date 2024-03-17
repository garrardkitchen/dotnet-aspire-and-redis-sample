# Getting started

This relates to my blob post called **[.NET Aspire and Redis](https://blog.garrardkitchen.com/dotneet-aspire-and-redis)**.

## Prerequisite

- Docker for Desktop
- .NET Aspire workload
- Visual Studio 2022 Community edition

## How to run

-  Run the `http` launch profile

---

# AZD

This sample can be deployed to the Azure cloud provider by using the **[Azure Developer CLI](https://learn.microsoft.com/en-us/azure/developer/azure-developer-cli/)**

## How to deploy as an Azure Container App

This **[post](https://learn.microsoft.com/en-us/dotnet/aspire/deployment/azure/aca-deployment-azd-in-depth?tabs=windows)** explains how to deploy the application to Azure.  It's straight forward.  If you're familiar with AZD, there's nothing new to do here.

However, when I tried this I got the following when deploying webfrontend or apiservice:

> [!CAUTION]  
> error CONTAINER1013: Failed to push to the output registry: The request was canceled due to the configured HttpClient.Timeout of 100 seconds elapsing

This error is mentioned **[here](https://blog.garrardkitchen.com/posts/dotnet-aspire-and-redis/#addendum)**.

Since trying this the first time, both Aspire and AZD have had updates.  I had hoped with both of these components updated, it'll fix this documented issue. To update these components:

```powershell
# Update Aspire to Preview 4
dotnet workload update
dotnet workload restore

# Updated AZD CLI to 1.7.0
winget update Microsoft.Azd
```

This did not fix the issue.

Not wanting this to end without success for a second time, I revisited the GitHub Issue included in the above post.  It hadn't been addressed.  I continued looking for a possible fix.  I then came acros this **[discussion](https://github.com/Azure/azure-dev/discussions/3212)** and from this I set the environment variable `SDK_CONTAINER_DEBUG_REGISTRY_FORCE_CHUNKED_UPLOAD`.

By setting this, the chunks will only be 64kB which might cause a slow upload, but this I can live with [for now]:

```powershell
$env:SDK_CONTAINER_DEBUG_REGISTRY_FORCE_CHUNKED_UPLOAD="true"
```

After a `azd deploy webfrontend` (the apiservice deployed successfully) and I got:

```
SUCCESS: Your application was deployed to Azure in 2 minutes 50 seconds.
```

So, in conclusion, before you do your `azd init` and provide answers to all the AZD configuration prompts, first set the `SDK_CONTAINER_DEBUG_REGISTRY_FORCE_CHUNKED_UPLOAD` env var to `true`.