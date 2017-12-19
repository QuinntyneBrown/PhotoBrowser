using MediatR;
using PhotoBrowser.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;
using System.Security.Claims;
using System;

namespace PhotoBrowser.Features.Security
{
    public class GetClaimsForUserQuery
    {
        public class Request : IRequest<Response>
        {
            public string Username { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class Response
        {
            public ICollection<Claim> Claims { get; set; }
        }

        public class Handler : IAsyncRequestHandler<Request, Response>
        {
            public Handler(IPhotoBrowserContext context)
            {
                _context = context;
            }

            public async Task<Response> Handle(Request message)
            {

                var claims = new List<System.Security.Claims.Claim>();

                var user = await _context.Users
                    .Include(x => x.Roles)
                    .Include(x=>x.Tenant)
                    .SingleAsync(x => x.Username == message.Username && x.Tenant.UniqueId == message.TenantUniqueId);

                claims.Add(new Claim(ClaimTypes.Name, message.Username));

                claims.Add(new Claim(ClaimTypes.TenantUniqueId, $"{user.Tenant.UniqueId}"));

                claims.Add(new Claim(ClaimTypes.UserId, $"{user.Id}"));

                claims.Add(new Claim(ClaimTypes.TenantId, $"{user.Tenant.Id}"));

                foreach (var role in user.Roles.Select(x => x.Name))
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                return new Response()
                {
                    Claims = claims
                };
            }

            private readonly IPhotoBrowserContext _context;            
        }
    }
}
