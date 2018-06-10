FROM microsoft/aspnetcore:2.0-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0-nanoserver-1709 AS build
WORKDIR /src
COPY Core/FreeParkingSystem.Parking/FreeParkingSystem.Parking.csproj Core/FreeParkingSystem.Parking/
RUN dotnet restore Core/FreeParkingSystem.Parking/FreeParkingSystem.Parking.csproj
COPY . .
WORKDIR /src/Core/FreeParkingSystem.Parking
RUN dotnet build FreeParkingSystem.Parking.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish FreeParkingSystem.Parking.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "FreeParkingSystem.Parking.dll"]
