# ?? Referencia Técnica API PSP - Endpoints C1 y C7 (para Lovable)

## ?? URL Base del Backend
```
https://tu-backend.azurewebsites.net
```

---

## ?? Autenticación

Todos los endpoints requieren un parámetro `UAT` (User Authentication Token) que identifica al usuario administrador autenticado en el sistema.

**Obtención del UAT:**
- El UAT se obtiene al hacer login en el sistema SmartClick
- Es un token único por sesión de usuario
- Ejemplo: `"ABC123XYZ456"`

---

# ?? C1: Obtener Información de Cuentas del Usuario

## Endpoint
```
GET /api/psp/Entities/AccountsInfo
```

## Descripción
Obtiene la información de todas las cuentas del usuario logueado en el PSP (CVU, alias, saldos, tipo de cuenta, etc.).

**? CARACTERÍSTICA AUTOMÁTICA:** 
Este endpoint obtiene automáticamente el token del usuario del PSP. NO necesitas enviarlo.

---

## Request

### Método HTTP
```
GET
```

### Headers
```
Content-Type: application/json
```

### Query Parameters

| Parámetro | Tipo | Obligatorio | Descripción | Ejemplo |
|-----------|------|-------------|-------------|---------|
| `uat` | string | ? Sí | Token de autenticación del usuario administrador | `"ABC123XYZ456"` |

### URL Completa (Ejemplo)
```
GET https://tu-backend.azurewebsites.net/api/psp/Entities/AccountsInfo?uat=ABC123XYZ456
```

---

## Response

### Response Exitoso (200 OK)

**Código de Estado:** `200 OK`

**Body (JSON):**
```json
{
  "Status": 200,
  "UAT": "ABC123XYZ456",
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
    },
    {
      "accountId": 12346,
      "accountNumber": "0000003100095844447659",
      "accountTypeId": 3,
      "accountTypeDescription": "Cuenta Virtual Uniforme",
      "alias": "JUAN.AHORRO.MUTUAL",
      "name": "Juan Pérez",
      "balance": "5000.00",
      "currencyTypeId": 1,
      "currencyTypeDescription": "Pesos Argentinos",
      "entityId": 7890,
      "tributaryIdentifier": "20345678901",
      "tributaryIdentifierType": "CUIL"
    }
  ]
}
```

### Estructura del Response Exitoso

| Campo | Tipo | Descripción | Valores Posibles |
|-------|------|-------------|------------------|
| `Status` | integer | Código HTTP de estado | `200`, `400`, `401`, `500` |
| `UAT` | string | Token UAT enviado en el request | Mismo valor enviado |
| `Mensaje` | string | Mensaje descriptivo del resultado | Texto descriptivo |
| `Success` | boolean | Indica si la operación fue exitosa | `true`, `false` |
| `Accounts` | array | Lista de cuentas del usuario | Ver estructura abajo |

### Estructura de `Accounts[]` (cada cuenta)

| Campo | Tipo | Descripción | Ejemplo | Valores Posibles |
|-------|------|-------------|---------|------------------|
| `accountId` | integer | ID interno de la cuenta en el PSP | `12345` | Número entero |
| `accountNumber` | string | CVU/CBU de la cuenta (22 dígitos) | `"0000003100095844447658"` | 22 caracteres numéricos |
| `accountTypeId` | integer | ID del tipo de cuenta | `3` | `3` = CVU, `1` = Caja de ahorro |
| `accountTypeDescription` | string | Descripción del tipo de cuenta | `"Cuenta Virtual Uniforme"` | Texto descriptivo |
| `alias` | string | Alias de la cuenta | `"PEPE.JUAN.MUTUAL"` | Formato: `PALABRA.PALABRA.PALABRA` |
| `name` | string | Nombre del titular | `"Juan Pérez"` | Texto |
| `balance` | string | Saldo disponible (formato decimal) | `"15000.50"` | Número decimal como string |
| `currencyTypeId` | integer | ID del tipo de moneda | `1` | `1` = Pesos Argentinos |
| `currencyTypeDescription` | string | Descripción de la moneda | `"Pesos Argentinos"` | Texto |
| `entityId` | integer | ID de la entidad asociada | `7890` | Número entero |
| `tributaryIdentifier` | string | CUIT/CUIL del titular (sin guiones) | `"20345678901"` | 11 dígitos numéricos |
| `tributaryIdentifierType` | string | Tipo de identificador tributario | `"CUIL"` | `"CUIL"`, `"CUIT"` |

