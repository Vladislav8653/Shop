# Используем официальный образ .NET
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 2000

# Используем образ для сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем все проекты в папку /src
COPY ProductManagement.Presentation/ProductManagement.Presentation.csproj ProductManagement.Presentation/
COPY ProductManagement.Infrastructure/ProductManagement.Infrastructure.csproj ProductManagement.Infrastructure/
COPY ProductManagement.Application/ProductManagement.Application.csproj ProductManagement.Application/
COPY ProductManagement.Domain/ProductManagement.Domain.csproj ProductManagement.Domain/
#COPY ProductManagement.Tests/ProductManagement.Tests.csproj ProductManagement.Tests/

# Восстанавливаем зависимости
RUN dotnet restore ProductManagement.Presentation/ProductManagement.Presentation.csproj

# Копируем все файлы проекта
COPY ProductManagement.Presentation/ ProductManagement.Presentation/
COPY ProductManagement.Infrastructure/ ProductManagement.Infrastructure/
COPY ProductManagement.Application/ ProductManagement.Application/
COPY ProductManagement.Domain/ ProductManagement.Domain/
#COPY Tests/ Tests/

# Строим проект
WORKDIR "ProductManagement.Presentation"
RUN dotnet build "ProductManagement.Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProductManagement.Presentation.csproj" -c Release -o /app/publish

# Стартуем приложение
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductManagement.Presentation.dll"]