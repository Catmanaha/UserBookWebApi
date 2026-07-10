FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY UserWebApi .
RUN dotnet publish -c Release -o dist

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /src/dist .
ENTRYPOINT ["dotnet", "UserWebApi.dll"]
