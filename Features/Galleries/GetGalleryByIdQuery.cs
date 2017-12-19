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
    public class GetGalleryByIdQuery
    {
        public class Request : BaseRequest, IRequest<Response> { 
            public int Id { get; set; }            
        }

        public class Response
        {
            public GalleryApiModel Gallery { get; set; } 
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
                return new Response()
                {
                    Gallery = GalleryApiModel.FromGallery(await _context.Galleries
                    .Include(x => x.Tenant)				
					.SingleAsync(x=>x.Id == request.Id &&  x.Tenant.UniqueId == request.TenantUniqueId))
                };
            }

            private readonly IPhotoBrowserContext _context;
            private readonly ICache _cache;
        }

    }

}
