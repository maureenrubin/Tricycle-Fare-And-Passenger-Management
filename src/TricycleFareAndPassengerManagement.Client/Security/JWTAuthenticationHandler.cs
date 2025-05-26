using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace TricycleFareAndPassengerManagement.Client.Security
{
    public class JWTAuthenticationHandler : AuthenticationHandler<CustomOption>
    {
        #region Fields

        private readonly ILocalStorageService _localStorageService;

        #endregion Fields

        #region Public Constructors

        public JWTAuthenticationHandler(
            ILocalStorageService localStorageService,
            IOptionsMonitor<CustomOption> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock) : base(options, logger, encoder, clock)
        {
            _localStorageService = localStorageService;
        }

        #endregion Public Constructors

        #region Protected Methods

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            try
            {
                var token = await _localStorageService.GetItemAsync<string>("authToken");
                if (string.IsNullOrEmpty(token))
                {
                    return AuthenticateResult.NoResult();
                }

                var readJWT = new JwtSecurityTokenHandler().ReadJwtToken(token);
                var identity = new ClaimsIdentity(readJWT.Claims, "JWT");
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);
                return AuthenticateResult.Success(ticket);
            }
            catch
            {
                return AuthenticateResult.NoResult();
            }
        }

        protected override Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            Response.Redirect("/");
            return Task.CompletedTask;
        }

        protected override Task HandleForbiddenAsync(AuthenticationProperties properties)
        {
            Response.Redirect("/notfound404");
            return Task.CompletedTask;
        }

        #endregion Protected Methods
    }

    public class CustomOption : AuthenticationSchemeOptions
    {
    }
}