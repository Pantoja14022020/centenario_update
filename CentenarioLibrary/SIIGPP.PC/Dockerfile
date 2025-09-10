FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY CentenarioLibrary/CentenarioLibrary.csproj CentenarioLibrary/
COPY SIIGPP.PC/SIIGPP.PC.csproj SIIGPP.PC/

RUN dotnet restore SIIGPP.PC/SIIGPP.PC.csproj

COPY CentenarioLibrary/. CentenarioLibrary/
COPY SIIGPP.PC/. SIIGPP.PC/

RUN dotnet publish SIIGPP.PC/SIIGPP.PC.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 5000
ENTRYPOINT ["dotnet", "SIIGPP.PC.dll"]
