# CENTENARIO LIBRARY #

1. Se creó esta librería con el propósito de reubicar las carpetas SIIGPP.Datos y SIIGPP.Entidades, con el fin de evitar la duplicación de datos entre proyectos.

2. Se agregaron los proyectos existentes (módulos) a la solución, permitiendo que cada uno pueda acceder a las carpetas Datos y Entidades centralizadas en la nueva librería.

3. Se eliminaron las carpetas duplicadas en cada proyecto.

4. Es importante agregar la siguiente referencia en cada módulo que requiera acceso a estas carpetas:

	<ItemGroup>
		<ProjectReference Include="..\CentenarioLibrary\CentenarioLibrary.csproj" />
	</ItemGroup>

5. Se configuró la solución para que todos los proyectos (módulos) puedan iniciarse de forma simultánea. Para ello:

	* Dar clic derecho sobre la solución.
	* Seleccionar Configurar proyectos de inicio.
	* Elegir la opción Varios proyectos de inicio.
	* En el apartado "Acción", seleccionar Inicio para cada uno de los proyectos.
   Esta configuración genera automáticamente el archivo CentenarioLibrary.slnLaunch.user.

6. Al ejecutar la solución, todos los módulos se inician simultáneamente, evitando la necesidad de iniciarlos uno por uno.