using System.Security.Claims;

namespace NSE.WebApp.MVC.Extensions;

public class AspNetUser : IUser
{
    private readonly IHttpContextAccessor _contextAccessor;

    public AspNetUser(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public string Name => _contextAccessor.HttpContext.User.Identity.Name;

    public Guid GetUserId()
    {
        return IsUserAuthenticated() ? Guid.Parse(_contextAccessor.HttpContext.User.GetUserId()) : Guid.Empty;
    }

    public string GetUserEmail()
    {
        return IsUserAuthenticated() ? _contextAccessor.HttpContext.User.GetUserEmail() : string.Empty;
    }

    public string GetUserToken()
    {
        return IsUserAuthenticated() ? _contextAccessor.HttpContext.User.GetUserToken() : string.Empty;
    }

    public bool IsUserAuthenticated()
    {
        return _contextAccessor.HttpContext.User.Identity.IsAuthenticated;
    }

    public bool IsInRole(string role)
    {
        return _contextAccessor.HttpContext.User.IsInRole(role);
    }

    public IEnumerable<Claim> GetUserClaims()
    {
        return _contextAccessor.HttpContext.User.Claims;
    }

    public HttpContext GetUserHttpContext()
    {
        return _contextAccessor.HttpContext;
    }
}