USE [master]
GO
/* For security reasons the login is created disabled and with a random password. */
CREATE LOGIN [DietrichUser] WITH PASSWORD=N'8RgaXffR8FCWmoK8+90eq+7SCj+DDXKK0eNnWHyFy1Y=', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=ON
GO
ALTER LOGIN [DietrichUser] DISABLE
GO
