# Устанавливаем базовый образ, содержащий ASP.NET для приложения
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base

# Устанавливаем рабочую директорию для контейнера
WORKDIR /app

# Открываем порт 80 для веб-трафика и порт 443 для HTTPS
EXPOSE 80

# Устанавливаем образ SDK для сборки приложения
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Устанавливаем аргумент для конфигурации сборки, по умолчанию Release
ARG BUILD_CONFIGURATION=Release

# Устанавливаем рабочую директорию для сборки проекта
WORKDIR /src

# Копируем проект приложения в контейнер
COPY ["ToDoList.App/ToDoList.App.csproj", "ToDoList.App/"]

# Восстанавливаем зависимости проекта
RUN dotnet restore "ToDoList.App/ToDoList.App.csproj"

# Копируем все файлы проекта в контейнер
COPY . .

# Устанавливаем рабочую директорию внутри проекта
WORKDIR "/src/ToDoList.App"

# Собираем проект в рамках указанной конфигурации и указываем путь для выходных файлов
RUN dotnet build "ToDoList.App.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Создаем новый этап с именем publish, наследующий от предыдущего этапа сборки
FROM build AS publish

ARG BUILD_CONFIGURATION=Release

# Публикуем приложение в указанной конфигурации и указываем путь для выходных файлов
RUN dotnet publish "ToDoList.App.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Создаем последний этап с именем final, наследующий от базового образа ASP.NET
FROM base AS final

# Устанавливаем рабочую директорию для приложения
WORKDIR /app

# Копируем скомпилированное приложение из этапа publish в рабочую директорию final
COPY --from=publish /app/publish .

# Устанавливаем точку входа для контейнера
ENTRYPOINT ["dotnet", "ToDoList.App.dll"]
