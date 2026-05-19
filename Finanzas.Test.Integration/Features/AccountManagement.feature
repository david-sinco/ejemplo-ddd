#language: es
Requisito: Gestion de Cuentas
  Como usuario quiero administrar mis cuentas físicas y lógicas
  Para tener un reflejo fiel de dónde está mi dinero

  @Unitary
  Escenario: Apertura exitosa de una nueva cuenta
    Dado que no existe una cuenta llamada "Ahorros"
    Cuando abro una cuenta con los siguientes datos:
      | Nombre  | Saldo Inicial | Moneda | Icono        |
      | Ahorros | 1000          | USD    | university   |
    Entonces la cuenta "Ahorros" debe estar activa
    Y el saldo actual debe ser 1000 "USD"
    Y el saldo inicial registrado debe ser 1000 "USD"

  @Unitary @Validation
  Escenario: Impedir la creación de una cuenta con nombre muy corto
    Cuando intento abrir una cuenta con el nombre "CC"
    Entonces el sistema debe rechazar la operación con el mensaje "El nombre de la cuenta debe tener al menos 3 caracteres"

  @Integration @Validation
  Escenario: Impedir nombres de cuenta duplicados
    Dado que ya existe una cuenta llamada "Efectivo"
    Cuando intento abrir una cuenta con el nombre "Efectivo"
    Entonces el sistema debe rechazar la operación con el mensaje "Ya existe una cuenta con el nombre especificado"

  @Unitary @Validation
  Escenario: No permitir cerrar una cuenta que aún tiene fondos
    Dado una cuenta llamada "Nómina" con saldo de 150 "USD"
    Cuando cierro la cuenta "Nómina"
    Entonces el sistema debe rechazar la operación con el mensaje "No se puede cerrar una cuenta con saldo mayor a cero"

  @Unitary
  Escenario: Cierre exitoso de cuenta sin saldo
    Dado una cuenta llamada "Billetera Vieja" con saldo de 0 "USD"
    Cuando cierro la cuenta "Billetera Vieja"
    Entonces la cuenta "Billetera Vieja" debe estar inactiva
    Y el sistema debe rechazar cualquier intento de registrar movimientos con el mensaje "La cuenta está inactiva"

  @Unitary
  Escenario: Ajuste manual de saldo por discrepancia
    Dado una cuenta llamada "Banco" con saldo de 500 "USD"
    Cuando ajusto el saldo de la cuenta "Banco" a 480 "USD" por motivo "Gasto no registrado"
    Entonces el saldo actual de la cuenta "Banco" debe ser 480 "USD"
    Y debe existir un registro de ajuste en los movimientos por "20 USD"