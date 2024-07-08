## Template Steps (Remove this when completed)
### Rename template placeholders
1) Rename `PRODUCT.sln` file and `PRODUCT.sql.DotSettings`
2) Update namespaces from `COMPANY_NAME.PRODUCT` for every project
   - Project -> properties -> Assembly name & Root namespace 
   - From the Solution -> Refactor This -> Adjust Namespaces
3) Update assembly name in `Program.cs` from `COMPANY_NAME.PRODUCT`
4) Update `PRODUCT` in `Dockerfile` 
5) Update `product-deps` in `Makefile`
6) Update `product-app` in `Makefile`
7) Update `PRODUCT` in `appsettings.json`
8) Update `DB_NAME` below in `README.md`
9) Update `DB_NAME` in `create-local-dbs.sql`
10) Update `DB_NAME` in `appsettings.json`
11) Update `DB_NAME` in `IntegrationDbFixture` and `FunctionalDbFixture`
12) Delete `/src/Infrastructure/Migrations` folder
---

# Architecture/Tooling

## `/src`
- [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [Fast Endpoints](https://fast-endpoints.com/) to replace standard MVC and is based off MinimalApi
  - FE configures and uses [FluentValidation](https://docs.fluentvalidation.net/en/latest/) for request validation 
- [MediatR](https://github.com/jbogard/MediatR) for use case separation into command and queries (CQRS)
- [AutoMapper](https://automapper.org/) for any object mapping needs
- [Lamar](https://jasperfx.github.io/lamar/) for IoC
- [EntityFramework](https://learn.microsoft.com/en-us/ef/core/) for data access
- [Serilog](https://serilog.net/) for structured logging

## `/tests`
- [Testing Pyramid](https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/test-asp-net-core-mvc-apps)
- [xUnit](https://xunit.net/) testing framework
- [moq](https://github.com/devlooped/moq) for mocking
- [AutoFixture](https://github.com/AutoFixture/AutoFixture) automates creating test fixtures and works with xUnit and moq to "autoMock" suts
- [FluentAssertions](https://fluentassertions.com/) for easy to read test assertions
- [Respawn](https://github.com/jbogard/Respawn) for resetting the DB to a known state before test execution
- [Verify](https://github.com/VerifyTests/Verify) for snapshot assertions
- [Bogus](https://github.com/bchavez/Bogus) is provided from the `FastEndpoint.Testing` base classes

# Project Setup

## Prerequisites
- NET8
- Docker
- make: `choco install make`
- Install required packages: `make install`

## Build & Run
- Run dependencies: `make deps`
- Run project
    - IDE: Run `Web` project
    - CLI: `make build` followed by `make run`
- Alternatively you can run `make` or `make all` to create dependencies and app in docker

## Local Docker Dependencies
- DB Server
    - Server: `localhost,1433` with username: `sa` and password `Localp@55`
    - Database `DB_NAME` will have seed data from `seed-database.sql`

## Makefile Targets
To run a target, simply type `make` followed by the target. i.e. `make build` will run the `build` target
- `docker-compose` - runs the docker compose to set up base instances of dependencies
- `db` - runs the db scripts to create them, migrate schema, and seed
- `deps` - sets up dependencies by running both `docker-compose` and `db`
- `build` - builds a new app docker image
- `run` - starts a container of the latest app image
- `stop` - stops and removes the app container
- `clean` - runs the `stop` target and tears downs docker compose images
- `test` - runs all tests within the docker image as the build
- `coverage` - runs all tests and collects coverage results in `tests/TestResults`
- `install` - installs required tools
- `migration` - with parameter `name=<name>` will create a new EF migration with name
- `migration-remove` - removes the last migration
- `db-script` - creates an idempotent SQL script for migrations
