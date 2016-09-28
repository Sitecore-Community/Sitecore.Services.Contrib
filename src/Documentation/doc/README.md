# Sitecore.Services.Contrib Project

Welcome to the [Sitecore.Services.Contrib](https://github.com/Sitecore-Community/Sitecore.Services.Contrib) project. The project provides extensions to the Sitecore Services Client framework and Web API 2.


## Development Tooling

### Source Control and Branching Workflow

* [Git](https://git-scm.com/)
* [Gitflow](https://www.atlassian.com/git/tutorials/comparing-workflows/gitflow-workflow)
* [GitVersion](https://github.com/GitTools/GitVersion)


### Configuring a Local Development Machine

The [Chocolatey](https://chocolatey.org/install) package manager can be used to install all of the tooling required to work on the repository.


```
    choco install git
    choco install gitversion.portable
```

Intialising Gitflow for the repository is required if you wish to work with this extension. When prompted, use the defaults provided 
by the `git flow init`command to initialise the branching workflow.

```
    git flow init
```


### Command Line Build

The project employs [MSBuild](https://msdn.microsoft.com/en-us/library/0k6kkbsd.aspx) for its build system.

To run the build from the command line enter the following from the root of the repository

```
    build.cmd
```

More information can be found in the [Project Setup](setup.md) documentation.


## Contributing

You are welcome to fork the project and make pull requests.

Anyone can collaborate to the Sitecore.Services.Contrib project. In order to do so, please read the [Project Setup](setup.md) documentation. 

Previous [contributors](who.md) are duly honoured.


## Reporting a bug

If you have found a bug or want to request a features, you are welcome to do so in the bug tracker of the repository.