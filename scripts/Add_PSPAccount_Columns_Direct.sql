-- =====================================================
-- Script DIRECTO: Agregar columnas faltantes a PSPAccounts
-- Base de datos: SmartClick_prod
-- NOTA: Este script NO valida si las columnas existen
-- =====================================================

USE [SmartClick_prod];
GO

BEGIN TRANSACTION;

BEGIN TRY
    PRINT '?? Agregando columnas faltantes a PSPAccounts...';
    
    -- Agregar todas las columnas de una vez
    ALTER TABLE [dbo].[PSPAccounts] ADD 
        [CVU] NVARCHAR(100) NULL,
        [EncryptedPassword] NVARCHAR(MAX) NULL,
        [LastC1ResponseJson] NVARCHAR(MAX) NULL,
        [LastC7ResponseJson] NVARCHAR(MAX) NULL,
        [LastStatusCheck] DATETIME2 NULL,
        [TributaryIdentifierType] NVARCHAR(50) NULL,
        [AccountTypeId] INT NULL,
        [CVU_CBUAlias] NVARCHAR(100) NULL,
        [CurrencyDescription] NVARCHAR(100) NULL,
        [CurrencyName] NVARCHAR(50) NULL,
        [CurrencySymbol] NVARCHAR(10) NULL,
        [CurrencyTypeId] INT NULL,
        [DeleteAccountSolicitude] BIT NULL,
        [EntityStatus] INT NULL,
        [EntityStatusDescription] NVARCHAR(200) NULL,
        [StatusDescription] NVARCHAR(200) NULL;
    
    COMMIT TRANSACTION;
    PRINT '? Columnas agregadas exitosamente';
    
    -- Verificar columnas
    SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE
    FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'PSPAccounts'
    ORDER BY ORDINAL_POSITION;
    
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;
    PRINT '? ERROR al agregar columnas:';
    PRINT ERROR_MESSAGE();
END CATCH;
GO
