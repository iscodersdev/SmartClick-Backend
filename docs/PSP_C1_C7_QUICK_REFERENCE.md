# PSP C1 y C7 - Guía Rápida para Desarrolladores

## Endpoints Implementados

### 1. C1: Obtener Datos de Cuenta del Usuario
```http
POST /api/psp/Entities/GetAccountData
Content-Type: application/json

{
  "UAT": "token_autenticacion"
}
```

**Respuesta**:
```json
{
  "Status": 200,
  "Success": true,
  "Accounts": [
    {
      "accountNumber": "30707609032-00000550",
      "cvU_CBU": "0000247100000000016016",
      "cvU_CBUAlias": "divix.py",
      "tributaryIdentifier": "30707609032",
      "name": "Divix paycloud",
      "entityId": 1601
    }
  ]
}
```

### 2. C7: Obtener Entidad por CUIT/CUIL
```http
POST /api/psp/Entities/GetEntityByTributaryId
Content-Type: application/json

{
  "UAT": "token_autenticacion",
  "TributaryIdentifier": "27422125073"
}
```

**Respuesta**:
```json
{
  "Status": 200,
  "Success": true,
  "Data": [
    {
      "entityName": "Brian Ariel Arrondo",
      "entityStatus": 1,
      "entityStatusDescription": "Activa",
      "accounts": [
        {
          "accountNumber": "20385321393-00000557",
          "cvU_CBU": "0000247100000000016511",
          "status": 1,
          "statusDescription": "Activa"
        }
      ]
    }
  ]
}
```

## Códigos de Estado

### Estados de Entidad (`entityStatus`)
- `1` = Activa
- `2` = Dada de baja
- `3` = Pendiente

### Estados de Cuenta (`status`)
- `1` = Activa
- `2` = Cerrada

## Mensajes Comunes del PSP

### C1 (GetAccountData)
- ? `200`: Cuenta obtenida exitosamente
- ? `400`: "No se pudo obtener el token del usuario PSP"
- ? `401`: "Usuario no autenticado"

### C7 (GetEntityByTributaryId)
- ? `200`: Entidad encontrada (o lista vacía si no existe)
- ? `400`: "TributaryIdentifier es requerido"
- ? `401`: "Usuario no autenticado"

## Ejemplos de Integración

### Frontend (JavaScript/TypeScript)

#### Obtener cuenta del usuario:
```javascript
async function getUserAccount(uat) {
  const response = await fetch('/api/psp/Entities/GetAccountData', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ UAT: uat })
  });
  
  const data = await response.json();
  
  if (data.Success && data.Accounts.length > 0) {
    console.log('Cuenta:', data.Accounts[0].cvU_CBU);
    console.log('Alias:', data.Accounts[0].cvU_CBUAlias);
    return data.Accounts[0];
  } else {
    console.error('No tiene cuenta PSP:', data.Mensaje);
    return null;
  }
}
```

#### Verificar estado de entidad por CUIT:
```javascript
async function checkEntityStatus(uat, cuit) {
  const response = await fetch('/api/psp/Entities/GetEntityByTributaryId', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ 
      UAT: uat,
      TributaryIdentifier: cuit 
    })
  });
  
  const data = await response.json();
  
  if (data.Success && data.Data.length > 0) {
    const entity = data.Data[0];
    
    switch (entity.entityStatus) {
      case 1:
        console.log('? Entidad ACTIVA:', entity.entityName);
        break;
      case 2:
        console.log('? Entidad DADA DE BAJA:', entity.entityName);
        break;
      case 3:
        console.log('? Entidad PENDIENTE:', entity.entityName);
        break;
    }
    
    return entity;
  } else {
    console.error('Entidad no encontrada para CUIT:', cuit);
    return null;
  }
}
```

### Backend (C#)

#### Verificar si usuario tiene cuenta PSP:
```csharp
public async Task<bool> UsuarioTieneCuentaPSP(string uat)
{
    var request = new PSPBaseResponseDTO { UAT = uat };
    var response = await _httpClient.PostAsJsonAsync(
        "/api/psp/Entities/GetAccountData", 
        request
    );
    
    var data = await response.Content.ReadAsAsync<AccountsInfoWithUATResponseDTO>();
    
    return data.Success && data.Accounts.Any();
}
```

#### Obtener todas las cuentas de una entidad:
```csharp
public async Task<List<EntityAccountStatus>> ObtenerCuentasEntidad(string uat, string cuit)
{
    var request = new EntityStatusWithUATRequestDTO 
    { 
        UAT = uat,
        TributaryIdentifier = cuit 
    };
    
    var response = await _httpClient.PostAsJsonAsync(
        "/api/psp/Entities/GetEntityByTributaryId", 
        request
    );
    
    var data = await response.Content.ReadAsAsync<EntityStatusWithUATResponseDTO>();
    
    if (data.Success && data.Data.Any())
    {
        return data.Data[0].Accounts;
    }
    
    return new List<EntityAccountStatus>();
}
```

## Troubleshooting

### Error: "No se pudo obtener el token del usuario PSP"
**Causa**: El usuario no tiene credenciales PSP guardadas en la tabla `PSPAccount`.

**Solución**:
1. Verificar que existe un registro en `PSPAccount` para el usuario
2. Verificar que `UserName` y `EncryptedPassword` no sean null
3. Ejecutar el endpoint `/api/psp/Entities/GuardarCredencialesPSP` primero

### Error: "Usuario no autenticado" (401)
**Causa**: El UAT proporcionado es inválido o ha expirado.

**Solución**:
1. Regenerar el UAT haciendo login nuevamente
2. Verificar que el UAT existe en la tabla `UAT`

### Error: "TributaryIdentifier es requerido" (400)
**Causa**: No se envió el campo `TributaryIdentifier` en C7.

**Solución**:
```json
{
  "UAT": "tu_uat",
  "TributaryIdentifier": "27422125073"  // ? Agregar este campo
}
```

### Respuesta vacía en C7 pero Success=true
**Causa**: El CUIT consultado no existe o está en proceso de validación en el PSP.

**Interpretación**: Esto es normal - el PSP devuelve `[]` cuando:
- El CUIT nunca se registró
- La entidad está en revisión
- La entidad fue rechazada

## Testing en Postman

### Collection Variables
```
baseUrl: https://localhost:5001
uat: [obtener desde login]
```

### Request C1
```
POST {{baseUrl}}/api/psp/Entities/GetAccountData
Body (raw JSON):
{
  "UAT": "{{uat}}"
}
```

### Request C7
```
POST {{baseUrl}}/api/psp/Entities/GetEntityByTributaryId
Body (raw JSON):
{
  "UAT": "{{uat}}",
  "TributaryIdentifier": "27422125073"
}
```

## Notas Importantes

1. **C1 usa UserToken PSP** (token del usuario en el PSP)
2. **C7 usa SystemToken** (token general del sistema)
3. **NO se personalizan errores** - se devuelve lo que el PSP responde
4. **Gestión automática de tokens** - no necesitas enviar el UserToken manualmente
5. **RawResponse disponible** en C7 para debugging

## Documentación Completa

Ver: `docs/PSP_C1_C7_IMPLEMENTATION_SUMMARY.md`
