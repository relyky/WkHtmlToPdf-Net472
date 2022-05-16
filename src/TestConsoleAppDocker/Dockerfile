#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

# Install dependencies
RUN apt-get update \
    && apt-get install -y --no-install-recommends \
        zlib1g \
		libgdiplus \
        libc6-dev \
		ca-certificates \
		fontconfig \
		fontconfig-config \
		fonts-dejavu-core \
		libbsd0 \
		libexpat1 \
		libfontconfig1 \
		libfontenc1 \
		libfreetype6 \
		libjpeg62-turbo \
		libpng16-16 \
		libssl1.1 \
		libx11-6 \
		libx11-data \
		libxau6 \
		libxcb1 \
		libxdmcp6 \
		libxext6 \
		libxrender1 \
		ucf \
		x11-common \
		xfonts-75dpi \
		xfonts-base \
		xfonts-encodings \
		xfonts-utils \
     && rm -rf /var/lib/apt/lists/*

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TestConsoleAppDocker/TestConsoleAppDocker.csproj", "TestConsoleAppDocker/"]
RUN dotnet restore "TestConsoleAppDocker/TestConsoleAppDocker.csproj"
COPY . .
WORKDIR "/src/TestConsoleAppDocker"
RUN dotnet build "TestConsoleAppDocker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestConsoleAppDocker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestConsoleAppDocker.dll"]
