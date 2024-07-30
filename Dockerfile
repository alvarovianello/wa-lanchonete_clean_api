FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

ENV ASPNETCORE_URLS=http://+:3001
ENV ASPNETCORE_ENVIRONMENT=Development
EXPOSE 3001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY ./WA.Api/WA.Api.csproj WA.Api/
COPY ./WA.Application/WA.Application.csproj WA.Application/
COPY ./WA.Persistence/WA.Persistence.csproj WA.Persistence/
COPY ./WA.Domain/WA.Domain.csproj WA.Domain/

RUN dotnet restore WA.Api/WA.Api.csproj

COPY . .

RUN dotnet build WA.Api/WA.Api.csproj -c Release -o /app

FROM build AS publish

RUN dotnet publish WA.Api/WA.Api.csproj -c Release -o /app

FROM base AS final

WORKDIR /app

COPY --from=publish /app .

ENTRYPOINT ["dotnet", "WA.Api.dll"]