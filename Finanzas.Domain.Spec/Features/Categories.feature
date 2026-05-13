Feature: Clasificación y Organización
  Como usuario quiero organizar mis tipos de gasto
  Para tener reportes limpios

  Scenario: Crear jerarquía de categorías
    Given una categoría padre llamada "Transporte"
    When creo una sub-categoría llamada "Gasolina" bajo "Transporte"
    Then la categoría "Gasolina" debe pertenecer a "Transporte"

  Scenario: Personalizar categoría para la interfaz
    When asigno el icono "car-front" y el color "blue" a la categoría "Transporte"
    Then la categoría "Transporte" debe guardar sus metadatos visuales