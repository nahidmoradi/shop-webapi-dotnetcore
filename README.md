# shop-webapi-dotnetcore
🛒 Shop API

A complete RESTful API for an online store built with ASP.NET Core 9.0 and the Onion (Clean) Architecture.

📋 Table of Contents

Features

Project Architecture

Technologies

Prerequisites

Installation & Setup

Project Structure

API Endpoints

Configuration

✨ Features

🏗️ Onion (Clean) Architecture – complete separation of layers and dependencies

🔐 JWT Authentication – secure login and registration system

📦 Repository Pattern – generic, testable data access

🗺️ AutoMapper – automatic mapping between entities and DTOs

📊 Entity Framework Core – powerful ORM for SQL Server

🔍 Swagger/OpenAPI – automatic API documentation

⚡ CORS – support for cross-origin requests

🛡️ Exception Handling Middleware – centralized error handling

📝 Serilog – advanced logging

🏛️ Project Architecture

The project is designed using Onion Architecture (Clean Architecture):
┌─────────────────────────────────────────┐
│         Presentation Layer              │
│      (Controllers, Program.cs)          │
├─────────────────────────────────────────┤
│       Application Layer                 │
│   (DTOs, Services, Mappings)            │
├─────────────────────────────────────────┤
│      Infrastructure Layer               │
│ (Data, Repositories, Middleware)        │
├─────────────────────────────────────────┤
│          Core/Domain Layer              │
│     (Entities, Interfaces)              │
└─────────────────────────────────────────┘
Advantages

✅ Full separation of business logic from implementation details

✅ High testability

✅ Easy to maintain and extend

✅ Dependency inversion (dependencies point inward)

🚀 Technologies

Framework: ASP.NET Core 9.0

Database: SQL Server (Entity Framework Core)

Authentication: JWT Bearer

Mapping: AutoMapper

Logging: Serilog

API Documentation: Swagger/Swashbuckle

Architecture Pattern: Onion Architecture

Design Pattern: Generic Repository Pattern

📋 Prerequisites

.NET 9.0 SDK

SQL Server
 (LocalDB or SQL Server Express)

An IDE such as Visual Studio 2022
 or VS Code

⚙️ Installation & Setup
1. Clone the repository
  git clone https://github.com/nahidmoradi/shop-webapi-dotnetcore.git
  cd shop-webapi-dotnetcore
2. Restore packages
   dotnet restore
3. Configure the Connection String

Edit appsettings.json:
  {
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ShopDb;Trusted_Connection=True;"
  }
}
4. Run the migration
  dotnet ef database update
5. Run the project
  dotnet run
The API will be available at http://localhost:5008.

6. Open Swagger

Visit:
  http://localhost:5008/swagger
Shop/
├── Core/
│   └── Domain/
│       ├── Entities/
│       │   ├── Product.cs
│       │   ├── Category.cs
│       │   ├── User.cs
│       │   ├── Order.cs
│       │   └── OrderItem.cs
│       └── Interfaces/
│           ├── IRepository.cs
│           ├── IProductRepository.cs
│           ├── ICategoryRepository.cs
│           └── IUserRepository.cs
│
├── Application/
│   ├── DTOs/
│   │   ├── ProductDto.cs
│   │   ├── CategoryDto.cs
│   │   └── UserDto.cs
│   ├── Services/
│   └── Mappings/
│       └── MappingProfile.cs
│
├── Infrastructure/
│   ├── Data/
│   │   ├── AppDbContext.cs
│   │   └── SeedData.cs
│   ├── Repositories/
│   │   ├── Repository.cs
│   │   ├── ProductRepository.cs
│   │   ├── CategoryRepository.cs
│   │   └── UserRepository.cs
│   └── Middleware/
│       └── ExceptionHandlingMiddleware.cs
│
├── Presentation/
│   └── Controllers/
│       ├── ProductsController.cs
│       ├── CategoriesController.cs
│       └── AuthController.cs
│

├── Migrations/
├── Program.cs
├── appsettings.json
└── README.md
🔌 API Endpoints
🔐 Authentication
| Method | Endpoint             | Description         | Auth |
| ------ | -------------------- | ------------------- | ---- |
| POST   | `/api/auth/register` | Register a new user | ❌    |
| POST   | `/api/auth/login`    | Login and get token | ❌    |
📦 Products
| Method | Endpoint             | Description                        | Auth    |
| ------ | -------------------- | ---------------------------------- | ------- |
| GET    | `/api/products`      | Get product list (with pagination) | ❌       |
| GET    | `/api/products/{id}` | Get product details                | ❌       |
| POST   | `/api/products`      | Create a new product               | ✅ Admin |
| PUT    | `/api/products/{id}` | Update a product                   | ✅ Admin |
| DELETE | `/api/products/{id}` | Delete a product                   | ✅ Admin |
📑 Categories
| Method | Endpoint               | Description           | Auth    |
| ------ | ---------------------- | --------------------- | ------- |
| GET    | `/api/categories`      | Get category list     | ❌       |
| GET    | `/api/categories/{id}` | Get category details  | ❌       |
| POST   | `/api/categories`      | Create a new category | ✅ Admin |
| PUT    | `/api/categories/{id}` | Update a category     | ✅ Admin |
| DELETE | `/api/categories/{id}` | Delete a category     | ✅ Admin |
Query Parameters
  - page – page number (default: 1)  
  - pageSize – items per page (default: 10)  
  - q – search by name or description
Example:
  GET /api/products?page=1&pageSize=20&q=laptop
⚙️ Configuration
JWT Configuration

In appsettings.json:
  {
  "Jwt": {
    "Key": "your-secret-key-min-32-characters",
    "Issuer": "ShopAPI",
    "Audience": "ShopClient"
  }
}
CORS Configuration

In Program.cs, configure allowed origins:
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
🧪 Testing the API
Register a new user
POST /api/auth/register
Content-Type: application/json

{
  "username": "testuser",
  "email": "test@example.com",
  "password": "Test@123"
}
Login
POST /api/auth/login
Content-Type: application/json

{
  "username": "testuser",
  "password": "Test@123"
}
Get products
  GET /api/products?page=1&pageSize=10
Create a product (requires Token)
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
🔧 Useful Commands
# Build the project
dotnet build

# Run the project
dotnet run

# Create a new migration
dotnet ef migrations add MigrationName

# Apply migrations
dotnet ef database update

# Remove the last migration
dotnet ef migrations remove

# Drop the database
dotnet ef database drop

## 👨‍💻 Developer
- GitHub: [nahidmoradi](https://github.com/nahidmoradi)
- Email: n.morady@gmail.com
