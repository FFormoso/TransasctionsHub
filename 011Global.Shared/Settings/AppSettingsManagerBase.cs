using Microsoft.Extensions.Configuration;

namespace _011Global.Shared.Settings;

public class AppSettingsManagerBase(IConfiguration configuration)
{
    /// <summary>
    /// Allows getting different configuration parameters
    /// </summary>
    /// <typeparam name="T">Returned type</typeparam>
    /// <param name="setting">Configuration parameter path</param>
    /// <returns>Configuration parameter of type T</returns>
    /// <exception cref="NullReferenceException">Exception thrown if the parameter not found</exception>
    protected T GetAppSettingValue<T>(string setting)
    {
        return configuration.GetSection(setting).Get<T>() ?? 
               throw new InvalidOperationException($"the property {setting} is not defined in the configuration");
    }
    
    public string USAePayBaseUrl => GetAppSettingValue<string>("USAePay:Routes:BaseUrl");
    
    public string USAePayTranstransctionsEndpoint => GetAppSettingValue<string>("USAePay:Routes:Transactions");
    
    public string USAePayApiSeed => GetAppSettingValue<string>("USAePay:Authentication:ApiSeed");
    
    public string USAePayApiPin => GetAppSettingValue<string>("USAePay:Authentication:ApiPin");
    
    public string USAePayApiKey => GetAppSettingValue<string>("USAePay:Authentication:ApiKey");
    
    public string AccessTokenSecretKey => GetAppSettingValue<string>("JwtSettings:AccessToken:SecretKey");
}