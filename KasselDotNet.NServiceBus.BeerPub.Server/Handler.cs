using KasselDotNet.NServiceBus.BeerPub.Core;
using NServiceBus;
using System;
using System.Threading.Tasks;

namespace KasselDotNet.NServiceBus.BeerPub.Server {
    public class Handler : IHandleMessages<OrderBeerCommand> {
        public Task Handle(OrderBeerCommand message, IMessageHandlerContext context) {
            Console.WriteLine($"Beer order received for {message.Name}. Amount: {message.Amount}");

            return context.Send(new PickUpYourBeerCommand { Name = message.Name });
            // return Task.CompletedTask;
            // return Task.FromResult(0); // for < .NET 4.6
            // return null; // Never do this! This will result in an Exception
        }
    }
}
