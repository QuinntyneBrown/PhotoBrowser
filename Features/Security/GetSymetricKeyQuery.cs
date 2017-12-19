using MediatR;
using PhotoBrowser.Data;
using PhotoBrowser.Features.Core;
using System;
using System.Threading.Tasks;

namespace PhotoBrowser.Features.Security
{
    public class GetSymetricKeyQuery
    {
        public class Request : IRequest<Response> { }

        public class Response {
            public string Key { get; set; }
        }

        public class Handler : IAsyncRequestHandler<Request, Response>
        {
            public Handler(PhotoBrowserContext context, ICache cache, Lazy<IAuthConfiguration> lazyAuthConfiguration)
            {
                _context = context;
                _cache = cache;
                _authConfiguration = lazyAuthConfiguration.Value;
            }

            public async Task<Response> Handle(Request request)
            {
                return new Response()
                {
                    Key = _authConfiguration.JwtKey
                };
            }

            private readonly PhotoBrowserContext _context;
            private readonly ICache _cache;
            private IAuthConfiguration _authConfiguration;
        }
    }
}
