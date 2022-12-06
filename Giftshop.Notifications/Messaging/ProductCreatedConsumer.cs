using Giftshop.Core.Messaging;
using Giftshop.Notifications.Hub;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Giftshop.Notifications.Messaging
{
    public class ProductCreatedConsumer : IConsumer<ProductCreateMessage>
    {
        private readonly IHubContext<NotificationHub> _publisher;

        public ProductCreatedConsumer(IHubContext<NotificationHub> publisher)
        {
            _publisher = publisher;
        }

        public async Task Consume(ConsumeContext<ProductCreateMessage> context)
        {
            await _publisher.Clients
                .Group("AuthenticatedUsersGroup")
                .SendAsync("ReceiveMessage", context.Message);
        }
    }
}
