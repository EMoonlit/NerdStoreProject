﻿FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Customer/NSE.Customer.API/NSE.Customer.API.csproj", "src/Services/Customer/NSE.Customer.API/"]
RUN dotnet restore "src/Services/Customer/NSE.Customer.API/NSE.Customer.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Customer/NSE.Customer.API"
RUN dotnet build "NSE.Customer.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NSE.Customer.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NSE.Customer.API.dll"]
