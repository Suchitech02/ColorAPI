FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 5300

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["ColorAPI.csproj", "./"]
RUN dotnet restore "ColorAPI.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "ColorAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ColorAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ColorAPI.dll"]
