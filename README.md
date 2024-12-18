# troodon

**Fast, easy and timesaving project builder for .NET web apis.**

## Table of contents

- [Installation](#Installation)
- [Features](#Features)
- [Quick Start](#Quick-Start)
- [License](#License)

## Installation

[download .NET](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) project. .NET 8 or higher is required.
[download Node.js](https://nodejs.org/en/download/) project. Node.js 18 or higher is required.

## Features

- Feature folder architecture.
- EF Core setup.
- [Swagger](https://swagger.io/).
- Generates
  - Interfaces, Controllers, Services and Repositories with CRUD operations 
  - Basic Crud operations implemented
  - Dependency injects lower level services

### Quick Start

#### Node and npm usage
Install the tool
```sh
npm install -g troodon
```

Run the builder command and follow the instructions.
```sh
troodon build
```

#### Dotnet and NuGet usage
Install the tool
```sh
dotnet tool install -g troodon
```

Run the builder command and follow the instructions.
```sh
troodon
```