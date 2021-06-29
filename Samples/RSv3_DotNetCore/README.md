#Introduction 
The repository contains a client sample application for the following APIs using RemoteServices V3 service.
    1. Generate EMV Configuration Update Token
    2. Transform EMV Configuration
    3. Retrieve list of keys for a given device protocol
    4. Generate Key Update Token
    

# Clone the repository
    1. Navigate to the main page of the  **repository**
    2. Under the **repository** name, click  **Clone**
    3. Use any Git Client (e.g.: GitBash, Git Hub for windows, Source tree ) to  **clone**  the  **repository**  using HTTPS

Note: reference [Cloning a Github Repository](https://help.github.com/en/articles/cloning-a-repository)


# Getting Started

    1.  Install .net core 3.1 LTS

        - Client app requires dotnet core 3.1 LTS


    2.  Software dependencies (following NUGET packages are automatically installed when we open and run the project), please recheck and add the references from NUGET

		- Microsoft.Extensions.Configuration.Json
		- Microsoft.Extensions.DependencyInjection
		- Newtonsoft.Json        
        - Microsoft.Extensions.Configuration.Binder
        - System.ServiceModel.Primitives


###Latest releases
- Initial release with all commits and changes as on June 2021


# Build and Test

 From the cmd, navigate to the cloned folder and go to "RSv3_DotNetCore/src"
    
 Run the following commands
    
 ```dotnet clean RS3SampleCode.sln```

 ```dotnet build RS3SampleCode.sln```


 Navigate from command prompt to RS3SampleCode.App folder and run below command

 ```dotnet run --project RS3SampleCode.App.csproj```

 This should open the application running in console.