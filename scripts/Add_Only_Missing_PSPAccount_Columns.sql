-- =====================================================
-- Script INTELIGENTE: Agregar solo columnas faltantes
-- Base de datos: SmartClick_prod
-- =====================================================

USE [SmartClick_prod];
GO

PRINT '?? Iniciando verificación de columnas en PSPAccounts...';
PRINT '============================================================';

-- Variables para contar
DECLARE @ColumnasAgregadas INT = 0;
DECLARE @ColumnasExistentes INT = 0;

-- 1. CVU
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PSPAccounts' AND COLUMN_NAME = 'CVU')
BEGIN
    ALTER TABLE [dbo].[PSPAccounts] ADD [CVU] NVARCHAR(100) NULL;
    PRINT '? [CVU] agregada';
    SET @ColumnasAgregadas = @ColumnasAgregadas + 1;
END
ELSE
BEGIN
    PRINT '??  [CVU] ya existe';
    SET @ColumnasExistentes = @ColumnasExistentes + 1;
END

-- 2. EncryptedPassword
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PSPAccounts' AND COLUMN_NAME = 'EncryptedPassword')
BEGIN
    ALTER TABLE [dbo].[PSPAccounts] ADD [EncryptedPassword] NVARCHAR(MAX) NULL;
    PRINT '? [EncryptedPassword] agregada';
    SET @ColumnasAgregadas = @ColumnasAgregadas + 1;
END
ELSE
BEGIN
    PRINT '??  [EncryptedPassword] ya existe';
    SET @ColumnasExistentes = @ColumnasExistentes + 1;
END

-- 3. LastC1ResponseJson
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PSPAccounts' AND COLUMN_NAME = 'LastC1ResponseJson')
BEGIN
    ALTER TABLE [dbo].[PSPAccounts] ADD [LastC1ResponseJson] NVARCHAR(MAX) NULL;
    PRINT '? [LastC1ResponseJson] agregada';
    SET @ColumnasAgregadas = @ColumnasAgregadas + 1;
END
ELSE
BEGIN
    PRINT '??  [LastC1ResponseJson] ya existe';
    SET @ColumnasExistentes = @ColumnasExistentes + 1;
END

-- 4. LastC7ResponseJson
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PSPAccounts' AND COLUMN_NAME = 'LastC7ResponseJson')
BEGIN
    ALTER TABLE [dbo].[PSPAccounts] ADD [LastC7ResponseJson] NVARCHAR(MAX) NULL;
    PRINT '? [LastC7ResponseJson] agregada';
    SET @ColumnasAgregadas = @ColumnasAgregadas + 1;
END
ELSE
BEGIN
    PRINT '??  [LastC7ResponseJson] ya existe';
    SET @ColumnasExistentes = @ColumnasExistentes + 1;
END

-- 5. LastStatusCheck
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PSPAccounts' AND COLUMN_NAME = 'LastStatusCheck')
BEGIN
    ALTER TABLE [dbo].[PSPAccounts] ADD [LastStatusCheck] DATETIME2 NULL;
    PRINT '? [LastStatusCheck] agregada';
    SET @ColumnasAgregadas = @ColumnasAgregadas + 1;
END
ELSE
BEGIN
    PRINT '??  [LastStatusCheck] ya existe';
    SET @ColumnasExistentes = @ColumnasExistentes + 1;
END

-- 6. TributaryIdentifierType
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PSPAccounts' AND COLUMN_NAME = 'TributaryIdentifierType')
BEGIN
    ALTER TABLE [dbo].[PSPAccounts] ADD [TributaryIdentifierType] NVARCHAR(50) NULL;
    PRINT '? [TributaryIdentifierType] agregada';
    SET @ColumnasAgregadas = @ColumnasAgregadas + 1;
END
ELSE
BEGIN
    PRINT '??  [TributaryIdentifierType] ya existe';
    SET @ColumnasExistentes = @ColumnasExistentes + 1;
END

-- 7. AccountTypeId
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PSPAccounts' AND COLUMN_NAME = 'AccountTypeId')
BEGIN
    ALTER TABLE [dbo].[PSPAccounts] ADD [AccountTypeId] INT NULL;
    PRINT '? [AccountTypeId] agregada';
    SET @ColumnasAgregadas = @ColumnasAgregadas + 1;
END
ELSE
BEGIN
    PRINT '??  [AccountTypeId] ya existe';
    SET @ColumnasExistentes = @ColumnasExistentes + 1;
END

-- 8. CVU_CBUAlias
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PSPAccounts' AND COLUMN_NAME = 'CVU_CBUAlias')
BEGIN
    ALTER TABLE [dbo].[PSPAccounts] ADD [CVU_CBUAlias] NVARCHAR(100) NULL;
    PRINT '? [CVU_CBUAlias] agregada';
    SET @ColumnasAgregadas = @ColumnasAgregadas + 1;
END
ELSE
BEGIN
    PRINT '??  [CVU_CBUAlias] ya existe';
    SET @ColumnasExistentes = @ColumnasExistentes + 1;
END