---

### Response de Error (401 Unauthorized)

**Código de Estado:** `401 Unauthorized`

**Body (JSON):**
```json
{
  "Status": 401,
  "UAT": "ABC123XYZ456",
  "Mensaje": "Usuario no autenticado",
  "Success": false,
  "Accounts": []
}
```

**Causa:** El token UAT es inválido o ha expirado.

**Solución:** Solicitar al usuario que vuelva a hacer login.

---

### Response de Error (400 Bad Request - Sin Token PSP)

**Código de Estado:** `400 Bad Request`

**Body (JSON):**
```json
{
  "Status": 400,
  "UAT": "ABC123XYZ456",
  "Mensaje": "No se pudo obtener el token del usuario del PSP. Verifique que el usuario tenga cuenta en el PSP.",
  "Success": false,
  "Accounts": []
}
```

**Causa:** El usuario no tiene una cuenta creada en el PSP o el sistema no pudo obtener el token del usuario.

**Solución:** El usuario debe crear una cuenta en el PSP usando el endpoint `/api/psp/Entities/CrearUsuario`.

---

### Response de Error (500 Internal Server Error)

**Código de Estado:** `500 Internal Server Error`

**Body (JSON):**
```json
{
  "Status": 500,
  "UAT": "ABC123XYZ456",
  "Mensaje": "Error interno del servidor",
  "Success": false,
  "Accounts": []
}
```

**Causa:** Error de comunicación con el PSP o error interno del backend.

**Solución:** Contactar al equipo de backend.

---

## Códigos de Error Resumen (C1)

| Código | Mensaje | Causa | Acción del Frontend |
|--------|---------|-------|---------------------|
| 200 | "Información de cuentas obtenida exitosamente" | Éxito | Mostrar cuentas al usuario |
| 400 | "No se pudo obtener el token del usuario del PSP..." | Usuario sin cuenta PSP | Redirigir a crear cuenta PSP |
| 401 | "Usuario no autenticado" | UAT inválido/expirado | Solicitar login nuevamente |
| 500 | "Error interno del servidor" | Error del backend/PSP | Mostrar error genérico |

---

## Casos de Uso (C1)

### Caso 1: Usuario con Cuenta PSP Activa
```
1. Frontend envía: GET /api/psp/Entities/AccountsInfo?uat=ABC123
2. Backend busca token del usuario en BD
3. Backend consulta cuentas en el PSP
4. Frontend recibe: Status 200 + lista de cuentas
5. Frontend muestra las cuentas al usuario
```

### Caso 2: Usuario sin Cuenta PSP
```
1. Frontend envía: GET /api/psp/Entities/AccountsInfo?uat=ABC123
2. Backend no encuentra token del usuario
3. Frontend recibe: Status 400 + mensaje "No se pudo obtener el token..."
4. Frontend redirige al usuario a crear cuenta PSP
```

### Caso 3: UAT Expirado
```
1. Frontend envía: GET /api/psp/Entities/AccountsInfo?uat=ABC123_EXPIRADO
2. Backend rechaza el UAT
3. Frontend recibe: Status 401 + mensaje "Usuario no autenticado"
4. Frontend redirige al usuario a login
```

---

# ?? C7: Consultar Estado de Entidad por CUIT/CUIL

## Endpoint
```
GET /api/psp/Entities/Status
```

