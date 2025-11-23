# 🛒 Shop API

A complete RESTful API for an online store built with **ASP.NET Core 9.0** using **Onion (Clean) Architecture**.

## 📋 Table of Contents

- [Features](#features)
- [Project Architecture](#project-architecture)
- [Technologies](#technologies)
- [Prerequisites](#prerequisites)
- [Installation & Setup](#installation--setup)
- [Project Structure](#project-structure)
- [API Endpoints](#api-endpoints)
- [Configuration](#configuration)

## ✨ Features

- 🏗️ **Onion (Clean) Architecture** - Full separation of layers and dependencies  
- 🔐 **JWT Authentication** - Secure login and registration system  
- 📦 **Repository Pattern** - Generic and testable data access  
- 🗺️ **AutoMapper** - Automatic mapping between Entity and DTO  
- 📊 **Entity Framework Core** - Powerful ORM for SQL Server  
- 🔍 **Swagger/OpenAPI** - Automatic API documentation  
- ⚡ **CORS** - Cross-Origin request support  
- 🛡️ **Exception Handling Middleware** - Centralized error management  
- 📝 **Serilog** - Advanced logging  

## 🏛️ Project Architecture

The project is designed following **Onion Architecture (Clean Architecture):**

```
┌─────────────────────────────────────────┐
│         Presentation Layer              │
│      (Controllers, Program.cs)          │
├─────────────────────────────────────────┤
│       Application Layer                 │
│   (DTOs, Services, Mappings)           │
├─────────────────────────────────────────┤
│      Infrastructure Layer               │
│ (Data, Repositories, Middleware)       │
├─────────────────────────────────────────┤
│          Core/Domain Layer              │
│     (Entities, Interfaces)             │
└─────────────────────────────────────────┘
```

### Benefits of this architecture:

- ✅ Full separation of business logic from implementation details  
- ✅ High testability  
- ✅ Easy maintenance and scalability  
- ✅ Dependency inversion (dependencies point inward)  

## 🚀 Technologies

- **Framework:** ASP.NET Core 9.0  
- **Database:** SQL Server (Entity Framework Core)  
- **Authentication:** JWT Bearer  
- **Mapping:** AutoMapper  
- **Logging:** Serilog  
- **API Documentation:** Swagger/Swashbuckle  
- **Architecture Pattern:** Onion Architecture  
- **Design Pattern:** Repository Pattern (Generic)  

## 📋 Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)  
- [SQL Server](https://www.microsoft.com/sql-server) (LocalDB or SQL Server Express)  
- An IDE like [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)  

## ⚙️ Installation & Setup

### 1. Clone the repository

```bash
git clone https://github.com/nahidmoradi/shop-webapi-dotnetcore.git
cd shop-webapi-dotnetcore
```

### 2. Restore packages

```bash
dotnet restore
```

### 3. Configure Connection String

Edit `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ShopDb;Trusted_Connection=True;"
  }
}
```

### 4. Run Migration

```bash
dotnet ef database update
```

### 5. Run the project

```bash
dotnet run
```

The API will be available at `http://localhost:5008`.

### 6. Open Swagger

Visit:
```
http://localhost:5008/swagger
```

## 📁 Project Structure

```
Shop/
├── Core/
│   └── Domain/
│       ├── Entities/           # Domain models
│       │   ├── Product.cs
│       │   ├── Category.cs
│       │   ├── User.cs
│       │   ├── Order.cs
│       │   └── OrderItem.cs
│       └── Interfaces/         # Repository interfaces
│           ├── IRepository.cs
│           ├── IProductRepository.cs
│           ├── ICategoryRepository.cs
│           └── IUserRepository.cs
│
├── Application/
│   ├── DTOs/                   # Data Transfer Objects
│   │   ├── ProductDto.cs
│   │   ├── CategoryDto.cs
│   │   └── UserDto.cs
│   ├── Services/               # Application services
│   └── Mappings/               # AutoMapper Profiles
│       └── MappingProfile.cs
│
├── Infrastructure/
│   ├── Data/                   # DbContext and Configuration
│   │   ├── AppDbContext.cs
│   │   └── SeedData.cs
│   ├── Repositories/           # Repository implementations
│   │   ├── Repository.cs
│   │   ├── ProductRepository.cs
│   │   ├── CategoryRepository.cs
│   │   └── UserRepository.cs
│   └── Middleware/             # Middleware
│       └── ExceptionHandlingMiddleware.cs
│
├── Presentation/
│   └── Controllers/            # API Controllers
│       ├── ProductsController.cs
│       ├── CategoriesController.cs
│       └── AuthController.cs
│
├── Migrations/                 # EF Core Migrations
├── Program.cs                  # Entry point
├── appsettings.json           # Settings
└── README.md
```

## 🔌 API Endpoints

### 🔐 Authentication

| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| POST | `/api/auth/register` | Register a new user | ❌ |
| POST | `/api/auth/login` | Login and get token | ❌ |

### 📦 Products

| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| GET | `/api/products` | Get list of products (with pagination) | ❌ |
| GET | `/api/products/{id}` | Get product details | ❌ |
| POST | `/api/products` | Create new product | ✅ Admin |
| PUT | `/api/products/{id}` | Update product | ✅ Admin |
| DELETE | `/api/products/{id}` | Delete product | ✅ Admin |

### 📑 Categories

| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| GET | `/api/categories` | Get list of categories | ❌ |
| GET | `/api/categories/{id}` | Get category details | ❌ |
| POST | `/api/categories` | Create new category | ✅ Admin |
| PUT | `/api/categories/{id}` | Update category | ✅ Admin |
| DELETE | `/api/categories/{id}` | Delete category | ✅ Admin |

### Query Parameters (for products & categories)

- `page` - Page number (default: 1)  
- `pageSize` - Items per page (default: 10)  
- `q` - Search in name and description  

**Example:**
```
GET /api/products?page=1&pageSize=20&q=laptop
```

## ⚙️ Configuration

### JWT Configuration

In `appsettings.json`:

```json
{
  "Jwt": {
    "Key": "your-secret-key-min-32-characters",
    "Issuer": "ShopAPI",
    "Audience": "ShopClient"
  }
}
```

### CORS Configuration

In `Program.cs`, set allowed origins:

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});
```

## 🧪 API Testing

### Register User

```bash
POST /api/auth/register
Content-Type: application/json

{
  "username": "testuser",
  "email": "test@example.com",
  "password": "Test@123"
}
```

### Login

```bash
POST /api/auth/login
Content-Type: application/json

{
  "username": "testuser",
  "password": "Test@123"
}
```

### Get Products

```bash
GET /api/products?page=1&pageSize=10
```

### Create Product (requires Token)

```bash
POST /api/products
Authorization: Bearer {your-jwt-token}
Content-Type: application/json

{
  "name": "Asus Laptop",
  "description": "Gaming laptop",
  "price": 25000000,
  "stock": 10,
  "categoryId": 1
}
```

## 🔧 Useful Commands

```bash
# Build the project
dotnet build

# Run the project
dotnet run

# Add a new migration
dotnet ef migrations add MigrationName

# Apply migration to the database
dotnet ef database update

# Remove the last migration
dotnet ef migrations remove

# Drop the database
dotnet ef database drop
```

## 👨‍💻 Developer

- GitHub: [nahidmoradi](https://github.com/nahidmoradi)  
- Email: n.morady@gmail.com
