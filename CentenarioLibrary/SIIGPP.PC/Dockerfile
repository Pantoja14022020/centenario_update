FROM mcr.microsoft.com/dotnet/sdk:10.0.100-preview.3-windowsservercore-ltsc2022 AS windows-build
WORKDIR /app
COPY SIIGPP.PC.csproj .
RUN dotnet restore "SIIGPP.PC.csproj"
COPY . .
RUN powershell -Command "New-Item -ItemType Directory -Path 'C:\\app\\Carpetas' -Force"
RUN powershell -Command "dotnet clean 'SIIGPP.PC.csproj' -c Release; dotnet build 'SIIGPP.PC.csproj' -c Release; dotnet publish 'SIIGPP.PC.csproj' -c Release -o 'C:\\app\\publish'"

FROM mcr.microsoft.com/dotnet/sdk:10.0.100-preview.3-windowsservercore-ltsc2022 AS windows-runtime
WORKDIR /app
COPY --from=windows-build C:/app/publish .
RUN powershell -Command "New-Item -ItemType Directory -Path 'C:\\app\\Carpetas' -Force"
EXPOSE 5006
ENTRYPOINT ["dotnet", "SIIGPP.PC.dll"]