Feature: Registro de Movimientos
  Como usuario quiero registrar mis ingresos, egresos y transferencias
  Para mantener mis saldos actualizados

  @Unitary
  Scenario: Registro de un ingreso de dinero exitoso
    Given una cuenta llamada "Efectivo" con saldo de 0 "USD"
    When registro un ingreso de 2000 "USD" por "Pago de Nómina"
    Then el saldo de la cuenta "Efectivo" debe ser 2000 "USD"

  @Unitary @Validation
  Scenario: Impedir el registro de movimientos con monto cero o negativo
    Given una cuenta llamada "Efectivo"
    When intento registrar un ingreso de 0 "USD"
    Then el sistema debe rechazar la operación con el mensaje "El monto del movimiento debe ser mayor a cero"

  @Unitary
  Scenario: Registro de un egreso simple
    Given una cuenta llamada "Billetera" con saldo de 100 "USD"
    When registro un egreso de 20 "USD" en la categoría "Alimentación"
    Then el saldo de la cuenta "Billetera" debe ser 80 "USD"

  @Unitary @Validation
  Scenario: Impedir egresos que superen el saldo disponible
    Given una cuenta llamada "Billetera" con saldo de 50 "USD"
    When intento registrar un egreso de 60 "USD"
    Then el sistema debe rechazar la operación con el mensaje "Saldo insuficiente para realizar el movimiento"

  @Unitary
  Scenario: Transferencia entre cuentas propias
    Given una cuenta "Ahorros" con 1000 "USD"
    And una cuenta "Efectivo" con 50 "USD"
    When transfiero 200 "USD" desde "Ahorros" hacia "Efectivo"
    Then el saldo de "Ahorros" debe ser 800 "USD"
    And el saldo de "Efectivo" debe ser 250 "USD"

  @Unitary @Validation
  Scenario: Impedir transferencias si la cuenta origen no tiene fondos suficientes
    Given una cuenta "Ahorros" con 100 "USD"
    And una cuenta "Efectivo" con 0 "USD"
    When intento transferir 150 "USD" desde "Ahorros" hacia "Efectivo"
    Then el sistema debe rechazar la operación con el mensaje "Saldo insuficiente para realizar la transferencia"

  @Unitary
  Scenario: Eliminar un movimiento existente
    Given una cuenta "Banco" con saldo de 500 "USD"
    And un movimiento de egreso registrado por 50 "USD"
    When elimino el movimiento de 50 "USD"
    Then el movimiento ya no debe existir en el historial
    And el saldo de la cuenta "Banco" debe ser 550 "USD"