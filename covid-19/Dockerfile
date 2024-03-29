#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0-bullseye-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim AS build
WORKDIR /src
COPY ["covid-19/covid-19.csproj", "covid-19/"]
RUN dotnet restore "covid-19/covid-19.csproj"
COPY . .
WORKDIR "/src/covid-19"
RUN dotnet build "covid-19.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "covid-19.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "covid-19.dll"]