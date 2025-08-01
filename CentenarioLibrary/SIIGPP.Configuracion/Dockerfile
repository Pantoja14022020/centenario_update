FROM mcr.microsoft.com/dotnet/sdk:10.0.100-preview.3-windowsservercore-ltsc2022 AS windows-build
WORKDIR /app
COPY SIIGPP.Configuracion.csproj .
RUN dotnet restore "SIIGPP.Configuracion.csproj"
COPY . .
RUN powershell -Command "New-Item -ItemType Directory -Path 'C:\\app\\Carpetas' -Force"
RUN powershell -Command "dotnet clean 'SIIGPP.Configuracion.csproj' -c Release; dotnet build 'SIIGPP.Configuracion.csproj' -c Release; dotnet publish 'SIIGPP.Configuracion.csproj' -c Release -o 'C:\\app\\publish'"

FROM mcr.microsoft.com/dotnet/sdk:10.0.100-preview.3-windowsservercore-ltsc2022 AS windows-runtime
WORKDIR /app
COPY --from=windows-build C:/app/publish .
RUN powershell -Command "New-Item -ItemType Directory -Path 'C:\\app\\Carpetas' -Force"
EXPOSE 44360
ENTRYPOINT ["dotnet", "SIIGPP.Configuracion.dll"]