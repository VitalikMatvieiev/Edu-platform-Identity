# Edu-platform_Identity
This is the Identity Service project for handling authentication, authorization, and user identity details. It ensures secure and efficient management of user credentials and identity information.

## Table of Contents

- [Introduction](#introduction)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
- [Usage](#usage)
- [Features](#features)
- [Security](#security)

## Introduction

IdentityService is designed to provide robust authentication and authorization functionalities for applications. It handles user credentials and identity details, ensuring secure access control and identity management. This service integrates seamlessly with various components of an application to provide a unified security framework.

## Getting Started

Follow these instructions to set up the Identity Service on your local machine for development and testing purposes.

### Prerequisites

The service is built on .NET 6 and uses the following key libraries:

IdentityServer4: for implementing OAuth 2.0 protocol.
Entity Framework Core: for handling database operations related to identity data.

### Installation

For non-Windows operating systems (Linux/MacOS), install .NET 6:

Install .NET on macOS/Linux (https://learn.microsoft.com/en-us/dotnet/core/install/macos)
Then, proceed with the following steps:

*# Clone the repository
git clone https://github.com/VitalikMatvieiev/Edu-platform-Identity.git

*# Change directory
cd IdentityService

*# Restore dependencies
dotnet restore

*# Build the project
dotnet build

*# Run the project
dotnet run

## Usage

The service configuration is stored in appsettings.json. This file includes settings for:
- Database connection strings.
- IdentityServer configurations.
- API resources and clients.

## Features

- User Authentication: Secure login mechanism for users.
- User Authorization: Manage access control based on user roles and claims.
- OAuth 2.0 and custom JWT middleware: Support for modern authentication and authorization protocols.
- User Management: CRUD operations for managing user credentials and details.

## Security

Emphasize the security practices and protocols implemented in the service, like encryption of sensitive data, secure token handling, and regular security updates.