using MediatR;
using PhotoBrowser.Data;
using System.Threading.Tasks;
using PhotoBrowser.Features.Core;

namespace PhotoBrowser.Features.DigitalAssets
{
    public class GetDigitalAssetByIdQuery
    {
        public class Request : IRequest<Response> { 
			public int Id { get; set; }
		}

        public class Response
        {
            public DigitalAssetApiModel DigitalAsset { get; set; } 
		}

        public class GetDigitalAssetByIdHandler : IAsyncRequestHandler<Request, Response>
        {
            public GetDigitalAssetByIdHandler(IPhotoBrowserContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<Response> Handle(Request request)
            {                
                return new Response()
                {
                    DigitalAsset = DigitalAssetApiModel.FromDigitalAsset(await _context.DigitalAssets.FindAsync(request.Id))
                };
            }

            private readonly IPhotoBrowserContext _context;
            private readonly ICache _cache;
        }
    }
}
