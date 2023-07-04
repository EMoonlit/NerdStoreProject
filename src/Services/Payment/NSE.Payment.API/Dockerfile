FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Payment/NSE.Payment.API/NSE.Payment.API.csproj", "src/Services/Payment/NSE.Payment.API/"]
RUN dotnet restore "src/Services/Payment/NSE.Payment.API/NSE.Payment.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Payment/NSE.Payment.API"
RUN dotnet build "NSE.Payment.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NSE.Payment.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NSE.Payment.API.dll"]
