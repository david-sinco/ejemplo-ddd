#language: es
@Accounts
Requisito: Gestion de Cuentas
  Como usuario autenticado 
  Quiero administrar mis cuentas físicas y lógicas
  Para tener un reflejo fiel de dónde está mi dinero

  Antecedentes:
    Dado que existe un usuario con los siguientes datos:
    | Id                                   | UserName                | Email                   | Password |
    | 4f9df00a-f180-4d44-bd75-98413e5562d2 | david.gonzalez@sinco.co | david.gonzalez@sinco.co |          |
    Dado que el usuario "david.gonzalez@sinco.co" con id "4f9df00a-f180-4d44-bd75-98413e5562d2" ha iniciado sesión

  Escenario: Apertura exitosa de una nueva cuenta
    Dado que no existe una cuenta llamada "Ahorros"
    Cuando abro una cuenta con los siguientes datos:
      | Nombre  | Saldo Inicial | Moneda | Icono      |
      | Ahorros |          1000 | USD    | university |
    Entonces la cuenta "Ahorros" debe estar activa
    Y la cuenta "Ahorros" debe tener un saldo de "1000" "USD"
    Y la cuenta "Ahorros" debe tener un saldo inicial de "1000" "USD"
    Y el id del usuario de la cuenta "Ahorros" debe pertenecer a "david.gonzalez@sinco.co"

  Escenario: Impedir la creación de una cuenta con nombre muy corto
    Cuando intento abrir una cuenta con el nombre "CC"
    Entonces el sistema debe rechazar la operación con el mensaje "El nombre de la cuenta debe tener al menos 3 caracteres"

  Escenario: Impedir nombres de cuenta duplicados
    Dado que ya existe una cuenta llamada "Efectivo"
    Cuando intento abrir una cuenta con el nombre "Efectivo"
    Entonces el sistema debe rechazar la operación con el mensaje "Ya existe una cuenta con el nombre especificado"

  Escenario: No permitir cerrar una cuenta que aún tiene fondos
    Dado una cuenta llamada "Nómina" con saldo de 150 "USD"
    Cuando cierro la cuenta "Nómina"
    Entonces el sistema debe rechazar la operación con el mensaje "No se puede cerrar una cuenta con saldo mayor a cero"

  Escenario: Cierre exitoso de cuenta sin saldo
    Dado una cuenta llamada "Billetera Vieja" con saldo de 0 "USD"
    Cuando cierro la cuenta "Billetera Vieja"
    Entonces la cuenta "Billetera Vieja" debe estar inactiva
    Y el sistema debe rechazar cualquier intento de registrar movimientos con el mensaje "La cuenta está inactiva"

  Escenario: Ajuste manual de saldo por discrepancia
    Dado una cuenta llamada "Banco" con saldo de 500 "USD"
    Cuando ajusto el saldo de la cuenta "Banco" a 480 "USD" por motivo "Gasto no registrado"
    Entonces el saldo actual de la cuenta "Banco" debe ser 480 "USD"
    Y debe existir un registro de ajuste en los movimientos por "20 USD"