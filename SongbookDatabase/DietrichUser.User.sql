CREATE USER [DietrichUser] FOR LOGIN [DietrichUser] WITH DEFAULT_SCHEMA=[Songbook]
GO
sys.sp_addrolemember @rolename = N'db_datareader', @membername = N'DietrichUser'
GO
sys.sp_addrolemember @rolename = N'db_datawriter', @membername = N'DietrichUser'
GO
