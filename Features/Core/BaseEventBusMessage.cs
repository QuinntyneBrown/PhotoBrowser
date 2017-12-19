using System;

namespace PhotoBrowser.Features.Core
{
    public abstract class BaseEventBusMessage: IEventBusMessage
    {
        public dynamic Payload { get; set; }

        public abstract string Type { get; set; }

        public Guid TenantUniqueId { get; set; }
    }
}