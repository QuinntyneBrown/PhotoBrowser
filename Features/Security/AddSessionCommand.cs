using PhotoBrowser.Data;
using PhotoBrowser.Features.Core;
using MediatR;
using System;
using System.Threading.Tasks;

namespace PhotoBrowser.Features.Security
{
    public class AddSessionCommand
    {
        public class Request : IRequest<Response>
        {
            public Guid TenantUniqueId { get; set; }
            public DateTimeOffset? StartedOn { get; set; }
            public DateTimeOffset? ExpiresOn { get; set; }
            public string AccessToken { get; set; }
        }

        public class Response { }

        public class Handler : IAsyncRequestHandler<Request, Response>
        {
            public Handler(PhotoBrowserContext context)
            {
                _context = context;
            }

            public async Task<Response> Handle(Request request)
            {
                return new Response();
            }

            private readonly PhotoBrowserContext _context;           
        }
    }
}