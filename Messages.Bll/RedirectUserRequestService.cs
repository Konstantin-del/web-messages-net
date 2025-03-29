
using Messages.Bll.integrations;
using Messages.Bll.Interfaces;
using Messages.Bll.ModelsBll;
namespace Messages.Bll;

public class RedirectUserRequestService : IRedirectUserRequestService
{
    private readonly CommonHttpClient _httpClient; 
    public RedirectUserRequestService(HttpMessageHandler? handler = null)
    {
        _httpClient = new CommonHttpClient("https://jsonplaceholder.typicode.com", handler);
    }

    public async Task<User?> GetUserFromJsonPlaceholderAsync(int id)
    {
        return await _httpClient.GetRequest<User?>($"/users/{id}");
    }
}
