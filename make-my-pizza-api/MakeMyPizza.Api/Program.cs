using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore.Diagnostics;

using Newtonsoft.Json;
using Serilog;

using MakeMyPizza.Api.Middlewares;
using MakeMyPizza.Data.Repository;
using MakeMyPizza.Data.Models;
using MakeMyPizza.Api.Utils;
using MakeMyPizza.Data.IRepository;
using MakeMyPizza.Domain.IService;
using MakeMyPizza.Domain.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext()
                .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddControllers().AddNewtonsoftJson(options => { options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
var allowedOriginStr = builder.Configuration.GetSection("AllowedOrigins").Get<string>();
var allowedOrigins = allowedOriginStr?.Split(";") ?? new string[] { "*" };

builder.Services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder.WithOrigins(allowedOrigins)
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            }));
builder.Services.AddScoped<IPizzaOrderManagementDbContext>(provider => provider.GetRequiredService<PizzaOrderManagementDbContext>());
builder.Services.AddDbContext<PizzaOrderManagementDbContext>(options => 
                                                                options.UseInMemoryDatabase("MakeMyPizzaDb")
                                                                        .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                                                            );
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IBaseRepository<Order>, BaseRepository<Order>>();
builder.Services.AddScoped<IBaseRepository<Pizza>, BaseRepository<Pizza>>();
builder.Services.AddScoped<IBaseRepository<PizzaPrice>, BaseRepository<PizzaPrice>>();
builder.Services.AddScoped<IBaseRepository<NonPizza>, BaseRepository<NonPizza>>();
builder.Services.AddScoped<IBaseRepository<Customer>, BaseRepository<Customer>>();
builder.Services.AddScoped<IBaseRepository<User>, BaseRepository<User>>();
builder.Services.AddScoped<IBaseRepository<Sauce>, BaseRepository<Sauce>>();
builder.Services.AddScoped<IBaseRepository<Topping>, BaseRepository<Topping>>();
builder.Services.AddScoped<IBaseRepository<Drink>, BaseRepository<Drink>>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();

builder.Services.AddScoped<IBaseService<Pizza>, BaseService<Pizza>>();
builder.Services.AddScoped<IBaseService<PizzaPrice>, BaseService<PizzaPrice>>();
builder.Services.AddScoped<IBaseService<NonPizza>, BaseService<NonPizza>>();
builder.Services.AddScoped<IBaseService<Customer>, BaseService<Customer>>();
builder.Services.AddScoped<IBaseService<User>, BaseService<User>>();
builder.Services.AddScoped<IBaseService<Sauce>, BaseService<Sauce>>();
builder.Services.AddScoped<IBaseService<Topping>, BaseService<Topping>>();
builder.Services.AddScoped<IBaseService<Drink>, BaseService<Drink>>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IPizzaService, PizzaService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddAutoMapper(typeof(Program));
// builder.Services.AddSeeds();

var app = builder.Build();

var config = builder.Configuration;

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseErrorHandlerMiddleware();

//app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.SeedData(config); // Data seeding

app.MapControllers();

app.Run();
