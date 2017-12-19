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
    public class RemoveGalleryCommand
    {
        public class Request : BaseRequest, IRequest<Response>
        {
            public int Id { get; set; }
        }

        public class Response { }

        public class Handler : IAsyncRequestHandler<Request, Response>
        {
            public Handler(IPhotoBrowserContext context)
            {
                _context = context;
            }

            public async Task<Response> Handle(Request request)
            {
                var gallery = await _context.Galleries.SingleAsync(x=>x.Id == request.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                gallery.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new Response();
            }

            private readonly IPhotoBrowserContext _context;
        }
    }
}
