FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BackendTask/BackendTask.csproj", "BackendTask/"]
RUN dotnet restore "BackendTask/BackendTask.csproj"
COPY . .
WORKDIR "/src/BackendTask"
RUN dotnet build "BackendTask.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BackendTask.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BackendTask.dll"]
