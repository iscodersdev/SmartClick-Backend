# Documentación para Frontend (Lovable) - Endpoints C1 y C7

## ?? Configuración Inicial (appsettings.json)

Los endpoints **C1** y **C7** utilizan credenciales del PSP configuradas en el backend. El frontend **NO necesita enviar estas credenciales** directamente.

```json
{
  "PSP": {
    "BaseUrl": "https://btn-des-webapp02.azurewebsites.net",
    "ClientId": "prever",
    "ClientSecret": "$RFVbgt5",
    "Username": "bchristiansen",
    "Password": "Abcd1234",
    "TestMode": false
  }
}
```

**IMPORTANTE**: Estas credenciales están en el backend y **NO deben exponerse al frontend**.

---

## ?? C1: Obtener Información de Cuentas del Usuario

### Endpoint
```
GET /api/psp/Entities/AccountsInfo
```

### Descripción
Obtiene la información de todas las cuentas del usuario logueado en el PSP (CVU, alias, saldos, etc.).

### ? Token Automático
Este endpoint **obtiene automáticamente** el `UserToken` del usuario, ya sea desde la base de datos local o solicitándolo al PSP. El frontend **SOLO necesita enviar el UAT** del administrador.

**Flujo Interno Automático:**
1. Valida el UAT del usuario administrador
2. Busca el `UserToken` del usuario en la tabla `PSPAccount`
3. Si el token está expirado o no existe, lo obtiene del PSP
4. Usa el token para consultar las cuentas en el PSP
5. Devuelve la información de las cuentas

### Headers Requeridos
```
Content-Type: application/json
```

### Query Parameters
| Parámetro | Tipo | Requerido | Descripción |
|-----------|------|-----------|-------------|
| `uat` | string | ? Sí | Token de autenticación del usuario administrador del sistema |

### Ejemplo de Request (JavaScript/TypeScript)
```typescript
const baseUrl = "https://tu-backend.azurewebsites.net";
const uat = "tu_uat_token_admin"; // Token del usuario administrador

const response = await fetch(
  `${baseUrl}/api/psp/Entities/AccountsInfo?uat=${uat}`,
  {
    method: "GET",
    headers: {
      "Content-Type": "application/json"
    }
  }
);

const data = await response.json();
console.log(data);
```

### Response Exitoso (200 OK)
```json
{
  "Status": 200,
  "UAT": "tu_uat_token_admin",
  "Mensaje": "Información de cuentas obtenida exitosamente",
  "Success": true,
  "Accounts": [
    {
      "accountId": 12345,
      "accountNumber": "0000003100095844447658",
      "accountTypeId": 3,
      "accountTypeDescription": "Cuenta Virtual Uniforme",
      "alias": "PEPE.JUAN.MUTUAL",
      "name": "Juan Pérez",
      "balance": "15000.50",
      "currencyTypeId": 1,
      "currencyTypeDescription": "Pesos Argentinos",
      "entityId": 7890,
      "tributaryIdentifier": "20345678901",
      "tributaryIdentifierType": "CUIL"
    }
  ]
}
```

### Response Error (400/401/500)
```json
{
  "Status": 401,
  "UAT": "tu_uat_token_admin",
  "Mensaje": "Usuario no autenticado",
  "Success": false,
  "Accounts": []
}
```

### Campos de la Respuesta
| Campo | Tipo | Descripción |
|-------|------|-------------|
| `Status` | int | Código HTTP de estado (200, 400, 401, 500) |
| `UAT` | string | Token UAT enviado en el request |
| `Mensaje` | string | Mensaje descriptivo del resultado |
| `Success` | bool | Indica si la operación fue exitosa |
| `Accounts` | array | Lista de cuentas del usuario (ver estructura abajo) |

#### Estructura de `Accounts[i]`
| Campo | Tipo | Descripción |
|-------|------|-------------|
| `accountId` | int | ID interno de la cuenta en el PSP |
| `accountNumber` | string | CVU/CBU de la cuenta (22 dígitos) |
| `accountTypeId` | int | Tipo de cuenta (3 = CVU) |
| `accountTypeDescription` | string | Descripción del tipo de cuenta |
| `alias` | string | Alias de la cuenta (ej: "PEPE.JUAN.MUTUAL") |
| `name` | string | Nombre del titular |
| `balance` | string | Saldo disponible en formato decimal |
| `currencyTypeId` | int | ID del tipo de moneda (1 = Pesos) |
| `currencyTypeDescription` | string | Descripción de la moneda |
| `entityId` | int | ID de la entidad asociada |
| `tributaryIdentifier` | string | CUIT/CUIL del titular |
| `tributaryIdentifierType` | string | Tipo (CUIT/CUIL) |

