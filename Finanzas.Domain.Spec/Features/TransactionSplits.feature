Feature: Distribución Detallada (Splits)
  Como usuario quiero desglosar un movimiento en varias categorías
  Para saber exactamente en qué gasto mi dinero

  @Unitary
  Scenario: Gasto multi-categoría exitoso en una sola compra
    Given una cuenta "Efectivo" con 500 "USD"
    When registro un movimiento de 100 "USD" llamado "Supermercado"
    And distribuyo el gasto en los siguientes splits:
      | Categoría   | Monto |
      | Alimentos   | 70    |
      | Limpieza    | 30    |
    Then el saldo de "Efectivo" debe ser 400 "USD"
    And el movimiento debe tener 2 distribuciones registradas
    And el monto total de los splits debe ser igual a 100 "USD"

  @Unitary @Validation
  Scenario: Error al intentar distribuir un monto diferente al total del movimiento
    Given un movimiento de 100 "USD"
    When intento distribuir el gasto en:
      | Categoría   | Monto |
      | Alimentos   | 50    |
      | Limpieza    | 40    |
    Then el sistema debe rechazar la operación con el mensaje "La suma de los splits debe ser igual al total del movimiento"

  @Unitary @Validation
  Scenario: Impedir splits con montos negativos o cero
    Given un movimiento de 100 "USD"
    When intento distribuir el gasto en:
      | Categoría   | Monto |
      | Alimentos   | -10   |
      | Limpieza    | 110   |
    Then el sistema debe rechazar la operación con el mensaje "El monto de cada distribución debe ser mayor a cero"

  @Unitary
  Scenario: Categorización parcial de un movimiento
    When registro un movimiento de 100 "USD"
    And solo asigno 60 "USD" a la categoría "Salud"
    Then el sistema debe asignar automáticamente 40 "USD" a la categoría "Sin Clasificar"
    And el movimiento debe estar marcado como "Distribución Incompleta"

  @Unitary @Validation
  Scenario: Impedir asignar splits a una categoría inexistente
    Given un movimiento de 100 "USD"
    When intento asignar 100 "USD" a una categoría que no existe
    Then el sistema debe rechazar la operación con el mensaje "La categoría especificada no existe"