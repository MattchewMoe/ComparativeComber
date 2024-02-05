FROM mcr.microsoft.com/dotnet/aspnet:8.0-jammy AS base
# Disable globalization-invariant mode
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0-jammy AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ComparativeComber/ComparativeComber.csproj", "ComparativeComber/"]
RUN dotnet restore "ComparativeComber/ComparativeComber.csproj"
COPY . .
WORKDIR "/src/ComparativeComber"
RUN dotnet build "ComparativeComber.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "ComparativeComber.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ComparativeComber.dll"]
