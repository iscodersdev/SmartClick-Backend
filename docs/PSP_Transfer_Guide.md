Guía para Front: Transferencias a cuentas externas (mismo titular local)

Resumen rápido
- El front debe siempre referenciar la cuenta destino por CVU/CBU/alias (no enviar solo CUIT).
- Flujo recomendado:
  1) Validar cuenta destino (Mostrar nombre/CUIT).  
  2) Confirmar monto/usuario.  
  3) Crear transacción.

Endpoints principales (backend)
Base: https://{TU_BACKEND_HOST}/api/psp/Entities

1) Validar cuenta destino
- URL: POST /api/psp/Entities/ValidateExternalAccount
- Headers: Content-Type: application/json
- Body (ejemplo):
  {
    "UAT": "uat-del-admin",
    "TextSearch": "0000570800000000017422",
    "UserToken": "token-del-usuario-en-psp"  // opcional pero recomendado
  }
- Qué hace el backend:
  - Llama al PSP /Person/ContactNotebook/Get con JSON { "textSearch": "..." } usando Authorization: Bearer {userToken} si se pasó, o token de sistema como fallback.
  - Devuelve data con accountNumber, displayName, tributaryIdentifier (CUIT), accountTypeDescription, pspBankDescription, virtual, etc.
- Respuesta ejemplo:
  {
    "Status":200,
    "UAT":"uat-del-admin",
    "Mensaje":"Cuenta externa validada",
    "Success":true,
    "Data":{
      "externalAccountId":236,
      "accountNumber":"0000570800000000017422",
      "displayName":"camaraImpuestos",
      "tributaryIdentifier":"30714093661",
      "tributaryIdentifierType":"CUIT",
      "pspBankDescription":"Banco Industrial S.A.",
      "virtual":true
    }
  }

2) Crear transacción
- URL: POST /api/psp/Entities/CreateTransaction
- Headers: Content-Type: application/json
- Body (esquema mínimo):
  {
    "UAT": "uat-del-admin",
    "UserToken": "token-del-usuario-en-psp",
    "Transaction": {
      "destinationAccount": { "accountNumber": "0000570800000000017422" },
      "balance": 1500.50,
      "currencyTypeId": "1",
      "isExternal": true,
      "transactionTypeId": 1,
      "concept": "Pago factura"
    }
  }
- Flujo interno resumido:
  a) Valida UAT.
  b) Si destinationAccount.accountNumber existe en BD local (Billeteras.CVU) -> transferencia 100% local: crear movimientos, actualizar saldos y responder OK.
  c) Si no es CVU local y isExternal == true:
     - Si cliente local tiene CUIL guardado: backend llama ValidateExternalAccount (usa userToken si fue pasado al endpoint anterior o aquí) y compara tributaryIdentifier (PSP) con CUIL local (solo dígitos).
       - Si no coinciden -> rechaza (400).
       - Si coinciden -> pasa localCuit a CreateTransactionAsync para optimizar validación.
     - Llama PSP /Accounts/Transactions/Add con Authorization: Bearer {UserToken} para crear la transacción.
  d) Si PSP OK, backend intenta registrar débito local (MovimientoBilletera) en la billetera origen para reflejar salida.
- Respuesta éxito ejemplo:
  {
    "Status":200,
    "UAT":"uat-del-admin",
    "Mensaje":"Transacción iniciada",
    "Success":true,
    "TransactionId": 12345,
    "RawResponse": "{...respuesta PSP...}"
  }

Casos de uso (UX)
- Caso A: destino CVU local -> mostrar confirmación y que la transferencia fue procesada localmente.
- Caso B: destino externo pero mismo titular -> backend crea transacción en PSP y debita localmente; informar "iniciada" y mostrar TransactionId si existe.
- Caso C: destino externo distinto titular -> backend devuelve 400 con mensaje "CUIT/CUIL local no coincide...". Mostrar al usuario y abortar.

Recomendaciones front
- Siempre llamar ValidateExternalAccount antes de pedir confirmación al usuario.
- Mostrar displayName, accountNumber, banco y opcionalmente tributaryIdentifier (CUIT) al usuario.
- Solicitar confirmación explícita (CVU, nombre, CUIT, monto).
- En CreateTransaction pasar UAT y UserToken junto al objeto Transaction.
- Manejar respuestas 200 (éxito), 400 (errores de validación), 401 (UAT inválido) y 500 (error interno).

Tokens y seguridad
- UAT: token de sesión local (siempre en body) para que backend valide sesión y obtenga cliente/Persona.
- UserToken: token del usuario en PSP (debe enviarse en CreateTransaction y es recomendable en ValidateExternalAccount).
- Transmitir siempre por HTTPS y no loguear tokens en el cliente ni en el backend.
- El backend valida UAT; adicionalmente se recomienda verificar que el UserToken pertenezca al usuario asociado al UAT (por ejemplo mediante GetAccountsInfo) si hace falta mayor seguridad.

Limitaciones y notas técnicas
- La operación PSP y el débito local no son atómicas. Puede ocurrir que PSP confirme y luego falle el registro local. Implementar conciliación/alertas en producción.
- En TestMode (config PSP:TestMode = true) muchas operaciones responden simuladas.
- Si el front no envía UserToken, el backend usa token de sistema para ValidateExternalAccount; esto puede devolver resultados distintos.

Ejemplos curl
1) ValidateExternalAccount (usando user token):

curl -X POST "https://{TU_BACKEND_HOST}/api/psp/Entities/ValidateExternalAccount" \
  -H "Content-Type: application/json" \
  -d '{"UAT":"uat-del-admin","TextSearch":"0000570800000000017422","UserToken":"<USER_TOKEN>"}'

2) CreateTransaction:

curl -X POST "https://{TU_BACKEND_HOST}/api/psp/Entities/CreateTransaction" \
  -H "Content-Type: application/json" \
  -d '{"UAT":"uat-del-admin","UserToken":"<USER_TOKEN>","Transaction": {"destinationAccount": {"accountNumber":"0000570800000000017422"},"balance":100.50,"currencyTypeId":"1","isExternal":true}}'

Dónde está este documento
- Archivo creado en el repositorio local: docs/PSP_Transfer_Guide.md

Cómo obtenerlo / descargarlo
- En local (desde la raíz del repo): abrir docs/PSP_Transfer_Guide.md con tu editor.
- Para obtenerlo vía Git (si quieres subirlo al remote):
  git add docs/PSP_Transfer_Guide.md
  git commit -m "Agregar guía PSP para Front"
  git push origin psp-actualizacion

Soporte
- Si querés que haga push yo (commit + push), confirmá y lo ejecuto.
- Puedo ajustar el README con más ejemplos o agregar un archivo de Postman si lo necesitás.
