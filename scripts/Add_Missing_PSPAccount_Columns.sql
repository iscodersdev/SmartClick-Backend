-- =====================================================
-- Script: Agregar columnas faltantes a PSPAccounts
-- Base de datos: SmartClick_prod
-- Fecha: 2025-01-07
-- =====================================================

USE [SmartClick_prod]; -- ?? AJUSTA EL NOMBRE DE TU BASE DE DATOS
GO

PRINT '?? Verificando columnas existentes en PSPAccounts...';
GO

-- Verificar columnas actuales
SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE, CHARACTER_MAXIMUM_LENGTH
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'PSPAccounts'
ORDER BY ORDINAL_POSITION;
GO

PRINT '-----------------------------------------------------';
PRINT '?? Agregando columnas faltantes...';
PRINT '-----------------------------------------------------';

-- 1. Agregar CVU (si no existe)
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
               WHERE TABLE_NAME = 'PSPAccounts' AND COLUMN_NAME = 'CVU')
BEGIN
    ALTER TABLE [dbo].[PSPAccounts] ADD [CVU] NVARCHAR(100) NULL;
    PRINT '? Columna [CVU] agregada';
END
ELSE
    PRINT '?? Columna [CVU] ya existe';
GO

-- 2. Agregar EncryptedPassword (si no existe)
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
               WHERE TABLE_NAME = 'PSPAccounts' AND COLUMN_NAME = 'EncryptedPassword')
BEGIN
    ALTER TABLE [dbo].[PSPAccounts] ADD [EncryptedPassword] NVARCHAR(MAX) NULL;
    PRINT '? Columna [EncryptedPassword] agregada';
END
ELSE
    PRINT '?? Columna [EncryptedPassword] ya existe';
GO

-- 3. Agregar LastC1ResponseJson (si no existe)
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
               WHERE TABLE_NAME = 'PSPAccounts' AND COLUMN_NAME = 'LastC1ResponseJson')
BEGIN
    ALTER TABLE [dbo].[PSPAccounts] ADD [LastC1ResponseJson] NVARCHAR(MAX) NULL;
    PRINT '? Columna [LastC1ResponseJson] agregada';
END
ELSE
    PRINT '?? Columna [LastC1ResponseJson] ya existe';
GO

-- 4. Agregar LastC7ResponseJson (si no existe)
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
               WHERE TABLE_NAME = 'PSPAccounts' AND COLUMN_NAME = 'LastC7ResponseJson')
BEGIN
    ALTER TABLE [dbo].[PSPAccounts] ADD [LastC7ResponseJson] NVARCHAR(MAX) NULL;
    PRINT '? Columna [LastC7ResponseJson] agregada';
END
ELSE
    PRINT '?? Columna [LastC7ResponseJson] ya existe';
GO

-- 5. Agregar LastStatusCheck (si no existe)
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
               WHERE TABLE_NAME = 'PSPAccounts' AND COLUMN_NAME = 'LastStatusCheck')
BEGIN
    ALTER TABLE [dbo].[PSPAccounts] ADD [LastStatusCheck] DATETIME2 NULL;
    PRINT '? Columna [LastStatusCheck] agregada';
END
ELSE
    PRINT '?? Columna [LastStatusCheck] ya existe';
GO

-- 6. Agregar TributaryIdentifierType (si no existe)
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
               WHERE TABLE_NAME = 'PSPAccounts' AND COLUMN_NAME = 'TributaryIdentifierType')
BEGIN
    ALTER TABLE [dbo].[PSPAccounts] ADD [TributaryIdentifierType] NVARCHAR(50) NULL;
    PRINT '? Columna [TributaryIdentifierType] agregada';
END
ELSE
    PRINT '?? Columna [TributaryIdentifierType] ya existe';
GO

-- 7. Agregar AccountTypeId (si no existe)
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
               WHERE TABLE_NAME = 'PSPAccounts' AND COLUMN_NAME = 'AccountTypeId')
BEGIN
    ALTER TABLE [dbo].[PSPAccounts] ADD [AccountTypeId] INT NULL;
    PRINT '? Columna [AccountTypeId] agregada';
END
ELSE
    PRINT '?? Columna [AccountTypeId] ya existe';
GO

-- 8. Agregar CVU_CBUAlias (si no existe)
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
               WHERE TABLE_NAME = 'PSPAccounts' AND COLUMN_NAME = 'CVU_CBUAlias')
