#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Bootstrapper/Factum.Bootstrapper/Factum.Bootstrapper.csproj", "src/Bootstrapper/Factum.Bootstrapper/"]
COPY ["src/Modules/Documents/Factum.Modules.Documents.Api/Factum.Modules.Documents.Api.csproj", "src/Modules/Documents/Factum.Modules.Documents.Api/"]
COPY ["src/Shared/Factum.Shared.Abstractions/Factum.Shared.Abstractions.csproj", "src/Shared/Factum.Shared.Abstractions/"]
COPY ["src/Modules/Documents/Factum.Modules.Documents.Infrastructure/Factum.Modules.Documents.Infrastructure.csproj", "src/Modules/Documents/Factum.Modules.Documents.Infrastructure/"]
COPY ["src/Shared/Factum.Shared.Infrastructure/Factum.Shared.Infrastructure.csproj", "src/Shared/Factum.Shared.Infrastructure/"]
COPY ["src/Modules/Documents/Factum.Modules.Documents.Application/Factum.Modules.Documents.Application.csproj", "src/Modules/Documents/Factum.Modules.Documents.Application/"]
COPY ["src/Modules/Documents/Factum.Modules.Documents.Core/Factum.Modules.Documents.Core.csproj", "src/Modules/Documents/Factum.Modules.Documents.Core/"]
COPY ["src/Modules/Ledger/Factum.Modules.Ledger.Api/Factum.Modules.Ledger.Api.csproj", "src/Modules/Ledger/Factum.Modules.Ledger.Api/"]
COPY ["src/Modules/Ledger/Factum.Modules.Ledger.Application/Factum.Modules.Ledger.Application.csproj", "src/Modules/Ledger/Factum.Modules.Ledger.Application/"]
COPY ["src/Modules/Ledger/Factum.Modules.Ledger.Core/Factum.Modules.Ledger.Core.csproj", "src/Modules/Ledger/Factum.Modules.Ledger.Core/"]
COPY ["src/Modules/Ledger/Factum.Modules.Ledger.Infrastructure/Factum.Modules.Ledger.Infrastructure.csproj", "src/Modules/Ledger/Factum.Modules.Ledger.Infrastructure/"]
COPY ["src/Modules/Saga/Factum.Modules.Saga.Api/Factum.Modules.Saga.Api.csproj", "src/Modules/Saga/Factum.Modules.Saga.Api/"]
COPY ["src/Modules/Saga/Factum.Modules.Saga.Core/Factum.Modules.Saga.Core.csproj", "src/Modules/Saga/Factum.Modules.Saga.Core/"]
COPY ["src/Modules/Saga/Factum.Modules.Saga.Infrastructure/Factum.Modules.Saga.Infrastructure.csproj", "src/Modules/Saga/Factum.Modules.Saga.Infrastructure/"]
COPY ["src/Modules/Users/Factum.Modules.Users.Api/Factum.Modules.Users.Api.csproj", "src/Modules/Users/Factum.Modules.Users.Api/"]
COPY ["src/Modules/Users/Factum.Modules.Users.Core/Factum.Modules.Users.Core.csproj", "src/Modules/Users/Factum.Modules.Users.Core/"]
COPY ["src/Modules/Validation/Factum.Modules.Validation.Api/Factum.Modules.Validation.Api.csproj", "src/Modules/Validation/Factum.Modules.Validation.Api/"]
COPY ["src/Modules/Validation/Factum.Modules.Validation.Core/Factum.Modules.Validation.Core.csproj", "src/Modules/Validation/Factum.Modules.Validation.Core/"]
RUN dotnet restore "src/Bootstrapper/Factum.Bootstrapper/Factum.Bootstrapper.csproj"
COPY . .
WORKDIR "/src/src/Bootstrapper/Factum.Bootstrapper"
RUN dotnet build "Factum.Bootstrapper.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Factum.Bootstrapper.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
VOLUME ["/app/blob"]
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Factum.Bootstrapper.dll"]