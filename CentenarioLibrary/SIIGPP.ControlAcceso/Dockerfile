FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY CentenarioLibrary/CentenarioLibrary.csproj CentenarioLibrary/
COPY SIIGPP.ControlAcceso/SIIGPP.ControlAcceso.csproj SIIGPP.ControlAcceso/

RUN dotnet restore SIIGPP.ControlAcceso/SIIGPP.ControlAcceso.csproj

COPY CentenarioLibrary/. CentenarioLibrary/
COPY SIIGPP.ControlAcceso/. SIIGPP.ControlAcceso/

RUN dotnet publish SIIGPP.ControlAcceso/SIIGPP.ControlAcceso.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
COPY SIIGPP.ControlAcceso/Responsivas /app/Responsivas
EXPOSE 5000
ENTRYPOINT ["dotnet", "SIIGPP.ControlAcceso.dll"]
