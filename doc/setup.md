#How we are setup

##Automated

This project is automated and each commit into the **MASTER** branch will run the tests and prepare a package which will be ready to use.

##Fork

You are welcome to fork the project and make pull requests.

##Reporting a bug

If you have found a bug or want to request a features, you are welcome to do so in the bug tracker of the repository.

##Code source's structure

    |-/build
    |-/lib
    |-/src
    |-/test
    |-/doc


#Build folder

Here you will find all the tasks and code related to the build process of the project. Please keep in mind, everything related with the build process should be contained inside this project.


##How to run the build
From the checkout directory just run `.\build` to invoke an automated release build of the project.

For more information about the build environment, please refer to the README.md located in the /build folder.

##nuget

The Sitecore Services Contrib project is released on the nuget gallery.

###configuration
The package is build based on the information contained inside the `./build/packaging/Sitecore.Services.Contrib.nuspec`

###dependencies

The dependencies for the package needs to be defined under the `<dependencies>` node of the configuration file.

Please note, any addition to the dependencies list **must be approved** by the Lead developer and the Architect.

#Lib folder

This folder contains the library which are not published in nuget gallery. Ex: the sitecore's kernel.

#src folder

    -/src|-sitecore.services.contrib


#test folder

All the tests

#doc folder

All the documentation in relation of the Sitecore Services Contrib project.