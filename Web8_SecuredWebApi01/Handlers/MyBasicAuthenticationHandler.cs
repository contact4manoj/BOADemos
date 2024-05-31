using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace Web8_SecuredWebApi01.Handlers;

// StatusCodes.Status401Unauthorized        when authentication fails
// StatusCodes.Status403Forbidden           when authorization fails

public class MyBasicAuthenticationHandler
    : AuthenticationHandler<AuthenticationSchemeOptions>
{

    public MyBasicAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder )
    : base(options, logger, encoder)
    {
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        string? username;
        string? password;

        try
        {
            // HTTP HEADER -> Authorization: "username:password"  Eg: "user01:Password%4f123
            var authHeader
                = AuthenticationHeaderValue.Parse(Request.Headers.Authorization.ToString());
            var credentials
                = Encoding.UTF8
                          .GetString(Convert.FromBase64String(authHeader.Parameter ?? string.Empty))
                          .Split(':');
            username = credentials.FirstOrDefault();
            password = credentials.LastOrDefault();
        }
        catch (Exception ex)
        {
            return AuthenticateResult.Fail($"Auth failed: {ex.Message}");
        }

        if (username is null || password is null)
        {
            return AuthenticateResult.Fail("Auth failed: Invalid credentials!");
        }

        //TODO: now check the username & password against some datastore.

        // Add the Claims to the user (preferably from the database!
        var claims = new[] {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, "Admin"),
            new Claim(ClaimTypes.Role, "User")
        };

        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return await Task.Run( () => AuthenticateResult.Success(ticket) );
    }
}
