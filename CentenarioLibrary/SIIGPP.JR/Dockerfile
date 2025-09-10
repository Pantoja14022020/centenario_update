FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY CentenarioLibrary/CentenarioLibrary.csproj CentenarioLibrary/
COPY SIIGPP.JR/SIIGPP.JR.csproj SIIGPP.JR/

RUN dotnet restore SIIGPP.JR/SIIGPP.JR.csproj

COPY CentenarioLibrary/. CentenarioLibrary/
COPY SIIGPP.JR/. SIIGPP.JR/

RUN dotnet publish SIIGPP.JR/SIIGPP.JR.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 5000
ENTRYPOINT ["dotnet", "SIIGPP.JR.dll"]
