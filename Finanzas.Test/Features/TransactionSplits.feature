#language: es
Requisito: Distribución Detallada (Splits)
  Como usuario quiero desglosar un movimiento en varias categorías
  Para saber exactamente en qué gasto mi dinero

  @Unitary
  Escenario: Gasto multi-categoría exitoso en una sola compra
    Dado una cuenta "Efectivo" con 500 "USD"
    Cuando registro un movimiento de 100 "USD" llamado "Supermercado"
    Y distribuyo el gasto en los siguientes splits:
      | Categoría   | Monto |
      | Alimentos   | 70    |
      | Limpieza    | 30    |
    Entonces el saldo de "Efectivo" debe ser 400 "USD"
    Y el movimiento debe tener 2 distribuciones registradas
    Y el monto total de los splits debe ser igual a 100 "USD"

  @Unitary @Validation
  Escenario: Error al intentar distribuir un monto diferente al total del movimiento
    Dado un movimiento de 100 "USD"
    Cuando intento distribuir el gasto en:
      | Categoría   | Monto |
      | Alimentos   | 50    |
      | Limpieza    | 40    |
    Entonces el sistema debe rechazar la operación con el mensaje "La suma de los splits debe ser igual al total del movimiento"

  @Unitary @Validation
  Escenario: Impedir splits con montos negativos o cero
    Dado un movimiento de 100 "USD"
    Cuando intento distribuir el gasto en:
      | Categoría   | Monto |
      | Alimentos   | -10   |
      | Limpieza    | 110   |
    Entonces el sistema debe rechazar la operación con el mensaje "El monto de cada distribución debe ser mayor a cero"

  @Unitary
  Escenario: Categorización parcial de un movimiento
    Cuando registro un movimiento de 100 "USD"
    Y solo asigno 60 "USD" a la categoría "Salud"
    Entonces el sistema debe asignar automáticamente 40 "USD" a la categoría "Sin Clasificar"
    Y el movimiento debe estar marcado como "Distribución Incompleta"

  @Unitary @Validation
  Escenario: Impedir asignar splits a una categoría inexistente
    Dado un movimiento de 100 "USD"
    Cuando intento asignar 100 "USD" a una categoría que no existe
    Entonces el sistema debe rechazar la operación con el mensaje "La categoría especificada no existe"