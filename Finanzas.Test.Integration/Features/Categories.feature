#language: es
@Categories
Requisito: Gestión Categorias
  Como usuario autenticado
  Quiero administrar las etiquetas de mis movimientos
  Para clasificar mis gastos mediante nombres unicos y colores permitidos

  Antecedentes:
    Dado que existe un usuario con los siguientes datos:
    | UserName                | Email                   | Password |
    | david.gonzalez@sinco.co | david.gonzalez@sinco.co |          |
    Dado que el usuario "david.gonzalez@sinco.co" ha iniciado sesión
    Y la paleta de colores permitida es: "Verde, Gris, Negro, Azul, Rojo"

  Escenario: La configuración inicial si se guarda
    Entonces la paleta de colores del sistema debe contener exactamente:
      | Color  |
      | Verde  |
      | Gris   |
      | Negro  |
      | Azul   |
      | Rojo   |

  Escenario: Crear una categoría con nombre valido
    Cuando creo una categoría con los siguientes datos:
      | Name         | Color | Icon      |
      | Comida       | Verde | fast-food |
    Entonces la categoría "Comida" debe estar disponible para su uso

  Escenario: Impedir nombres demasiado cortos
    Cuando creo una categoría con los siguientes datos:
      | Name         | Color | Icon      |
      | Yo       | Verde | fast-food |
    Entonces el sistema debe rechazar la creación con el mensaje "El nombre debe tener al menos 3 caracteres"

  Escenario: Impedir colores no permitidos por la paleta
    Cuando creo una categoría con los siguientes datos:
      | Name         | Color    | Icon      |
      | Comida       | Amarillo | fast-food |
    Entonces el sistema debe rechazar la creación con el mensaje "El color no pertenece a la paleta permitida"

  Escenario: Impedir nombres duplicados en el sistema
    Dado que ya existe una categoría con los siguientes datos:
      | Name         | Color  | Icon      |
      | Trasporte    | Verde  | fast-food |
    Cuando creo una categoría con los siguientes datos:
      | Name         | Color  | Icon      |
      | Trasporte    | Gris   | fast-food |
    Entonces el sistema debe rechazar la creación con el mensaje "Ya existe una categoría con ese nombre"

  Escenario: Actualizar datos de una categoría
    Dado que ya existe una categoría con los siguientes datos:
      | Name     | Color  | Icon      |
      | Salud    | Verde  | fast-food |
    Cuando actualizo la categoría con los siguientes datos:
      | Name     | Color  | Icon      |
      | Salud    | Azul   | fast-food |
    Entonces la categoría "Salud" debe tener el color "Azul"