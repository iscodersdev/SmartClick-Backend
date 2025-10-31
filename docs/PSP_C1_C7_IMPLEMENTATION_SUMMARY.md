# PSP C1 y C7 - Implementación de Endpoints

## Resumen de Implementación

Se implementaron correctamente los endpoints C1 y C7 del PSP en el controller `EntitiesController.cs` para consultar datos de cuenta y entidades.

## Endpoints Implementados

### 1. C1: GetAccountData (Accounts/All/Get)
**URL**: `POST /api/psp/Entities/GetAccountData`

**Descripción**: Consulta los datos de la cuenta del usuario logueado (con userToken PSP).

**Request DTO**:
```json
{
  "UAT": "token_autenticacion_smartclick"
}
```

**Response DTO**:
```json
{
  "Status": 200,
  "UAT": "token_autenticacion_smartclick",
  "Mensaje": "Datos de cuenta obtenidos exitosamente",
  "Success": true,
  "Accounts": [
    {
      "accountNumber": "30707609032-00000550",
      "accountTypeId": 1,
      "tributaryIdentifierType": "CUIT",
      "tributaryIdentifier": "30707609032",
      "currencyDescription": "Pesos Argentinos",
      "currencyName": "Pesos",
      "currencySymbol": "ARS",
      "currencyTypeId": 1,
      "cvU_CBU": "0000247100000000016016",
      "cvU_CBUAlias": "divix.py",
      "name": "Divix paycloud",
      "deleteAccountSolicitude": false,
      "entityId": 1601
    }
  ]
}
```

**Casos de Respuesta**:
- **200 OK**: Datos de cuenta obtenidos exitosamente
- **401 Unauthorized**: UAT inválido o usuario no autenticado
- **400 Bad Request**: No se pudo obtener el UserToken PSP
- **500 Internal Server Error**: Error en la llamada al PSP

**Notas Importantes**:
- ? **NO** se usa el token de cuenta recaudadora
- ? Busca automáticamente el UserToken del PSP desde la base de datos (tabla `PSPAccount`)
- ? Si el token está expirado, intenta obtener uno nuevo automáticamente
- ? Si no hay credenciales PSP guardadas, devuelve un error claro
- ? Devuelve SIEMPRE el response del PSP sin personalizar errores (para no "adivinar" qué pasó)

---

### 2. C7: GetEntityByTributaryId (Accounts/Children/Get)
**URL**: `POST /api/psp/Entities/GetEntityByTributaryId`

**Descripción**: Obtiene la entidad hija por su identificador tributario (CUIT/CUIL).

**Request DTO**:
```json
{
  "UAT": "token_autenticacion_smartclick",
  "TributaryIdentifier": "27422125073"
}
```

**Response DTO**:
```json
{
  "Status": 200,
  "UAT": "token_autenticacion_smartclick",
  "Mensaje": "Entidad obtenida exitosamente",
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
          "cvU_CBUAlias": "karto.cesio.jornb.pc",
          "status": 1,
          "statusDescription": "Activa",
          "entityId": 12345
        }
      ]
    }
  ],
  "RawResponse": "{...}" // JSON crudo del PSP para debugging
}
```

**Ejemplos de Respuesta**:

1. **Entidad Activa**:
```json
{
  "Status": 200,
  "Success": true,
  "Data": [
    {
      "entityName": "Brian Ariel Arrondo",
      "entityStatus": 1,
      "entityStatusDescription": "Activa",
      "accounts": [...]
    }
  ]
}
```

2. **Entidad Pendiente** (sin cuentas):
```json
{
  "Status": 200,
  "Success": true,
  "Data": [
    {
      "entityName": "gastronomico 006",
      "entityStatus": 3,
      "entityStatusDescription": "Pendiente",
      "accounts": []
    }
  ]
}
```

3. **Entidad Dada de Baja**:
```json
{
  "Status": 200,
  "Success": true,
  "Data": [
    {
      "entityName": "angular04",
      "entityStatus": 2,
      "entityStatusDescription": "Dada de baja",
      "accounts": [
        {
          "accountNumber": "27422125073-00000465",
          "status": 2,
          "statusDescription": "Cerrada"
        }
      ]
    }
  ]
}
```

**Casos de Respuesta**:
- **200 OK**: Entidad encontrada (puede tener múltiples entidades asociadas al CUIT)
- **401 Unauthorized**: UAT inválido o usuario no autenticado
- **400 Bad Request**: TributaryIdentifier no proporcionado o error del PSP
- **500 Internal Server Error**: Error en la llamada al PSP

