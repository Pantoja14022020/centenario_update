FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY CentenarioLibrary/CentenarioLibrary.csproj CentenarioLibrary/
COPY SIIGPP.Configuracion/SIIGPP.Configuracion.csproj SIIGPP.Configuracion/

RUN dotnet restore SIIGPP.Configuracion/SIIGPP.Configuracion.csproj

COPY CentenarioLibrary/. CentenarioLibrary/
COPY SIIGPP.Configuracion/. SIIGPP.Configuracion/

RUN dotnet publish SIIGPP.Configuracion/SIIGPP.Configuracion.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 5000
ENTRYPOINT ["dotnet", "SIIGPP.Configuracion.dll"]
