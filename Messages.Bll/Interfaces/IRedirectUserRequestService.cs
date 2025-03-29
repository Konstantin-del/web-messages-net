
using Messages.Bll.ModelsBll;

namespace Messages.Bll.Interfaces;

public interface IRedirectUserRequestService
{
    public Task<User?> GetUserFromJsonPlaceholderAsync(int id);
}
