using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Shop.Core.Domain.Entities;
using Shop.Core.Domain.Interfaces;
using Shop.Infrastructure.Data;
using Shop.Infrastructure.Repositories;
using Shop.Infrastructure.Middleware;
using Shop.Application.Mappings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ---------------------------
// 1️⃣ Connection String
// ---------------------------
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? "Server=(localdb)\\mssqllocaldb;Database=ShopDb;Trusted_Connection=True;";

// ---------------------------
// 2️⃣ Add DbContext
// ---------------------------
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// ---------------------------
// 3️⃣ AutoMapper
// ---------------------------
builder.Services.AddAutoMapper(typeof(MappingProfile));

// ---------------------------
// 4️⃣ Register Repositories
// ---------------------------
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

//  JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

// ---------------------------
// 4️⃣ Controllers & Swagger
// ---------------------------
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

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));
var app = builder.Build();

// ---------------------------
// 5️⃣ Middleware
// ---------------------------
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); 
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shop API V1");
        c.RoutePrefix = "swagger"; // URL: http://localhost:5008/swagger/index.html
    });
}

// app.UseHttpsRedirection();
app.UseCors("AllowAngular");
app.UseAuthentication(); 
app.UseAuthorization();
app.MapControllers();

app.Run();
