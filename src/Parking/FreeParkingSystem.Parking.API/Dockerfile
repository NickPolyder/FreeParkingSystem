FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV Deployment_Environment_Name=$Env:ASPNETCORE_ENVIRONMENT

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build

WORKDIR /source

COPY . .
#RUN dotnet restore "src/Account/FreeParkingSystem.Accounts.API/FreeParkingSystem.Accounts.API.csproj"

#WORKDIR "/src/src/Account/FreeParkingSystem.Accounts.API"

#RUN dotnet build "FreeParkingSystem.Accounts.API.csproj" -c Release -o /app
#
#FROM build AS publish
#RUN dotnet publish "FreeParkingSystem.Accounts.API.csproj" -c Release -o /app \
#& .\tools\SetupEnvironmentVariables.ps1 /app/appsettings.{environment_name}.json $env
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app .
#ENTRYPOINT ["dotnet", "FreeParkingSystem.Accounts.API.dll"]