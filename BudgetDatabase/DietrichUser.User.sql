USE [Budget]
GO
CREATE USER [DietrichUser] FOR LOGIN [DietrichUser] WITH DEFAULT_SCHEMA=[songbook]
GO
ALTER ROLE [db_datareader] ADD MEMBER [DietrichUser]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [DietrichUser]
GO
