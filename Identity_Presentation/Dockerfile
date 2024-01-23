FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Identity_Presentation/Identity_Presentation.csproj", "Identity_Presentation/"]
COPY ["Identity_Application/Identity_Application.csproj", "Identity_Application/"]
COPY ["Identity_Domain/Identity_Domain.csproj", "Identity_Domain/"]
COPY ["Identity_Infrastructure/Identity_Infrastructure.csproj", "Identity_Infrastructure/"]
RUN dotnet restore "./Identity_Presentation/./Identity_Presentation.csproj"
COPY . .
COPY ["Identity_Infrastructure/SeedData", "/app/SeedData"]
WORKDIR "/src/Identity_Presentation"
RUN dotnet build "./Identity_Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Identity_Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Identity_Presentation.dll"]