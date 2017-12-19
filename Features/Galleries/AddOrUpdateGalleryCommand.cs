using MediatR;
using PhotoBrowser.Data;
using PhotoBrowser.Model;
using PhotoBrowser.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace PhotoBrowser.Features.Galleries
{
    public class AddOrUpdateGalleryCommand
    {
        public class Request : BaseRequest, IRequest<Response>
        {
            public GalleryApiModel Gallery { get; set; }            
        }

        public class Response { }

        public class Handler : IAsyncRequestHandler<Request, Response>
        {
            public Handler(IPhotoBrowserContext context, IEventBus bus)
            {
                _context = context;
            }

            public async Task<Response> Handle(Request request)
            {
                var entity = await _context.Galleries
                    .Include(x => x.Tenant)
                    .SingleOrDefaultAsync(x => x.Id == request.Gallery.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                
                if (entity == null) {
                    var tenant = await _context.Tenants.SingleAsync(x => x.UniqueId == request.TenantUniqueId);
                    _context.Galleries.Add(entity = new Gallery() { TenantId = tenant.Id });
                }

                entity.Name = request.Gallery.Name;
                
                await _context.SaveChangesAsync();

                return new Response();
            }

            private readonly IPhotoBrowserContext _context;
        }
    }
}