## Descripción
Obtiene el estado de una entidad registrada en el PSP usando su CUIT/CUIL. Permite verificar si una cuenta fue creada, está activa, pendiente, rechazada, etc.

**? CARACTERÍSTICA AUTOMÁTICA:** 
Este endpoint obtiene automáticamente el token del sistema PSP. NO necesitas enviarlo.

---

## Request

### Método HTTP
```
GET
```

### Headers
```
Content-Type: application/json
```

### Query Parameters

| Parámetro | Tipo | Obligatorio | Descripción | Ejemplo |
|-----------|------|-------------|-------------|---------|
| `TributaryIdentifier` | string | ? Sí | CUIT/CUIL de la entidad a consultar (solo números, sin guiones) | `"20345678901"` |
| `UAT` | string | ? Sí | Token de autenticación del usuario administrador | `"ABC123XYZ456"` |

### URL Completa (Ejemplo)
```
GET https://tu-backend.azurewebsites.net/api/psp/Entities/Status?TributaryIdentifier=20345678901&UAT=ABC123XYZ456
```

---

## Response

### Response Exitoso - Entidad Encontrada (200 OK)

**Código de Estado:** `200 OK`

**Body (JSON):**
```json
{
  "Status": 200,
  "UAT": "ABC123XYZ456",
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
        },
        {
          "AccountNumber": "0000003100095844447659",
          "cvU_CBU": "0000003100095844447659",
          "Status": 2,
          "StatusDescription": "Pendiente",
          "EntityId": 7890
        }
      ]
    }
  ]
}
```

### Estructura del Response Exitoso

| Campo | Tipo | Descripción | Valores Posibles |
|-------|------|-------------|------------------|
| `Status` | integer | Código HTTP de estado | `200`, `404`, `400`, `401`, `500` |
| `UAT` | string | Token UAT enviado en el request | Mismo valor enviado |
| `Mensaje` | string | Mensaje descriptivo del resultado | Texto descriptivo |
| `Success` | boolean | Indica si la operación fue exitosa | `true`, `false` |
| `Data` | array | Lista de entidades encontradas | Ver estructura abajo |

### Estructura de `Data[]` (cada entidad)

| Campo | Tipo | Descripción | Ejemplo | Valores Posibles |
|-------|------|-------------|---------|------------------|
| `EntityName` | string | Nombre completo de la entidad | `"Juan Pérez"` | Texto |
| `EntityStatus` | integer | Estado de la entidad | `1` | Ver tabla de estados |
| `EntityStatusDescription` | string | Descripción del estado | `"Activa"` | Ver tabla de estados |
| `Accounts` | array | Lista de cuentas de la entidad | Ver estructura abajo | Array |

### Estructura de `Accounts[]` (cada cuenta de la entidad)

| Campo | Tipo | Descripción | Ejemplo | Valores Posibles |
|-------|------|-------------|---------|------------------|
| `AccountNumber` | string | CVU/CBU de la cuenta | `"0000003100095844447658"` | 22 caracteres numéricos |
| `cvU_CBU` | string | Alias del CVU/CBU (mismo valor) | `"0000003100095844447658"` | 22 caracteres numéricos |
| `Status` | integer | Estado de la cuenta | `1` | Ver tabla de estados |
| `StatusDescription` | string | Descripción del estado de la cuenta | `"Activa"` | Ver tabla de estados |
| `EntityId` | integer | ID de la entidad asociada | `7890` | Número entero |

### Tabla de Estados de Entidad (`EntityStatus`)

| Código | Descripción | Significado | Color Sugerido |
|--------|-------------|-------------|----------------|
| `1` | Activa | Entidad totalmente operativa | ?? Verde |
| `2` | Pendiente | En proceso de validación | ?? Amarillo |
| `3` | Rechazada | No aprobada por el PSP | ?? Rojo |
| `4` | Suspendida | Temporalmente deshabilitada | ?? Naranja |

