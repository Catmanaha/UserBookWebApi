# UserBookWebApi

UserBookWebApi is a learning project composed of two ASP.NET Core services:

- `UserWebApi` stores users in SQL Server through Entity Framework Core.
- `BooksWebApi` stores per-user books in MongoDB.

The user service calls the books service to assemble a combined details
response. The project demonstrates service-to-service HTTP, two persistence
models, Docker Compose, and explicit configuration boundaries. It is not a
production microservice deployment.

## Request flow

```text
client
  |
  v
UserWebApi ----> SQL Server
  |
  +-----------> BooksWebApi ----> MongoDB
  |
  v
combined user + books response
```

If a requested user does not exist, `UserWebApi` returns HTTP 404 without
calling the books service. An existing user with no books receives an empty
collection.

## Run with Docker Compose

Requirements: Docker with Compose support.

```powershell
Set-Location BookUserApp
Copy-Item .env.example .env
# Replace the placeholder in .env with a strong local-only SQL Server password.
docker compose up --build
```

Services are exposed locally at:

- User API and Swagger: `http://localhost:5246/swagger`
- Books API and Swagger: `http://localhost:5283/swagger`
- MongoDB: `localhost:27017`
- SQL Server: `localhost:15241`

Compose injects database connection strings at runtime. No populated `.env`
file or application development settings should be committed.

## Build without containers

The services require .NET 10. Configure these environment variables before
running them directly:

```powershell
$env:MongoDbOption__MongoDbConnectionString = "mongodb://localhost:27017"
$env:MongoDbOption__DatabaseName = "UserBookDb"
$env:MongoDbOption__CollectionName = "Books"
$env:ConnectionStrings__DefaultConnectionString = "Server=localhost,15241;Database=UserBookDb;User Id=sa;Password=YOUR_PASSWORD;TrustServerCertificate=True;"
$env:BooksApiBaseAddress = "http://localhost:5283/"

dotnet build BookUserApp/BookUserApp.sln --configuration Release
```

## Verification

```powershell
dotnet build BookUserApp/BookUserApp.sln --configuration Release
dotnet list BookUserApp/BookUserApp.sln package --vulnerable --include-transitive
docker compose -f BookUserApp/docker-compose.yaml config
```

## Current limitations

- There are no automated tests yet.
- Cross-service failure handling is minimal; an unavailable books service
  currently makes the combined details request fail.
- There is no authentication or authorization.
- SQL Server migrations exist, but Compose does not automatically apply them.
- A SQL Server password was committed in earlier history. Any reused credential
  must be rotated; current-tree cleanup does not rewrite shared history.
