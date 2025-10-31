# ?? **DOCUMENTACIÓN API PSP - SMARTCLICK**
## **Para integración con Lovable.dev**

---

## ?? **BASE URL**

```
https://tudominio.com/api/psp/Entities
```

**?? Nota:** Reemplazar `tudominio.com` con el dominio real del backend SmartClick.

---

## ?? **AUTENTICACIÓN**

Todos los endpoints requieren un **UAT (User Authentication Token)** en el cuerpo de la petición.

**Formato:**
```json
{
  "UAT": "string"  // Token obtenido tras login del usuario
}
```

---

## ?? **ENDPOINTS DISPONIBLES**

### **1?? Consultar Estado de Cuenta PSP**

Verifica si el usuario tiene una cuenta en el PSP, su estado y obtiene datos de la misma.

**URL:** `POST /Status`

**Content-Type:** `application/json`

**Request Body:**
```json
{
  "UAT": "abc123xyz456",           // ? OBLIGATORIO - Token del usuario en SmartClick
  "Cuil": "20123456789",           // ? OBLIGATORIO - CUIL sin guiones (11 dígitos)
  "UserToken": "eyJhbGci..."       // ?? OPCIONAL - Token del usuario PSP (si ya lo tiene)
}
```

**?? Comportamiento:**
- Si **NO** se envía `UserToken`: El sistema usa automáticamente el token del sistema PSP (cuenta principal)
- Si se envía `UserToken`: Se usa ese token para consultar datos específicos del usuario

**Response (200 OK):**

#### ? **Caso: Cuenta Activa**
```json
{
  "Success": true,
  "Estado": "activa",
  "Mensaje": "La cuenta está activa y lista para operar.",
  "EntityId": "1061",
  "Cvu": "0000247100000000016016"
}
```

#### ? **Caso: En Espera de Aprobación**
```json
{
  "Success": true,
  "Estado": "espera",
  "Mensaje": "La entidad está en estado de espera: 'Pendiente de Aprobación'."
}
```

#### ? **Caso: Debe Crear Cuenta**
```json
{
  "Success": true,
  "Estado": "crear_cuenta",
  "Mensaje": "La entidad no existe en el PSP. Es necesario crear la cuenta."
}
```

#### ? **Caso: Error - Datos Incompletos**
```json
{
  "Success": false,
  "Estado": "error_datos_incompletos",
  "Mensaje": "La cuenta está activa pero faltan datos esenciales (CVU/AccountNumber). Contacte a soporte."
}
```

#### ? **Caso: Error - Sin Cuentas**
```json
{
  "Success": false,
  "Estado": "error_sin_cuenta_activa",
  "Mensaje": "La entidad está activa en PSP pero no se encontraron cuentas asociadas. Contacte a soporte."
}
```

**Response Codes:**
- `200` - Consulta exitosa (puede tener Success=false si hay errores lógicos)
- `401` - UAT inválido
- `500` - Error interno del servidor

---

### **2?? Crear Cuenta PSP (Orquestado)**

Crea usuario PSP, registra entidad y sube archivos en un solo endpoint.

**URL:** `POST /CrearCuentaPSPOrquestado`

**Content-Type:** `multipart/form-data`

**Form Data:**
```
CUIL: "20123456789"              // ? OBLIGATORIO
NOMBRE: "Juan"                   // ? OBLIGATORIO
APELLIDO: "Pérez"                // ? OBLIGATORIO
EMAIL: "juan@example.com"        // ? OBLIGATORIO
PASSWORD: "Abcd1234"             // ? OBLIGATORIO
PHONECODE: "+54"                 // ?? OPCIONAL
TELEFONO: "1123456789"           // ?? OPCIONAL
DIRECCION: "Av. Corrientes 1234" // ?? OPCIONAL
PROVINCIA: "1"                   // ?? OPCIONAL (ID de provincia)
CITYID: "17934"                  // ?? OPCIONAL (ID de ciudad)
POSTALCODE: "1043"               // ?? OPCIONAL

// Archivos (opcionales):
DNI_FRENTE: [archivo]            // ?? OPCIONAL
DNI_DORSO: [archivo]             // ?? OPCIONAL
SELFIE: [archivo]                // ?? OPCIONAL
AFIP_INSCRIPCION: [archivo]      // ?? OPCIONAL
```

**Response (200 OK):**
```json
{
  "Status": 200,
  "Mensaje": "Cuenta PSP creada y archivos subidos correctamente",
  "Success": true,
  "AccountId": 42,
  "PSPUserId": "12345",
  "Identifier": "abc-def-123",
  "UserTokenPreview": "eyJhbGciOiJIUzI1NiIs..."
}
```

