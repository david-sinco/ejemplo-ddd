Feature: Consultas e Informes
  Como usuario quiero ver el estado global de mis finanzas
  Para tomar mejores decisiones económicas basado en datos reales

  @Reporting @ReadOnly
  Scenario: Visualizar resumen de gastos por categoría basado en splits
    Given las siguientes cuentas configuradas:
      | Nombre   | Saldo Inicial | Moneda |
      | Banco    | 1500          | USD    |
      | Efectivo | 500           | USD    |
    And las siguientes categorías: "Comida, Transporte, Limpieza"
    And se han registrado los siguientes movimientos:
      | Descripción   | Cuenta   | Monto Total |
      | Supermercado  | Banco    | 200         |
      | Gasolinera    | Efectivo | 50          |
    And el movimiento "Supermercado" se distribuyó en:
      | Categoría | Monto |
      | Comida    | 150   |
      | Limpieza  | 50    |
    When consulto el "Resumen de Gastos por Categoría" del mes actual
    Then el reporte debe mostrar los siguientes totales:
      | Categoría | Total Gastado |
      | Comida    | 150           |
      | Limpieza  | 50            |
      | Transporte| 0            |
    And el Patrimonio Neto total debe ser 1750 "USD"