### Tabla de Estados de Cuenta (`Status`)

| Código | Descripción | Significado | Color Sugerido |
|--------|-------------|-------------|----------------|
| `1` | Activa | Cuenta operativa | ?? Verde |
| `2` | Pendiente | En proceso de activación | ?? Amarillo |
| `3` | Bloqueada | Temporalmente bloqueada | ?? Rojo |
| `4` | Cerrada | Cuenta cerrada definitivamente | ? Gris |

---

### Response Exitoso - Entidad NO Encontrada (404 Not Found)

**Código de Estado:** `404 Not Found`

**Body (JSON):**
```json
{
  "Status": 404,
  "UAT": "ABC123XYZ456",
  "Mensaje": "Estado de la entidad obtenido exitosamente",
  "Success": true,
  "Data": []
}
```

**Causa:** El CUIT/CUIL consultado no existe en el PSP.

**Solución:** Verificar que el CUIT/CUIL sea correcto o informar al usuario que no tiene cuenta en el PSP.

---

### Response de Error (401 Unauthorized)

**Código de Estado:** `401 Unauthorized`

**Body (JSON):**
```json
{
  "Status": 401,
  "UAT": "ABC123XYZ456",
  "Mensaje": "Usuario no autenticado",
  "Success": false,
  "Data": null
}
```

**Causa:** El token UAT es inválido o ha expirado.

**Solución:** Solicitar al usuario que vuelva a hacer login.

---

### Response de Error (400 Bad Request - Sin CUIT/CUIL)

**Código de Estado:** `400 Bad Request`

**Body (JSON):**
```json
{
  "Status": 400,
  "UAT": "ABC123XYZ456",
  "Mensaje": "TributaryIdentifier (CUIT/CUIL) requerido",
  "Success": false,
  "Data": null
}
```

**Causa:** No se envió el parámetro `TributaryIdentifier` o está vacío.

**Solución:** Asegurarse de enviar el parámetro `TributaryIdentifier` en el query string.

---

### Response de Error (500 Internal Server Error)

**Código de Estado:** `500 Internal Server Error`

**Body (JSON):**
```json
{
  "Status": 500,
  "UAT": "ABC123XYZ456",
  "Mensaje": "Error interno del servidor",
  "Success": false,
  "Data": null
}
```

**Causa:** Error de comunicación con el PSP o error interno del backend.

**Solución:** Contactar al equipo de backend.

---

## Códigos de Error Resumen (C7)

| Código | Mensaje | Causa | Acción del Frontend |
|--------|---------|-------|---------------------|
| 200 | "Estado de la entidad obtenido exitosamente" | Éxito - Entidad encontrada | Mostrar estado de la entidad |
| 404 | "Estado de la entidad obtenido exitosamente" (con `Data: []`) | CUIT/CUIL no encontrado | Informar que no tiene cuenta PSP |
| 400 | "TributaryIdentifier (CUIT/CUIL) requerido" | Falta parámetro | Verificar el request |
| 401 | "Usuario no autenticado" | UAT inválido/expirado | Solicitar login nuevamente |
| 500 | "Error interno del servidor" | Error del backend/PSP | Mostrar error genérico |

---

## Casos de Uso (C7)

### Caso 1: Consultar Entidad Activa
```
1. Frontend envía: GET /api/psp/Entities/Status?TributaryIdentifier=20345678901&UAT=ABC123
2. Backend obtiene token del sistema PSP
3. Backend consulta estado en el PSP
4. Frontend recibe: Status 200 + Data con entidad activa
5. Frontend muestra: "? Cuenta Activa"
```

### Caso 2: Consultar Entidad Pendiente
```
1. Frontend envía: GET /api/psp/Entities/Status?TributaryIdentifier=20345678901&UAT=ABC123
2. Backend consulta estado en el PSP
3. Frontend recibe: Status 200 + EntityStatus=2 (Pendiente)
4. Frontend muestra: "? Cuenta en proceso de validación"
```

