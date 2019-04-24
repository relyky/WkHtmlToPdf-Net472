FROM mcr.microsoft.com/dotnet/core/runtime:2.2 AS base

# Install libwkhtmltox dependencies
RUN apt-get update \
    && apt-get install -y --no-install-recommends \
        zlib1g \
        fontconfig \
        libfreetype6 \
        libx11-6 \
        libxext6 \
        libxrender1 \
		libjpeg62-turbo

WORKDIR /app


FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /src
COPY ["DinkToPdf.TestConsoleAppDocker/DinkToPdf.TestConsoleAppDocker.csproj", "."]
RUN dotnet restore -s "https://www.myget.org/F/haukcode-public/api/v3/index.json" -s "https://api.nuget.org/v3/index.json" "DinkToPdf.TestConsoleAppDocker.csproj"
COPY DinkToPdf.TestConsoleAppDocker/*.cs ./
WORKDIR "/src"
RUN dotnet build "DinkToPdf.TestConsoleAppDocker.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "DinkToPdf.TestConsoleAppDocker.csproj" -c Release -o /app

FROM base AS final

WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "DinkToPdf.TestConsoleAppDocker.dll"]
