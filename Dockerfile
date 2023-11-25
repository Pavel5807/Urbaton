FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY "Urbatron.csproj" "Urbatron.csproj"

RUN dotnet restore "Urbatron.csproj"

COPY . .
WORKDIR /src
RUN dotnet publish --no-restore -c Release -o /app

FROM build AS publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Urbatron.dll"]
