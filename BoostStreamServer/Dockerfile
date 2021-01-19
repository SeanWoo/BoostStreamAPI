#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 3306

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["BoostStreamServer/BoostStreamServer.csproj", "BoostStreamServer/"]
RUN dotnet restore "BoostStreamServer/BoostStreamServer.csproj"
COPY . .
WORKDIR "/src/BoostStreamServer"
RUN dotnet build "BoostStreamServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BoostStreamServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BoostStreamServer.dll"]