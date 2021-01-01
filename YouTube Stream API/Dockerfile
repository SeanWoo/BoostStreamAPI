#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 3306

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["YouTube Stream API/YouTube Stream API.csproj", "YouTube Stream API/"]
RUN dotnet restore "YouTube Stream API/YouTube Stream API.csproj"
COPY . .
WORKDIR "/src/YouTube Stream API"
RUN dotnet build "YouTube Stream API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "YouTube Stream API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "YouTube Stream API.dll"]