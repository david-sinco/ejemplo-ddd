#language: es
Requisito: Registro de Movimientos
  Como usuario quiero registrar mis ingresos, egresos y transferencias
  Para mantener mis saldos actualizados

  @Unitary
  Escenario: Registro de un ingreso de dinero exitoso
    Dado una cuenta llamada "Efectivo" con saldo de 0 "USD"
    Cuando registro un ingreso de 2000 "USD" por "Pago de Nómina"
    Entonces el saldo de la cuenta "Efectivo" debe ser 2000 "USD"

  @Unitary @Validation
  Escenario: Impedir el registro de movimientos con monto cero o negativo
    Dado una cuenta llamada "Efectivo"
    Cuando intento registrar un ingreso de 0 "USD"
    Entonces el sistema debe rechazar la operación con el mensaje "El monto del movimiento debe ser mayor a cero"

  @Unitary
  Escenario: Registro de un egreso simple
    Dado una cuenta llamada "Billetera" con saldo de 100 "USD"
    Cuando registro un egreso de 20 "USD" en la categoría "Alimentación"
    Entonces el saldo de la cuenta "Billetera" debe ser 80 "USD"

  @Unitary @Validation
  Escenario: Impedir egresos que superen el saldo disponible
    Dado una cuenta llamada "Billetera" con saldo de 50 "USD"
    Cuando intento registrar un egreso de 60 "USD"
    Entonces el sistema debe rechazar la operación con el mensaje "Saldo insuficiente para realizar el movimiento"

  @Unitary
  Escenario: Transferencia entre cuentas propias
    Dado una cuenta "Ahorros" con 1000 "USD"
    Y una cuenta "Efectivo" con 50 "USD"
    Cuando transfiero 200 "USD" desde "Ahorros" hacia "Efectivo"
    Entonces el saldo de "Ahorros" debe ser 800 "USD"
    Y el saldo de "Efectivo" debe ser 250 "USD"

  @Unitary @Validation
  Escenario: Impedir transferencias si la cuenta origen no tiene fondos suficientes
    Dado una cuenta "Ahorros" con 100 "USD"
    Y una cuenta "Efectivo" con 0 "USD"
    Cuando intento transferir 150 "USD" desde "Ahorros" hacia "Efectivo"
    Entonces el sistema debe rechazar la operación con el mensaje "Saldo insuficiente para realizar la transferencia"

  @Unitary
  Escenario: Eliminar un movimiento existente
    Dado una cuenta "Banco" con saldo de 500 "USD"
    Y un movimiento de egreso registrado por 50 "USD"
    Cuando elimino el movimiento de 50 "USD"
    Entonces el movimiento ya no debe existir en el historial
    Y el saldo de la cuenta "Banco" debe ser 550 "USD"