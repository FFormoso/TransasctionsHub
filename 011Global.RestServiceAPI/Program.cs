using System.Text.Json.Serialization;
using _011Global.Shared;
using _011Global.Shared.Interfaces;
using _011Global.Shared.Services;
using _011Global.Shared.Settings;
using Azure.Identity;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure appsettings
builder.Configuration
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddUserSecrets<Program>()
    .AddEnvironmentVariables();

if (!builder.Environment.IsDevelopment())
{
    var keyVaultUrl = new Uri(builder.Configuration.GetValue<string>("KeyVaultUrl"));
    builder.Configuration.AddAzureKeyVault(keyVaultUrl, new ManagedIdentityCredential());
}

// Add Logging
Log.Logger = new LoggerConfiguration()
    .ReadFrom
    .Configuration(builder.Configuration)
    .CreateLogger();

builder.Services.AddSerilog(Log.Logger);

Log.Information("Logger has been configured");

// Add DBContexts
builder.Services.RegisterDbContexts(builder.Configuration.GetConnectionString("TransactionsHubDB"));

// Add AppSettingsManager
builder.Services.AddSingleton<AppSettingsManagerBase>();

// Add TokenService
builder.Services.AddScoped<ITokenService, TokenService>();

// Add HttpContextAccessor
builder.Services.AddHttpContextAccessor();

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