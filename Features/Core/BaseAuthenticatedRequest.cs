using System;
using System.Linq;
using System.Security.Claims;

namespace PhotoBrowser.Features.Core
{
    public class BaseAuthenticatedRequest: BaseRequest
    {
        public ClaimsPrincipal ClaimsPrincipal { get; set; }

        public new Guid TenantUniqueId
        {
            get
            {
                return new Guid(ClaimsPrincipal?.Claims.Single(x => x.Type == Core.ClaimTypes.TenantUniqueId).Value);
            }
        }

        public string Username
        {
            get { return ClaimsPrincipal?.Identity.Name; }        
        }
    }
}