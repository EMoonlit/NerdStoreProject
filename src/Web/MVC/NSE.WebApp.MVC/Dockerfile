FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Web/MVC/NSE.WebApp.MVC/NSE.WebApp.MVC.csproj", "src/Web/MVC/NSE.WebApp.MVC/"]
RUN dotnet restore "src/Web/MVC/NSE.WebApp.MVC/NSE.WebApp.MVC.csproj"
COPY . .
WORKDIR "/src/src/Web/MVC/NSE.WebApp.MVC"
RUN dotnet build "NSE.WebApp.MVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NSE.WebApp.MVC.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NSE.WebApp.MVC.dll"]
