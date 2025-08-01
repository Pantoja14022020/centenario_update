##################################
# INSTALACION EN WINDOWS SERVER  #
##################################

# INSTALAR DOCKER, SOLO SE HACE UNA VEZ

### HABILITAR LAS CARACTERISTICAS DE CONTENEDORES, EN POWERSHELL INGRESAR EL COMANDO. IGUAL EN WINDOWS SERVER PANNEL HABILITAR Hypervisor
1. Install-WindowsFeature -Name Containers

### INSTALAR EL PROVEEDOR DE DOCKER, EN POWERSHELL PONER EL COMANDO
2. Install-Module -Name DockerMsftProvider -Repository PSGallery -Force

### DESCARGAR DOCKER EN LA SIGUIENTE PAGINA. LUEGO DESCOMPRIMIR Y SE OBTENDRA DOS ARCHIVOS QUE SE MOVERAN
### A OTRA RUTA PARA QUE PUEDA SER RECONOCIDO COMO VARIABLE DE ENTORNO
3. Este es el enlace: https://download.docker.com/win/static/stable/x86_64/

### CREAR LA CARPETA DONDE SE ALMACENARA LOS ARCHIVOS, RESULTADO DE DESCOMPRIMIR EL ZIP DEL PASO 3
4. New-Item -ItemType Directory -Path "C:\Program Files\Docker" -Force

### MOVER LOS ARCHIVO A LA CARPETA DOCKER

### AGREGAR LA RUTA DONDE SE MOVIERON LOS ARCHIVOS COMO UN PATH. EN POWERSHELL INGRESAMOS EL COMANDO
5. [Environment]::SetEnvironmentVariable("Path", $env:Path + ";C:\Program Files\Docker", [EnvironmentVariableTarget]::Machine)

### REINICIAR EL SERVIDOR. EN POWERSHELL COLOCAR:
6. Restart-Computer

### HABILITAR EL SERVICIO DE DOCKER EN EL SERVIDOR, EN EL POWERSHELL COLOCAR LOS COMANDOS
7. dockerd --register-service
8. Start-Service docker
9. docker version

















##################################
#  INSTALACION EN WINDOWS 11     #
##################################

# INSTALAR DOCKER, SOLO SE HACE UNA VEZ

# IMPORTANTE MANTENER ACTUALIZADO EL SO

# VERIFICAR LA VERSION DE WSL DEL SISTEMA, EN POWERSHELL USAR EL COMANDO: 
# la version minima de wsl para docker es la version 2.0
1. wsl --version
# en caso de no existir instalar con
1. wsl --install

# ACTUALIZAR WSL, EN POWERSHELL COLOCAR LOS COMANDOS
1. wsl --update

# DESCARGAR E INSTALAR DOCKER DESKTOP
1. Enlace de descarga: https://www.docker.com/products/docker-desktop/

# ELEGIR EL PAQUETE CORRECTO, INSTALAR Y EJECUTAR

# COMANDOS �TILES
# Eliminar todos los contenedores detenidos, �til para desocupar puertos
1. docker container prune
# Eliminar todas las imagenes no usadas
2. docker image prune
# Eliminar imagenes, contenedores, volumenes y redes no usadas
3. docker system prune -a














#################################################################################################
# INSTALACION EN PRODUCTIVO EN SERVIDOR CON SISTEMA OPERATIVO WINDOWS SERVER archivo Dockerfile #
#################################################################################################

# COMANDOS PARA GENERAR IMAGEN Y LEVANTAR CONTENEDOR
## Construir imagen
1. docker build --isolation=hyperv -t policia-investigacion-img-prod-ws .
## Levantar contenedor
2. docker run --isolation=hyperv -p 5007:5007 --name policia-investigacion-container-prod-ws policia-investigacion-img-prod-ws
## Eliminar volumenes
3. docker volume prune -f













################################################################################################
# INSTALACION EN PRODUCTIVO EN SERVIDOR CON SISTEMA OPERATIVO LINUX archivo Dockerfile.linux   #
################################################################################################


# TENER INSTALADO DOCKER

# CREAR LA IMAGEN, EN LA TERMINAL INGRESAR
1. docker build -f Dockerfile.linux -t siigpp-pi-img-prod-linux .

# LEVANTAR EL CONTENEDOR
2. docker run -d -p 5007:5007 --name siigpp-pi-container-prod-linux siigpp-pi-img-prod-linux














################################################################################
# INSTALACION PARA ENTORNO DE DESARROLLO en LINUX archivo Dockerfile.dev.linux #
################################################################################

# CREAR LA IMAGEN, EN LA TERMINAL INGRESAR
1. docker build -f Dockerfile.dev.linux -t siigpp-pi-dev-linux .

# EJECUTAR CONTENEDOR EN UN ENTORNO LOCAL
2. docker run -it -v "$PWD":/app -p 5007:5007 --name siigpp-pi-dev-linux siigpp-pi-dev-linux

















######################################################################################
# INSTALACION PARA ENTORNO DE DESARROLLO en WINDOWS SERVER archivo Dockerfile.dev.ws #
######################################################################################

# CREAR LA IMAGEN, EN LA TERMINAL INGRESAR
1. docker build -f Dockerfile.dev.ws -t siigpp-pi-dev-ws .

# EJECUTAR CONTENEDOR EN UN ENTORNO LOCAL modo Hot Reload
2. docker run -it --mount type=bind,source="<Ruta donde se encuentra tu carpeta de proyecto>",target="C:\app" -p 5007:5007 --name siigpp-pi-dev-ws siigpp-pi-dev-ws

Ejemplo:

    docker run -it --mount type=bind,source="C:\Users\Administrator\Desktop\policia-inv-cent-microservice",target="C:\app" -p 5007:5007 --name siigpp-pi-dev-ws siigpp-pi-dev-ws















######################################################################################
# EJECUCION EN VISUAL STUDIO                                                         #
######################################################################################

# AL ABRIR LA CARPETA DEL PROYECTO
1. Eliminar las carpetas bin y obj dentro del proyecto en caso de que se creen
2. Dentro del proyecto existen dos carpetas SIIGPP.Datos y SIIGPP.Entidades, en caso de que se existan las carpetas obj y bin, eliminarlas antes del primer inicio

# EJECUTAR LOS COMANDOS DENTRO DE LA CARPETA DEL PROYECTO: (CLICK DERECHO: ABRIR EN TERMINAL)
1. dotnet restore "SIIGPP.PI.csproj"
2. dotnet clean "SIIGPP.PI.csproj"

# CORRER EL PROYECTO
1. Usar el comando: dotnet watch run
2. Permitir el acceso publico a redes

Nota: en caso de encontrar el siguiente error:
System.IO.DirectoryNotFoundException: 'C:\ruta\policia-inv-cent-microservice\bin\Debug\net9.0\Carpetas\'
-crear una carpeta llamada "Carpetas" dentro de bin/Debug/net9.0 y volver a correr el proyecto

