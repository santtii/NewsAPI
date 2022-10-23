#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
COPY --from=mcr.microsoft.com/dotnet/aspnet:2.1 /usr/share/dotnet /usr/share/dotnet
COPY --from=mcr.microsoft.com/dotnet/aspnet:3.1 /usr/share/dotnet /usr/share/dotnet
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
COPY --from=mcr.microsoft.com/dotnet/sdk:2.1 /usr/share/dotnet /usr/share/dotnet
COPY --from=mcr.microsoft.com/dotnet/sdk:3.1 /usr/share/dotnet /usr/share/dotnet
WORKDIR /src
COPY ["nuget.config", "."]
COPY ["src/NewsAPI/NewsAPI.csproj", "src/NewsAPI/"]
COPY ["src/NewsAPI.Infrastructure/NewsAPI.Infrastructure.csproj", "src/NewsAPI.Infrastructure/"]
COPY ["src/NewsAPI.Core/NewsAPI.Core.csproj", "src/NewsAPI.Core/"]
COPY ["src/NewsAPI.SharedKernel/NewsAPI.SharedKernel.csproj", "src/NewsAPI.SharedKernel/"]
RUN dotnet restore "src/NewsAPI/NewsAPI.csproj"
COPY . .
WORKDIR "/src/src/NewsAPI"
RUN dotnet build "NewsAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NewsAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ["src/NewsAPI/aspnetapp.pfx", "/https/"]
ENTRYPOINT ["dotnet", "NewsAPI.dll"]