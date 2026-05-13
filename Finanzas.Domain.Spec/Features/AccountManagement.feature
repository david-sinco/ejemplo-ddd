Feature: Gestión de Cuentas
  Como usuario quiero administrar mis cuentas físicas y lógicas
  Para tener un reflejo fiel de dónde está mi dinero

  Scenario: Apertura de una nueva cuenta
    Given que no existe una cuenta llamada "Ahorros"
    When abro una cuenta llamada "Ahorros" con saldo inicial de 1000 "USD"
    Then la cuenta "Ahorros" debe estar activa
    And el saldo de la cuenta "Ahorros" debe ser 1000 "USD"

  Scenario: Cierre de cuenta para evitar nuevos movimientos
    Given una cuenta llamada "Efectivo" con saldo de 50 "USD"
    When cierro la cuenta "Efectivo"
    Then la cuenta "Efectivo" debe estar inactiva
    And no se debe permitir registrar nuevos movimientos en la cuenta "Efectivo"

  Scenario: Ajuste manual de saldo por discrepancia
    Given una cuenta llamada "Banco" con saldo de 500 "USD"
    When ajusto el saldo de la cuenta "Banco" a 480 "USD" por motivo "Comisión no registrada"
    Then el saldo de la cuenta "Banco" debe ser 480 "USD"