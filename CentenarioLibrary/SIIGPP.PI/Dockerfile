FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY CentenarioLibrary/CentenarioLibrary.csproj CentenarioLibrary/
COPY SIIGPP.PI/SIIGPP.PI.csproj SIIGPP.PI/

RUN dotnet restore SIIGPP.PI/SIIGPP.PI.csproj

COPY CentenarioLibrary/. CentenarioLibrary/
COPY SIIGPP.PI/. SIIGPP.PI/

RUN dotnet publish SIIGPP.PI/SIIGPP.PI.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
COPY SIIGPP.PI/Carpetas /app/Carpetas
EXPOSE 5000
ENTRYPOINT ["dotnet", "SIIGPP.PI.dll"]
