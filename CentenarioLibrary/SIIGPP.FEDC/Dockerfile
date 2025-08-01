FROM mcr.microsoft.com/dotnet/sdk:10.0.100-preview.3-windowsservercore-ltsc2022 AS windows-build
WORKDIR /app
COPY SIIGPP.FEDC.csproj .
RUN dotnet restore "SIIGPP.FEDC.csproj"
COPY . .
RUN powershell -Command "New-Item -ItemType Directory -Path 'C:\\app\\Carpetas' -Force"
RUN powershell -Command "dotnet clean 'SIIGPP.FEDC.csproj' -c Release; dotnet build 'SIIGPP.FEDC.csproj' -c Release; dotnet publish 'SIIGPP.FEDC.csproj' -c Release -o 'C:\\app\\publish'"

FROM mcr.microsoft.com/dotnet/sdk:10.0.100-preview.3-windowsservercore-ltsc2022 AS windows-runtime
WORKDIR /app
COPY --from=windows-build C:/app/publish .
RUN powershell -Command "New-Item -ItemType Directory -Path 'C:\\app\\Carpetas' -Force"
EXPOSE 44348
ENTRYPOINT ["dotnet", "SIIGPP.FEDC.dll"]