### Códigos de Error Comunes
| Status | Mensaje | Causa Probable |
|--------|---------|----------------|
| 401 | "Usuario no autenticado" | UAT inválido o expirado |
| 400 | "No se pudo obtener el token del usuario del PSP. Verifique que el usuario tenga cuenta en el PSP." | El usuario no tiene cuenta en el PSP o no se pudo obtener el token |
| 500 | "Error interno del servidor" | Error de comunicación con el PSP |

---

## ?? C7: Consultar Estado de Entidad por CUIT/CUIL

### Endpoint
```
GET /api/psp/Entities/Status
```

### Descripción
Obtiene el estado de una entidad registrada en el PSP usando su CUIT/CUIL. Permite verificar si una cuenta fue creada, está activa, pendiente o rechazada.

### ? Token Automático
Este endpoint **obtiene automáticamente** el token del sistema PSP internamente. El frontend **NO necesita** enviar `UserToken`, solo el `UAT` de administrador.

### Headers Requeridos
```
Content-Type: application/json
```

### Query Parameters
| Parámetro | Tipo | Requerido | Descripción |
|-----------|------|-----------|-------------|
| `TributaryIdentifier` | string | ? Sí | CUIT/CUIL de la entidad a consultar (solo números, sin guiones) |
| `UAT` | string | ? Sí | Token de autenticación del usuario administrador del sistema |

### Ejemplo de Request (JavaScript/TypeScript)
```typescript
const baseUrl = "https://tu-backend.azurewebsites.net";
const uat = "tu_uat_token_admin"; // Token del usuario administrador
const cuil = "20345678901"; // CUIL sin guiones

const response = await fetch(
  `${baseUrl}/api/psp/Entities/Status?TributaryIdentifier=${cuil}&UAT=${uat}`,
  {
    method: "GET",
    headers: {
      "Content-Type": "application/json"
    }
  }
);

const data = await response.json();
console.log(data);
```

### Response Exitoso (200 OK) - Entidad Encontrada
```json
{
  "Status": 200,
  "UAT": "tu_uat_token_admin",
  "Mensaje": "Estado de la entidad obtenido exitosamente",
  "Success": true,
  "Data": [
    {
      "EntityName": "Juan Pérez",
      "EntityStatus": 1,
      "EntityStatusDescription": "Activa",
      "Accounts": [
        {
          "AccountNumber": "0000003100095844447658",
          "cvU_CBU": "0000003100095844447658",
          "Status": 1,
          "StatusDescription": "Activa",
          "EntityId": 7890
        }
      ]
    }
  ]
}
```

### Response Exitoso (404 Not Found) - Entidad No Encontrada
```json
{
  "Status": 404,
  "UAT": "tu_uat_token_admin",
  "Mensaje": "Estado de la entidad obtenido exitosamente",
  "Success": true,
  "Data": []
}
```

### Response Error (400/401/500)
```json
{
  "Status": 401,
  "UAT": "tu_uat_token_admin",
  "Mensaje": "Usuario no autenticado",
  "Success": false,
  "Data": null
}
```

### Campos de la Respuesta
| Campo | Tipo | Descripción |
|-------|------|-------------|
| `Status` | int | Código HTTP de estado (200, 404, 400, 401, 500) |
| `UAT` | string | Token UAT enviado en el request |
| `Mensaje` | string | Mensaje descriptivo del resultado |
| `Success` | bool | Indica si la operación fue exitosa |
| `Data` | array | Lista de entidades encontradas (vacío si no existe) |

#### Estructura de `Data[i]`
| Campo | Tipo | Descripción |
|-------|------|-------------|
| `EntityName` | string | Nombre completo de la entidad |
| `EntityStatus` | int | Estado de la entidad (1 = Activa, 2 = Pendiente, etc.) |
| `EntityStatusDescription` | string | Descripción del estado |
| `Accounts` | array | Lista de cuentas asociadas a la entidad |

