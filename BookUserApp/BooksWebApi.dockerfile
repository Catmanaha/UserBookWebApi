FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY BooksWebApi .
RUN dotnet publish -c Release -o dist

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /src/dist .
ENTRYPOINT ["dotnet", "BooksWebApi.dll"]
