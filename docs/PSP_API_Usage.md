Documentación rápida: PSP endpoints para Front
===============================================

Resumen
-------
Documento breve con ejemplos (payload + respuestas) para que el equipo frontend pueda probar los endpoints PSP expuestos en la API.
Todos los endpoints están bajo: /api/psp/Entities

1) CreateTransaction (crear transferencia)
----------------------------------------
- Método: POST
- URL: /api/psp/Entities/CreateTransaction
- Content-Type: application/json
- Autorización: Se valida UAT en el cuerpo (token interno). Además la operación requiere el UserToken (token PSP) del usuario que autoriza la transferencia.

Request JSON (ejemplo)
{
  "UAT": "uat_del_admin",
  "UserToken": "token_del_usuario_psp",
  "Transaction": {
    "currencyTypeId": "1",
    "balance": 10.0,
    "transactionTypeId": 1,
    "availabilityDate": "2025-02-21 15:37:45",
    "concept": "Pago servicio",
    "validationCode": "",
    "isExternal": true,
    "originAccount": {
      "accountNumber": "30707609032-00000550",
      "accountTypeId": 0,
      "tributaryIdentifierType": "",
      "tributaryIdentifier": ""
    },
    "destinationAccount": {
      "accountNumber": "0000173700000000013703",
      "accountTypeId": 0,
      "tributaryIdentifierType": "CUIT",
      "tributaryIdentifier": "27422125073",
      "isExternal": false,
      "currencyTypeId": 0,
      "name": "Cliente Destino"
    }
  }
}

Notas sobre campos
- UAT: token interno usado por la API para validar que quien llama está autorizado.
- UserToken: token del usuario PSP (necesario para llamadas que actúan "a nombre" del usuario). Para transacciones externas debe proveerse.
- availabilityDate: formato "yyyy-MM-dd HH:mm:ss".
- isExternal: true = se valida contra PSP (se comparará tributaryIdentifier del lookup con los CUITs del usuario interno obtenido vía AccountsInfo).
- originAccount / destinationAccount: estructura mínima; el campo accountNumber es usado para validar/crear la transferencia.

Respuesta exitosa (ejemplo)
{
  "Status": 200,
  "UAT": "uat_del_admin",
  "Mensaje": "Transacción iniciada",
  "Success": true,
  "TransactionId": 33460,
  "RawResponse": "{\"data\":{\"transactionId\":33460,\"messageResultTransfer\":\"Transacción pendiente de validación\"}}"
}

Respuesta de error (ejemplo de validación CUIT)
{
  "Status": 400,
  "UAT": "uat_del_admin",
  "Mensaje": "La cuenta externa no pertenece al mismo CUIT/CUIL que el usuario autenticado",
  "Success": false
}

Curl de ejemplo
curl -X POST "https://tu-api/api/psp/Entities/CreateTransaction" \
  -H "Content-Type: application/json" \
  -d '{"UAT":"uat_del_admin","UserToken":"token_usuario","Transaction":{...}}'


2) ValidateExternalAccount (validar cuenta externa)
---------------------------------------------------
- Método: POST
- URL: /api/psp/Entities/ValidateExternalAccount
- Body: { "UAT": "uat_del_admin", "TextSearch": "0000173700000000013703" }
- Respuesta: ExternalAccountWithUATResponseDTO con Data = ExternalAccountData (incluye tributaryIdentifier y tipo)

Request ejemplo
{
  "UAT": "uat_del_admin",
  "TextSearch": "0000173700000000013703"
}

Respuesta exitosa (ejemplo)
{
  "Status": 200,
  "UAT": "uat_del_admin",
  "Mensaje": "Cuenta externa validada",
  "Success": true,
  "Data": {
    "externalAccountId": 999,
    "accountNumber": "0000173700000000013703",
    "tributaryIdentifier": "30714093661",
    "tributaryIdentifierType": "CUIT",
    ...
  }
}

Notas: en modo Test (appsettings PSP:TestMode = true) el servicio devuelve un mock con tributaryIdentifier = "30714093661".

3) AccountsInfo (obtener CUIT(s) del usuario PSP)
-------------------------------------------------
- Método: GET
- URL: /api/psp/Entities/AccountsInfo?userToken={userToken}&uat={uat}
- Requiere userToken (token PSP del usuario) y UAT (administrador)
- Respuesta: lista de AccountInfoDTO con campo tributaryIdentifier (CUIT/CUIL)

Uso recomendado por Front
- Para probar validación de cuentas externas: primero hacer ValidateExternalAccount (si queréis sólo ver datos de la cuenta). Para correr CreateTransaction el front debe incluir UserToken y UAT.
- En integración real: front obtiene userToken mediante el flujo de autenticación PSP del usuario (no se gestiona aquí).

Comportamiento de test
- Si PSP:TestMode = true en appsettings, ValidateExternalAccount y CreateTransaction responderán con mocks para facilitar pruebas sin PSP real.

Puntos importantes para el equipo
- Formato de availabilityDate: "yyyy-MM-dd HH:mm:ss" (debe enviarlo el frontend).
- El endpoint CreateTransaction no persiste movimientos locales (saldo) todavía — sólo orquesta la llamada al PSP y devuelve resultado. La persistencia la implementaremos en otra tarea.
- Revisar logs de respuesta (RawResponse) para debugging.

Si querés, genero un ejemplo Postman collection o un README en la carpeta del frontend con estos ejemplos. ¿Lo genero ahora? 
