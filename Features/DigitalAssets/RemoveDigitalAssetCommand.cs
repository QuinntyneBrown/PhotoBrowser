using MediatR;
using PhotoBrowser.Data;
using System.Threading.Tasks;
using PhotoBrowser.Features.Core;

namespace PhotoBrowser.Features.DigitalAssets
{
    public class RemoveDigitalAssetCommand
    {
        public class Request : IRequest<Response>
        {
            public int Id { get; set; }
        }

        public class Response { }

        public class RemoveDigitalAssetHandler : IAsyncRequestHandler<Request, Response>
        {
            public RemoveDigitalAssetHandler(IPhotoBrowserContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<Response> Handle(Request request)
            {
                var digitalAsset = await _context.DigitalAssets.FindAsync(request.Id);
                digitalAsset.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new Response();
            }

            private readonly IPhotoBrowserContext _context;
            private readonly ICache _cache;
        }
    }
}
