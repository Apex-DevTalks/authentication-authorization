using System.Net;
using System.Text.Json;
using DevTalk.Authorization.Model;

namespace DevTalk.Authorization.Api.Auth;
public class IdentityServiceResponse
{
    internal HttpStatusCode Status;
    #pragma warning disable CS8618
    internal string Message;
    #pragma warning restore CS8618
    internal Exception? Exception;
}
public interface IIdentityService
{
    Task<IdentityServiceResponse> GetToken(string userName, string password);
}

public class IdentityService : IIdentityService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<IdentityService> _logger;
    private readonly string _remoteServiceBaseUrl = string.Empty;

    public IdentityService(HttpClient httpClient, ILogger<IdentityService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<IdentityServiceResponse> GetToken(string userName, string password)
    {
        var config = new {
            IdentityServerConfigs = new {
                BaseUrl = "http://localhost:5001",
                ClientId = "t8agr5xKt4$3",
                ClientSecret = "eb300de4-add9-42f4-a3ac-abd3c60f1919",
                GrantType = new {
                    PASSWORD = "password"
                }
            }
        };
        try
        {
            var httpResponseMessage = await _httpClient.PostAsync($"{config.IdentityServerConfigs.BaseUrl}/connect/token", new FormUrlEncodedContent(
                new List<KeyValuePair<string,string>> 
                {
                    new KeyValuePair<string, string> ("client_id", config.IdentityServerConfigs.ClientId),
                    new KeyValuePair<string, string> ("client_secret", config.IdentityServerConfigs.ClientSecret),
                    new KeyValuePair<string, string> ("grant_type", config.IdentityServerConfigs.GrantType.PASSWORD),
                    new KeyValuePair<string, string> ("username", userName),
                    new KeyValuePair<string, string> ("password", password)
                }));
            return new IdentityServiceResponse
            {
                Status = httpResponseMessage.StatusCode,
                Message = (await httpResponseMessage.Content.ReadFromJsonAsync<GetTokenResponse>())?.Token ?? string.Empty
            };
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new IdentityServiceResponse
            {
                Status = HttpStatusCode.InternalServerError,
                Message = ex.Message
            };
        }
    }
}