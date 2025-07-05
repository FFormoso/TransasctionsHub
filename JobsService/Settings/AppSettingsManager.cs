using _011Global.Shared.Settings;

namespace _011Global.JobsService.Settings;

public class AppSettingsManager(IConfiguration configuration) : AppSettingsManagerBase(configuration)
{
    public int RecurringChargeJobIterationTime => GetAppSettingValue<int>("Jobs:RecurringCharge:IterationTime");
    
    public string RecurringChargeJobPaymentGateway => GetAppSettingValue<string>("Jobs:RecurringCharge:PaymentGatewayUsed");
}