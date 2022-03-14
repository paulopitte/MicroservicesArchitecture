#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Discount/src/Discount.Api.csproj", "Discount/src/"]
COPY ["Core/Core.Common/Core.Common.csproj", "Core/Core.Common/"]
COPY ["Core/Core.Contracts/Core.Contracts.csproj", "Core/Core.Contracts/"]
RUN dotnet restore "Discount/src/Discount.Api.csproj"
COPY . .
WORKDIR "/src/Discount/src"
RUN dotnet build "Discount.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Discount.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Discount.Api.dll"]
