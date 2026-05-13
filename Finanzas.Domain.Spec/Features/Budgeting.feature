Feature: Control de Presupuestos
  Como usuario quiero establecer límites de gasto
  Para no gastar más de lo que gano

  Scenario: Establecer un presupuesto mensual
    When defino un presupuesto de 400 "USD" para la categoría "Ocio" en "Mayo 2026"
    Then el presupuesto de "Ocio" debe estar activo para el periodo actual

  Scenario: Alerta por exceso de presupuesto
    Given un presupuesto de 100 "USD" para "Restaurantes"
    And un gasto previo de 80 "USD" en "Restaurantes"
    When registro un nuevo egreso de 30 "USD" en "Restaurantes"
    Then el sistema debe emitir una alerta de exceso de presupuesto