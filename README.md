# 🛒 Shop API

یک RESTful API کامل برای فروشگاه آنلاین که با ASP.NET Core 9.0 و معماری Onion (Clean Architecture) پیاده‌سازی شده است.

## 📋 فهرست مطالب

- [ویژگی‌ها](#ویژگیها)
- [معماری پروژه](#معماری-پروژه)
- [تکنولوژی‌ها](#تکنولوژیها)
- [پیش‌نیازها](#پیشنیازها)
- [نصب و راه‌اندازی](#نصب-و-راهاندازی)
- [ساختار پروژه](#ساختار-پروژه)
- [API Endpoints](#api-endpoints)
- [تنظیمات](#تنظیمات)

## ✨ ویژگی‌ها

- 🏗️ **معماری Onion (Clean Architecture)** - جداسازی کامل لایه‌ها و وابستگی‌ها
- 🔐 **احراز هویت JWT** - سیستم امن ورود و ثبت‌نام
- 📦 **الگوی Repository** - دسترسی به داده‌ها به صورت Generic و Testable
- 🗺️ **AutoMapper** - نگاشت خودکار بین Entity و DTO
- 📊 **Entity Framework Core** - ORM قدرتمند برای SQL Server
- 🔍 **Swagger/OpenAPI** - مستندات خودکار API
- ⚡ **CORS** - پشتیبانی از درخواست‌های Cross-Origin
- 🛡️ **Exception Handling Middleware** - مدیریت متمرکز خطاها
- 📝 **Serilog** - لاگ‌گیری پیشرفته

## 🏛️ معماری پروژه

پروژه بر اساس **Onion Architecture** (Clean Architecture) طراحی شده است:

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

### مزایای این معماری:

- ✅ جداسازی کامل منطق کسب‌وکار از جزئیات پیاده‌سازی
- ✅ تست‌پذیری بالا
- ✅ قابلیت نگهداری و توسعه آسان
- ✅ وابستگی به سمت داخل (Dependency Inversion)

## 🚀 تکنولوژی‌ها

- **Framework:** ASP.NET Core 9.0
- **Database:** SQL Server (Entity Framework Core)
- **Authentication:** JWT Bearer
- **Mapping:** AutoMapper
- **Logging:** Serilog
- **API Documentation:** Swagger/Swashbuckle
- **Architecture Pattern:** Onion Architecture
- **Design Pattern:** Repository Pattern (Generic)

## 📋 پیش‌نیازها

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [SQL Server](https://www.microsoft.com/sql-server) (LocalDB یا SQL Server Express)
- یک IDE مانند [Visual Studio 2022](https://visualstudio.microsoft.com/) یا [VS Code](https://code.visualstudio.com/)

## ⚙️ نصب و راه‌اندازی

### 1. کلون کردن پروژه

```bash
git clone https://github.com/nahidmoradi/shop-webapi-dotnetcore.git
cd shop-webapi-dotnetcore
```

### 2. بازیابی پکیج‌ها

```bash
dotnet restore
```

### 3. تنظیم Connection String

فایل `appsettings.json` را ویرایش کنید:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ShopDb;Trusted_Connection=True;"
  }
}
```

### 4. اجرای Migration

```bash
dotnet ef database update
```

### 5. اجرای پروژه

```bash
dotnet run
```

API در آدرس `http://localhost:5008` در دسترس خواهد بود.

### 6. مشاهده Swagger

به آدرس زیر مراجعه کنید:
```
http://localhost:5008/swagger
```

## 📁 ساختار پروژه

```
Shop/
├── Core/
│   └── Domain/
│       ├── Entities/           # مدل‌های دامین
│       │   ├── Product.cs
│       │   ├── Category.cs
│       │   ├── User.cs
│       │   ├── Order.cs
│       │   └── OrderItem.cs
│       └── Interfaces/         # اینترفیس‌های Repository
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
│   ├── Services/               # سرویس‌های Application
│   └── Mappings/               # AutoMapper Profiles
│       └── MappingProfile.cs
│
├── Infrastructure/
│   ├── Data/                   # DbContext و Configuration
│   │   ├── AppDbContext.cs
│   │   └── SeedData.cs
│   ├── Repositories/           # پیاده‌سازی Repository
│   │   ├── Repository.cs
│   │   ├── ProductRepository.cs
│   │   ├── CategoryRepository.cs
│   │   └── UserRepository.cs
│   └── Middleware/             # Middleware ها
│       └── ExceptionHandlingMiddleware.cs
│
├── Presentation/
│   └── Controllers/            # API Controllers
│       ├── ProductsController.cs
│       ├── CategoriesController.cs
│       └── AuthController.cs
│
├── Migrations/                 # EF Core Migrations
├── Program.cs                  # نقطه شروع برنامه
├── appsettings.json           # تنظیمات
└── README.md
```

## 🔌 API Endpoints

### 🔐 Authentication

| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| POST | `/api/auth/register` | ثبت‌نام کاربر جدید | ❌ |
| POST | `/api/auth/login` | ورود و دریافت Token | ❌ |

### 📦 Products

| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| GET | `/api/products` | دریافت لیست محصولات (با صفحه‌بندی) | ❌ |
| GET | `/api/products/{id}` | دریافت جزئیات یک محصول | ❌ |
| POST | `/api/products` | ایجاد محصول جدید | ✅ Admin |
| PUT | `/api/products/{id}` | ویرایش محصول | ✅ Admin |
| DELETE | `/api/products/{id}` | حذف محصول | ✅ Admin |

### 📑 Categories

| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| GET | `/api/categories` | دریافت لیست دسته‌بندی‌ها | ❌ |
| GET | `/api/categories/{id}` | دریافت جزئیات یک دسته‌بندی | ❌ |
| POST | `/api/categories` | ایجاد دسته‌بندی جدید | ✅ Admin |
| PUT | `/api/categories/{id}` | ویرایش دسته‌بندی | ✅ Admin |
| DELETE | `/api/categories/{id}` | حذف دسته‌بندی | ✅ Admin |

### Query Parameters (لیست محصولات و دسته‌بندی‌ها)

- `page` - شماره صفحه (پیش‌فرض: 1)
- `pageSize` - تعداد آیتم در هر صفحه (پیش‌فرض: 10)
- `q` - جستجو در نام و توضیحات

**مثال:**
```
GET /api/products?page=1&pageSize=20&q=laptop
```

## ⚙️ تنظیمات

### JWT Configuration

در فایل `appsettings.json`:

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

در `Program.cs`، origin های مجاز را تنظیم کنید:

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

## 🧪 تست API

### ثبت‌نام کاربر

```bash
POST /api/auth/register
Content-Type: application/json

{
  "username": "testuser",
  "email": "test@example.com",
  "password": "Test@123"
}
```

### ورود

```bash
POST /api/auth/login
Content-Type: application/json

{
  "username": "testuser",
  "password": "Test@123"
}
```

### دریافت محصولات

```bash
GET /api/products?page=1&pageSize=10
```

### ایجاد محصول (نیاز به Token)

```bash
POST /api/products
Authorization: Bearer {your-jwt-token}
Content-Type: application/json

{
  "name": "لپ تاپ ایسوس",
  "description": "لپ تاپ گیمینگ",
  "price": 25000000,
  "stock": 10,
  "categoryId": 1
}
```

## 🔧 دستورات مفید

```bash
# بیلد پروژه
dotnet build

# اجرای پروژه
dotnet run

# ایجاد Migration جدید
dotnet ef migrations add MigrationName

# اعمال Migration به دیتابیس
dotnet ef database update

# حذف آخرین Migration
dotnet ef migrations remove

# پاک کردن دیتابیس
dotnet ef database drop
```


## 👨‍💻 توسعه‌دهنده

- GitHub: [nahidmoradi](https://github.com/nahidmoradi)
- Email: n.morady@gmail.com

