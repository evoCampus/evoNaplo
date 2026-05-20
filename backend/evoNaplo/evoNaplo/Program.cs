using evoNaplo.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddDatabaseServices(builder.Configuration);


builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Health check
builder.Services.AddHealthChecks();

var app = builder.Build();

// Health check endpoint /healthz
app.MapHealthChecks("/healthz");

// Synchronously seed database on startup. This will clear existing data and insert new fake data every run.
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var db = services.GetRequiredService<evoNaplo.Data.AppDbContext>();
    var includeInvalid = false;
    var envVal = builder.Configuration["EVONAPLO_SEED_INVALID"] ?? Environment.GetEnvironmentVariable("EVONAPLO_SEED_INVALID");
    if (!string.IsNullOrEmpty(envVal) && bool.TryParse(envVal, out var parsed)) includeInvalid = parsed;

    var logger = services.GetRequiredService<ILoggerFactory>().CreateLogger("SeedData");
    evoNaplo.Data.SeedData.Seed(db, includeInvalid, logger);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
