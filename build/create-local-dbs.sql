IF DB_ID('DB_NAME') IS NOT NULL
    BEGIN
        ALTER DATABASE [DB_NAME] set single_user with rollback immediate
        DROP DATABASE [DB_NAME];
    END;
GO
CREATE DATABASE [DB_NAME];
GO
