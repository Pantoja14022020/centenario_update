FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY CentenarioLibrary/CentenarioLibrary.csproj CentenarioLibrary/
COPY SIIGPP.FEDC/SIIGPP.FEDC.csproj SIIGPP.FEDC/

RUN dotnet restore SIIGPP.FEDC/SIIGPP.FEDC.csproj

COPY CentenarioLibrary/. CentenarioLibrary/
COPY SIIGPP.FEDC/. SIIGPP.FEDC/

RUN dotnet publish SIIGPP.FEDC/SIIGPP.FEDC.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 5000
ENTRYPOINT ["dotnet", "SIIGPP.FEDC.dll"]