BEGIN
    ALTER TABLE [dbo].[PSPAccounts] ADD [CVU_CBUAlias] NVARCHAR(100) NULL;
    PRINT '? Columna [CVU_CBUAlias] agregada';
END
ELSE
    PRINT '?? Columna [CVU_CBUAlias] ya existe';
GO

-- 9. Agregar CurrencyDescription (si no existe)
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
               WHERE TABLE_NAME = 'PSPAccounts' AND COLUMN_NAME = 'CurrencyDescription')
BEGIN
    ALTER TABLE [dbo].[PSPAccounts] ADD [CurrencyDescription] NVARCHAR(100) NULL;
    PRINT '? Columna [CurrencyDescription] agregada';
END
ELSE
    PRINT '?? Columna [CurrencyDescription] ya existe';
GO

-- 10. Agregar CurrencyName (si no existe)
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
               WHERE TABLE_NAME = 'PSPAccounts' AND COLUMN_NAME = 'CurrencyName')
BEGIN
    ALTER TABLE [dbo].[PSPAccounts] ADD [CurrencyName] NVARCHAR(50) NULL;
    PRINT '? Columna [CurrencyName] agregada';
END
ELSE
    PRINT '?? Columna [CurrencyName] ya existe';
GO

-- 11. Agregar CurrencySymbol (si no existe)
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
               WHERE TABLE_NAME = 'PSPAccounts' AND COLUMN_NAME = 'CurrencySymbol')
BEGIN
    ALTER TABLE [dbo].[PSPAccounts] ADD [CurrencySymbol] NVARCHAR(10) NULL;
    PRINT '? Columna [CurrencySymbol] agregada';
END
ELSE
    PRINT '?? Columna [CurrencySymbol] ya existe';
GO

-- 12. Agregar CurrencyTypeId (si no existe)
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
               WHERE TABLE_NAME = 'PSPAccounts' AND COLUMN_NAME = 'CurrencyTypeId')
BEGIN
    ALTER TABLE [dbo].[PSPAccounts] ADD [CurrencyTypeId] INT NULL;
    PRINT '? Columna [CurrencyTypeId] agregada';
END
ELSE
    PRINT '?? Columna [CurrencyTypeId] ya existe';
GO

-- 13. Agregar DeleteAccountSolicitude (si no existe)
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
               WHERE TABLE_NAME = 'PSPAccounts' AND COLUMN_NAME = 'DeleteAccountSolicitude')
BEGIN
    ALTER TABLE [dbo].[PSPAccounts] ADD [DeleteAccountSolicitude] BIT NULL;
    PRINT '? Columna [DeleteAccountSolicitude] agregada';
END
ELSE
    PRINT '?? Columna [DeleteAccountSolicitude] ya existe';
GO

-- 14. Agregar EntityStatus (si no existe)
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
               WHERE TABLE_NAME = 'PSPAccounts' AND COLUMN_NAME = 'EntityStatus')
BEGIN
    ALTER TABLE [dbo].[PSPAccounts] ADD [EntityStatus] INT NULL;
    PRINT '? Columna [EntityStatus] agregada';
END
ELSE
    PRINT '?? Columna [EntityStatus] ya existe';
GO

-- 15. Agregar EntityStatusDescription (si no existe)
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
               WHERE TABLE_NAME = 'PSPAccounts' AND COLUMN_NAME = 'EntityStatusDescription')
BEGIN
    ALTER TABLE [dbo].[PSPAccounts] ADD [EntityStatusDescription] NVARCHAR(200) NULL;
    PRINT '? Columna [EntityStatusDescription] agregada';
END
ELSE
    PRINT '?? Columna [EntityStatusDescription] ya existe';
GO

-- 16. Agregar StatusDescription (si no existe)
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
               WHERE TABLE_NAME = 'PSPAccounts' AND COLUMN_NAME = 'StatusDescription')
BEGIN
    ALTER TABLE [dbo].[PSPAccounts] ADD [StatusDescription] NVARCHAR(200) NULL;
    PRINT '? Columna [StatusDescription] agregada';
END
ELSE
    PRINT '?? Columna [StatusDescription] ya existe';
GO

PRINT '-----------------------------------------------------';
PRINT '? SCRIPT COMPLETADO';
PRINT '-----------------------------------------------------';

-- Verificar columnas finales
PRINT '?? Verificando columnas después del script...';
SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE, CHARACTER_MAXIMUM_LENGTH
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'PSPAccounts'
ORDER BY ORDINAL_POSITION;
GO

PRINT '? Todas las columnas han sido verificadas/agregadas exitosamente.';
GO
