using System.Text.Json.Serialization;
using _011Global.RestServiceAPI.Settings;
using _011Global.Shared;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure appsettings
builder.Configuration
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddUserSecrets<Program>()
    .AddEnvironmentVariables();

// Add AppSettingsManager
builder.Services.AddSingleton<AppSettingsManager>();

// Add Logging
Log.Logger = new LoggerConfiguration()
    .ReadFrom
    .Configuration(builder.Configuration)
    .CreateLogger();

builder.Services.AddSerilog(Log.Logger);

Log.Information("Logger has been configured");

// Add DBContexts
builder.Services.RegisterDbContexts(builder.Configuration.GetConnectionString("TransactionsHubDB"));

// Add REST Controllers
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    //app.UseRouting();
    app.MapGet("/DebugConfig", () => builder.Configuration.GetDebugView());
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();