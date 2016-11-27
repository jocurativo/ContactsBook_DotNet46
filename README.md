# Contacts Book API example using dotnet 4.6

API example using best practices:
- DDD (services, repositories and entities)
- Unit of work
- IoC (Autofac)
- TDD (RhinoMocks)
- Multiple repositories implementation: ORM (EF) and Sql (using Dapper).

Included a postman collection with all cases

## Instructions ##

Create the database and change the connection string on the web.config file.

To run the EF version:
- Set the unit of work type as EFUnitOfWork on the Global.asax
- Open the package manager console and select the EF project
- run update-database

To run the dapper version:
- Set the unit of work type as DapperUnitOfWork on the Global.asax
- Open a query on the database
- run the provided sql script

