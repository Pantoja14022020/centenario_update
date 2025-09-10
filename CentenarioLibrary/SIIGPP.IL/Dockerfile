FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY CentenarioLibrary/CentenarioLibrary.csproj CentenarioLibrary/
COPY SIIGPP.IL/SIIGPP.IL.csproj SIIGPP.IL/

RUN dotnet restore SIIGPP.IL/SIIGPP.IL.csproj

COPY CentenarioLibrary/. CentenarioLibrary/
COPY SIIGPP.IL/. SIIGPP.IL/

RUN dotnet publish SIIGPP.IL/SIIGPP.IL.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 5000
ENTRYPOINT ["dotnet", "SIIGPP.IL.dll"]
