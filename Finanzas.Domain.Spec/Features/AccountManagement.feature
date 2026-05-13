Feature: Gestión de Cuentas
  Como usuario quiero administrar mis cuentas físicas y lógicas
  Para tener un reflejo fiel de dónde está mi dinero

  @Unitary
  Scenario: Apertura exitosa de una nueva cuenta
    Given que no existe una cuenta llamada "Ahorros"
    When abro una cuenta con los siguientes datos:
      | Nombre  | Saldo Inicial | Moneda | Icono        |
      | Ahorros | 1000          | USD    | university   |
    Then la cuenta "Ahorros" debe estar activa
    And el saldo actual debe ser 1000 "USD"
    And el saldo inicial registrado debe ser 1000 "USD"

  @Unitary @Validation
  Scenario: Impedir la creación de una cuenta con nombre muy corto
    When intento abrir una cuenta con el nombre "CC"
    Then el sistema debe rechazar la operación con el mensaje "El nombre de la cuenta debe tener al menos 3 caracteres"

  @Integration @Validation
  Scenario: Impedir nombres de cuenta duplicados
    Given que ya existe una cuenta llamada "Efectivo"
    When intento abrir una cuenta con el nombre "Efectivo"
    Then el sistema debe rechazar la operación con el mensaje "Ya existe una cuenta con el nombre especificado"

  @Unitary @Validation
  Scenario: No permitir cerrar una cuenta que aún tiene fondos
    Given una cuenta llamada "Nómina" con saldo de 150 "USD"
    When cierro la cuenta "Nómina"
    Then el sistema debe rechazar la operación con el mensaje "No se puede cerrar una cuenta con saldo mayor a cero"

  @Unitary
  Scenario: Cierre exitoso de cuenta sin saldo
    Given una cuenta llamada "Billetera Vieja" con saldo de 0 "USD"
    When cierro la cuenta "Billetera Vieja"
    Then la cuenta "Billetera Vieja" debe estar inactiva
    And el sistema debe rechazar cualquier intento de registrar movimientos con el mensaje "La cuenta está inactiva"

  @Unitary
  Scenario: Ajuste manual de saldo por discrepancia
    Given una cuenta llamada "Banco" con saldo de 500 "USD"
    When ajusto el saldo de la cuenta "Banco" a 480 "USD" por motivo "Gasto no registrado"
    Then el saldo actual de la cuenta "Banco" debe ser 480 "USD"
    And debe existir un registro de ajuste en los movimientos por "20 USD"