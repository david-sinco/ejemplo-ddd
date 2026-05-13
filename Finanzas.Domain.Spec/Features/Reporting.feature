Feature: Consultas e Informes
  Como usuario quiero ver el estado global de mis finanzas
  Para tomar mejores decisiones económicas

  Scenario: Visualizar resumen financiero mensual (Reporte)
    Given las siguientes cuentas y saldos:
      | Cuenta   | Saldo    |
      | Banco    | 1000 USD |
      | Efectivo | 200 USD  |
    And un presupuesto de 300 USD para "Comida" con gasto real de 100 USD
    When consulto el informe de situación actual
    Then el Patrimonio Neto total debe ser 1200 USD
    And el gasto por categoría debe mostrar "Comida" con 100 USD
    And la disponibilidad del presupuesto "Comida" debe ser 200 USD