FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/ShoppingCart/NSE.ShoppingCart.API/NSE.ShoppingCart.API.csproj", "src/Services/ShoppingCart/NSE.ShoppingCart.API/"]
RUN dotnet restore "src/Services/ShoppingCart/NSE.ShoppingCart.API/NSE.ShoppingCart.API.csproj"
COPY . .
WORKDIR "/src/src/Services/ShoppingCart/NSE.ShoppingCart.API"
RUN dotnet build "NSE.ShoppingCart.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NSE.ShoppingCart.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NSE.ShoppingCart.API.dll"]
