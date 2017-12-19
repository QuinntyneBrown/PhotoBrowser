using MediatR;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using PhotoBrowser.Features.Core;

namespace PhotoBrowser.Features.Galleries
{
    [Authorize]
    [RoutePrefix("api/galleries")]
    public class GalleriesController : BaseApiController
    {
        public GalleriesController(IMediator mediator)
            :base(mediator) { }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateGalleryCommand.Response))]
        public async Task<IHttpActionResult> Add(AddOrUpdateGalleryCommand.Request request) => Ok(await Send(request));

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateGalleryCommand.Response))]
        public async Task<IHttpActionResult> Update(AddOrUpdateGalleryCommand.Request request) => Ok(await Send(request));
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetGalleriesQuery.Response))]
        public async Task<IHttpActionResult> Get() => Ok(await Send(new GetGalleriesQuery.Request()));

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetGalleryByIdQuery.Response))]
        public async Task<IHttpActionResult> GetById([FromUri]GetGalleryByIdQuery.Request request) => Ok(await Send(request));

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveGalleryCommand.Response))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveGalleryCommand.Request request) => Ok(await Send(request));
    }
}
