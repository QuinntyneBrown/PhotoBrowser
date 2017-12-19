using Newtonsoft.Json.Linq;

namespace PhotoBrowser.Features.Core
{
    public interface IEventBusMessageHandler
    {
        void Handle(JObject message);
    }
}