**Notas Importantes**:
- ? Usa el **token del sistema** (no del usuario) para hacer la consulta
- ? Puede devolver múltiples entidades para un mismo CUIT (entidad padre e hijas)
- ? Incluye el campo `RawResponse` con el JSON crudo del PSP para debugging
- ? Si el CUIT no existe o está en revisión, el PSP devuelve error específico

---

## Arquitectura de la Implementación

### 1. Flujo de Autenticación

#### Para C1 (GetAccountData):
```
Cliente ? [UAT] ? EntitiesController.GetAccountData()
                    ?
                  TraeUsuarioUAT(uat) ? Usuario
                    ?
                  ObtenerUserTokenPSP(usuario) ? UserToken PSP
                    ?
                  PSPService.GetAccountDataAsync(userToken)
                    ?
                  PSP: /a/multicuenta/api/v1/Accounts/All/Get
                    ?
                  Response ? AccountsInfoWithUATResponseDTO
```

#### Para C7 (GetEntityByTributaryId):
```
Cliente ? [UAT + TributaryIdentifier] ? EntitiesController.GetEntityByTributaryId()
                                          ?
                                        TraeUsuarioUAT(uat) ? Usuario
                                          ?
                                        PSPService.GetAccessTokenAsync() ? System Token
                                          ?
                                        PSPService.GetEntityByTributaryIdAsync(cuit, systemToken)
                                          ?
                                        PSP: /a/multicuenta/api/v1/Accounts/Children/Get?TributaryIdentifier={cuit}
                                          ?
                                        Response ? EntityStatusWithUATResponseDTO
```

### 2. Métodos Helper Utilizados

#### `ObtenerUserTokenPSP(usuario)`
Este método es CLAVE para C1 y gestiona inteligentemente el token del usuario PSP:

1. **Busca token en BD** (`PSPAccount.EncryptedUserToken`)
2. **Valida expiración** (con buffer de 5 minutos)
3. **Desencripta** usando `common.Decrypt()`
4. Si está expirado o no existe:
   - **Intenta obtener uno nuevo** llamando `GetAccessTokenUserAsync()`
   - Usa credenciales de `PSPAccount` (si existen)
   - Fallback a credenciales de `Cliente.Password`
5. **Guarda el nuevo token** encriptado en BD
6. **Actualiza `TokenExpiry`** para próximas validaciones

**Ventajas**:
- ? No requiere que el cliente envíe el UserToken en cada request
- ? Reutiliza tokens válidos (mejora performance)
- ? Se auto-regenera cuando expira
- ? Centraliza la lógica de gestión de tokens

---

## DTOs Nuevos Creados

### 1. `EntityStatusWithUATResponseDTO.cs`
```csharp
using DAL.DTOs.PSP;
using System.Collections.Generic;

namespace DAL.DTOs.API
{
    public class EntityStatusWithUATResponseDTO : PSPBaseResponseDTO
    {
        public List<EntityStatusData> Data { get; set; }
        public string RawResponse { get; set; }
    }
}
```

**Ubicación**: `DAL/DTOs/API/EntityStatusWithUATResponseDTO.cs`

**Propósito**: Envolver la respuesta de C7 con campos estándar de UAT, Status, Mensaje, Success.

---

## Estados Posibles de Entidades y Cuentas

### Estados de Entidad (`entityStatus`)
- **1**: Activa
- **2**: Dada de baja
- **3**: Pendiente (en proceso de validación)

### Estados de Cuenta (`status`)
- **1**: Activa
- **2**: Cerrada

---

## Casos de Uso Recomendados

### Caso 1: Verificar si el usuario tiene cuenta PSP activa
```csharp
POST /api/psp/Entities/GetAccountData
{
  "UAT": "usuario_uat_token"
}

// Si Success=true y Accounts.Length > 0 ? Tiene cuenta
// Si Success=false ? No tiene cuenta o no está activa
```

### Caso 2: Verificar estado de una entidad hija por CUIT
```csharp
POST /api/psp/Entities/GetEntityByTributaryId
{
  "UAT": "admin_uat_token",
  "TributaryIdentifier": "27422125073"
}

// Analizar Data[0].entityStatus:
// 1 = Activa ? Puede operar
// 2 = Dada de baja ? Cuenta cerrada
// 3 = Pendiente ? En validación
```

### Caso 3: Listar todas las cuentas de una entidad
```csharp
POST /api/psp/Entities/GetEntityByTributaryId
{
  "UAT": "admin_uat_token",
  "TributaryIdentifier": "30707609032"
}

// Recorrer Data[].accounts[] para obtener:
// - accountNumber
// - cvU_CBU
// - cvU_CBUAlias
// - status / statusDescription
```

