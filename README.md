# UserBookWebApi

I built UserBookWebApi to learn how two ASP.NET Core services can work together while using different databases:

- `UserWebApi` stores users in SQL Server with Entity Framework Core.
- `BooksWebApi` stores each user's books in MongoDB.

The user service calls the books service when it needs to return a combined user-and-books response. A missing user returns HTTP 404 without calling the books service, while an existing user with no books gets an empty collection.

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

## Stack

- .NET 10 and ASP.NET Core
- Entity Framework Core and SQL Server
- MongoDB
- Docker Compose

## Run with Docker Compose

You need Docker with Compose support.

```powershell
Set-Location BookUserApp
Copy-Item .env.example .env
# Replace the placeholder with a strong password used only for local SQL Server.
docker compose up --build
```

The local services are available at:

- User API and Swagger: `http://localhost:5246/swagger`
- Books API and Swagger: `http://localhost:5283/swagger`
- MongoDB: `localhost:27017`
- SQL Server: `localhost:15241`

Compose reads the database settings at runtime. Do not commit the populated `.env` file.

## Build without containers

Set the service configuration before building or running directly:

```powershell
$env:MongoDbOption__MongoDbConnectionString = "mongodb://localhost:27017"
$env:MongoDbOption__DatabaseName = "UserBookDb"
$env:MongoDbOption__CollectionName = "Books"
$env:ConnectionStrings__DefaultConnectionString = "Server=localhost,15241;Database=UserBookDb;User Id=sa;Password=YOUR_PASSWORD;TrustServerCertificate=True;"
$env:BooksApiBaseAddress = "http://localhost:5283/"

dotnet build BookUserApp/BookUserApp.sln --configuration Release
```

## Checks

There are no automated tests yet. These commands build both services, check dependencies, and validate the Compose file:

```powershell
dotnet build BookUserApp/BookUserApp.sln --configuration Release
dotnet list BookUserApp/BookUserApp.sln package --vulnerable --include-transitive
docker compose -f BookUserApp/docker-compose.yaml config
```

## Still to do

- Add automated tests.
- Handle an unavailable books service more gracefully.
- Add authentication and authorization.
- Apply the existing SQL Server migrations automatically when using Compose.
