FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
# Install system components
RUN apt-get update -y && apt-get install -y  curl gnupg apt-transport-https && curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add - && sh -c 'echo "deb [arch=amd64] https://packages.microsoft.com/repos/microsoft-debian-stretch-prod stretch main" > /etc/apt/sources.list.d/microsoft.list' && apt-get update -y && apt-get install -y powershell

# Start PowerShell
SHELL ["pwsh", "-Command"]


FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS startup
# Install system components
RUN apt-get update -y && apt-get install -y  curl gnupg apt-transport-https && curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add - && sh -c 'echo "deb [arch=amd64] https://packages.microsoft.com/repos/microsoft-debian-stretch-prod stretch main" > /etc/apt/sources.list.d/microsoft.list' && apt-get update -y && apt-get install -y powershell

# Start PowerShell
SHELL ["pwsh", "-Command"]

FROM startup AS build

WORKDIR /source

COPY . .

RUN dotnet restore "src/Account/FreeParkingSystem.Accounts.API/FreeParkingSystem.Accounts.API.csproj"

WORKDIR "/source/src/Account/FreeParkingSystem.Accounts.API"

RUN dotnet build "FreeParkingSystem.Accounts.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "FreeParkingSystem.Accounts.API.csproj" -c Release -o /app

WORKDIR "/"
ADD tools app/tools
ADD src/Account/FreeParkingSystem.Accounts.API/Properties/Run.ps1 /app/Run.ps1
RUN Remove-Item .\source -Recurse -Force
FROM base AS final
WORKDIR /app
COPY --from=publish /app .

ENTRYPOINT ["pwsh","-File", "Run.ps1"]