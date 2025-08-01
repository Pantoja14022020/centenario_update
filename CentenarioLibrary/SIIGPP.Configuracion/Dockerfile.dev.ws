# Dockerfile.dev para entorno Windows Server con dotnet watch
FROM mcr.microsoft.com/dotnet/sdk:10.0.100-preview.3-windowsservercore-ltsc2022 AS dev

WORKDIR /app

# Copiar primero el archivo de proyecto y restaurar dependencias
COPY SIIGPP.Configuracion.csproj .
RUN dotnet restore

# Copiar el resto del código fuente
COPY . .

# Crear la carpeta necesaria
RUN powershell -Command "New-Item -ItemType Directory -Path 'C:\\app\\Carpetas' -Force"

# Exponer el puerto
EXPOSE 44360

# Ejecutar con watch para hot reload
CMD ["dotnet", "watch", "run", "--urls=http://0.0.0.0:44360"]