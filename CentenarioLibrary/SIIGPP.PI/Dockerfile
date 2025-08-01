FROM mcr.microsoft.com/dotnet/sdk:10.0.100-preview.3-windowsservercore-ltsc2022 AS windows-build
WORKDIR /app
COPY SIIGPP.PI.csproj .
RUN dotnet restore "SIIGPP.PI.csproj"
COPY . .
RUN powershell -Command "New-Item -ItemType Directory -Path 'C:\\app\\Carpetas' -Force"
RUN powershell -Command "dotnet clean 'SIIGPP.PI.csproj' -c Release; dotnet build 'SIIGPP.PI.csproj' -c Release; dotnet publish 'SIIGPP.PI.csproj' -c Release -o 'C:\\app\\publish'"

FROM mcr.microsoft.com/dotnet/sdk:10.0.100-preview.3-windowsservercore-ltsc2022 AS windows-runtime
WORKDIR /app
COPY --from=windows-build C:/app/publish .
RUN powershell -Command "New-Item -ItemType Directory -Path 'C:\\app\\Carpetas' -Force"
EXPOSE 5007
ENTRYPOINT ["dotnet", "SIIGPP.PI.dll"]