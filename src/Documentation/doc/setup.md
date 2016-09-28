# Sitecore.Services.Contrib Project Setup

## Repository Structure

    |-/build
    |-/src
    |-/test


### Build folder

Here you will find all the tasks and code related to the build process of the project. Please keep in mind, 
everything related with the build process should be contained inside this project.


## Building the Project

From the checkout directory just run invoke the commaind line build of the project as follows:

```
    .\build.cmd
```

For more information about the build environment, please refer to the README.md located in the *build* folder.


## Nuget Packaging

The NuGet package produced by the automated build is based on the information contained inside the 
*build\packaging\Sitecore.Services.Contrib.nuspec* file.

Package dependencies are defined under the *dependencies* node of the *.nuspec* configuration file.