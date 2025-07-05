using _011Global.JobsService.JobInterfaces;
using _011Global.JobsService;
using _011Global.JobsService.Settings;
using _011Global.Shared;
using _011Global.Shared.PaymentGateways.Interfaces;
using _011Global.Shared.PaymentGateways.USAePay;
using _011Global.Shared.Settings;
using Serilog;

var builder = new HostApplicationBuilder(args);
    
// Configure AppSettings
builder.Configuration
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddUserSecrets<Program>();

// Add AppSettingsManager
builder.Services.AddSingleton<AppSettingsManagerBase, AppSettingsManager>();
builder.Services.AddSingleton<AppSettingsManager>();

// Add Logging
Log.Logger = new LoggerConfiguration()
    .ReadFrom
    .Configuration(builder.Configuration)
    .CreateLogger();

builder.Services.AddSerilog(Log.Logger);

Log.Information("Logger has been configured");

// Add CancellationToken
builder.Services.AddSingleton<CancellationTokenSource>(_ => (new CancellationTokenSource()));
builder.Services.AddTransient<CancellationTokenBase, WorkerCancellationToken>();

// Add DBContexts
builder.Services.RegisterDbContexts(builder.Configuration.GetConnectionString("TransactionsHubDB"));

// Add PaymentGateways
builder.Services.AddKeyedScoped<IPaymentGateway, USAePayPaymentGatewayAdapter>("USAePay");
builder.Services.AddScoped<USAePayService>();

// builder.Services.AddKeyedScoped<IPaymentGateway, PayPalPaymentGatewayAdapter>("PayPal");
// builder.Services.AddScoped<PayPalService>();

// Add HttpClient
builder.Services.AddHttpClient();

// Add JobsService
builder.Services.LoadInterfacesSingleton<IJob>();
builder.Services.AddHostedService<Worker>();

// Systemd
builder.Services.AddSystemd();

var app = builder.Build();

app.Run();
