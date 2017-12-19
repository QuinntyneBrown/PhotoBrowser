using MediatR;
using PhotoBrowser.Data;
using PhotoBrowser.Features.Core;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace PhotoBrowser.Features.DigitalAssets
{
    public class GetDigitalAssetByUniqueIdQuery
    {
        public class Request : IRequest<Response>
        {
            public string UniqueId { get; set; }
        }

        public class Response
        {
            public DigitalAssetApiModel DigitalAsset { get; set; }
        }

        public class GetDigitalAssetByUniqueIdHandler : IAsyncRequestHandler<Request, Response>
        {
            public GetDigitalAssetByUniqueIdHandler(PhotoBrowserContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<Response> Handle(Request request)
            {
                return new Response()
                {
                    DigitalAsset = DigitalAssetApiModel.FromDigitalAsset(await _context.DigitalAssets.SingleAsync(x=>x.UniqueId.ToString() == request.UniqueId))
                };
            }

            private readonly PhotoBrowserContext _context;
            private readonly ICache _cache;
        }

    }

}
