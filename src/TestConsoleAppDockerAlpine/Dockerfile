#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app

# Install dependencies
RUN apk update && \
	apk add --no-cache \
		bash \
		icu-libs \
		krb5-libs \
		libgcc \
		libintl \
		libssl1.1 \
		libstdc++ \
		zlib \
		libstdc++ \
		libx11 \
		libxrender \
		libxext \
		libssl1.1 \
		libjpeg \
		gcompat \
		ca-certificates \
		fontconfig \
		freetype \
		ttf-dejavu \
		ttf-droid \
		ttf-freefont \
		ttf-liberation && \
	apk add --no-cache libgdiplus --repository https://dl-3.alpinelinux.org/alpine/edge/testing/ && \
	ln -s /usr/lib/libjpeg.so.8 /usr/lib/libjpeg.so.62

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TestConsoleAppDockerAlpine/TestConsoleAppDockerAlpine.csproj", "TestConsoleAppDockerAlpine/"]
RUN dotnet restore "TestConsoleAppDockerAlpine/TestConsoleAppDockerAlpine.csproj"
COPY . .
WORKDIR "/src/TestConsoleAppDockerAlpine"
RUN dotnet build "TestConsoleAppDockerAlpine.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestConsoleAppDockerAlpine.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestConsoleAppDockerAlpine.dll"]
