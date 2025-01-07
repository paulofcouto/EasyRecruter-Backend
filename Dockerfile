FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app 
COPY ["Easy.API/Easy.API.csproj", "Easy.API/"] 
RUN dotnet restore "Easy.API/Easy.API.csproj" 
COPY . .
WORKDIR "/app/Easy.API" 
RUN dotnet build "Easy.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Easy.API.csproj" -c Release -o /app/publish 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Easy.API.dll"]