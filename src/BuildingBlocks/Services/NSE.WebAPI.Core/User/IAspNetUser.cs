using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace NSE.WebAPI.Core.User;

public interface IAspNetUser
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