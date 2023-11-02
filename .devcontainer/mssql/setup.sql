--CREATE DATABASE ApplicationDB;
--GO

CREATE DATABASE MarketData
GO

USE MarketData
GO

CREATE LOGIN MarketData WITH PASSWORD = 'M@rk3tD@t@'
GO

CREATE USER MarketData FOR LOGIN MarketData
GO

EXEC sp_addrolemember 'db_owner', 'MarketData'
GO