**Response (400 Bad Request):**
```json
{
  "Status": 400,
  "Mensaje": "Error creando usuario en PSP",
  "Detail": "Email already exists"
}
```

---

### **3?? Obtener Provincias**

Lista todas las provincias disponibles para registros.

**URL:** `GET /Provinces?uat={UAT}`

**Query Parameters:**
- `uat` (string, obligatorio): Token del usuario

**Response (200 OK):**
```json
{
  "Status": 200,
  "UAT": "abc123",
  "Mensaje": "Provincias obtenidas exitosamente",
  "Success": true,
  "Provinces": [
    {
      "id": 1,
      "name": "Buenos Aires",
      "provinceCode": "BA"
    },
    {
      "id": 2,
      "name": "Córdoba",
      "provinceCode": "CB"
    }
  ]
}
```

---

### **4?? Obtener Ciudades por Provincia**

Lista ciudades de una provincia específica.

**URL:** `GET /Cities?provinceId={ID}&uat={UAT}`

**Query Parameters:**
- `provinceId` (int, obligatorio): ID de la provincia
- `uat` (string, obligatorio): Token del usuario

**Response (200 OK):**
```json
{
  "Status": 200,
  "UAT": "abc123",
  "Mensaje": "Ciudades obtenidas exitosamente para provincia 1",
  "Success": true,
  "Cities": [
    {
      "id": 17934,
      "name": "La Plata",
      "provinceId": 1,
      "postalCode": "1900"
    },
    {
      "id": 17935,
      "name": "Mar del Plata",
      "provinceId": 1,
      "postalCode": "7600"
    }
  ]
}
```

---

### **5?? Obtener Información de Cuentas**

Consulta las cuentas del usuario en el PSP.

**URL:** `GET /AccountsInfo?userToken={TOKEN}&uat={UAT}`

**Query Parameters:**
- `userToken` (string, obligatorio): Token del usuario PSP
- `uat` (string, obligatorio): Token del usuario SmartClick

**Response (200 OK):**
```json
{
  "Status": 200,
  "UAT": "abc123",
  "Mensaje": "Información de cuentas obtenida exitosamente",
  "Success": true,
  "Accounts": [
    {
      "accountNumber": "1234567890",
      "cvU_CBU": "0000247100000000016016",
      "entityId": 1061,
      "name": "Juan Pérez",
      "tributaryIdentifier": "20123456789",
      "accountTypeId": 3,
      "accountTypeDescription": "Cuenta Virtual Uniforme"
    }
  ]
}
```

---

### **6?? Validar Cuenta Externa**

Valida un CVU/CBU/Alias externo antes de transferir.

**URL:** `POST /ValidateExternalAccount`

**Content-Type:** `application/json`

**Request Body:**
```json
{
  "UAT": "abc123",
  "TextSearch": "0000247100000000016016",  // CVU, CBU o Alias
  "UserToken": "eyJhbGci..."               // ?? OPCIONAL
}
```

**Response (200 OK):**
```json
{
  "Status": 200,
  "UAT": "abc123",
  "Mensaje": "Cuenta externa validada",
  "Success": true,
  "Data": {
    "accountNumber": "0000247100000000016016",
    "displayName": "JUAN PEREZ",
    "accountTypeDescription": "Cuenta Virtual Uniforme",
    "tributaryIdentifier": "20123456789",
    "tributaryIdentifierType": "CUIL",
    "pspBankDescription": "Banco Ejemplo"
  }
}
```

**Response (400 Bad Request):**
```json
{
  "Status": 400,
  "UAT": "abc123",
  "Mensaje": "Cuenta externa no encontrada o inválida",
  "Success": false
}
```

---

### **7?? Crear Transferencia**

Realiza una transferencia (interna o externa, se detecta automáticamente).

**URL:** `POST /CreateTransaction`

**Content-Type:** `application/json`

**Request Body:**
```json
{
  "UAT": "abc123",
  "Transaction": {
    "balance": 1000.50,                           // Monto a transferir
    "description": "Pago de servicios",           // ?? OPCIONAL
    "concept": "VAR",                             // ?? OPCIONAL (por defecto "VAR")
    "destinationAccount": {
      "accountNumber": "0000247100000000016016",  // CVU/CBU destino
      "accountTypeId": 3,                         // ?? OPCIONAL
      "tributaryIdentifierType": "CUIL",          // ?? OPCIONAL
      "tributaryIdentifier": "20987654321"        // ?? OPCIONAL
    },
    "currencyTypeId": "1",                        // ?? OPCIONAL (por defecto "1" = Pesos)
    "transactionTypeId": 1,                       // ?? OPCIONAL (por defecto 1 = Débito)
    "availabilityDate": "2024-01-15 14:30:00"     // ?? OPCIONAL (por defecto: ahora)
  }
}
```

