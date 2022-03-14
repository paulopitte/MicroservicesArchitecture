#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Basket/Basket.Api/Basket.Api.csproj", "Basket/Basket.Api/"]
COPY ["Core/Core.Common/Core.Common.csproj", "Core/Core.Common/"]
COPY ["Core/Core.Contracts/Core.Contracts.csproj", "Core/Core.Contracts/"]
RUN dotnet restore "Basket/Basket.Api/Basket.Api.csproj"
COPY . .
WORKDIR "/src/Basket/Basket.Api"
RUN dotnet build "Basket.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Basket.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Basket.Api.dll"]
