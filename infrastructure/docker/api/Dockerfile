﻿FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Fiap.Health.Med.Bff.Api/Fiap.Health.Med.Bff.Api.csproj", "src/Fiap.Health.Med.Bff.Api/"]
COPY ["src/Fiap.Health.Med.Bff.Application/Fiap.Health.Med.Bff.Application.csproj", "src/Fiap.Health.Med.Bff.Application/"]
COPY ["src/Fiap.Health.Med.Bff.CrossCutting/Fiap.Health.Med.Bff.CrossCutting.csproj", "src/Fiap.Health.Med.Bff.CrossCutting/"]
COPY ["src/Fiap.Health.Med.Bff.Infrastructure/Fiap.Health.Med.Bff.Infrastructure.Http.csproj", "src/Fiap.Health.Med.Bff.Infrastructure/"]
RUN dotnet restore "src/Fiap.Health.Med.Bff.Api/Fiap.Health.Med.Bff.Api.csproj"
COPY . .
WORKDIR "/src/src/Fiap.Health.Med.Bff.Api"
RUN dotnet build "./Fiap.Health.Med.Bff.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Fiap.Health.Med.Bff.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

USER root
COPY infrastructure/docker/wait-for-it.sh .
RUN chmod +x wait-for-it.sh
USER $APP_UID

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fiap.Health.Med.Bff.Api.dll"]
