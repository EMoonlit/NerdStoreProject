using System.Net.Http.Headers;
using NSE.WebApp.MVC.Extensions;

namespace NSE.WebApp.MVC.Services.Handlers;

public class HttpClientAuthDelegatingHandler : DelegatingHandler
{
    private readonly IUser _user;

    public HttpClientAuthDelegatingHandler(IUser user)
    {
        _user = user;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var authorizationHeader = _user.GetUserHttpContext().Request.Headers["Authorization"];

        if (!string.IsNullOrEmpty(authorizationHeader))
        {
            request.Headers.Add("Authorization", new List<string?>() { authorizationHeader });
        }

        var token = _user.GetUserToken();

        if (token != null)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        
        return base.SendAsync(request, cancellationToken);
    }
}