### Caso 3: Entidad NO Existe
```
1. Frontend envía: GET /api/psp/Entities/Status?TributaryIdentifier=20999999999&UAT=ABC123
2. Backend consulta en el PSP
3. Frontend recibe: Status 404 + Data=[]
4. Frontend muestra: "? No se encontró cuenta con ese CUIT/CUIL"
```

### Caso 4: Entidad Rechazada
```
1. Frontend envía: GET /api/psp/Entities/Status?TributaryIdentifier=20345678901&UAT=ABC123
2. Backend consulta en el PSP
3. Frontend recibe: Status 200 + EntityStatus=3 (Rechazada)
4. Frontend muestra: "?? Cuenta rechazada. Contacte con soporte."
```

---

## Validación de Formato CUIT/CUIL

### ? Formato Correcto
```
20345678901
```
- Solo números
- 11 dígitos
- Sin guiones, espacios ni otros caracteres

### ? Formatos Incorrectos
```
20-34567890-1  ? Tiene guiones
20 34567890 1  ? Tiene espacios
2034567890     ? Faltan dígitos
203456789011   ? Sobran dígitos
```

---

# ?? Flujos de Trabajo Recomendados

## Flujo 1: Mostrar Cuentas del Usuario Logueado

```
???????????????????????????????????????????????????????????
? 1. Usuario hace login                                   ?
?    ? Frontend obtiene UAT                               ?
???????????????????????????????????????????????????????????
                     ?
                     ?
???????????????????????????????????????????????????????????
? 2. Frontend llama a C1 (AccountsInfo)                  ?
?    GET /api/psp/Entities/AccountsInfo?uat={UAT}        ?
???????????????????????????????????????????????????????????
                     ?
        ???????????????????????????
        ?                         ?
        ?                         ?
???????????????          ???????????????
? Status 200  ?          ? Status 400  ?
? (Éxito)     ?          ? (Sin cuenta)?
???????????????          ???????????????
       ?                        ?
       ?                        ?
???????????????          ???????????????
? Mostrar     ?          ? Redirigir a ?
? cuentas     ?          ? crear cuenta?
???????????????          ???????????????
```

---

## Flujo 2: Verificar Estado de Entidad

```
???????????????????????????????????????????????????????????
? 1. Usuario ingresa CUIT/CUIL para verificar            ?
?    ? Frontend valida formato (11 dígitos)              ?
???????????????????????????????????????????????????????????
                     ?
                     ?
???????????????????????????????????????????????????????????
? 2. Frontend llama a C7 (Status)                        ?
?    GET /api/psp/Entities/Status?                       ?
?        TributaryIdentifier={CUIL}&UAT={UAT}            ?
???????????????????????????????????????????????????????????
                     ?
        ???????????????????????????
        ?                         ?
        ?                         ?
???????????????          ???????????????
? Status 200  ?          ? Status 404  ?
? (Encontrada)?          ? (No existe) ?
???????????????          ???????????????
       ?                        ?
       ?                        ?
???????????????          ???????????????
? Mostrar     ?          ? Informar que?
? estado de   ?          ? no tiene    ?
? la entidad  ?          ? cuenta PSP  ?
???????????????          ???????????????
```

---

## Flujo 3: Validar Estado Antes de Transferencia

```
???????????????????????????????????????????????????????????
? 1. Usuario quiere hacer transferencia externa          ?
?    ? Frontend pide CVU/CBU/Alias destino               ?
???????????????????????????????????????????????????????????
                     ?
                     ?
???????????????????????????????????????????????????????????
? 2. Frontend llama a C1 para obtener cuentas propias    ?
?    GET /api/psp/Entities/AccountsInfo?uat={UAT}        ?
???????????????????????????????????????????????????????????
                     ?
                     ?
???????????????????????????????????????????????????????????
? 3. Frontend muestra cuentas y solicita monto           ?
???????????????????????????????????????????????????????????
                     ?
                     ?
???????????????????????????????????????????????????????????
? 4. Frontend llama a /ValidateExternalAccount           ?
?    para validar la cuenta destino                       ?
???????????????????????????????????????????????????????????
                     ?
                     ?
???????????????????????????????????????????????????????????
? 5. Frontend confirma y llama a /CreateTransaction      ?
???????????????????????????????????????????????????????????
```

