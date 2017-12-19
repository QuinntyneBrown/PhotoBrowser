using MediatR;
using PhotoBrowser.Data;
using PhotoBrowser.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace PhotoBrowser.Features.Galleries
{
    public class GetGalleriesQuery
    {
        public class Request : BaseRequest, IRequest<Response> { }

        public class Response
        {
            public ICollection<GalleryApiModel> Galleries { get; set; } = new HashSet<GalleryApiModel>();
        }

        public class Handler : IAsyncRequestHandler<Request, Response>
        {
            public Handler(IPhotoBrowserContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<Response> Handle(Request request)
            {
                var gallerys = await _context.Galleries
                    .Include(x => x.Tenant)
                    .Where(x => x.Tenant.UniqueId == request.TenantUniqueId )
                    .ToListAsync();

                return new Response()
                {
                    Galleries = gallerys.Select(x => GalleryApiModel.FromGallery(x)).ToList()
                };
            }

            private readonly IPhotoBrowserContext _context;
            private readonly ICache _cache;
        }
    }
}
