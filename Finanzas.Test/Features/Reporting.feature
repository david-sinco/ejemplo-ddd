#language: es
Requisito: Consultas e Informes
  Como usuario quiero ver el estado global de mis finanzas
  Para tomar mejores decisiones económicas basado en datos reales

  @Reporting @ReadOnly
  Escenario: Visualizar resumen de gastos por categoría basado en splits
    Dado las siguientes cuentas configuradas:
      | Nombre   | Saldo Inicial | Moneda |
      | Banco    | 1500          | USD    |
      | Efectivo | 500           | USD    |
    Y las siguientes categorías: "Comida, Transporte, Limpieza"
    Y se han registrado los siguientes movimientos:
      | Descripción   | Cuenta   | Monto Total |
      | Supermercado  | Banco    | 200         |
      | Gasolinera    | Efectivo | 50          |
    Y el movimiento "Supermercado" se distribuyó en:
      | Categoría | Monto |
      | Comida    | 150   |
      | Limpieza  | 50    |
    Cuando consulto el "Resumen de Gastos por Categoría" del mes actual
    Entonces el reporte debe mostrar los siguientes totales:
      | Categoría | Total Gastado |
      | Comida    | 150           |
      | Limpieza  | 50            |
      | Transporte| 0            |
    Y el Patrimonio Neto total debe ser 1750 "USD"