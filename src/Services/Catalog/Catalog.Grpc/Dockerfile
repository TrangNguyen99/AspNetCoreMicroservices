FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["Services/Catalog/Catalog.Grpc/Catalog.Grpc.csproj", "Services/Catalog/Catalog.Grpc/"]
COPY ["Services/Catalog/Catalog.Core/Catalog.Core.csproj", "Services/Catalog/Catalog.Core/"]
RUN dotnet restore "Services/Catalog/Catalog.Grpc/Catalog.Grpc.csproj"
COPY . .
WORKDIR "/src/Services/Catalog/Catalog.Grpc"
RUN dotnet build "Catalog.Grpc.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Catalog.Grpc.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.Grpc.dll"]