**?? Comportamiento Automático:**
- Si el `accountNumber` destino existe en SmartClick ? **Transferencia INTERNA** (sin PSP)
- Si el `accountNumber` destino NO existe localmente ? **Transferencia EXTERNA** (vía PSP)

**Response (200 OK) - Transferencia Interna:**
```json
{
  "Status": 200,
  "UAT": "abc123",
  "Mensaje": "Transferencia interna realizada exitosamente: $1000.50",
  "Success": true,
  "TransactionId": null
}
```

**Response (200 OK) - Transferencia Externa:**
```json
{
  "Status": 200,
  "UAT": "abc123",
  "Mensaje": "Transferencia externa realizada exitosamente: $1000.50",
  "Success": true,
  "TransactionId": 98765,
  "RawResponse": "{\"data\": {...}}"
}
```

**Response (400 Bad Request):**
```json
{
  "Status": 400,
  "UAT": "abc123",
  "Mensaje": "Saldo insuficiente para transferencia externa",
  "Success": false
}
```

---

## ?? **FLUJO RECOMENDADO PARA LOVABLE**

### **?? Caso 1: Usuario Nuevo (Sin Cuenta PSP)**

```
1. Usuario ingresa datos ? POST /CrearCuentaPSPOrquestado
2. Esperar respuesta exitosa
3. Llamar POST /Status para verificar estado
4. Mostrar estado al usuario:
   - "crear_cuenta" ? Cuenta pendiente de creación
   - "espera" ? En aprobación
   - "activa" ? Cuenta lista ?
```

### **?? Caso 2: Usuario Existente (Verificar Estado)**

```
1. Usuario hace login ? Obtener UAT + CUIL
2. Llamar POST /Status (sin UserToken)
3. Según respuesta:
   - "activa" ? Habilitar transferencias
   - "espera" ? Mostrar mensaje de espera
   - "crear_cuenta" ? Redirigir a formulario de creación
```

### **?? Caso 3: Realizar Transferencia**

```
1. Usuario ingresa CVU/CBU destino ? POST /ValidateExternalAccount
2. Mostrar datos del titular para confirmar
3. Usuario confirma ? POST /CreateTransaction
4. Mostrar mensaje de éxito/error
```

---

## ?? **TESTING CON LOVABLE**

### **Ejemplo de Fetch en Lovable:**

```javascript
// 1. Consultar estado de cuenta
const response = await fetch('https://tudominio.com/api/psp/Entities/Status', {
  method: 'POST',
  headers: {
    'Content-Type': 'application/json'
  },
  body: JSON.stringify({
    UAT: userToken,
    Cuil: '20123456789'
  })
});

const data = await response.json();

if (data.Success && data.Estado === 'activa') {
  console.log('Cuenta activa, CVU:', data.Cvu);
} else {
  console.log('Estado:', data.Estado, 'Mensaje:', data.Mensaje);
}
```

---

## ?? **ERRORES COMUNES**

| Error | Causa | Solución |
|-------|-------|----------|
| `401 Unauthorized` | UAT inválido o expirado | Solicitar login nuevamente |
| `400 Bad Request` + `"TextSearch requerido"` | Falta parámetro obligatorio | Verificar JSON enviado |
| `400 Bad Request` + `"Saldo insuficiente"` | No hay fondos | Informar al usuario |
| `500 Internal Server Error` | Error en el servidor | Contactar soporte técnico |

---

## ?? **SOPORTE**

**Backend SmartClick:**
- Repositorio: https://github.com/iscodersdev/SmartClick-Backend
- Contacto: equipo@smartclick.com.ar

**PSP Bitsion:**
- URL Base: `https://btn-des-webapp02.azurewebsites.net`
- Ambiente: Desarrollo

---

## ?? **VERSIONADO**

- **Versión:** 1.0.0
- **Fecha:** Enero 2024
- **Última actualización:** 15/01/2024

---

## ? **CHECKLIST DE INTEGRACIÓN**

- [ ] Obtener credenciales de UAT (login de usuario)
- [ ] Configurar BASE_URL en Lovable
- [ ] Probar endpoint `/Status` con usuario existente
- [ ] Probar flujo de creación de cuenta `/CrearCuentaPSPOrquestado`
- [ ] Implementar validación de cuentas externas
- [ ] Implementar flujo de transferencias
- [ ] Manejar errores 400/401/500
- [ ] Mostrar estados de cuenta al usuario
