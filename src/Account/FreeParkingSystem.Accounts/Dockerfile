FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["src/Account/FreeParkingSystem.Accounts/FreeParkingSystem.Accounts.csproj", "src/Account/FreeParkingSystem.Accounts/"]
RUN dotnet restore "src/Account/FreeParkingSystem.Accounts/FreeParkingSystem.Accounts.csproj"
COPY . .
WORKDIR "/src/src/Account/FreeParkingSystem.Accounts"
RUN dotnet build "FreeParkingSystem.Accounts.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "FreeParkingSystem.Accounts.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "FreeParkingSystem.Accounts.dll"]