FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["./Identity-Server-4/Identity-Server-4.csproj", "Identity-Server-4/"]
RUN dotnet restore "Identity-Server-4/Identity-Server-4.csproj"
COPY . .
WORKDIR "/src/Identity-Server-4"
RUN dotnet build "Identity-Server-4.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Identity-Server-4.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Identity-Server-4.dll"]