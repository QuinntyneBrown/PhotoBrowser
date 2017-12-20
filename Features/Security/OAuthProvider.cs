using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using MediatR;

namespace PhotoBrowser.Features.Security
{
    public class OAuthProvider : OAuthAuthorizationServerProvider
    {
        public OAuthProvider(string authType, IMediator mediator)
        {
            _authType = _authType;
            _mediator = mediator;
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(_authType);
            var username = context.OwinContext.Get<string>($"{_authType}:username");
            var tenantUniqueId = new Guid(context.Request.Headers.Get("Tenant"));

            var response = await _mediator.Send(new GetClaimsForUserQuery.Request() { Username = username, TenantUniqueId = tenantUniqueId });

            foreach (var claim in response.Claims)
            {
                identity.AddClaim(claim);
            }
            context.Validated(identity);
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            try
            {
                var username = context.Parameters["username"];
                var password = context.Parameters["password"];
                var tenantUniqueId = new Guid(context.Request.Headers.Get("Tenant"));

                var response = await _mediator.Send(new AuthenticateCommand.Request() { Username = username, Password = password, TenantUniqueId = tenantUniqueId });

                if (response.IsAuthenticated)
                {
                    context.OwinContext.Set($"{_authType}:username", username);
                    context.Validated();
                }
                else
                {
                    context.SetError("Invalid credentials");
                    context.Rejected();
                }
            }
            catch (Exception exception)
            {
                context.SetError(exception.Message);
                context.Rejected();
            }            
        }

        public override async Task TokenEndpointResponse(OAuthTokenEndpointResponseContext context)
        {            
            await _mediator.Send(new AddSessionCommand.Request() {
                TenantUniqueId = new Guid(context.Request.Headers.Get("Tenant")),
                StartedOn = context.Properties.IssuedUtc,
                ExpiresOn = context.Properties.ExpiresUtc
            });

            await base.TokenEndpointResponse(context);
        }

        protected IMediator _mediator { get; set; }
        protected string _authType { get; set; }

    }
}
