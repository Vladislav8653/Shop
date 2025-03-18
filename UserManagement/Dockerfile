# Используем официальный образ .NET
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 3000

# Используем образ для сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем все проекты в папку /src
COPY UserManagement.Presentation/UserManagement.Presentation.csproj UserManagement.Presentation/
COPY UserManagement.Infrastructure/UserManagement.Infrastructure.csproj UserManagement.Infrastructure/
COPY UserManagement.Application/UserManagement.Application.csproj UserManagement.Application/
COPY UserManagement.Domain/UserManagement.Domain.csproj UserManagement.Domain/
#COPY UserManagement.Tests/UserManagement.Tests.csproj UserManagement.Tests/

# Восстанавливаем зависимости
RUN dotnet restore UserManagement.Presentation/UserManagement.Presentation.csproj

# Копируем все файлы проекта
COPY UserManagement.Presentation/ UserManagement.Presentation/
COPY UserManagement.Infrastructure/ UserManagement.Infrastructure/
COPY UserManagement.Application/ UserManagement.Application/
COPY UserManagement.Domain/ UserManagement.Domain/
#COPY Tests/ Tests/

# Строим проект
WORKDIR "UserManagement.Presentation"
RUN dotnet build "UserManagement.Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserManagement.Presentation.csproj" -c Release -o /app/publish

# Стартуем приложение
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserManagement.Presentation.dll"]