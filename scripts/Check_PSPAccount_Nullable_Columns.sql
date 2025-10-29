-- =====================================================
-- Script: Verificar columnas de PSPAccounts y sus propiedades NULL
-- Base de datos: SmartClick_prod
-- =====================================================

USE [SmartClick_prod];
GO

PRINT '?? Verificando TODAS las columnas de PSPAccounts...';
PRINT '-----------------------------------------------------';

-- Consulta completa de columnas con información de NULL
SELECT 
    COLUMN_NAME AS 'Columna',
    DATA_TYPE AS 'Tipo de Dato',
    IS_NULLABLE AS '¿Permite NULL?',
    CHARACTER_MAXIMUM_LENGTH AS 'Longitud Máxima',
    COLUMN_DEFAULT AS 'Valor por Defecto',
    ORDINAL_POSITION AS 'Posición'
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'PSPAccounts'
ORDER BY ORDINAL_POSITION;

PRINT '-----------------------------------------------------';
PRINT '?? Verificando columnas que NO permiten NULL...';
PRINT '-----------------------------------------------------';

-- Columnas que NO permiten NULL
SELECT 
    COLUMN_NAME AS 'Columna (NOT NULL)',
    DATA_TYPE AS 'Tipo'
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'PSPAccounts'
  AND IS_NULLABLE = 'NO'
ORDER BY ORDINAL_POSITION;

PRINT '-----------------------------------------------------';
PRINT '? Verificación completada';
GO
