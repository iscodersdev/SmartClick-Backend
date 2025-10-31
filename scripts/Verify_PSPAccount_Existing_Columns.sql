-- =====================================================
-- Script: Verificar columnas existentes en PSPAccounts
-- =====================================================

USE [SmartClick_prod];
GO

PRINT '?? COLUMNAS EXISTENTES EN PSPAccounts:';
PRINT '-----------------------------------------------------';

SELECT 
    ORDINAL_POSITION AS '#',
    COLUMN_NAME AS 'Columna',
    DATA_TYPE AS 'Tipo',
    IS_NULLABLE AS '¿NULL?',
    CHARACTER_MAXIMUM_LENGTH AS 'Longitud'
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'PSPAccounts'
ORDER BY ORDINAL_POSITION;

PRINT '-----------------------------------------------------';
PRINT '? Verificación completada';
GO
