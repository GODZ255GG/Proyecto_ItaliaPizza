# Proyecto_ItaliaPizza
Proyecto de la materia de Desarrollo de Software 

## Software necesarios para la instalación
* SQL Server Management Studio 2019
* Visual Studio 2022 Community

## Configuración de la base de datos
* Suponemos que cuenta con una instacia de SQL Server asi que ahora siga los siguientes pasos:
* 1.- Iniciar sesión como administrador.
* 2.- Crear la base de datos con el nombre "BDItaliaPizza"
* 3.- Ejecute el Script que se encuentra en el repositorio.
* 4.- Cree al Usuario que tendra permisos para usar la base de datos recien creada. Los datos del usuario son:
* Usuario: Gerente y Contraseña: oZTyf744Q1nC. Y recuerde dale permisos para que pueda usar la Base de datos.

## Configuración de Visual Studio Community
### Cliente
* Para poder ejecutar el sistema primero descargue o clone el repositorio y guardelo en una dirección que recuerde.
* Ahora abra una ventana en Visual Studio Community y de clic al botón de "Abrir un proyecto o una solución"
![Abrir](/RecursosREADME/AbrirSolucion.png)
* Y ahora busque en la carpeta de ItaliaPizzaClient la solución del sistema, se vera algo asi.
![Seleccionar](/RecursosREADME/soucion.png)
* Al darle clic se le mostrara una ventana donde vera el sistema de parte del cliente y para asegurar que todo esta bien en la parte de arriba donde dice "Compilar" primero de clic en "Limpiar solución" y despues en "Recompilar solución".
![Compilar](/RecursosREADME/compilar.png)
* Si no marca ningun error ya quedaria listo el cliente, ahora vamos con el servidor.

### Servidor
* Para poder ejecutar el servidor necesita ejecutar Visual Studio Community como administrador.
![Admin](/RecursosREADME/administrador.png)
* Despues haga los mismos pasos para abrir la solución del servidor solo que este se encontrara en la carpeta de ItaliaPizzaServer.
![Servidor](/RecursosREADME/servidor.png)
* Al darle clic se le mostrara una ventana donde vera el sistema de parte del servidor y para asegurar que todo esta bien en la parte de arriba donde dice "Compilar" primero de clic en "Limpiar solución" y despues en "Recompilar solución". 
![Servidor](/RecursosREADME/CopilarServer.png)
* Si no marca ningun error ya solo quedaria eliminar el modelo que se encuentra en el proyecto de "DataAccess" y que se llama "BDItaliaPizzaModel".
![Servidor](/RecursosREADME/EliminarModel.png)
* Por alguna razon que desconocemos al eliminar el modelo no elimina sus referencias asi que se deben de eliminar manualmente y se encuentran en la carpeta "Proyecto_ItaliaPizza\ItaliaPizza\DataAccess\obj\Debug\edmxResourcesToEmbed"
![Resouces](/RecursosREADME/Resouces.png)
* y "Proyecto_ItaliaPizza\ItaliaPizza\DataAccess\obj\Debug\TempPE"
![Temp](/RecursosREADME/Temp.png)
* Ahora en el proyecto debe de eliminar unas lineas de codigo en el App.config del proyecto de "Data Access" y de igual manera en el "App.config" del proyecto "Server" esta liena se volvera a poner automaticamente en el "DataAccess" una vez cree el nuevo modelo asi que tambien debe agregarlo en el "Server" en el mismo lugar que estaba antes.
![AppModel](/RecursosREADME/AppModel.png)
![AppServer](/RecursosREADME/AppServer.png)
* Ahora en el proyecto de "DataAccess" le damos clic derecho sobre el proyecto y le damos en "Agregar" y "Nuevo Elemento"
![Nuevo](/RecursosREADME/Nuevo.png)
* Y en la nueva ventana seleccionamos el que dice "ADO.NET Entity Data Model" y le damos el nombre de "BDItaliaPizzaModel".
 ![Model](/RecursosREADME/Model.png)
 * Ahora seleccionamos la primera opción que aparece.
![Asistente](/RecursosREADME/Asistente.png)
* Y en la siguiente selecciona en "Nueva Conexión".
![NuevaConexion](/RecursosREADME/NuevaConexion.png)
* Selecciona el nombre de su servidor, selecciona la auntenticación de SQL Server y pone los datos del usuario que creo en la base de datos, selecciona en el checkBox "Confiar en el certificado de servidor" y selecciona el nombre de la base de datos que en este caso sera "BDItaliaPizza" y da en Aceptar.
![Conexion](/RecursosREADME/Conexion.png)
* Ahora le da clic en incluir datos confidenciales y le da clic en Siguiente.
![incluir](/RecursosREADME/incluir.png)
* Da clic donde dice tablas y le da en Finalizar. Ya con eso seria todo solo quedaria en volver a poner la linea de codigo que se borro en el proyecto de "Server" en el "App.config" y solo quedaria compilar el proyecto para ver que no tenga problemas.
![Tablas](/RecursosREADME/Tablas.png)
* Si le llegara a salir un mensaje al momento de que finalizo la creación del modelo, de clic en la opcion de "Si a todo".
* Si no sale ningun error ya estaria listo el Server para usarse, de clic en "Iniciar" y le mostrara lo siguiente:
![Iniciar](/RecursosREADME/Iniciar.png)
* Y listo ya estaria listo el proyecto para ser usado, las cuentas para usar el proyecto estan en la tabla de Empleados en la base de datos y son:
* 1.- Correo: gerente@outlook.com, Contraseña: 1111
* 2.- Correo: cajero@outlook.com, Contraseña: 1111
* 3.- Correo: cocinero@outlook.com, Contraseña: 1111