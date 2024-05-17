# Используем официальный образ ASP.NET Core 7.0 в качестве базового для нашего приложения
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base

# Устанавливаем рабочую директорию внутри контейнера для приложения
WORKDIR /app

# Открываем порт 80 для HTTP-трафика
EXPOSE 80

# Используем официальный образ .NET SDK 7.0 для сборки приложения
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Задаем аргумент для конфигурации сборки, по умолчанию это Release
ARG BUILD_CONFIGURATION=Release

# Устанавливаем рабочую директорию для исходного кода приложения
WORKDIR /src

# Копируем файл проекта в контейнер для восстановления зависимостей
COPY ["BaseToDoList/BaseToDoList.csproj", "BaseToDoList/"]

# Восстанавливаем зависимости, указанные в файле проекта
RUN dotnet restore "BaseToDoList/BaseToDoList.csproj"

# Копируем все оставшиеся файлы исходного кода в контейнер
COPY . .

# Устанавливаем рабочую директорию внутри контейнера для проекта
WORKDIR "/src/BaseToDoList"

# Собираем проект в конфигурации Release и выводим результат в указанную директорию
RUN dotnet build "BaseToDoList.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Создаем новый этап публикации, который наследует предыдущий этап сборки
FROM build AS publish
ARG BUILD_CONFIGURATION=Release

# Публикуем приложение в конфигурации Release, указывая выходную директорию и отключая использование AppHost
RUN dotnet publish "BaseToDoList.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Создаем финальный этап на основе базового образа ASP.NET Core
FROM base AS final

# Устанавливаем рабочую директорию внутри контейнера для опубликованного приложения
WORKDIR /app

# Копируем опубликованные файлы из предыдущего этапа в рабочую директорию
COPY --from=publish /app/publish .

# Устанавливаем точку входа для запуска приложения в контейнере
ENTRYPOINT ["dotnet", "BaseToDoList.dll"]