#### Estructura de `Accounts[i]`
| Campo | Tipo | Descripción |
|-------|------|-------------|
| `AccountNumber` | string | CVU/CBU de la cuenta |
| `cvU_CBU` | string | Alias del CVU/CBU (mismo valor) |
| `Status` | int | Estado de la cuenta (1 = Activa, 2 = Pendiente, etc.) |
| `StatusDescription` | string | Descripción del estado de la cuenta |
| `EntityId` | int | ID de la entidad asociada |

### Estados Posibles de Entidad y Cuenta

#### Estados de Entidad (`EntityStatus`)
| Código | Descripción | Significado |
|--------|-------------|-------------|
| 1 | Activa | Entidad totalmente operativa |
| 2 | Pendiente | En proceso de validación |
| 3 | Rechazada | No aprobada por el PSP |
| 4 | Suspendida | Temporalmente deshabilitada |

#### Estados de Cuenta (`Status`)
| Código | Descripción | Significado |
|--------|-------------|-------------|
| 1 | Activa | Cuenta operativa |
| 2 | Pendiente | En proceso de activación |
| 3 | Bloqueada | Temporalmente bloqueada |
| 4 | Cerrada | Cuenta cerrada definitivamente |

### Códigos de Error Comunes
| Status | Mensaje | Causa Probable |
|--------|---------|----------------|
| 401 | "Usuario no autenticado" | UAT inválido o expirado |
| 400 | "TributaryIdentifier (CUIT/CUIL) requerido" | Falta el parámetro `TributaryIdentifier` |
| 404 | "Estado de la entidad obtenido exitosamente" (con `Data: []`) | CUIT/CUIL no registrado en el PSP |
| 500 | "Error interno del servidor" | Error de comunicación con el PSP |

---

## ?? Flujo de Trabajo Recomendado

### Escenario 1: Usuario Nuevo (Primera Vez)
```
1. Frontend ? [POST] /api/psp/Entities/CrearUsuario
   ? Backend crea usuario en PSP y guarda UserToken en BD
   
2. Frontend ? [GET] /api/psp/Entities/AccountsInfo?uat={UAT}
   ? Backend obtiene UserToken automáticamente y devuelve cuentas
   
3. Frontend ? [GET] /api/psp/Entities/Status?TributaryIdentifier={CUIL}&UAT={UAT}
   ? Verificar estado de la entidad asociada
```

### Escenario 2: Usuario Existente (Login)
```
1. Frontend ? [GET] /api/psp/Entities/AccountsInfo?uat={UAT}
   ? Backend busca UserToken en BD o lo obtiene del PSP automáticamente
```

### Escenario 3: Verificar Estado de Entidad (Sin Usuario Logueado)
```
1. Frontend ? [GET] /api/psp/Entities/Status?TributaryIdentifier={CUIL}&UAT={UAT}
   ? Verificar si una entidad existe y su estado
```

---

## ? Resumen de Cambios (C1 Actualizado)

### ? ANTES (Requería UserToken explícito)
```typescript
// El frontend necesitaba el UserToken del usuario final
const response = await fetch(
  `${baseUrl}/api/psp/Entities/AccountsInfo?userToken=${userToken}&uat=${uat}`,
  { method: "GET" }
);
```

### ? AHORA (Automático como C7)
```typescript
// El frontend SOLO necesita el UAT del administrador
const response = await fetch(
  `${baseUrl}/api/psp/Entities/AccountsInfo?uat=${uat}`,
  { method: "GET" }
);
```

**Ventajas:**
- ? Simplifica la integración del frontend
- ? El backend gestiona tokens automáticamente
- ? Cachea tokens en la base de datos para mejorar performance
- ? Renueva tokens expirados automáticamente
- ? Consistente con el comportamiento de C7

---

## ? Preguntas Frecuentes (FAQ)

### ¿Los endpoints C1 y C7 trabajan orquestados?
**NO**. Son endpoints independientes:
- **C1 (AccountsInfo)**: Obtiene automáticamente el `UserToken` del usuario final desde BD o PSP, luego consulta sus cuentas
- **C7 (Status)**: Obtiene automáticamente el token del sistema PSP y consulta el estado de una entidad por CUIT/CUIL

### ¿Necesito enviar ClientId, ClientSecret, Username o Password desde el frontend?
**NO**. Estas credenciales están configuradas en el backend (`appsettings.json`) y **NO deben exponerse al frontend**.

### ¿Qué token necesito para cada endpoint?
- **C1 (AccountsInfo)**: Solo requiere `UAT` (administrador) - El backend obtiene el `UserToken` automáticamente
- **C7 (Status)**: Solo requiere `UAT` (administrador) - El backend obtiene el token del sistema automáticamente

### ¿Cómo funciona la gestión automática de tokens en C1?
El backend:
1. Busca el `UserToken` en la tabla `PSPAccount`
2. Verifica si está expirado (con 5 minutos de buffer)
3. Si está válido, lo descifra y lo usa
4. Si está expirado o no existe, lo solicita al PSP
5. Guarda/actualiza el token cifrado en la BD para futuras consultas

### ¿Qué pasa si el usuario no tiene cuenta en el PSP?
El endpoint devuelve un error `400` con el mensaje: "No se pudo obtener el token del usuario del PSP. Verifique que el usuario tenga cuenta en el PSP."

### ¿Qué formato debe tener el CUIT/CUIL?
- **Formato correcto**: `20345678901` (solo números, sin guiones)
- **Formato incorrecto**: `20-34567890-1` ?

### ¿Qué pasa si llamo a C7 con un CUIL que no existe?
El endpoint devuelve `200 OK` con `Data: []` (array vacío), indicando que la entidad no fue encontrada.

---

## ?? Próximos Pasos para Producción

### ? Verificaciones Antes de Publicar

1. **Configuración de Producción** (`appsettings.json`):
   ```json
   {
     "PSP": {
       "BaseUrl": "https://btn-des-webapp02.azurewebsites.net", // ? Verificar URL correcta
       "ClientId": "prever",
       "ClientSecret": "$RFVbgt5",
       "Username": "bchristiansen",
       "Password": "Abcd1234",
       "TestMode": false  // ? DEBE estar en false
     }
   }
   ```

2. **Endpoints Validados**:
   - ? C1 (`/api/psp/Entities/AccountsInfo`) - ? **MODIFICADO: Ahora obtiene UserToken automáticamente**
   - ? C7 (`/api/psp/Entities/Status`) - Obtiene token del sistema automáticamente

3. **Seguridad**:
   - ? UAT validado en ambos endpoints
   - ? Credenciales PSP no expuestas al frontend
   - ? Tokens de usuario cifrados en la base de datos con `common.Encrypt`
   - ? Verificación de expiración de tokens con buffer de 5 minutos

4. **Logs**:
   - ? Logs de Serilog configurados para trazar errores de PSP
   - ? Logs de obtención/renovación de tokens
   - ? Logs de descifrado de tokens almacenados

### ? Base de Datos

Asegúrate de que la tabla `PSPAccount` exista con los siguientes campos:
- `Id` (int, PK)
- `UsuarioId` (int, FK a Usuario)
- `UserName` (string) - Email del usuario
- `EncryptedUserToken` (text) - Token cifrado del PSP
- `TokenExpiry` (DateTime nullable) - Fecha de expiración del token
- `Status` (string) - Estado de la cuenta PSP
- `CreatedAt` (DateTime)
- `UpdatedAt` (DateTime nullable)

### ? Recomendación Final

Los endpoints **C1 y C7 están correctos** y funcionan de forma automática. Puedes proceder a publicar en producción.

**Ventajas de la nueva implementación de C1:**
- ? **Simplifica la integración** del frontend (solo requiere UAT)
- ? **Mejora la performance** (cachea tokens en BD)
- ? **Renueva tokens automáticamente** (evita errores de token expirado)
- ? **Consistente con C7** (misma filosofía de uso)
- ? **Más seguro** (tokens cifrados en BD, no expuestos al frontend)

---

## ?? Soporte Técnico

Si tienes problemas al integrar estos endpoints, verifica:
1. ? Que el `UAT` sea válido y no esté expirado
2. ? Que el usuario tenga una cuenta creada en el PSP (usando `/api/psp/Entities/CrearUsuario`)
3. ? Que el `TributaryIdentifier` (en C7) tenga formato correcto (solo números)
4. ? Que `TestMode` esté en `false` en producción
5. ? Que la tabla `PSPAccount` exista en la base de datos
6. ? Que el usuario tenga un password guardado en la tabla `Clientes` (requerido para obtener token del PSP)