---

# ?? Ejemplos de Integración con Lovable

## Ejemplo 1: Configurar Request C1 en Lovable

### Configuración del Request
```
Type: GET
URL: https://tu-backend.azurewebsites.net/api/psp/Entities/AccountsInfo
```

### Query Parameters
```
uat: {{user.uat}}  // Variable del usuario logueado
```

### Headers
```
Content-Type: application/json
```

### Mapeo de Response (Success)
```
accounts ? response.Accounts
message ? response.Mensaje
status ? response.Status
success ? response.Success
```

### Condiciones
```
IF response.Success === true:
  ? Mostrar lista de cuentas
  ? Para cada cuenta en response.Accounts:
    - accountNumber ? CVU/CBU
    - alias ? Alias
    - balance ? Saldo
    - accountTypeDescription ? Tipo de cuenta

IF response.Success === false AND response.Status === 400:
  ? Mostrar mensaje: "No tienes cuenta en el PSP"
  ? Botón: "Crear Cuenta PSP"

IF response.Success === false AND response.Status === 401:
  ? Redirigir a Login
```

---

## Ejemplo 2: Configurar Request C7 en Lovable

### Configuración del Request
```
Type: GET
URL: https://tu-backend.azurewebsites.net/api/psp/Entities/Status
```

### Query Parameters
```
TributaryIdentifier: {{form.cuil}}  // Input del formulario
UAT: {{user.uat}}                   // Variable del usuario logueado
```

### Headers
```
Content-Type: application/json
```

### Mapeo de Response (Success)
```
entityData ? response.Data[0]
entityName ? response.Data[0].EntityName
entityStatus ? response.Data[0].EntityStatus
accounts ? response.Data[0].Accounts
```

### Condiciones
```
IF response.Status === 200 AND response.Data.length > 0:
  ? Mostrar información de la entidad
  ? Mostrar estado con color:
    - EntityStatus === 1 ? Verde (Activa)
    - EntityStatus === 2 ? Amarillo (Pendiente)
    - EntityStatus === 3 ? Rojo (Rechazada)
    - EntityStatus === 4 ? Naranja (Suspendida)
  ? Listar cuentas de la entidad

IF response.Status === 404 OR response.Data.length === 0:
  ? Mostrar: "No se encontró entidad con el CUIT/CUIL ingresado"
  ? Botón: "Verificar otro CUIT/CUIL"

IF response.Status === 401:
  ? Redirigir a Login
```

---

## Ejemplo 3: Validación de CUIT/CUIL en Formulario (Lovable)

### Input Field Validation
```
Field Name: cuil
Type: text
Pattern: ^[0-9]{11}$
Min Length: 11
Max Length: 11
Error Message: "Ingrese CUIT/CUIL sin guiones (11 dígitos)"
```

### Transform Input (remover guiones automáticamente)
```
Input Value: {{form.cuil.replace(/[^0-9]/g, '')}}
```

---

# ?? Componentes UI Sugeridos

## Componente: Lista de Cuentas (C1)

