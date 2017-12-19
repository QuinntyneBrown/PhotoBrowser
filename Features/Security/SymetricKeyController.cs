using MediatR;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace PhotoBrowser.Features.Security
{
    [AllowAnonymous]
    [RoutePrefix("api/symetrickeys")]
    public class SymetricKeysController: ApiController
    {
        public SymetricKeysController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("get")]
        [HttpGet]
        [ResponseType(typeof(GetSymetricKeyQuery.Response))]
        public async Task<IHttpActionResult> Get() => Ok(await _mediator.Send(new GetSymetricKeyQuery.Request()));
        
        private IMediator _mediator;
    }
}