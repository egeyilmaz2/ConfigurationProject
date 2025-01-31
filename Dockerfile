FROM mcr.microsoft.com/dotnet/runtime-deps:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ConfigurationLibrary/ConfigurationLibrary.csproj", "ConfigurationLibrary/"]
RUN dotnet restore "ConfigurationLibrary/ConfigurationLibrary.csproj"
COPY . .
WORKDIR "/src/ConfigurationLibrary"
RUN dotnet build "ConfigurationLibrary.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ConfigurationLibrary.csproj" -c Release -r linux-x64 --self-contained true -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["./ConfigurationLibrary"]
