FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Catalog/NSE.Catalog.API/NSE.Catalog.API.csproj", "src/Services/Catalog/NSE.Catalog.API/"]
RUN dotnet restore "src/Services/Catalog/NSE.Catalog.API/NSE.Catalog.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Catalog/NSE.Catalog.API"
RUN dotnet build "NSE.Catalog.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NSE.Catalog.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NSE.Catalog.API.dll"]
