# Video Game Catalogue API

This project is a simple RESTful API built using ASP.NET Core and Entity Framework Core for managing a catalogue of video games. The API allows users to browse, create, update, and delete video game entries. The backend is set up with a SQL Server database using Code-First approach with Entity Framework Core.

## Features

- **CRUD Operations**: Create, Read, Update, and Delete video game entries.
- **Database Integration**: Uses SQL Server (via EF Core) for data storage.
- **Unit Testing**: Includes unit tests for API controller functionality using an in-memory database.
- **API Endpoints**:
  - `GET /api/VideoGames`: Retrieve a list of all video games.
  - `GET /api/VideoGames/{id}`: Retrieve a specific video game by its ID.
  - `POST /api/VideoGames`: Create a new video game entry.
  - `PUT /api/VideoGames/{id}`: Update an existing video game entry.
  - `DELETE /api/VideoGames/{id}`: Delete a video game entry by its ID.

## Technologies

- **ASP.NET Core 8**
- **Entity Framework Core** (SQL Server)
- **xUnit** for unit testing
- **In-memory Database** for unit tests
- **Swagger** for API documentation

## Getting Started

### Screenshots

![image](https://github.com/user-attachments/assets/0f08582f-2b26-4cdf-9772-4e1d928d244a)

### Prerequisites

- .NET SDK (8.0 or later)
- SQL Server (local or cloud)
- Visual Studio or Visual Studio Code

### Setup

1. **Clone the repository**:
    ```bash
    git clone https://github.com/patelsmit11/VideoGameCatalogueApi.git
    cd VideoGameCatalogueApi
    ```

2. **Restore dependencies**:
    ```bash
    dotnet restore
    ```

3. **Create and apply the database migrations**:
    ```bash
    dotnet ef migrations add InitialCreate
    dotnet ef database update
    ```

4. **Run the API**:
    ```bash
    dotnet run
    ```

    The API will be available at `https://localhost:7097`

### Testing

Run the unit tests to verify the functionality:

```bash
dotnet test
