# Base de Datos de Gestión de Gastos

Para mantener un histórico de la estructura de la base de datos (Azure SQL Database) y poder restaurarla fácilmente, exporta el esquema actual y guárdalo en esta carpeta.

## Cómo exportar la estructura de la base de datos

Dado que la base de datos está alojada en **Azure SQL Database** y requiere autenticación Active Directory, la manera más limpia y robusta de extraer su estructura sin datos es utilizar herramientas visuales como **SQL Server Management Studio (SSMS)** o **Azure Data Studio**.

### Opción 1: Usando SQL Server Management Studio (SSMS) - *Recomendado*
1. Abre SSMS y conéctate a tu servidor: `gestion-gastos.database.windows.net`.
2. En el Explorador de Objetos, despliega **Bases de datos** y haz clic derecho en `gestionGastos-db`.
3. Selecciona **Tareas (Tasks)** > **Generar scripts... (Generate Scripts...)**.
4. En el asistente:
   - **Elija objetos:** Selecciona "Crear un script de toda la base de datos y todos sus objetos".
   - **Establecer opciones de scripting:** 
     - Haz clic en el botón **Opciones avanzadas (Advanced)**.
     - Busca la opción **Tipos de datos para incluir en el script (Types of data to script)** y asegúrate de que esté solo en **Esquema (Schema only)**.
     - Selecciona **Guardar en archivo (Save as script file)** y guarda el archivo directamente en esta misma carpeta: `c:\proyectos\gestion-gastos-api\GestionGastos-API\Database\schema.sql`.
5. Finaliza el asistente. ¡Y listo!

### Opción 2: Usando Azure Data Studio
1. Abre Azure Data Studio y conéctate a `gestion-gastos.database.windows.net`.
2. Haz clic derecho sobre tu base de datos `gestionGastos-db` y selecciona **Extraer (Extract)** o usa la extensión *SQL Server dacpac*.
3. Si usas la extensión oficial de SQL Server Profiler/Scripting, simplemente puedes darle a "Script as Create" sobre las tablas principales y pegarlo en un archivo `schema.sql`.

Una vez que tengas tu archivo `schema.sql` en este directorio, usa Git para subirlo a la rama `develop`. De este modo, en caso de catástrofe, podrás ejecutar `schema.sql` en un servidor nuevo y recuperar instantáneamente toda la estructura (tablas, columnas, claves primarias, etc.) lista para ser usada por la API.
