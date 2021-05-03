# ASP_DDD_SALARIOS
UNA APLICACION DE ASP.NET CORE IMPLEMENTANDO EL PATRON DDD

INSTRUCCIONES PARA CORRER EL EJEMPLO

1) Clonar o descargar el Repositorio en alguna carpeta local
2) Abrir la solución en visual studio 2019
3) Crear una base de datos en sql server (express)
4) en proyecto web asp.net core Salarios.Site, modificar el archivo appsettings.json como sigue:
   "ConnectionStrings": {
    "DefaultConnection": "Server[server_name];Database=[database_name];Trusted_Connection=True;MultipleActiveResultSets=true"
  },
5) Abrir en Package Manager Console (Tools->Nuget Package Manager->Package Manager Console)
6) En la Consola de comando ejecutar "add-migration Inicial" (Esto crear el script para crear la base de datos)
7) En la Consola de comando ejecutar "update-database" (Esto crea la base de datos en el servidor de sql server)
 
  
