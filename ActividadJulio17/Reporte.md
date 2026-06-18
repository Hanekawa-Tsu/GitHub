Del monolito a sistemas distribuidos

Cuando todo (interfaz, logica y base de datos) esta en una sola compu, hay problemas de escalabilidad y sincronizacion. Por eso nacen las arquitecturas distribuidas, donde cada parte corre en servidores distintos y atiende a varios usuarios.
Capas vs Niveles

    Capas (layers): organizacion logica del software (presentacion, negocio, datos).

    Niveles (tiers): distribucion fisica en servidores (web, aplicacion, base de datos).

Arquitectura de 3 niveles

    Presentacion: interfaz con el usuario (HTML, CSS, JS, Razor, ASP.NET MVC).

    Aplicacion: reglas de negocio y validaciones (ASP.NET Core, C#, APIs REST).

    Datos: almacenamiento y consultas (SQL Server, MySQL, PostgreSQL, Oracle).


Patron MVC
Problema del codigo espagueti

Si mezclas SQL, logica y vista en un archivo, el sistema se vuelve dificil de mantener y lleno de errores.
Separacion de responsabilidades

    Modelo: maneja datos y reglas.

    Vista: muestra info al usuario, sin logica ni SQL.

    Controlador: recibe peticiones, usa el modelo y manda datos a la vista.

    Ciclo de vida y rutas


Ejemplo de URL

{controller=Home}/{action=Index}/{id?}
URL	                             Controlador	       Metodo	     id
/ControlAcademico/Login	         ControlAcademico	   Login	      -
/Estudiante/Historial/20260123	 Estudiante	           Historial	 20260123
/Asignacion/Detalle/10	         Asignacion	           Detalle	     10
/Home	                         Home	               Index	     -

Referencias
Facultad de Ingenieria, USAC (2026). Sesiones 11 y 12 sobre sistemas distribuidos, N-Tier y MVC.