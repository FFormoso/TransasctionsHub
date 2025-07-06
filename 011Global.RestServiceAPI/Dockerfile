FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 7145
EXPOSE 5142

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["011Global.RestServiceAPI/011Global.RestServiceAPI.csproj", "011Global.RestServiceAPI/"]
COPY ["011Global.Shared/011Global.Shared.csproj", "011Global.Shared/"]
RUN dotnet restore "011Global.RestServiceAPI/011Global.RestServiceAPI.csproj"
COPY . .
WORKDIR "/src/011Global.RestServiceAPI"
RUN dotnet build "./011Global.RestServiceAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./011Global.RestServiceAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "011Global.RestServiceAPI.dll"]
