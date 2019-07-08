FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["src/Orders/FreeParkingSystem.Orders/FreeParkingSystem.Orders.csproj", "src/Orders/FreeParkingSystem.Orders/"]
RUN dotnet restore "src/Orders/FreeParkingSystem.Orders/FreeParkingSystem.Orders.csproj"
COPY . .
WORKDIR "/src/src/Orders/FreeParkingSystem.Orders"
RUN dotnet build "FreeParkingSystem.Orders.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "FreeParkingSystem.Orders.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "FreeParkingSystem.Orders.dll"]