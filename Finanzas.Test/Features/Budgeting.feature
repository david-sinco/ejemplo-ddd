#language: es
Requisito: Control de Presupuestos
  CORREGIR ESTE FEATURE POR QUE ESTA MAL LA ESPECIFICACION
  Como usuario quiero establecer límites de gasto
  Para no gastar más de lo que gano

  Escenario: Establecer un presupuesto mensual
    Cuando defino un presupuesto de 400 "USD" para la categoría "Ocio" en "Mayo 2026"
    Entonces el presupuesto de "Ocio" debe estar activo para el periodo actual

  Escenario: Alerta por exceso de presupuesto
    Cuando un presupuesto de 100 "USD" para "Restaurantes"
    Y un gasto previo de 80 "USD" en "Restaurantes"
    Cuando registro un nuevo egreso de 30 "USD" en "Restaurantes"
    Entonces el sistema debe emitir una alerta de exceso de presupuesto