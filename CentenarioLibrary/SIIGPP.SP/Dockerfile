FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY CentenarioLibrary/CentenarioLibrary.csproj CentenarioLibrary/
COPY SIIGPP.SP/SIIGPP.SP.csproj SIIGPP.SP/

RUN dotnet restore SIIGPP.SP/SIIGPP.SP.csproj

COPY CentenarioLibrary/. CentenarioLibrary/
COPY SIIGPP.SP/. SIIGPP.SP/

RUN dotnet publish SIIGPP.SP/SIIGPP.SP.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
COPY SIIGPP.SP/Carpetas /app/Carpetas
EXPOSE 5000
ENTRYPOINT ["dotnet", "SIIGPP.SP.dll"]
