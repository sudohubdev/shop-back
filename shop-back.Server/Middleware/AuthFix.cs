using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;

public class AuthFixHandler : IAuthorizationMiddlewareResultHandler
{
    private readonly AuthorizationMiddlewareResultHandler
         DefaultHandler = new AuthorizationMiddlewareResultHandler();

    public async Task HandleAsync(
        RequestDelegate requestDelegate,
        HttpContext httpContext,
        AuthorizationPolicy authorizationPolicy,
        PolicyAuthorizationResult policyAuthorizationResult)
    {

        if (policyAuthorizationResult.Challenged && !policyAuthorizationResult.Succeeded)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsync("{\"message\":\"Forbidden\"}");
            await httpContext.Response.CompleteAsync();
            return;
        }

        // Fallback to the default implementation.
        await DefaultHandler.HandleAsync(requestDelegate, httpContext, authorizationPolicy,
                               policyAuthorizationResult);
    }
}
