using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace PhotoBrowser.Features.Users
{
    [Authorize]
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {        
        [Route("current")]
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult Current()
        {            
            if (!User.Identity.IsAuthenticated)
                return Ok();
            
            return Ok(new {
                User = new
                {
                    Username = User.Identity.Name
                }
            });
        }
        
    }
}
