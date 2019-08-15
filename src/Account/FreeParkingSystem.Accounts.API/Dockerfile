FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
# Install system components
RUN apt-get update -y && apt-get install -y  curl gnupg apt-transport-https && curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add - && sh -c 'echo "deb [arch=amd64] https://packages.microsoft.com/repos/microsoft-debian-stretch-prod stretch main" > /etc/apt/sources.list.d/microsoft.list' && apt-get update -y && apt-get install -y powershell

# Start PowerShell
SHELL ["pwsh", "-Command"]

WORKDIR /source

COPY . .
RUN dotnet restore "src/Account/FreeParkingSystem.Accounts.API/FreeParkingSystem.Accounts.API.csproj"

WORKDIR "/source/src/Account/FreeParkingSystem.Accounts.API"

RUN dotnet build "FreeParkingSystem.Accounts.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "FreeParkingSystem.Accounts.API.csproj" -c Release -o /app 

WORKDIR "/source"
# Default environment variables.

ENV Deployment_Environment_Name='Staging' \
Deployment_LogLevel_Default='Error' \
Deployment_Db_Server='localhost,3306' \
Deployment_Db_Name='FreeParkingSystem.Database' \
Deployment_Db_User='sa' \
Deployment_Db_Password='S1mple@password' \
Deployment_PasswordOptions_MinimumCharacters='4'
RUN & .\tools\SetupEnvironmentVariables.ps1 "/app/appsettings.{environment_name}.json" 

WORKDIR "/"

RUN Remove-Item .\source -Recurse -Force

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "FreeParkingSystem.Accounts.API.dll", "ASPNETCORE_ENVIRONMENT $env:Deployment_Environment_Name"]