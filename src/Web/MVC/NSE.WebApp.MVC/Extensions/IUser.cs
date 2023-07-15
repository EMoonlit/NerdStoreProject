using System.Security.Claims;

namespace NSE.WebApp.MVC.Extensions;

public interface IUser
{
    string Name { get; set; }
    Guid GetUserId();
    string GetUserEmail();
    string GetUserToken();
    bool IsUserAuthenticated();
    IEnumerable<Claim> GetUserClaims();
    HttpContext GetUserHttpContext();
}