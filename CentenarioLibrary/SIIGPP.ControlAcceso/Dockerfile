FROM mcr.microsoft.com/dotnet/sdk:10.0.100-preview.3-windowsservercore-ltsc2022 AS windows-build
WORKDIR /app
COPY SIIGPP.ControlAcceso.csproj .
RUN dotnet restore "SIIGPP.ControlAcceso.csproj"
COPY . .
RUN powershell -Command "New-Item -ItemType Directory -Path 'C:\\app\\Carpetas' -Force"
RUN powershell -Command "New-Item -ItemType Directory -Path 'C:\\app\\Responsivas' -Force"
RUN powershell -Command "dotnet clean 'SIIGPP.ControlAcceso.csproj' -c Release; dotnet build 'SIIGPP.ControlAcceso.csproj' -c Release; dotnet publish 'SIIGPP.ControlAcceso.csproj' -c Release -o 'C:\\app\\publish'"

FROM mcr.microsoft.com/dotnet/sdk:10.0.100-preview.3-windowsservercore-ltsc2022 AS windows-runtime
WORKDIR /app
COPY --from=windows-build C:/app/publish .
RUN powershell -Command "New-Item -ItemType Directory -Path 'C:\\app\\Carpetas' -Force"
RUN powershell -Command "New-Item -ItemType Directory -Path 'C:\\app\\Responsivas' -Force"
EXPOSE 44321
ENTRYPOINT ["dotnet", "SIIGPP.ControlAcceso.dll"]