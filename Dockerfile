FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["hostname.resolver.aws.csproj", "./"]
RUN dotnet restore "hostname.resolver.aws.csproj" 
COPY . .
WORKDIR "/src/"
RUN dotnet build "hostname.resolver.aws.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV Kestrel__Endpoints__Http__Url="http://0.0.0.0:9999"
ENTRYPOINT ["dotnet","./hostname.resolver.aws.dll"]