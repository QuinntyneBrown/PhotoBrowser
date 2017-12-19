using MediatR;
using PhotoBrowser.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;
using PhotoBrowser.Model;
using static PhotoBrowser.Features.DigitalAssets.Constants;
using PhotoBrowser.Features.Core;

namespace PhotoBrowser.Features.DigitalAssets
{
    public class GetDigitalAssetsQuery
    {
        public class Request : IRequest<Response> { }

        public class Response
        {
            public ICollection<DigitalAssetApiModel> DigitalAssets { get; set; } = new HashSet<DigitalAssetApiModel>();
        }

        public class GetDigitalAssetsHandler : IAsyncRequestHandler<Request, Response>
        {
            public GetDigitalAssetsHandler(IPhotoBrowserContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<Response> Handle(Request request)
            {
                var digitalAssets = await _cache.FromCacheOrServiceAsync<List<DigitalAsset>>(() => _context.DigitalAssets.ToListAsync(), DigitalAssetCacheKeys.DigitalAssets);

                return new Response()
                {
                    DigitalAssets = digitalAssets.Select(x => DigitalAssetApiModel.FromDigitalAsset(x)).ToList()
                };
            }

            private readonly IPhotoBrowserContext _context;
            private readonly ICache _cache;
        }
    }
}