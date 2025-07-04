using _011Global.JobsService.JobInterfaces;
using _011Global.JobsService;
using _011Global.JobsService.Settings;
using _011Global.Shared;
using _011Global.Shared.PaymentGateways.Interfaces;
using _011Global.Shared.PaymentGateways.USAePay;
using _011Global.Shared.Settings;


IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((host, services) =>
    {

        services.AddSingleton<AppSettingsManagerBase, AppSettingsManager>()
        .AddSingleton<AppSettingsManager>()
        .AddSingleton<CancellationTokenSource>(_ => (new CancellationTokenSource()))
        .AddTransient<CancellationTokenBase, WorkerCancellationToken>()
        .RegisterDBContexts(host.Configuration.GetConnectionString("TransactionsHubDB"))
        .AddScoped<IPaymentGateway, USAePayPaymentGatewayAdapter>()
        .AddScoped<USAePayService>()
        .LoadInterfacesSingleton<IJob>()
        .AddHttpClient()
        .AddHostedService<Worker>();


    })
    .UseSystemd()
    .ConfigureAppConfiguration(configBuilder=>
    {
        var env =  Environment.GetEnvironmentVariables()["DOTNET_ENVIRONMENT"];
        configBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env}.json", true, true)
            .AddUserSecrets<Program>();
    })
    .Build();
 

host.Run();
