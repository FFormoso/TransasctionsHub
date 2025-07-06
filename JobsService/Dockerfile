FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["JobsService/011Global.JobsService.csproj", "JobsService/"]
COPY ["011Global.Shared/011Global.Shared.csproj", "011Global.Shared/"]
RUN dotnet restore "JobsService/011Global.JobsService.csproj"
COPY . .
WORKDIR "/src/JobsService"
RUN dotnet build "./011Global.JobsService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./011Global.JobsService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "011Global.JobsService.dll"]
