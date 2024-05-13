FROM mcr.microsoft.com/dotnet/aspnet:7.0 as base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 as build
WORKDIR /src
COPY ["ToDoList.App.csproj", "ToDoList.App"]
RUN dotnet restore "ToDoList.App/ToDoList.App.csproj"