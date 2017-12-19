using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Hubs;
using System;

namespace PhotoBrowser.Features.Core
{
    [HubName("eventHub")]
    public class EventHub: BaseHub {

        public override Task OnConnected()
        {
            return base.OnConnected();
        }       
    }
}