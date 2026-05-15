#language: es
Requisito: Gestión Categorias
  Como usuario quiero administrar las etiquetas de mis movimientos
  Para clasificar mis gastos mediante nombres unicos y colores permitidos

  Antecedentes:
    Dado la paleta de colores permitida es: "Verde, Gris, Negro, Azul, Rojo"

  @Unitary
  Escenario: Crear una categoría con nombre valido
    Cuando creo una categoría con los siguientes datos:
      | Name         | Color | Icon      |
      | Comida       | Verde | fast-food |
    Entonces la categoría "Comida" debe estar disponible para su uso

  @Unitary
  Escenario: Impedir nombres demasiado cortos
    Cuando creo una categoría con los siguientes datos:
      | Name         | Color | Icon      |
      | Yo       | Verde | fast-food |
    Entonces el sistema debe rechazar la creación con el mensaje "El nombre debe tener al menos 3 caracteres"

  @Unitary
  Escenario: Impedir colores no permitidos por la paleta
    Cuando creo una categoría con los siguientes datos:
      | Name         | Color    | Icon      |
      | Comida       | Amarillo | fast-food |
    Entonces el sistema debe rechazar la creación con el mensaje "El color no pertenece a la paleta permitida"

  @Integration
  Escenario: Impedir nombres duplicados en el sistema
    Dado que ya existe una categoría con los siguientes datos:
      | Name         | Color  | Icon      |
      | Trasporte    | Verde  | fast-food |
    Cuando creo una categoría con los siguientes datos:
      | Name         | Color  | Icon      |
      | Trasporte    | Gris   | fast-food |
    Entonces el sistema debe rechazar la creación con el mensaje "Ya existe una categoría con ese nombre"

  @Unitary
  Escenario: Actualizar datos de una categoría
    Dado que ya existe una categoría con los siguientes datos:
      | Name     | Color  | Icon      |
      | Salud    | Verde  | fast-food |
    Cuando actualizo la categoría con los siguientes datos:
      | Name     | Color  | Icon      |
      | Salud    | Azul   | fast-food |
    Entonces la categoría "Salud" debe tener el color "Azul"