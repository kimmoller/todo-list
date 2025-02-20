FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY *.csproj .
RUN dotnet restore

# copy everything else and build app
COPY ./. ./todo-list/
WORKDIR /source/todo-list
RUN dotnet publish -c release -o /app

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app ./

ENTRYPOINT ["dotnet", "todo-list.dll"]