---

## Configuración Requerida

### 1. Base de Datos
Asegurarse que la tabla `PSPAccount` tenga:
- `EncryptedUserToken` (text) ? Token encriptado del usuario PSP
- `TokenExpiry` (datetime?) ? Fecha de expiración del token
- `UserName` (string) ? Email/username del PSP
- `EncryptedPassword` (text) ? Password encriptado del PSP
- `CreatedAt` / `UpdatedAt` ? Timestamps

### 2. Configuración PSP (`appsettings.json`)
```json
{
  "PSP": {
    "BaseUrl": "https://btn-des-webapp02.azurewebsites.net",
    "ClientId": "tu_client_id",
    "ClientSecret": "tu_client_secret",
    "Username": "tu_username_sistema",
    "Password": "tu_password_sistema",
    "TestMode": false
  }
}
```

---

## Diferencias Clave con Endpoints Anteriores

### ? NO se personaliza el error
Los endpoints **devuelven directamente** lo que el PSP responde:
- Si el PSP dice "Usuario en proceso de validación" ? Se devuelve tal cual
- Si el PSP dice "Usuario no aprobado" ? Se devuelve tal cual
- **NO** se intenta "adivinar" o personalizar mensajes

### ? Usa token de USUARIO PSP (C1) vs token de SISTEMA (C7)
- **C1**: Requiere el `UserToken` del usuario logueado en el PSP
- **C7**: Usa el `SystemToken` (token general del sistema)

### ? Gestión automática de tokens
El método `ObtenerUserTokenPSP()` maneja toda la lógica de:
- Búsqueda en BD
- Validación de expiración
- Obtención de nuevo token
- Almacenamiento seguro

---

## Testing

### Test Manual con Postman

#### Test C1:
```
POST https://localhost:5001/api/psp/Entities/GetAccountData
Content-Type: application/json

{
  "UAT": "tu_uat_token_aqui"
}
```

**Respuesta Esperada (Success)**:
```json
{
  "Status": 200,
  "UAT": "tu_uat_token_aqui",
  "Mensaje": "Datos de cuenta obtenidos exitosamente",
  "Success": true,
  "Accounts": [...]
}
```

**Respuesta Esperada (No tiene cuenta)**:
```json
{
  "Status": 400,
  "UAT": "tu_uat_token_aqui",
  "Mensaje": "No se pudo obtener el token del usuario PSP. Verifique que las credenciales estén guardadas.",
  "Success": false,
  "Accounts": []
}
```

#### Test C7:
```
POST https://localhost:5001/api/psp/Entities/GetEntityByTributaryId
Content-Type: application/json

{
  "UAT": "tu_uat_token_aqui",
  "TributaryIdentifier": "27422125073"
}
```

**Respuesta Esperada (Encontrada)**:
```json
{
  "Status": 200,
  "UAT": "tu_uat_token_aqui",
  "Mensaje": "Entidad obtenida exitosamente",
  "Success": true,
  "Data": [
    {
      "entityName": "Brian Ariel Arrondo",
      "entityStatus": 1,
      "entityStatusDescription": "Activa",
      "accounts": [...]
    }
  ],
  "RawResponse": "{...}"
}
```

---

## Logs de Debug

Ambos endpoints generan logs detallados en Serilog:

### Logs de C1:
```
2024-01-15 10:30:45 [INF] Token recuperado desde BD para usuario user@example.com
2024-01-15 10:30:46 [INF] C1: Datos de cuenta obtenidos exitosamente para usuario user@example.com
```

### Logs de C7:
```
2024-01-15 10:32:10 [INF] ?? Llamando PSP C7 - URL: .../Accounts/Children/Get?TributaryIdentifier=27422125073
2024-01-15 10:32:10 [INF] ?? CUIL/CUIT consultado: 27422125073
2024-01-15 10:32:11 [INF] ?? PSP C7 Response - StatusCode: 200
2024-01-15 10:32:11 [INF] ? Respuesta del PSP - Success: True, HasData: True, Entities: 1
```

---

## Conclusión

? **Endpoints C1 y C7 implementados correctamente**  
? **Compilación exitosa sin errores**  
? **Devuelven respuestas directas del PSP (sin personalizar)**  
? **Gestión automática de tokens del usuario PSP**  
? **DTOs completos y documentados**  
? **Logs de debugging detallados**  

Los endpoints están listos para ser probados con el PSP real o en modo de prueba (TestMode).