### Cuando `Success === true`
```
???????????????????????????????????????????????????????????
? ?? MIS CUENTAS                                          ?
???????????????????????????????????????????????????????????
? ?????????????????????????????????????????????????????   ?
? ? ?? Cuenta Virtual Uniforme                        ?   ?
? ? CVU: 0000003100095844447658                       ?   ?
? ? Alias: PEPE.JUAN.MUTUAL                           ?   ?
? ? Saldo: $15,000.50                                 ?   ?
? ? Estado: ? Activa                                 ?   ?
? ?????????????????????????????????????????????????????   ?
?                                                           ?
? ?????????????????????????????????????????????????????   ?
? ? ?? Cuenta Virtual Uniforme                        ?   ?
? ? CVU: 0000003100095844447659                       ?   ?
? ? Alias: JUAN.AHORRO.MUTUAL                         ?   ?
? ? Saldo: $5,000.00                                  ?   ?
? ? Estado: ? Activa                                 ?   ?
? ?????????????????????????????????????????????????????   ?
???????????????????????????????????????????????????????????
```

### Cuando `Status === 400` (Sin cuenta PSP)
```
???????????????????????????????????????????????????????????
? ?? NO TIENES CUENTA EN EL PSP                          ?
???????????????????????????????????????????????????????????
?                                                           ?
? Para poder usar transferencias externas, necesitas      ?
? crear una cuenta en el PSP.                             ?
?                                                           ?
? [ Crear Cuenta PSP ]                                    ?
?                                                           ?
???????????????????????????????????????????????????????????
```

---

## Componente: Resultado de Consulta de Estado (C7)

### Cuando `EntityStatus === 1` (Activa)
```
???????????????????????????????????????????????????????????
? ? ENTIDAD ACTIVA                                       ?
???????????????????????????????????????????????????????????
? Nombre: Juan Pérez                                      ?
? CUIL: 20-34567890-1                                     ?
? Estado: Activa                                          ?
?                                                           ?
? ?? CUENTAS ASOCIADAS                                    ?
? ?????????????????????????????????????????????????????   ?
? ? CVU: 0000003100095844447658                       ?   ?
? ? Estado: ? Activa                                 ?   ?
? ?????????????????????????????????????????????????????   ?
???????????????????????????????????????????????????????????
```

### Cuando `EntityStatus === 2` (Pendiente)
```
???????????????????????????????????????????????????????????
? ? ENTIDAD PENDIENTE                                    ?
???????????????????????????????????????????????????????????
? Nombre: Juan Pérez                                      ?
? CUIL: 20-34567890-1                                     ?
? Estado: En proceso de validación                        ?
?                                                           ?
? Tu cuenta está siendo validada por el PSP.              ?
? Este proceso puede tomar hasta 48hs hábiles.            ?
???????????????????????????????????????????????????????????
```

### Cuando `Status === 404` (No encontrada)
```
???????????????????????????????????????????????????????????
? ? ENTIDAD NO ENCONTRADA                                ?
???????????????????????????????????????????????????????????
?                                                           ?
? No se encontró ninguna entidad registrada con el        ?
? CUIT/CUIL: 20-99999999-9                               ?
?                                                           ?
? [ Verificar otro CUIT/CUIL ]                           ?
?                                                           ?
???????????????????????????????????????????????????????????
```

---

# ??? Troubleshooting

## Problema 1: Error 401 en C1 o C7
**Síntoma:** Response `Status: 401, Mensaje: "Usuario no autenticado"`

**Causas Posibles:**
- El token UAT expiró
- El token UAT es inválido
- El usuario no está logueado

**Solución:**
1. Verificar que el usuario esté logueado
2. Verificar que el UAT sea válido
3. Si el UAT expiró, redirigir a login

---

## Problema 2: Error 400 en C1
**Síntoma:** Response `Status: 400, Mensaje: "No se pudo obtener el token del usuario del PSP..."`

**Causas Posibles:**
- El usuario no tiene cuenta en el PSP
- El usuario no tiene password guardado en la BD local

**Solución:**
1. Verificar si el usuario tiene cuenta PSP llamando a C7
2. Si no tiene cuenta, redirigir a `/api/psp/Entities/CrearUsuario`

---

## Problema 3: Error 404 en C7
**Síntoma:** Response `Status: 404, Data: []`

