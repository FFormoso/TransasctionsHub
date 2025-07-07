using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using _011Global.Shared.Settings;
using _011Global.Shared.PaymentGateways.USAePay.Requests;
using _011Global.Shared.PaymentGateways.USAePay.Responses;
using _011Global.Shared.PaymentGateways.USAePay.Utils;

namespace _011Global.Shared.PaymentGateways.USAePay;

public class USAePayService
{
    private readonly HttpClient _httpClient;
    
    private readonly JsonSerializerOptions _serOptions;

    private readonly AppSettingsManagerBase _appSettings;

    public USAePayService(HttpClient httpClient, AppSettingsManagerBase appSettings)
    {
        _httpClient = httpClient;
        
        _httpClient.BaseAddress = new Uri(appSettings.USAePayBaseUrl);
        
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", 
            AuthHeaderGenerator.Generate(appSettings.USAePayApiSeed, appSettings.USAePayApiKey, appSettings.USAePayApiPin));
        
        _serOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
        
        _appSettings = appSettings;
    }

    
    
    public async Task<SaleResponse> SaleAsync(SaleRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(_appSettings.USAePayTranstransctionsEndpoint,
                request, _serOptions);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<SaleResponse>() ??
                   throw new InvalidOperationException("Response content was empty or could not be deserialized.");
        }
        catch (HttpRequestException ex)
        {
            throw new ApplicationException("An error occurred while sending the HTTP request.", ex);
        }
        catch (NotSupportedException ex)
        {
            throw new ApplicationException("The response content is not in the expected format.", ex);
        }
        catch (JsonException ex)
        {
            throw new ApplicationException("An error occurred while deserializing the response.", ex);
        }
    }
}