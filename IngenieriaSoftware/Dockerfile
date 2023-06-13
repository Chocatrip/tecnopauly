# Use the official image as a parent image.
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["IngenieriaSoftware.csproj", "./"]
RUN dotnet restore "./IngenieriaSoftware.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "IngenieriaSoftware.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IngenieriaSoftware.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IngenieriaSoftware.dll"]
