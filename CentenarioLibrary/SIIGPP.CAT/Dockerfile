FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY SIIGPP.CAT/SIIGPP.CAT.csproj SIIGPP.CAT/
COPY CentenarioLibrary/CentenarioLibrary.csproj CentenarioLibrary/

RUN dotnet restore SIIGPP.CAT/SIIGPP.CAT.csproj

COPY SIIGPP.CAT/. SIIGPP.CAT/
COPY CentenarioLibrary/. CentenarioLibrary/

RUN dotnet publish SIIGPP.CAT/SIIGPP.CAT.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 5000
ENTRYPOINT ["dotnet", "SIIGPP.CAT.dll"]
