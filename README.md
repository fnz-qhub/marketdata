# Introduction 
TODO: This is an example project to test some of the core skills we require at FNZ Q-Hub. 

# Getting Started
1.	Installation process
2.	Software dependencies
3.	Configuration
4.	Task Notes

## 1. Install process

You will need to have .NET 7.0 installed and an appropriate IDE (Visual Studio, Visual Studio Code, or JetBrains Rider)
* Install .NET SDK: https://dotnet.microsoft.com/en-us/download/visual-studio-sdks
* Install Visual Studio (such as free Community Edition): https://visualstudio.microsoft.com/vs/community/
* OR Install Visual Studio Code: https://code.visualstudio.com/download
* OR JetBrains Rider: https://www.jetbrains.com/help/rider/Installation_guide.html

## 2. Software dependencies

The unit tests run with in-memory instances, but to run the API you will need either
a local installation of `SQL Server Express with LocalDb` or a access to a full 
`SQL Server` instance (dev and express are both fine), and an instance of `MongoDb`.

You can install these locally, or run them via Docker.
* To install Docker: https://www.docker.com/get-started/
* To install SQL Server Express with LocalDb: https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver16
* To run SQL Server in Docker: https://hub.docker.com/_/microsoft-mssql-server
    * e.g. `docker run --name sqlserver -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=yourStrong(!)Password" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest`
* To install MongoDb: https://www.mongodb.com/docs/manual/administration/install-community/
* To run MongoDb within Docker: `docker run --rm -d -p 27017:27017 -e ALLOW_EMPTY_PASSWORD=yes --name mongo bitnami/mongodb:latest`

## 3. Configuration

If you use a full installation of SQL Server (not LocalDb) then you will need to create the `MarketData` database and user manually, and amend the relevant connection string as required (in `AppSettings.json` and `MarketDataDesignTimeDbContextFactory.cs`).

You can create the database and user using `sqlcmd`:
* Install sqlcmd: https://learn.microsoft.com/en-us/sql/tools/sqlcmd/sqlcmd-utility?view=sql-server-ver16&tabs=odbc%2Cwindows&pivots=cs1-bash#download-and-install-sqlcmd
* `sqlcmd -U sa -P yourStrong(!)Password -Q "CREATE DATABASE MarketData"`
* `sqlcmd -U sa -P yourStrong(!)Password -Q "CREATE LOGIN MarketData WITH PASSWORD = 'M@rk3tD@t@'"`
* `sqlcmd -U sa -P yourStrong(!)Password -Q "USE MarketData; CREATE USER MarketData FOR LOGIN MarketData"`
* `sqlcmd -U sa -P yourStrong(!)Password -Q "USE MarketData; EXEC sp_addrolemember 'db_owner', 'MarketData'"`

You can toggle which database is used in the API project using the `#define` statements at the top of `Program.cs`

When using SQL Server (full or LocalDb) then you will need to apply migrations to confgure the database schema. 
* To apply migrations to LocalDb:
```
cd MarketData.Db.EF
dotnet ef database update
```
* OR to apply migrations to a full SQL Server instance
```
cd MarketData.Db.EF
dotnet ef database update -- --full
```