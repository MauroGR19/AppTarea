# AppTarea
Api creada con arquitectura hexagonal (Prueba Técnica) 

# Instalacion 

una clonado el repositorio vas a abrir la carpeta del repo, luego la carpeta de AppTarea y lo abre en el visual. 

una ves clonado el repositorio, nos vamos a ir al proyecto llamado AppTarea.Infraestructura.Datos, luego a la carpeta Contexto 
y abrimos la clase AplicattionContexto, una ves ahi configuras la cadena de coneccion de tu base de datos en la linea 15. 

una ves realiazdo el paso anterior le vamos a dar click derecho AppTarea.Infraestructura.Datos y vamos a depurar solo ese proyecto.

luego de eso vas a seleccionar el proyecto de AppTarea.Infraestructura.APII y lo vas a establecer como proyecto de inicio.

luego debes crear un usuario, este metodo esta en el controlador de usuarioContro en el metodo agregar.

para efectos de la prueba, este metodo no tiene JWT, una ves creado un usuario se debe de loguear para generar un el token con el que vas a poder acceder a todas las acciones de tareaController. 