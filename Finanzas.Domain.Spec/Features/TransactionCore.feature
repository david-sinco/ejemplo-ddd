Feature: Registro de Movimientos
  Como usuario quiero registrar mis ingresos, egresos y transferencias
  Para mantener mis saldos actualizados

  Scenario: Registro de un ingreso de dinero
    Given una cuenta llamada "Sueldo" con saldo de 0 "USD"
    When registro un ingreso de 2000 "USD" por "Pago de Nómina"
    Then el saldo de la cuenta "Sueldo" debe ser 2000 "USD"

  Scenario: Registro de un egreso simple
    Given una cuenta llamada "Billetera" con saldo de 100 "USD"
    When registro un egreso de 20 "USD" en la categoría "Alimentación"
    Then el saldo de la cuenta "Billetera" debe ser 80 "USD"

  Scenario: Transferencia entre cuentas propias
    Given una cuenta "Ahorros" con 1000 "USD"
    And una cuenta "Efectivo" con 50 "USD"
    When transfiero 200 "USD" desde "Ahorros" hacia "Efectivo"
    Then el saldo de "Ahorros" debe ser 800 "USD"
    And el saldo de "Efectivo" debe ser 250 "USD"

  Scenario: Anulación de una transacción errónea
    Given una cuenta "Banco" con saldo de 500 "USD"
    And una transacción de egreso previa de 50 "USD"
    When anulo la transacción de 50 "USD"
    Then el saldo de la cuenta "Banco" debe ser 550 "USD"