Feature: Distribución Detallada (Splits)
  Como usuario quiero desglosar un movimiento en varias categorías
  Para saber exactamente en qué gasto mi dinero

  Scenario: Gasto multi-categoría en una sola compra
    Given una cuenta "Tarjeta" con 500 "USD"
    When registro un movimiento de 100 "USD" llamado "Supermercado"
    And distribuyo el gasto en los siguientes splits:
      | Categoría   | Monto |
      | Alimentos   | 70    |
      | Limpieza    | 30    |
    Then el saldo de "Tarjeta" debe ser 400 "USD"
    And el movimiento debe tener 2 distribuciones registradas

  Scenario: Error al intentar distribuir un monto diferente al total
    Given un movimiento de 100 "USD"
    When intento distribuir el gasto en:
      | Categoría   | Monto |
      | Alimentos   | 50    |
      | Limpieza    | 40    |
    Then el sistema debe rechazar la distribución por integridad de montos

  Scenario: Categorización parcial de un movimiento
    When registro un movimiento de 100 "USD"
    And solo asigno 60 "USD" a la categoría "Salud"
    Then el sistema debe marcar 40 "USD" como "Sin categoría"