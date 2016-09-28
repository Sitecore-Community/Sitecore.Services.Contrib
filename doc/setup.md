# How we are setup

## Repository Structure

    |-/build
    |-/src
    |-/test
    |-/doc


### Build folder

Here you will find all the tasks and code related to the build process of the project. Please keep in mind, 
everything related with the build process should be contained inside this project.

## Building the Project

From the checkout directory just run `.\build` to invoke an automated release build of the project.

For more information about the build environment, please refer to the README.md located in the /build folder.

## Nuget

The Sitecore Services Contrib project is released on the nuget gallery.

The NuGet package produced by the automated build is based on the information contained inside the 
`./build/packaging/Sitecore.Services.Contrib.nuspec` file.

The dependencies for the package are defined under the `<dependencies>` node of the configuration file.

## Contributing

You are welcome to fork the project and make pull requests.

## Reporting a bug

If you have found a bug or want to request a features, you are welcome to do so in the bug tracker of the repository.