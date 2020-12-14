#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/Effectory.Api/Effectory.Api.csproj", "src/Effectory.Api/"]
COPY ["src/Effectory.Application/Effectory.Application.csproj", "src/Effectory.Application/"]
COPY ["src/Effectory.Domain/Effectory.Core.csproj", "src/Effectory.Domain/"]
COPY ["src/Effectory.Shared/Effectory.Shared.csproj", "src/Effectory.Shared/"]
COPY ["src/Effectory.Infra/Effectory.Infra.csproj", "src/Effectory.Infra/"]
RUN dotnet restore "src/Effectory.Api/Effectory.Api.csproj"
COPY . .
WORKDIR "/src/src/Effectory.Api"
RUN dotnet build "Effectory.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Effectory.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Effectory.Api.dll"]
