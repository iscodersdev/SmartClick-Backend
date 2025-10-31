Guía para Front: Transferencias internas (entre billeteras locales)

Resumen rápido
- Las transferencias internas se procesan 100% en la base de datos local si el CVU/CBU destino existe en la tabla Billeteras (campo CVU).
- Endpoint a usar: POST /api/psp/Entities/CreateTransaction
- El front debe incluir UAT y UserToken; para transferencias internas no se llama al PSP.

Flujo frontend (recomendado)
1) El usuario selecciona contacto local o ingresa CVU/CBU.
2) El front puede opcionalmente llamar a un endpoint local para obtener información del contacto almacenado (API propia si existe).
3) Construir body para CreateTransaction y pedir confirmación:
   - Marcar isExternal = false (recomendado) cuando se trate de transferencia a CVU local.
4) Llamar POST /api/psp/Entities/CreateTransaction con UAT + UserToken + Transaction.

Body mínimo (ejemplo)
{
  "UAT": "uat-del-admin",
  "UserToken": "token-del-usuario-en-psp",
  "Transaction": {
    "destinationAccount": { "accountNumber": "0000570800000000017422" },
    "balance": 500.00,
    "currencyTypeId": "1",
    "isExternal": false,
    "transactionTypeId": 1,
    "concept": "Transferencia entre usuarios"
  }
}

Qué hace el backend (pasos internos)
1) Valida UAT y que Transaction y UserToken estén presentes.
2) Extrae destinationAccount.accountNumber y busca en _context.Billeteras por CVU == accountNumber.
3) Si encuentra billeteraDestino:
   a) Obtiene clienteOrigen asociado al UAT y su billeteraOrigen.
   b) Valida monto (Convert.ToDecimal y ChequeaDebito en billeteraOrigen).
   c) Crea MovimientoBilletera de ingreso para billeteraDestino y de envío para billeteraOrigen.
   d) Actualiza saldos (billeteraDestino.Saldo += monto; billeteraOrigen.Saldo -= monto).
   e) Añade contactos locales (ContactosBilletera) entre origen y destino.
   f) _context.Update y _context.SaveChanges() para persistir.
   g) Devuelve Status 200 con Mensaje "Transferencia interna realizada".
4) Si NO encuentra CVU local -> sigue flujo externo (se llama PSP en CreateTransactionAsync).

Respuesta exitosa (ejemplo)
{
  "Status": 200,
  "UAT": "uat-del-admin",
  "Mensaje": "Transferencia interna realizada",
  "Success": true
}

Errores comunes a manejar en Front
- 400 Monto inválido: monto mal formateado.
- 400 El monto supera el saldo: saldo insuficiente.
- 400 No se encontró billetera de origen: usuario sin billetera configurada.
- 401 Usuario no autenticado: UAT inválido.
- 500 Error interno: intentar de nuevo y reportar logs.

Consideraciones técnicas y recomendaciones
- isExternal: aunque la detección de CVU local no depende de isExternal, enviar isExternal=false aclara intención y evita validaciones innecesarias contra PSP.
- Atomicidad: la operación local es atómica a nivel DB (SaveChanges), pero no hay interacción PSP en este caso.
- Concurrencia: usar controles a nivel DB si múltiples envíos simultáneos pueden descontar saldo (optimista/pesimista según diseño). Recomendado: comprobar ChequeaDebito inmediatamente antes de disminuir saldo dentro de la misma transacción DB.
- Contactos: el código añade ContactosBilletera automáticamente; el front puede ofrecer opción para "guardar contacto" pero el backend lo hace de todos modos.
- Logs y conciliación: registrar id de transacción local en logs para auditoría.

Checks para QA
- Transferir monto menor que saldo -> OK y saldos actualizados.
- Transferir monto igual al saldo -> OK y origen saldo = 0.
- Transferir monto mayor que saldo -> se rechaza con mensaje.
- Intentar transferencia a CVU que no existe -> no es interna; backend seguirá flujo externo.

Dónde está este documento
- Archivo creado: docs/Local_Transfer_Guide.md

Cómo obtenerlo
- Abrir docs/Local_Transfer_Guide.md en el repo local.
- Para subir al remoto: git add docs/Local_Transfer_Guide.md && git commit -m "Agregar guía transferencias locales" && git push origin psp-docs-guide

Si querés, hago el commit+push ahora en la rama psp-docs-guide. ¿Procedo?"