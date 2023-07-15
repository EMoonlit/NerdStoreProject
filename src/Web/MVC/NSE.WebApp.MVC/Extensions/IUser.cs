using System.Security.Claims;

namespace NSE.WebApp.MVC.Extensions;

public interface IUser
{
    string Name { get; }
    Guid GetUserId();
    string GetUserEmail();
    string GetUserToken();
    bool IsUserAuthenticated();
    bool IsInRole(string role);
    IEnumerable<Claim> GetUserClaims();
    HttpContext GetUserHttpContext();
}