
using Messages.Bll.Exceptions;
using Messages.Bll.integrations;
using Messages.Bll.Interfaces;
using Messages.Bll.ModelsBll;
namespace Messages.Bll;

public class RedirectUserRequestService : IRedirectUserRequestService
{
    private readonly CommonHttpClient<User> _httpClient; 
    public RedirectUserRequestService(HttpMessageHandler? handler = null)
    {
        _httpClient = new CommonHttpClient<User>("https://jsonplaceholder.typicode.com", handler);
    }

    public async Task<User?> GetUserFromJsonPlaceholderAsync(int id)
    {
        return await _httpClient.GetRequest($"/users/{id}");
    }
}
