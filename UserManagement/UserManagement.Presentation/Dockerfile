FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["UserManagement/UserManagement.Presentation/UserManagement.Presentation.csproj", "UserManagement/UserManagement.Presentation/"]
RUN dotnet restore "UserManagement/UserManagement.Presentation/UserManagement.Presentation.csproj"
COPY . .
WORKDIR "/src/UserManagement/UserManagement.Presentation"
RUN dotnet build "UserManagement.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "UserManagement.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserManagement.Presentation.dll"]