-- 9. CurrencyDescription
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PSPAccounts' AND COLUMN_NAME = 'CurrencyDescription')
BEGIN
    ALTER TABLE [dbo].[PSPAccounts] ADD [CurrencyDescription] NVARCHAR(100) NULL;
    PRINT '? [CurrencyDescription] agregada';
    SET @ColumnasAgregadas = @ColumnasAgregadas + 1;
END
ELSE
BEGIN
    PRINT '??  [CurrencyDescription] ya existe';
    SET @ColumnasExistentes = @ColumnasExistentes + 1;
END

-- 10. CurrencyName
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PSPAccounts' AND COLUMN_NAME = 'CurrencyName')
BEGIN
    ALTER TABLE [dbo].[PSPAccounts] ADD [CurrencyName] NVARCHAR(50) NULL;
    PRINT '? [CurrencyName] agregada';
    SET @ColumnasAgregadas = @ColumnasAgregadas + 1;
END
ELSE
BEGIN
    PRINT '??  [CurrencyName] ya existe';
    SET @ColumnasExistentes = @ColumnasExistentes + 1;
END

-- 11. CurrencySymbol
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PSPAccounts' AND COLUMN_NAME = 'CurrencySymbol')
BEGIN
    ALTER TABLE [dbo].[PSPAccounts] ADD [CurrencySymbol] NVARCHAR(10) NULL;
    PRINT '? [CurrencySymbol] agregada';
    SET @ColumnasAgregadas = @ColumnasAgregadas + 1;
END
ELSE
BEGIN
    PRINT '??  [CurrencySymbol] ya existe';
    SET @ColumnasExistentes = @ColumnasExistentes + 1;
END

-- 12. CurrencyTypeId
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PSPAccounts' AND COLUMN_NAME = 'CurrencyTypeId')
BEGIN
    ALTER TABLE [dbo].[PSPAccounts] ADD [CurrencyTypeId] INT NULL;
    PRINT '? [CurrencyTypeId] agregada';
    SET @ColumnasAgregadas = @ColumnasAgregadas + 1;
END
ELSE
BEGIN
    PRINT '??  [CurrencyTypeId] ya existe';
    SET @ColumnasExistentes = @ColumnasExistentes + 1;
END

-- 13. DeleteAccountSolicitude
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PSPAccounts' AND COLUMN_NAME = 'DeleteAccountSolicitude')
BEGIN
    ALTER TABLE [dbo].[PSPAccounts] ADD [DeleteAccountSolicitude] BIT NULL;
    PRINT '? [DeleteAccountSolicitude] agregada';
    SET @ColumnasAgregadas = @ColumnasAgregadas + 1;
END
ELSE
BEGIN
    PRINT '??  [DeleteAccountSolicitude] ya existe';
    SET @ColumnasExistentes = @ColumnasExistentes + 1;
END

-- 14. EntityStatus
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PSPAccounts' AND COLUMN_NAME = 'EntityStatus')
BEGIN
    ALTER TABLE [dbo].[PSPAccounts] ADD [EntityStatus] INT NULL;
    PRINT '? [EntityStatus] agregada';
    SET @ColumnasAgregadas = @ColumnasAgregadas + 1;
END
ELSE
BEGIN
    PRINT '??  [EntityStatus] ya existe';
    SET @ColumnasExistentes = @ColumnasExistentes + 1;
END

-- 15. EntityStatusDescription
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PSPAccounts' AND COLUMN_NAME = 'EntityStatusDescription')
BEGIN
    ALTER TABLE [dbo].[PSPAccounts] ADD [EntityStatusDescription] NVARCHAR(200) NULL;
    PRINT '? [EntityStatusDescription] agregada';
    SET @ColumnasAgregadas = @ColumnasAgregadas + 1;
END
ELSE
BEGIN
    PRINT '??  [EntityStatusDescription] ya existe';
    SET @ColumnasExistentes = @ColumnasExistentes + 1;
END

-- 16. StatusDescription
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PSPAccounts' AND COLUMN_NAME = 'StatusDescription')
BEGIN
    ALTER TABLE [dbo].[PSPAccounts] ADD [StatusDescription] NVARCHAR(200) NULL;
    PRINT '? [StatusDescription] agregada';
    SET @ColumnasAgregadas = @ColumnasAgregadas + 1;
END
ELSE
BEGIN
    PRINT '??  [StatusDescription] ya existe';
    SET @ColumnasExistentes = @ColumnasExistentes + 1;
END

-- Resumen final
PRINT '============================================================';
PRINT '?? RESUMEN:';
PRINT '   - Columnas agregadas: ' + CAST(@ColumnasAgregadas AS NVARCHAR(10));
PRINT '   - Columnas que ya existían: ' + CAST(@ColumnasExistentes AS NVARCHAR(10));
PRINT '============================================================';
PRINT '';

-- Mostrar todas las columnas finales
PRINT '?? COLUMNAS FINALES DE PSPAccounts:';
SELECT 
    ORDINAL_POSITION AS '#',
    COLUMN_NAME AS 'Columna',
    DATA_TYPE AS 'Tipo',
    IS_NULLABLE AS '¿NULL?'
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'PSPAccounts'
ORDER BY ORDINAL_POSITION;

PRINT '? Script completado exitosamente';
GO