**Causas Posibles:**
- El CUIT/CUIL ingresado no existe en el PSP
- El CUIT/CUIL tiene formato incorrecto

**Solución:**
1. Verificar que el CUIT/CUIL tenga exactamente 11 dígitos
2. Verificar que no tenga guiones ni espacios
3. Informar al usuario que no se encontró la entidad

---

## Problema 4: Response Lento
**Síntoma:** El request tarda más de 5 segundos

**Causas Posibles:**
- El PSP está lento
- El backend está consultando el PSP en tiempo real

**Solución:**
1. Mostrar un loader/spinner mientras se espera la respuesta
2. Implementar timeout de 30 segundos
3. Si el timeout se cumple, mostrar error genérico

---

# ?? Tabla Comparativa C1 vs C7

| Característica | C1 (AccountsInfo) | C7 (Status) |
|----------------|-------------------|-------------|
| **Método HTTP** | GET | GET |
| **Requiere UAT** | ? Sí | ? Sí |
| **Requiere CUIL** | ? No | ? Sí |
| **Obtiene Token Automático** | ? Sí (del usuario) | ? Sí (del sistema) |
| **Devuelve Cuentas** | ? Sí (con saldos) | ? Sí (sin saldos) |
| **Devuelve Estado** | ? No | ? Sí |
| **Uso Principal** | Ver cuentas propias | Verificar estado de entidad |
| **Requiere Cuenta PSP** | ? Sí | ? No |
| **Puede devolver 404** | ? No | ? Sí |

---

# ? Checklist para Integración

## Antes de Integrar
- [ ] Obtener URL base del backend
- [ ] Verificar que el sistema de login devuelva el UAT
- [ ] Crear pantalla para mostrar cuentas
- [ ] Crear pantalla para consultar estado de entidad
- [ ] Definir colores para cada estado (Activa, Pendiente, Rechazada, etc.)

## Al Integrar C1
- [ ] Configurar request GET a `/api/psp/Entities/AccountsInfo`
- [ ] Pasar UAT como query parameter
- [ ] Manejar response exitoso (Status 200)
- [ ] Manejar error de autenticación (Status 401)
- [ ] Manejar error de cuenta no encontrada (Status 400)
- [ ] Mostrar loader mientras se carga
- [ ] Formatear saldo con separador de miles

## Al Integrar C7
- [ ] Configurar request GET a `/api/psp/Entities/Status`
- [ ] Validar formato de CUIT/CUIL (11 dígitos)
- [ ] Remover guiones automáticamente del input
- [ ] Pasar TributaryIdentifier y UAT como query parameters
- [ ] Manejar response exitoso con entidad encontrada (Status 200)
- [ ] Manejar response de entidad no encontrada (Status 404)
- [ ] Manejar error de autenticación (Status 401)
- [ ] Mostrar estado con color según el código
- [ ] Listar cuentas asociadas a la entidad

## Testing
- [ ] Probar C1 con usuario que tiene cuenta PSP
- [ ] Probar C1 con usuario sin cuenta PSP
- [ ] Probar C1 con UAT expirado
- [ ] Probar C7 con CUIL existente
- [ ] Probar C7 con CUIL no existente
- [ ] Probar C7 con CUIL con formato incorrecto
- [ ] Probar C7 con UAT expirado

---

# ?? Contacto y Soporte

**Equipo de Backend:**
- Para consultas sobre la API: [backend-team@ejemplo.com]
- Para reportar bugs: [support@ejemplo.com]

**Documentación Adicional:**
- Endpoint CrearUsuario: Ver `PSP_API_DOCUMENTATION_LOVABLE.md`
- Endpoint CreateTransaction: Ver `PSP_API_DOCUMENTATION_LOVABLE.md`

---

**Última actualización:** 2024
**Versión del documento:** 1.0
**Endpoints documentados:** C1 (AccountsInfo), C7 (Status)
