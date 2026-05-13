Feature: Gestion Categorias
  Como usuario quiero administrar las etiquetas de mis movimientos
  Para clasificar mis gastos mediante nombres unicos y colores permitidos

  Background:
    Given la paleta de colores permitida es: "Verde, Gris, Negro, Azul, Rojo"

  @Unitary
  Scenario: Crear una categoria con nombre valido
    When creo una categoria con los siguientes datos:
      | Name         | Color | Icon      |
      | Comida       | Verde | fast-food |
    Then la categoria "Comida" debe estar disponible para su uso

  @Unitary
  Scenario: Impedir nombres demasiado cortos
    When intento crear una categoria con el nombre "Yo"
    Then el sistema debe rechazar la creación con el mensaje "El nombre debe tener al menos 3 caracteres"

  @Unitary
  Scenario: Impedir colores no permitidos por la paleta
    When intento crear una categoría con el color "Amarillo"
    Then el sistema debe rechazar la creación con el mensaje "El color no pertenece a la paleta permitida"

  @Integration
  Scenario: Impedir nombres duplicados en el sistema
    Given que ya existe una categoría llamada "Transporte"
    When intento crear una categoría con el nombre "Transporte"
    Then el sistema debe rechazar la creación con el mensaje "Ya existe una categoría con ese nombre"

  @Unitary
  Scenario: Actualizar metadatos de una categoría
    Given una categoría existente llamada "Salud"
    When cambio su color a "Azul"
    Then la categoría "Salud" debe tener el color "Azul"