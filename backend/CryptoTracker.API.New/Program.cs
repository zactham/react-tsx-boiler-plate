using Microsoft.EntityFrameworkCore;
using CryptoTracker.API.New.Data;
using CryptoTracker.API.New.Data.Repositories;
using DotNetEnv;


var builder = WebApplication.CreateBuilder(args);

Env.Load();

// Add services to the container.
builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
connectionString = connectionString.Replace("{DB_HOST}", Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost");
connectionString = connectionString.Replace("{DB_NAME}", Environment.GetEnvironmentVariable("DB_NAME") ?? "cryptodb");
connectionString = connectionString.Replace("{DB_USER}", Environment.GetEnvironmentVariable("DB_USER") ?? "postgres");
connectionString = connectionString.Replace("{DB_PASSWORD}", Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "yourpasswordhere");
Console.WriteLine(connectionString);

builder.Services.AddDbContext<CryptoDbContext>(options =>
    options.UseNpgsql(connectionString));

// Add repositories
builder.Services.AddScoped<ICryptoRepository, CryptoRepository>();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", 
        policy => policy
            .WithOrigins("http://localhost:3000") // React app address
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

// Add API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// HTTPS warning fix
app.Urls.Clear();
app.Urls.Add("http://localhost:5000"); // HTTP port
app.Urls.Add("https://localhost:7283"); // HTTPS port

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    // Manually create and apply migrations in development
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<CryptoDbContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred during database migration");
        }
    }
}

app.UseHttpsRedirection();

// Configure CORS

app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapControllers();

app.Run();